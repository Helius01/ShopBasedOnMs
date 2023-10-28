using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Entities;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure.EntityConfiguration;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure;

/// <summary>
/// Catalog DbContext
/// </summary>
public class CatalogContext : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options) { }

    /// <summary>
    /// CatalogItems 
    /// </summary>
    /// <value></value>
    public DbSet<CatalogItem> CatalogItems { get; set; } = null!;

    /// <summary>
    /// Categories
    /// </summary>
    /// <value></value>
    public DbSet<Category> Categories { get; set; } = null!;

    /// <summary>
    /// Applying entity configurations
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CatalogItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
    }
}

/// <summary>
/// TODO:FILL
/// </summary>
public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<CatalogContext>
{
    /// <summary>
    /// TODO:FILL
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public CatalogContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>()
            .UseSqlServer("Server=.;Initial Catalog=CatalogDb;Integrated Security=true");

        return new CatalogContext(optionsBuilder.Options);
    }
}