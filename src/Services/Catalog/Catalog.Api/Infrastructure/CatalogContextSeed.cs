using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.AggregatesModel;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Infrastructure;

/// <summary>
/// The class provides functions to seed some basic data 
/// </summary>
public class CatalogContextSeed
{

    //TODO:Separate Migrate from Seed (First rule of SOLID :D)
    //TODO:Use LoggerMessage instead of ILogger
    /// <summary>
    /// Migrate and seed data
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public async Task MigrateAndSeedAsync(CatalogContext context, ILogger<CatalogContextSeed> logger)
    {
        await context.Database.MigrateAsync();

        if (!await context.Categories.AnyAsync())
        {
            logger.LogInformation("Basic categories added as seed");
            await context.Categories.AddRangeAsync(Categories);
        }

        // if (!await context.CatalogItems.AnyAsync())
        // {
        //     logger.LogInformation("Basic catalog items added as seed");
        //     await context.CatalogItems.AddRangeAsync(CatalogItems);
        // }

        if (context.ChangeTracker.HasChanges())
        {
            logger.LogInformation("Applied seed data on db");
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Seed categories
    /// </summary>
    /// <value></value>
    private readonly IImmutableList<Category> Categories = ImmutableList.Create(
                                              new Category("digital")
                                            , new Category("Books")
                                            , new Category("Beauty")
                                            );

    // /// <summary>
    // /// Seed catalog items
    // /// </summary>
    // /// <value></value>
    // private readonly IImmutableList<CatalogItem> CatalogItems = ImmutableList.Create(
    //         new CatalogItem { Name = "Apple 2022 MacBook Pro Laptop", CategoryId = 1, Price = 50_000_000, Discount = 500_000, ImageUrl = "https://m.media-amazon.com/images/I/61L5QgPvgqL._AC_SL1500_.jpg" },
    //         new CatalogItem { Name = "Apple 2020 MacBook Air Laptop M1", CategoryId = 1, Price = 60_000_000, Discount = 500_000, ImageUrl = "https://m.media-amazon.com/images/I/71jG+e7roXL._AC_SL1500_.jpg" },
    //         new CatalogItem { Name = "Joico Color Balance Blue Shampoo", CategoryId = 3, Price = 500_000, Discount = 0, ImageUrl = "https://m.media-amazon.com/images/I/71B0klu8oeL._SL1500_.jpg" },
    //         new CatalogItem { Name = "Iron Flame", CategoryId = 2, Price = 200_000, Discount = 0, ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/91ke43dIxkL._AC_UL254_SR254,254_.jpg" }
    // );

}