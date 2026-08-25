using ASP_Net_Core_MVC_Liberary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_Net_Core_MVC_Liberary.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorView = new ErrorViewModel
            {
                // Retrieve the error message from HttpContext.Items if available, otherwise use a default message
                Message = HttpContext.Items["Message"]?.ToString() ?? "An error occurred."
            };
            return View(errorView);
        }
    }
}
