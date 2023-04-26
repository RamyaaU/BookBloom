using BookBloomRazor.Data;
using BookBloomRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBloomRazor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly BookBloomRazorDbContext dbContext;

        public Category Category { get; set; }

        public CreateModel(BookBloomRazorDbContext razorDbContext)
        {
            dbContext = razorDbContext;
        }

        public void OnGet()
        {
            //CategoryList = dbContext.Category.ToList();
        }
        
    }
}
