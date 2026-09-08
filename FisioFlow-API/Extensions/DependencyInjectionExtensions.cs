using FisioFlow_API.Context;
using FisioFlow_API.Repositories;
using FisioFlow_API.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;

namespace FisioFlow_API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {         

            services.AddAuthorization();
            services.AddAuthentication("Bearer").AddJwtBearer();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPhysiotherapistRepository, PhysiotherapistRepository>();
            services.AddScoped<ITreatment, TreatmentRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }
    }
}