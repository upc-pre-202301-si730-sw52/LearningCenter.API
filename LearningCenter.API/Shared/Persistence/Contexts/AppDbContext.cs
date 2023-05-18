using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LearningCenter.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Tutorial> Tutorials { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Categories Configuration
        
        builder.Entity<Category>().ToTable("Categories");
        builder.Entity<Category>().HasKey(c => c.Id);
        builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(30);
        
        // Tutorials Configuration
        
        builder.Entity<Tutorial>().ToTable("Tutorials");
        builder.Entity<Tutorial>().HasKey(t => t.Id);
        builder.Entity<Tutorial>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Tutorial>().Property(t => t.Title).IsRequired().HasMaxLength(50);
        builder.Entity<Tutorial>().Property(t => t.Description).HasMaxLength(120);
        
        // Relationships

        builder.Entity<Category>()
            .HasMany(c => c.Tutorials)
            .WithOne(t => t.Category)
            .HasForeignKey(t => t.CategoryId);
        
        // Apply Snake Case Naming Convention
        
        builder.UseSnakeCaseNamingConvention();
    }
}