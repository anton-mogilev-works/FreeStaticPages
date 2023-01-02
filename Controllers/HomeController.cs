using FreeStaticPages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FreeStaticPages.Services;
using Microsoft.EntityFrameworkCore;

namespace FreeStaticPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext dbContext;
        List<Href> StaticPagesHrefs { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.dbContext = context;
            StaticPagesHrefs = Helper.GetStaticPagesHrefs(dbContext);

        }

        public IActionResult Index()
        {
            ViewBag.StaticPagesHrefs = StaticPagesHrefs;   
                  
            return View();
        }
        
        [Route("{path}.html")]
        [HttpGet]
        public IActionResult StaticPage(string path)
        {
            ViewBag.StaticPagesHrefs = StaticPagesHrefs;
            ViewBag.StaticPage = dbContext.StaticPages.Include(p => p.Link).Where(p => p.Link.Path == path).First();
            return View();
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
