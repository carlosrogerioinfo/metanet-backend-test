using AutoMapper;
using Esterdigi.Api.Core.Extensions;
using MetaNet.Microservices.Domain.Entities;
using MetaNet.Microservices.Domain.Http.Response;

namespace MetaNet.Microservices.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => src.UserType))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.UserType)))
                .ReverseMap();
        }
    }
}
