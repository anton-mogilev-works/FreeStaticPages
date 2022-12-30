using Microsoft.AspNetCore.Mvc;
using FreeStaticPages.Models;

namespace FreeStaticPages.Controllers
{
    public class AdminController : Controller
    {
        ApplicationContext dbContext;
        public AdminController(ApplicationContext context)
        {
            this.dbContext = context;
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Pages()
        {
            List<StaticPage> staticPages = dbContext.StaticPages.ToList<StaticPage>();

            if (staticPages.Count > 0)
            {
                foreach (StaticPage page in staticPages)
                {
                    if (page.Link is not null)
                    {
                        Console.WriteLine(page.Link.Path);
                    }
                    else
                    {
                        Console.WriteLine("Link is null");
                    }
                }
            }

            return View(staticPages);
        }

        [HttpGet]
        public ViewResult AddPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPage(StaticPage page)
        {
            dbContext.StaticPages.Add(page);
            dbContext.SaveChangesAsync();
            return RedirectToAction("Pages");
        }
    }
}
