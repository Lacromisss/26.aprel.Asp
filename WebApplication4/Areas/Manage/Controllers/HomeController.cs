using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
