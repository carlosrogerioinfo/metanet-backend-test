using AutoMapper;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Domain.Profiles
{
    public class SaleItemProfile : Profile
    {
        public SaleItemProfile()
        {
            CreateMap<SaleItem, SaleItemResponse>()
                //.ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.AccountType)))
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
                .ReverseMap();
        }
    }
}
