namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Entities;

//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591

/// <summary>
/// Category [Sql Table]
/// </summary>
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? ImageUrl { get; set; }

    /*************
     * RELATIONS *
     *************/
    public ICollection<CatalogItem>? CatalogItems { get; set; }
}