using Microsoft.AspNetCore.Mvc;

namespace BookBloom.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
