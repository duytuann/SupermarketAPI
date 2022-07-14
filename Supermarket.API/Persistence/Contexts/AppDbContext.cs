using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Persistence.Contexts
{
  public class AppDbContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Category>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        entity.Property(e => e.Name).IsRequired().HasMaxLength(30);
        entity.HasMany(e => e.Products).WithOne(e => e.Category).HasForeignKey(e => e.CategoryId);
      });

      builder.Entity<Product>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).IsRequired().ValueGeneratedOnAdd();
        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        entity.Property(e => e.QuantityInPackage).IsRequired();
        entity.Property(e => e.UnitOfMeasurement).IsRequired();
      });
    }
  }
}