using BookBloomRazor.Data;
using BookBloomRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBloomRazor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly BookBloomRazorDbContext dbContext;
     
        public Category Category { get; set; }

        public EditModel(BookBloomRazorDbContext razorDbContext)
        {
            dbContext = razorDbContext;
        }

        /// <summary>
        /// get method for category
        /// </summary>
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Category = dbContext.Category.Find(id);
            }
        }

        /// <summary>
        /// post method for edit category
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.Category.Update(category);
                dbContext.SaveChanges();
                //TempData["success"] = AppConstants.AppConstants.EDIT_CATEGORY_POST;
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
