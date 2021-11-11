using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using saavor.Application.BalanceKitchen.Query;
using saavor.Application.CountryState.Query.GetCountryState;
using saavor.Application.Kitchen.Commands.UpsertKitchenRequest;
using saavor.Application.Kitchen.Query;
using saavor.Application.Messages.Commands.Upsert;
using saavor.Application.Messages.Query;
using saavor.Application.Notification.Query;
using saavor.Application.SignUp.Commands.Upsert;
using saavor.Application.SignUp.Query.Login;
using saavor.Shared.Interfaces;
using saavor.Shared.Interfaces.SignUp;

namespace saavor.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUpsertSignupCommand, UpsertSignupCommand>();
            services.AddScoped<IGetLoginQuery, GetLoginQuery>();
            services.AddScoped<GetCountryStateQuery>();
            services.AddScoped<GetKitchenQuery>();
            services.AddScoped<GetKitchenDashboardQuery>();
            services.AddScoped<IUpsertKitchenRequestCommand, UpsertKitchenRequestCommand> ();
            services.AddScoped<MessageUpsertCommand>();
            services.AddScoped<GetMessageQuery>();
            services.AddScoped<GetKitchenBalanceQuery>();
            services.AddScoped<NotificationQuery>();
            return services;
        }
    }
}
