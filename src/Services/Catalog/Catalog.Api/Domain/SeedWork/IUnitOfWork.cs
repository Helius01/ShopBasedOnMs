namespace eShop.Services.Catalog.Domain.SeedWork;

/// <summary>
/// TODO:DOC
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// TODO:DOC
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// TODO:DOC
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
