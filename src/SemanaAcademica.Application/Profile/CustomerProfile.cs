using AutoMapper;
using SemanaAcademica.Application.Model.Customer;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Application.Profile
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, CustomerModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}