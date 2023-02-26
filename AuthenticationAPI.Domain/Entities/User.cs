using AuthenticationAPI.Domain.Base;
using AuthenticationAPI.Domain.Enums;

namespace AuthenticationAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public RoleType Role { get; set; }
    }
}
