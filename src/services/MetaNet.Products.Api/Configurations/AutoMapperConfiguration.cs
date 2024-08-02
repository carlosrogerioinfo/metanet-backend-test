﻿using MetaNet.Microservices.Domain.Profiles;

namespace MetaNet.Products.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(ProductProfile),
                typeof(SaleProfile),
                typeof(SaleItemProfile)
            );

            return services;
        }
    }
}
