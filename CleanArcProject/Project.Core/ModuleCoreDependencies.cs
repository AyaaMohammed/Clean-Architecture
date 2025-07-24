using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Project.Core.Behaviors;
using Project.Service.Abstracts;
using Project.Service.Implementation;
using System.Reflection;
using FluentValidation;
using Project.Core.Behaviors;

namespace Project.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            // Configure AutoMapper
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ModuleCoreDependencies).Assembly));
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ModuleCoreDependencies).Assembly));
            // Register other dependencies here
            services.AddTransient<IStudentService, StudentService>();

            // Get Validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
             
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
