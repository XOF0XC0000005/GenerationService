using GenerationService.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace GenerationService.Persisntence
{
    public class AppDbContext : DbContext
    {
        public DbSet<FileMeta> FileMetas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileMeta>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Path).IsRequired();
                entity.Property(e => e.Size).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
}
