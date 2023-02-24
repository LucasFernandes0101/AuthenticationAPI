namespace AuthenticationAPI.Domain.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; } = new Guid();
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
