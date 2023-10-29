using System.Runtime.Serialization;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.Exceptions;


/// <summary>
/// The type of this exception used in Catalog-Domain atmosphere
/// </summary>
public class CatalogDomainException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public CatalogDomainException()
    {
    }

    /// <summary>
    /// Constructor with exception message
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public CatalogDomainException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Constructor with message and inner exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    /// <returns></returns>
    public CatalogDomainException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}