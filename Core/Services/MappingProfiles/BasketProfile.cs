using AutoMapper;
using Domain.Entities.BasketModule;
using Shared.Dtos.BasketModule;

namespace Services.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
        }
    }
}
