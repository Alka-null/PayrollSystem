//using EWallet.Utility.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ConfigurationEntities;

namespace PayrollSystem.ExtensionMethods
{
    public static class AppSettingsExtension
    {
        public static IServiceCollection AddAppSetttings(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<JwtSettings>(_config.GetSection("JwtSettings"));


            services.Configure<BgServiceSettings>(_config.GetSection("BgServiceSettings"));


            return services;
        }
    }
}
