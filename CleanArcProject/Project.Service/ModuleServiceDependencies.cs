using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Abstracts;
using Project.Infrastructure.Repositories;

namespace Project.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
           services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }

    }
}
