using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Infrastructure.Context;

namespace WebApi.Infrastructure.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services,IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultString"),serverVersion)
            );
            return services;
        }
    }
}