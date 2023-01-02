using Microsoft.EntityFrameworkCore;

namespace FreeStaticPages.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<StaticPage> StaticPages => Set<StaticPage>();
        public DbSet<Link> Links => Set<Link>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Image> Images => Set<Image>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<StaticPage>(
            //     builder =>
            //     {
            //         builder.HasKey(x => x.Id);
            //         builder.OwnsOne(x => x.Link);
            //         builder.Navigation(x => x.Link).IsRequired();
            //     }
            // );

            // modelBuilder.Entity<Category>(
            //     builder =>
            //     {
            //         builder.HasKey(x => x.Id);
            //         builder.OwnsOne(x => x.Link);
            //         builder.Navigation(x => x.Link).IsRequired();
            //     }
            // );

            // modelBuilder.Entity<Item>(
            //     builder =>
            //     {
            //         builder.HasKey(x => x.Id);
            //         builder.OwnsOne(x => x.Link);
            //         builder.Navigation(x => x.Link).IsRequired();
            //     }
            // );

            modelBuilder
                .Entity<StaticPage>()
                .HasData(new StaticPage { Id = 1, Link = new Link() { Path = "Index"}, Name = "Главная страница", Content = "Главная страница вашего сайта" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
