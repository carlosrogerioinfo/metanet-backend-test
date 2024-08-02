using MetaNet.Microservices.Core.Jwt;
using MetaNet.Microservices.Domain.Repositories;
using MetaNet.Microservices.Infrastructure.Repositories;
using MetaNet.Microservices.Infrastructure.Transactions;
using MetaNet.Microservices.Service;

namespace MetaNet.Sales.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ProductService>();

            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<SaleService>();

            services.AddScoped<ISaleItemRepository, SaleItemRepository>();
            services.AddScoped<SaleItemService>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
