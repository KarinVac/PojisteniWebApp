using AutoMapper;
using PojisteniWebApp.Models;

namespace PojisteniWebApp
{
    // V této třídě definujeme všechna pravidla pro "překlad" mezi databázovými modely a ViewModely (DTO).
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {            
            CreateMap<Client, ClientViewModel>().ReverseMap();
            CreateMap<Insurance, InsuranceViewModel>().ReverseMap();
        }
    }
}