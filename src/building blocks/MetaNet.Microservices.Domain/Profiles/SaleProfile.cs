using AutoMapper;
using Esterdigi.Api.Core.Extensions;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Domain.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.SaleStatus, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.SaleStatus)))
                .ForMember(dest => dest.PaymentFormat, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.PaymentFormat)))
                .ReverseMap();
        }
    }
}
