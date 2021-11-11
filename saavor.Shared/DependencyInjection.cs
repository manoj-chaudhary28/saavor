using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using saavor.Shared.Interfaces;
using saavor.Shared.Services;

namespace saavor.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClaimService, ClaimService>();
            return services;
        }
    }
}
