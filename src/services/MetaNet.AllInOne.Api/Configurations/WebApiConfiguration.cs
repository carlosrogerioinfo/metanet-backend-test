using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;

namespace MetaNet.AllInOne.Api.Configurations
{
    public static class WebApiConfiguration
    {

        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration = null)
        {
            RedisConfigureConnection(services, configuration);

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }

        public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, bool useCors = false)
        {
            app.UseRouting();

            if (useCors)
            {
                app.UseCors(x => x
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            }

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

        private static void RedisConfigureConnection(IServiceCollection services, IConfiguration configuration)
        {
            var redisConfigurationMultiplexer = configuration["Redis:EndpointMultiplexer"];

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfigurationMultiplexer;
                options.InstanceName = "RedisInstance";

            });
        }


    }


}
