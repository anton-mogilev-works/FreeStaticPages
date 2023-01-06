using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using FreeStaticPages.Models;
using Microsoft.EntityFrameworkCore;

namespace FreeStaticPages.Services
{
    public class Helper
    {
        public static void BuildStaticSite() { }

        public static void InitData(ApplicationContext context)
        {
            List<StaticPage> staticPages = context.StaticPages.ToList();

            if (staticPages is not null && staticPages.Count == 0)
            {
                StaticPage staticPage = new StaticPage()
                {
                    Name = "Главная страница",
                    Content = "Ваша главная страница",
                    Link = new Link() { Path = "Index" }
                };
                context.SaveChanges();
            }
        }

        public static List<Href> GetStaticPagesHrefs(ApplicationContext context)
        {
            var staticPages = context.StaticPages
                .Select(p => new Href() { Caption = p.Name, Path = p.Link.Path + ".html" })
                .ToList();

            return staticPages;
        }
    }
}
