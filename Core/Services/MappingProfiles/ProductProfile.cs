using AutoMapper;
using Domain.Entities.ProductModule;
using Shared.Dtos.ProductModule;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand,BrandResultDto>();
            CreateMap<ProductType,TypeResultDto>();
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.BrandName, optains => optains.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, optains => optains.MapFrom(src =>src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, optains => optains.MapFrom<PictureUrlResolver>());
        }
    }
}
