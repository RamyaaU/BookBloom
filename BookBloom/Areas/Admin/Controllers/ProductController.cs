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
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> product = _unitOfWork.Product.GetAll().ToList();
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
                _unitOfWork.Product.Add(productView.Product);
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