using BookBloom.AppConstants;
using BookBloom.Data;
using BookBloom.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookBloom.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookBloomDbContext dbContext;

        public CategoryController(BookBloomDbContext bookBloomDbContext)
        {
            dbContext = bookBloomDbContext;
        }

        public IActionResult Index()
        {
            List<Category> categories = dbContext.Category.ToList();
            return View(categories);
        }

        /// <summary>
        /// get method for create
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// post method
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //custom validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            //model state validation
            if (ModelState.IsValid)
            {
                dbContext.Category.Add(category);
                dbContext.SaveChanges();
                TempData["success"] = AppConstants.AppConstants.CATEGORY_POST;
                return RedirectToAction("Index");
            }
            return View();
        }

        /// <summary>
        /// get method for edit
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = dbContext.Category.Find(id);
            //Category? categoryFromDb1 = dbContext.Category.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = dbContext.Category.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        /// <summary>
        /// post method for edit
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Category updateCategory)
        {
            ////custom validation
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            //}
            //model state validation
            if (ModelState.IsValid)
            {
                dbContext.Category.Update(updateCategory);
                dbContext.SaveChanges();
                TempData["success"] = AppConstants.AppConstants.EDIT_CATEGORY_POST;
                return RedirectToAction("Index");
            }
            return View();
        }

        /// <summary>
        /// get method for delete
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = dbContext.Category.Find(id);
            //Category? categoryFromDb1 = dbContext.Category.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = dbContext.Category.Where(u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        /// <summary>
        /// post method for delete
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteCategory(int? id)
        {
            Category? deleteCategory = dbContext.Category.Find(id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            dbContext.Category.Remove(deleteCategory);

            dbContext.SaveChanges();
            TempData["success"] = AppConstants.AppConstants.DELETE_CATEGORY_POST;
            return RedirectToAction("Index");
        }
    }
}
