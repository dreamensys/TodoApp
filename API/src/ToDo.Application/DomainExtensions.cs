using Microsoft.Extensions.DependencyInjection;
using ToDo.Application.Handlers;

namespace ToDo.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddDomainConfiguration(this IServiceCollection services, string environment)
        {
            services.AddScoped<ITodoItemQueryHandler, TodoItemQueryHandler>();
            services.AddScoped<ITodoItemCommandHandler, TodoItemCommandHandler>();
            return services;
        }
    }
}
