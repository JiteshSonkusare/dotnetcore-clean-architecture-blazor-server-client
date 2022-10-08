namespace Domain.Contracts
{
    public abstract class AuditableEntity<TId> : IAuditableEntity<TId?>
    {
        public TId? Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastModifiedOn { get; set; }
    }
}
