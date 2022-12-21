using FreeStaticPages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FreeStaticPages.Services;

namespace FreeStaticPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewResult view = View();

            // inside of a Controller method
            // string confirmation = await Helper.RenderViewToStringAsync(
            //     "Index",
            //     new Object(),
            //     ControllerContext
            // );


            // Console.Write(confirmation);

            return view;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
