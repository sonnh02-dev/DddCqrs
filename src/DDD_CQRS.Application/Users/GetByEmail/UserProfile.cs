using AutoMapper;
using DDD_CQRS.Application.Users.GetById;
using DDD_CQRS.Domain.Users;

namespace DDD_CQRS.Application.Users.GetByEmail
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDetailResponse>();
        }
    }

}
