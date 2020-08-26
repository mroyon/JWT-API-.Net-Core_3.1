using AspNetCore.CacheOutput.Redis.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JWTApiExample.Services
{
    public class RedisCacheInstaller : IInstaller
    {
      
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var conStr = configuration["RedisConnectionStrings:RedisCache"];
            services.AddRedisCacheOutput(conStr);
        }
    }
}
