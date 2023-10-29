using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Exceptions;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.SeedWork;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.AggregatesModel;

//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591

/// <summary>
/// Category [Sql Table]
/// </summary>
public class Category : Entity
{
    private readonly string _name;
    private string? _imageUrl;

    public Category(string name, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CatalogDomainException("Category name can't be null or empty");

        _name = name;
        _imageUrl = imageUrl;
    }

    public string Name => _name;
    public string? ImageUrl => _imageUrl;

    /*************
     * RELATIONS *
     *************/
    public ICollection<CatalogItem>? CatalogItems { get; set; }

    public void UpdateImageUrl(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
            throw new CatalogDomainException("The category image url can't be null or empty");

        _imageUrl = imageUrl;
    }

    //TODO:Implement Remove image url function
}