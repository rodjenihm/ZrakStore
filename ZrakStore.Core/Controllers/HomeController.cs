using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ZrakStore.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewData["TokenEndpoint"] = configuration["TokenEndpoint"];
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}