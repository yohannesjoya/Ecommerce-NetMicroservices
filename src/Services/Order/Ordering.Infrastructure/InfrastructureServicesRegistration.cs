using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models.EmailModels;
using Ordering.Infrastructure.Mail;


namespace Ordering.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration) {

          
            services.Configure<EmailSettings>(c=> configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();
           


            return services;
        
        }
    }
}
