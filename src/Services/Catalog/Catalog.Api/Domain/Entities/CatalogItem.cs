namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Entities;

//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591

/// <summary>
/// Category Item [Sql Table]
/// </summary>
public class CatalogItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal Discount { get; set; }

    public string? ImageUrl { get; set; }

    /*************
     * RELATIONS *
     *************/
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    /**********************
     * VIRTUAL PROPERTIES *
     **********************/
    public decimal FinalPrice => Price - Discount;
}