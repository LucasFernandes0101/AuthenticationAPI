using AuthenticationAPI.Domain.Entities;
using AuthenticationAPI.Domain.ViewModels;
using AutoMapper;

namespace AuthenticationAPI.Domain.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.ChangedAt, opt => opt.MapFrom(src => src.ChangedAt))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role));

            CreateMap<UserViewModel, User>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.IsActive, opt => opt.MapFrom(src => src.IsActive))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.ChangedAt, opt => opt.MapFrom(src => src.ChangedAt))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(x => x.Role, opt => opt.MapFrom(src => src.Role));
        }
    }
}
