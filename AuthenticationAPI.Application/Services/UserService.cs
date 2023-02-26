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
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper, IAuthenticationService authenticationService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<IEnumerable<UserViewModel>> GetAsync(GetUsersFilter filter)
        {
            var pagedResult = await _userRepository.GetAsync(filter);

            return _mapper.Map<IEnumerable<UserViewModel>>(pagedResult);
        }

        public async Task<string> CreateAsync(UserViewModel viewModel)
        {
            var entity = _mapper.Map<User>(viewModel);

            if (string.IsNullOrEmpty(viewModel.Password) || string.IsNullOrEmpty(viewModel.ConfirmationPassword))
                throw new Exception("Password cannot be null!");

            if (!viewModel.Password.SequenceEqual(viewModel.ConfirmationPassword))
                throw new Exception("Passwords do not match!");

            entity.Password = _authenticationService.EncryptUserPassword(viewModel.Password);

            await _userRepository.AddAsync(entity);

            return await _authenticationService.GenerateUserJWTAsync(entity);
        }

        public async Task<string> ValidateUserPasswordAsync(Guid idUser, string password)
        {
            var user = await _userRepository.GetByIdAsync(idUser);

            if (user is null)
                throw new Exception("User does not exist!");

            string token = await _authenticationService.ValidateUserPasswordAsync(user, password);

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Password do not match!");

            return token;
        }

        public async Task UpdateAsync(UserViewModel viewModel)
        {
            var entity = _mapper.Map<User>(viewModel);

            await _userRepository.UpdateAsync(entity);
        }
    }
}
