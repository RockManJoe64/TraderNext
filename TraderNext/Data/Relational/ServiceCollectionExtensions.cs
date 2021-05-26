using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TraderNext.Common;

namespace TraderNext.Data.Relational.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMySqlDbContext(this IServiceCollection services)
        {
            var mysqlConnectionString = Environment.GetEnvironmentVariable(EnvironmentVariables.MySqlConnectionString);

            var serverVersion = ServerVersion.AutoDetect(mysqlConnectionString);

            services.AddDbContextPool<LambdaDbContext>(
                options => options.UseMySql(mysqlConnectionString, serverVersion)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
            );

            return services;
        }
    }
}
