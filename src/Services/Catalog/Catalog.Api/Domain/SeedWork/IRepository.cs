namespace eShop.Services.Catalog.Domain.SeedWork;

/// <summary>
/// The generic repository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Unit Of Work
    /// </summary>
    /// <value></value>
    IUnitOfWork UnitOfWork { get; }
}
