using AutoMapper;
using DDD_CQRS.Domain.Users;

namespace DDD_CQRS.Application.Features.Users.Queries.GetByEmail
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>();
        }
    }

}
