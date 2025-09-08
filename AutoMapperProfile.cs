using AutoMapper;
using PojisteniWebApp.Models;

namespace PojisteniWebApp
{
    // překlad mezi databázovými modely a ViewModely (DTO).
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {            
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Insurance, InsuranceViewModel>().ReverseMap();
        }
    }
}