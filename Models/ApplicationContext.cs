using Microsoft.EntityFrameworkCore;

namespace FreeStaticPages.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<StaticPageModel> StaticPages  => Set<StaticPageModel>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


    }
}