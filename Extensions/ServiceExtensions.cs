using jex_assessment.Managers;
using jex_assessment.Managers.Interfaces;
using jex_assessment.Repositories;
using jex_assessment.Repositories.Interfaces;

namespace jex_assessment.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyManager, CompanyManager>();
            services.AddScoped<IVacancyRepository, VacancyRepository>();
            services.AddScoped<IVacancyManager, VacancyManager>();
            return services;
        }
    }
}
