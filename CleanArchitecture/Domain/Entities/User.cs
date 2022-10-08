using Domain.Contracts;

namespace Domain.Entities
{
    public class User : AuditableEntity<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName  { get; set; } = null!;
        public string UserId    { get; set; } = null!;
        public int? Mobile      { get; set; }
        public int? Phone       { get; set; }
        public string? Address  { get; set; }
        public string Status    { get; set; } = null!;
        public string Gender    { get; set; } = null!;
    }
}