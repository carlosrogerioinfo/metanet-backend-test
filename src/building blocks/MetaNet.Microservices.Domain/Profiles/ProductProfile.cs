using AutoMapper;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Domain.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ReverseMap();
        }
    }
}
