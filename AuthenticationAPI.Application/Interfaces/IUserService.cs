using AuthenticationAPI.Domain.Filters;
using AuthenticationAPI.Domain.ViewModels;

namespace AuthenticationAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateAsync(UserViewModel viewModel);
        Task UpdateAsync(UserViewModel viewModel);
        Task<IEnumerable<UserViewModel>> GetAsync(GetUsersFilter filter);
        Task<string> ValidateUserPasswordAsync(Guid idUser, string password);
    }
}
