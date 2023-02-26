using AuthenticationAPI.Domain.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace AuthenticationAPI.Domain.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmationPassword { get; set; }
        public RoleType Role { get; set; }
    }
}
