using Microsoft.AspNetCore.Mvc;
using FreeStaticPages.Models;
using FreeStaticPages.Services;
using Microsoft.EntityFrameworkCore;

using System.IO;

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
            List<StaticPage> staticPages = dbContext.StaticPages
                .Include(p => p.Link)
                .ToList<StaticPage>();

            return View(staticPages);
        }

        [HttpGet]
        public ViewResult AddPageGet()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPagePost(StaticPage page, IFormFile imageFile)
        {
            if (imageFile is not null && imageFile.Length > 0)
            {
                Image image = new Image();
                Console.WriteLine("Content type: " + imageFile.ContentType);

                string newFileName = "images/" + imageFile.FileName;
                using (var stream = System.IO.File.Create(newFileName))
                {
                    imageFile.CopyTo(stream);
                }

                image.Mime = imageFile.ContentType;

                Link fileLink = new Link() { Path = newFileName};
                image.Link = fileLink;

                dbContext.Images.Add(image);
                dbContext.SaveChanges();
                
                page.Images = new List<Image>() { image };                
            }

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
            ViewBag.Category = dbContext.Categories.Where(c => c.Id == id).FirstOrDefault();

            return View();
        }

        [HttpGet]
        public ViewResult AddItemGet(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddItemPost(Item item)
        {
            Console.WriteLine(item.ToString());
            try
            {
                dbContext.Items.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.ToString());
            }

            if (item is not null && item.Category is not null)
            {
                return Redirect("~/ShowCategory/" + item.Category.Id);
            }
            else
            {
                return Redirect("~/Admin/Catalog");
            }
        }

        [HttpGet]
        public IActionResult StaticSiteManagement()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeStaticSiteAsync(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            await Helper.BuildStaticSiteAsync(folderName, dbContext);
            return RedirectToAction("Index");
        }
    }
}
