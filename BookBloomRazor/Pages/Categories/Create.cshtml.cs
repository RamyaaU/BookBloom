using BookBloomRazor.Data;
using BookBloomRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBloomRazor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly BookBloomRazorDbContext dbContext;

        ///// <summary>
        ///// when working on razorpages, if we want teh proprty to be available
        ///// when working on post method then bindprop is used.
        ///// </summary>
        //[BindProperty]
        public Category Category { get; set; }

        public CreateModel(BookBloomRazorDbContext razorDbContext)
        {
            dbContext = razorDbContext;
        }

        /// <summary>
        /// get method for category
        /// </summary>
        public void OnGet()
        {
            //CategoryList = dbContext.Category.ToList();
        }

        /// <summary>
        /// post method for category
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost(Category category)
        {
            dbContext.Category.Add(category);
            dbContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
