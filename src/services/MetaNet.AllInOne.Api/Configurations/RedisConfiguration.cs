namespace MetaNet.AllInOne.Api.Configurations
{
    public static class RedisConfiguration
    {

        public static IServiceCollection AddRedisConfiguration(this IServiceCollection services, IConfiguration configuration = null)
        {
            var redisConfigurationMultiplexer = configuration["Redis:EndpointMultiplexer"];
            var redisInstanceName = configuration["Redis:RedisInstanceName"];

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConfigurationMultiplexer;
                options.InstanceName = redisInstanceName;

            });

            return services;
        }

    }


}
