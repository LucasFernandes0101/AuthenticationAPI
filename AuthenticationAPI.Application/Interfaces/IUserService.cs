using AuthenticationAPI.Domain.Filters;
using AuthenticationAPI.Domain.ViewModels;

namespace AuthenticationAPI.Application.Interfaces
{
    public interface IUserService
    {
        Task PostAsync(UserViewModel viewModel);
        Task PutAsync(UserViewModel viewModel);
        Task<IEnumerable<UserViewModel>> GetAsync(GetUsersFilter filter);
    }
}
