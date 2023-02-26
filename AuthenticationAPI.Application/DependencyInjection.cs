using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Application.Services;
using AuthenticationAPI.Domain.Interfaces;
using AuthenticationAPI.Domain.Mappers;
using AuthenticationAPI.Infrastructure.Contexts;
using AuthenticationAPI.Infrastructure.Managers;
using AuthenticationAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp;

namespace AuthenticationAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
            service.ResolveServices();
            service.ResolveRepositories();
            service.ResolveManagers();
            service.ResolveAutoMapperProfiles();

            //Add JWT
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("KEY_JWT") ?? string.Empty)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            return service;
        }

        private static void ResolveManagers(this IServiceCollection service)
        {
            service.AddSingleton<IVaultManager, VaultManager>(x =>
            {
                string urlVaultSharp = Environment.GetEnvironmentVariable("URL_VAULTSHARP") ?? string.Empty;
                string tokenVaultSharp = Environment.GetEnvironmentVariable("TOKEN_VAULTSHARP") ?? string.Empty;
                var vaultClientSettings = new VaultClientSettings(urlVaultSharp, new TokenAuthMethodInfo(tokenVaultSharp));
                var vaultClient = new VaultClient(vaultClientSettings);
                return new VaultManager(vaultClient);
            });
        }

        private static void ResolveRepositories(this IServiceCollection service)
        {
            service.AddDbContext<SqlDbContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("AuthenticationAPIConnectionString")));

            service.AddScoped<IUserRepository, UserRepository>();
        }

        private static void ResolveServices(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        private static void ResolveAutoMapperProfiles(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(UserProfile));
        }

    }
}
