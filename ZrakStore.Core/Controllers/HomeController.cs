using Microsoft.AspNetCore.Mvc;

namespace ZrakStore.Core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}