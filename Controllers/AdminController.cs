using Microsoft.AspNetCore.Mvc;
using FreeStaticPages.Models;
using FreeStaticPages.Services;
using Microsoft.EntityFrameworkCore;

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
            List<StaticPage> staticPages = dbContext.StaticPages.Include(p => p.Link).ToList<StaticPage>();           

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
            dbContext.SaveChanges();
            return RedirectToAction("Pages");
        }

        [HttpGet]
        public ViewResult Catalog()
        {
            ViewBag.Categories = dbContext.Categories.Include(p => p.Link).ToList();            
            return View();
        }

        [HttpGet]
        public ViewResult AddCategory()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            dbContext.Categories.Add(category);
            dbContext.SaveChanges();
            return Redirect("Catalog");
        }

        [HttpGet]
        public ViewResult ShowCategory(int id)        
        {
            ViewBag.Category = dbContext.Categories.Where(c => c.Id == id).First<Category>();
            return View();
        }

        [HttpGet]
        public ViewResult AddItem()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StaticSiteManagement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeStaticSiteAsync(string folderName)
        {
            if(!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            await Helper.BuildStaticSiteAsync(folderName, dbContext);
            return RedirectToAction("Index");
        }
    }
}
