using AuthenticationAPI.Domain.Entities;

namespace AuthenticationAPI.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> ValidateUserPasswordAsync(User user, string password);
        Task<string> GenerateUserJWTAsync(User user);
        string EncryptUserPassword(string password);
    }
}


