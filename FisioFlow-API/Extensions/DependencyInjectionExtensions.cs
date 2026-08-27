using FisioFlow_API.Repositories;
using FisioFlow_API.Repositories.Contracts;

namespace FisioFlow_API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnityOfWork>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}