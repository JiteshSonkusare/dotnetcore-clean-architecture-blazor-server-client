using Application.Features.Users.Commands.UpsertUser;
using Application.Features.Users.Queries.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpsertUserCommand, User>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
