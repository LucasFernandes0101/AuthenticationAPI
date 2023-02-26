using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IVaultManager _vaultManager;
        public AuthenticationService(IVaultManager vaultManager)
        {
            _vaultManager = vaultManager;
        }

        public async Task<string> ValidateUserPasswordAsync(User user, string password)
        {
            bool passwordMatches = false;

            if (!string.IsNullOrWhiteSpace(user.Password) && BCrypt.Net.BCrypt.Verify(password, user.Password))
                passwordMatches = true;

            if (!passwordMatches)
                return string.Empty;

            return await GenerateUserJWTAsync(user);
        }

        public async Task<string> GenerateUserJWTAsync(User user)
        {
            object? keyJWT;

            try
            {
                await _vaultManager.EnsureConnectionAsync();

                keyJWT = await _vaultManager.GetValueAsync("secret/authentication_api", "KEY_JWT");

                if (keyJWT == null)
                    throw new Exception("The JWT encryption key dont exists in the Vault.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Cant connect to Vault Server: {ex.Message}");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var bytesKeyJWT = Encoding.UTF8.GetBytes(keyJWT?.ToString() ?? string.Empty);
            var symmetricSecurityKey = new SymmetricSecurityKey(bytesKeyJWT);

            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddHours(1));

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public string EncryptUserPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
    }
}