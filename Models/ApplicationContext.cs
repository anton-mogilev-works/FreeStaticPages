using Microsoft.EntityFrameworkCore;

namespace FreeStaticPages.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<StaticPage> StaticPages => Set<StaticPage>();
        public DbSet<Link> Links => Set<Link>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Image> Images => Set<Image>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StaticPage>(
                builder =>
                {
                    builder.HasKey(x => x.Id);

                    builder.OwnsOne(x => x.Link);
                    builder.Navigation(x => x.Link).IsRequired(); 
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
