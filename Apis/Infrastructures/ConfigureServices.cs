using Application;
using Application.Interfaces;
using Application.Services;
using Infrastructures.Mappers;
using Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, string databaseConnection)
        {
            #region UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            #region Repositories
            // services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ICurrentTime, CurrentTime>();
            #endregion
            #region DbContext 
            services.AddDbContext<DbContext, ApplicationDbContext>(options =>
            {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (env == "Development")
                {
                    options.UseSqlServer(databaseConnection);
                }
                else
                {
                    options.UseSqlServer(databaseConnection);
                }
            });
            services.AddScoped<ApplicationDbContextInitializer>();

            // this configuration just use in-memory for fast develop
            //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));
            #endregion

            #region Mapping
            //TODO: remove later when we have mapping each component
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);

            #endregion

            #region Identity
            #endregion
            return services;
        }
    }
}
