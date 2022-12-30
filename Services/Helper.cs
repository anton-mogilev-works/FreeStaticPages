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
        public static void BuildStaticSite()
        {

        }

        public static List<Href> GetStaticPagesHrefs(ApplicationContext context)
        {
            var staticPages = context.StaticPages.Select(p => new Href()
            {
                Caption = p.Name,
                Path = p.Link.Path + ".html"
            }).ToList();

            return staticPages;
        }
    }
}
