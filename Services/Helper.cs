using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using FreeStaticPages.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FreeStaticPages.Services
{
    public class Helper
    {
        public static string? url;
        public static async Task BuildStaticSiteAsync(string folder, ApplicationContext context)
        {
            Console.WriteLine(folder);
            List<Link> links = context.Links.ToList();

            if (url is null)
            {
                Console.WriteLine("URL IS EMPTY!!!");
                return;
            }

            if (links.Count > 0)
            {
                foreach (Link link in links)
                {
                    string address = url + "/" + link.Path + ".html";

                    using (WebClient client = new WebClient())
                    {
                        string page = client.DownloadString(address);
                        using (StreamWriter writer = new StreamWriter(folder + "/" + link.Path + ".html", false))
                        {
                            await writer.WriteLineAsync(page);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Links not found");
            }

            CopyFilesRecursively("wwwroot", folder);
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            //Now Create all of the directories
            foreach (
                string dirPath in Directory.GetDirectories(
                    sourcePath,
                    "*",
                    SearchOption.AllDirectories
                )
            )
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (
                string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories)
            )
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
        }

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
