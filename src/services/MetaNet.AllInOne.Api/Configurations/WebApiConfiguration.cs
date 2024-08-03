﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System.Text.Json.Serialization;

namespace MetaNet.AllInOne.Api.Configurations
{
    public static class WebApiConfiguration
    {

        public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services, IConfiguration configuration = null)
        {
            var redisConfiguration = configuration["Redis:Endpoint"];
            var redisPassword = configuration["Redis:Password"];

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfiguration;
                options.InstanceName = "RedisInstance";
            });

            //services.Add(ServiceDescriptor.Singleton<IDistributedCache, RedisCache>());

            services.AddControllers()
                .AddJsonOptions(options => {
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

    }
}
