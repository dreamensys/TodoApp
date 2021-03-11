using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Contracts;
using ToDo.Infrastructure.Data;

namespace ToDo.Infrastructure.Extensions
{
    public static class DataExtension
    {
        public static IServiceCollection AddDataConfiguration(this IServiceCollection services, string environment)
        {
            services.AddScoped(typeof(IRepository<>), typeof(InMemoryDbRepository<>));
            services.AddScoped<DbContext>(sp => sp.GetService<ApplicationContext>());
            services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "TodoDB"));

            return services;
        }
    }
}
