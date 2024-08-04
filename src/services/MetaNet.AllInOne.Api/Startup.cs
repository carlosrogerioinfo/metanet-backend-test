using MetaNet.Microservices.Core.Jwt.Configuration;
using MetaNet.Microservices.Core.Jwt.Settings;
using MetaNet.Microservices.Infrastructure.Contexts;
using MetaNet.AllInOne.Api.Configurations;
using Microsoft.EntityFrameworkCore;
using MetaNet.Microservices.Domain.Repositories.Base;
using MetaNet.Microservices.Infrastructure.Repositories.Base;
using MetaNet.Microservices.Service;
using Microsoft.Data.SqlClient;
using System.Data;
using Npgsql;

namespace MetaNet.AllInOne.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddDataContextConfigurations(services);

            AddDapperConfigurations(services);

            services.AddAutoMapperConfiguration();

            services.AddWebApiConfiguration();

            services.AddRedisConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddDependencyInjectionConfiguration();

            services.AddJWTBearerConfiguration(Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJWTBearerConfiguration();

            app.UseWebApiConfiguration(true);

            app.UseSwaggerConfiguration(env);
        }

        private void AddDataContextConfigurations(IServiceCollection services)
        {
            services.AddDbContext<MetaNetDataContext>(opt =>
            {
                opt.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection"));
                opt.EnableSensitiveDataLogging();

            }, ServiceLifetime.Scoped);
        }

        private void AddDapperConfigurations(IServiceCollection services)
        {
            services.AddTransient<IDbConnection>((serviceProvider) => new NpgsqlConnection(Configuration.GetConnectionString("DatabaseConnection")));
        }
    }
}
