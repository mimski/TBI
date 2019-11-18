using Loanda.EmailClient;
using Loanda.EmailClient.BackgroundService;
using Loanda.EmailClient.Contracts;
using Loanda.Services;
using Loanda.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Loanda.Web.Infrastracture
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IEmailAttachmentService, EmailAttachmentService>();
            services.AddScoped<ILoanApplicationService, LoanApplicationService>();
            services.AddScoped<IGmailApi, GmailApi>();

            services.AddHostedService<GmailHostedService>();

            return services;
        }
    }
}
