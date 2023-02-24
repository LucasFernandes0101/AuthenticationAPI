using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.Filters;
using AuthenticationAPI.Domain.Interfaces;
using AuthenticationAPI.Domain.ViewModels;
using AutoMapper;

namespace AuthenticationAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> GetAsync(GetUsersFilter filter)
        {
            var pagedResult = await _userRepository.GetAsync(filter);

            return _mapper.Map<IEnumerable<UserViewModel>>(pagedResult);
        }

        public async Task PostAsync(UserViewModel viewModel)
        {
            var entity = _mapper.Map<User>(viewModel);

            await _userRepository.AddAsync(entity);
        }

        public async Task PutAsync(UserViewModel viewModel)
        {
            var entity = _mapper.Map<User>(viewModel);

            await _userRepository.UpdateAsync(entity);
        }
    }
}
