using MediatR;

namespace ShopBasedOnMs.Services.Catalog.CatalogApi.Domain.SeedWork;

#pragma warning disable 1591
/// <summary>
/// Base Entity
/// </summary>
public abstract class Entity
{
    int? _requestedHashCode;
    int _Id;
    public virtual int Id
    {
        get
        {
            return _Id;
        }
        protected set
        {
            _Id = value;
        }
    }

    private List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Add a new DomainEvent
    /// </summary>
    /// <param name="eventItem"></param>
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    /// <summary>
    /// Remove a DomainEvent if its not empty
    /// </summary>
    /// <param name="eventItem"></param>
    public void RemoveDomainEvent(INotification eventItem)
    {
        if (_domainEvents.Count == 0)
            return;

        _domainEvents.Remove(eventItem);
    }

    /// <summary>
    /// Clear all DomainEvents
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// Checks the Transient mode
    /// </summary>
    /// <returns></returns>
    public bool IsTransient()
    {
        return this.Id == default(Int32);
    }

    /// <summary>
    /// Overriding the Equals operator 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        //If the given object is not null or its not a subtype of Entity type = its not equal
        if (obj == null || !(obj is Entity))
            return false;

        //If both references are equals = its equal
        if (Object.ReferenceEquals(this, obj))
            return true;

        //If the types are not equals = its not equal
        if (this.GetType() != obj.GetType())
            return false;

        Entity item = (Entity)obj;

        //If the Id of one of them is 0 = its not equal
        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            //Checking their ids as result
            return item.Id == this.Id;
    }

    /// <summary>
    /// Overriding the GetHashCode function
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }

    /**********************
     * EQUALITY OPERATORS *
     **********************/
    public static bool operator ==(Entity left, Entity right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity left, Entity right)
    {
        return !(left == right);
    }
}
