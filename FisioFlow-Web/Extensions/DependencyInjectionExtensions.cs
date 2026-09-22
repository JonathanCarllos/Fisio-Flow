using FisioFlow_Web.Areas.Admin.Services;
using FisioFlow_Web.Areas.Admin.Services.Interfaces;

namespace FisioFlow_Web.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IPatientServices, PatientServices>();

            return services;
        }
    }
}