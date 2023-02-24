using AspNetCore.IQueryable.Extensions;

namespace AuthenticationAPI.Domain.Base
{
    public class BaseFilter : ICustomQueryable
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ChangedAt { get; set; }
        public bool? Active { get; set; }
        public int Page { get; set; } = 1;
        public int MaxResults { get; set; } = 10;
    }
}
