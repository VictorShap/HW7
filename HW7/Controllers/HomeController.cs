using Microsoft.AspNetCore.Mvc;

namespace HW7.Controllers
{
    public class HomeController : Controller 
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
