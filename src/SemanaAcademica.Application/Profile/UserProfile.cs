using AutoMapper;
using SemanaAcademica.Application.Model.User;
using SemanaAcademica.Application.Models.User;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Application.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserModel>().ReverseMap();
        }
    }
}