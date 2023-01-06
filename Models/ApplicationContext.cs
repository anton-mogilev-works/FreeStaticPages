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
            modelBuilder.Entity<Link>(
                builder =>
                {
                    builder.HasKey(x => x.Id);
                    builder.Property(p => p.Path);
                }
            );

            modelBuilder.Entity<StaticPage>(
                builder =>
                {
                    builder.HasKey(x => x.Id);
                    builder.Navigation(x => x.Link);
                }
            );

            modelBuilder.Entity<Category>(
                builder =>
                {
                    builder.HasKey(x => x.Id);
                    builder.Navigation(x => x.Link);
                }
            );

            modelBuilder.Entity<Item>(
                builder =>
                {
                    builder.HasKey(x => x.Id);
                    builder.Navigation(x => x.Link);
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
