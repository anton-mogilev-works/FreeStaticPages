using FreeStaticPages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FreeStaticPages.Services;

namespace FreeStaticPages.Controllers
{
    public class SiteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext dbContext;

        public SiteController(ILogger<HomeController> logger, ApplicationContext dbContext)
        {
            _logger = logger;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // List<StaticPageModel> staticPages = dbContext.StaticPages.

            ViewResult view = View();
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
