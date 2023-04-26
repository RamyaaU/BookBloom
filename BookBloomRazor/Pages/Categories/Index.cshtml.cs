using BookBloomRazor.Data;
using BookBloomRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookBloomRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {

        private readonly BookBloomRazorDbContext dbContext;

        public List<Category> CategoryList { get; set; }

        public IndexModel(BookBloomRazorDbContext razorDbContext)
        {
            dbContext = razorDbContext;
        }
        public void OnGet()
        {
            CategoryList = dbContext.Category.ToList();
        }
    }
}
