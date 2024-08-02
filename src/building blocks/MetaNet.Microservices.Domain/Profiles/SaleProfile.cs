using AutoMapper;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Domain.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<Sale, SaleResponse>()
                .ReverseMap();
        }
    }
}
