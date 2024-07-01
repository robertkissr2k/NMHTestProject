using Microsoft.EntityFrameworkCore;
using NMHTestProject.Data.Entities;

namespace NMHTestProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Title);
                e.HasMany(x => x.Author).WithMany(x => x.Articles);
                e.HasOne(x => x.Site).WithMany(x => x.Articles);
            });

            modelBuilder.Entity<Author>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Name).IsUnique();
                e.HasMany(x => x.Articles).WithMany(x => x.Author);
                e.HasOne(x => x.Image);
            });

            modelBuilder.Entity<Image>(e =>
            {
                e.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Site>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("LOCALTIMESTAMP");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
