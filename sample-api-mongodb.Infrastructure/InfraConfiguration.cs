using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.Entities;
using sample_api_mongodb.Core.Interfaces.Repositories;
using sample_api_mongodb.Infrastructure.Repositories;

namespace sample_api_mongodb.Infrastructure
{
    public static class InfraConfiguration
    {
        public static void InjectInfraConfig(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<DbSettings>>().Value);

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }
    }
}
