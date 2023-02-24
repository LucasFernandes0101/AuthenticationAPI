namespace AuthenticationAPI.Domain.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime ChangedAt { get; set; }
    }
}
