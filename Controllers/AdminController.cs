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
            List<StaticPageModel> staticPages = dbContext.StaticPages.ToList<StaticPageModel>();
            return View(staticPages);
        }

        [HttpGet]
        public ViewResult AddPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPage(StaticPageModel page)
        {            
            dbContext.StaticPages.Add(page);
            dbContext.SaveChangesAsync();
            return RedirectToAction("Pages");
        }
        
    }
}