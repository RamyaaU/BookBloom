using BookBloom.AppConstants;
using BookBloom.Data;
using BookBloom.DataAccess.Repository;
using BookBloom.DataAccess.Repository.IRepository;
using BookBloom.Models;
using BookBloom.Models.Models;
using BookBloom.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookBloom.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return View(product);
        }

        /// <summary>
        /// get method for upsert
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Upsert(int? id)
        {
            ProductViewModel productView = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            //if id is not present, then create so it will be returned to view
            if (id == null || id == 0)
            {
                //create
                return View(productView);
            }
            //if not then product gets updated
            else
            {
                //update
                productView.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productView);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel productView, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    //everytime the file gets uploaded, file might have some names, instead we can rename the file to random guid 
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    //if the image url is null or empty in update 
                    if (!string.IsNullOrEmpty(productView.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImage = Path.Combine(wwwRootPath, productView.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }

                        using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        productView.Product.ImageUrl = @"\images\product\" + fileName;
                    }
                }


                if (productView.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productView.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productView.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                //while returning back, populate the dropdown again
                productView.CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });

                return View(productView);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }
    }
}