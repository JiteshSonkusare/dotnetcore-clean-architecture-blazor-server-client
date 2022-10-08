using System;
namespace Domain.Contracts
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedOn { get; set; }

        DateTime? LastModifiedOn { get; set; }
    }
}
