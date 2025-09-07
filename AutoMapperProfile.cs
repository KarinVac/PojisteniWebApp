using AutoMapper;
using PojisteniWebApp.Models;

namespace PojisteniWebApp
{
    // V této třídě definujeme všechna pravidla pro "překlad" mezi databázovými modely a ViewModely (DTO).
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Vytvoříme pravidlo pro překlad z Client na ClientViewModel a naopak.
            CreateMap<Client, ClientViewModel>();
            CreateMap<ClientViewModel, Client>();
            
        }
    }
}