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
        List<Href> CatalogCategoriesHrefs { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            this.dbContext = context;
            StaticPagesHrefs = Helper.GetStaticPagesHrefs(dbContext);
            CatalogCategoriesHrefs = Helper.GetCatalogCategoriesHrefs(dbContext);
        }

        public IActionResult Index()
        {
            // ViewBag.StaticPagesHrefs = StaticPagesHrefs;

            // return View();
            return Redirect("index.html");
        }

        [Route("{path}.html")]
        [HttpGet]
        public IActionResult StaticPage(string path)
        {
            if (path is not null && path != string.Empty)
            {
                StaticPage? staticPage = dbContext.StaticPages
                    .Include(p => p.Link)
                    .Include(p => p.Images)
                    .Where(p => p.Link.Path == path)
                    .First();

                if (staticPage.Images is not null && staticPage.Images.Count > 0)
                {
                    foreach (Image image in staticPage.Images)
                    {
                        dbContext.Entry(image).Reference(u => u.Link).Load();
                    }
                }
                ViewBag.StaticPage = staticPage;
            }
            else
            {
                return Redirect("index.html");
            }

            ViewBag.StaticPagesHrefs = StaticPagesHrefs;
            ViewBag.CatalogCategoriesHrefs = CatalogCategoriesHrefs;
            return View();
        }

        [Route("catalog/{categoryLinkPath}/index.html")]
        [HttpGet]
        public IActionResult ShowCategory(string categoryLinkPath)
        {
            ViewBag.CatalogCategoriesHrefs = CatalogCategoriesHrefs;
            ViewBag.StaticPagesHrefs = StaticPagesHrefs;
            
            Category? category = dbContext.Categories
                .Include(c => c.Items)
                .Include(c => c.Link)
                .Where(c => c.Link.Path == categoryLinkPath)
                .First();            

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
