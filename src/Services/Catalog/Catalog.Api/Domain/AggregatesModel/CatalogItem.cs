using eShop.Services.Catalog.Domain.SeedWork;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Exceptions;
using ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.SeedWork;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.AggregatesModel;

//Ignoring warnings those related with missing-xml-documents. Who cares? :)
#pragma warning disable 1591

/// <summary>
/// Category Item [Sql Table]
/// </summary>
public class CatalogItem : Entity, IAggregateRoot
{
    private string _name;
    private decimal _price;
    private decimal _discount;
    private int _categoryId;
    private string? _imageUrl;
    private string? _description;

    /// <summary>
    /// Construct a new object via given data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="price"></param>
    /// <param name="discount"></param>
    /// <param name="categoryId"></param>
    /// <param name="description"></param>
    /// <param name="imageUrl"></param>D
    public CatalogItem(string name, decimal price, decimal discount, int categoryId, string? description = null, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CatalogDomainException("The Name of catalog item can't be null or empty");

        _name = name;

        _price = price == 0 ? throw new CatalogDomainException("The price of catalog item can't be zero or less") : price;

        if (_discount > price)
            throw new CatalogDomainException("The amount of discount can't eb grater than the amount of price");

        _discount = discount;

        _categoryId = categoryId;
        _description = description;
        _imageUrl = imageUrl;
    }

    public string Name => _name;
    public string? Description => _description;
    public decimal Price => _price;
    public decimal Discount => _discount;
    public string? ImageUrl => _imageUrl;
    public int CategoryId => _categoryId;


    /*************************
     * NAVIGATION PROPERTIES *
     *************************/
    public Category Category { get; private set; } = null!;

    /**********************
     * VIRTUAL PROPERTIES *
     **********************/
    public decimal FinalPrice => Price - Discount;

    public void UpdateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new CatalogDomainException($"Invalid catalog-item name = ({name})");
        
        _name = name;
    }

    public void UpdatePrice(decimal price)
    {
        if (price < 1 || price < _discount)
            throw new CatalogDomainException($"Invalid catalog-item price = ({price})");

        _price = price;
    }

    public void UpdateDiscount(decimal discount)
    {
        if (discount > _price)
            throw new CatalogDomainException($"Invalid catalog-item discount = ({discount})");

        _discount = discount;
    }

    public void ChangeCategory(int categoryId)
    {
        if (categoryId == default)
            throw new CatalogDomainException($"Invalid catalog-item categoryId = ({categoryId})");

        _categoryId = categoryId;
    }
}