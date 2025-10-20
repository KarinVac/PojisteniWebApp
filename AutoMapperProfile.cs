using AutoMapper;
using PojisteniWebApp.Models;

namespace PojisteniWebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {            
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Insurance, InsuranceViewModel>().ReverseMap();
        }
    }
}