using BookBloomRazor.Data;
using BookBloomRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBloomRazor.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly BookBloomRazorDbContext dbContext;

        public Category Category { get; set; }

        public DeleteModel(BookBloomRazorDbContext razorDbContext)
        {
            dbContext = razorDbContext;
        }

        /// <summary>
        /// get method for category
        /// </summary>
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = dbContext.Category.Find(id);
            }
        }


        /// <summary>
        /// post method for delete category
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(int? id)
        {
            Category? deleteCategory = dbContext.Category.Find(id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            dbContext.Category.Remove(deleteCategory);
            dbContext.SaveChanges();
            TempData["success"] = AppConstants.AppConstants.DELETE_CATEGORY_POST;
            return RedirectToPage("Index");
        }
    }
}
