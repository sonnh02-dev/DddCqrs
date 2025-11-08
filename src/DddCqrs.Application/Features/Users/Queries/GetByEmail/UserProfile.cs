using AutoMapper;
using DddCqrs.Domain.Users;

namespace DddCqrs.Application.Features.Users.Queries.GetByEmail
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }

}
