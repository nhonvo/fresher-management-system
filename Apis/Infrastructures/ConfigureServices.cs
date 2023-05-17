using Application;
using Application.Common.Interfaces;
using Application.Health;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Infrastructure.Files;
using Infrastructures.Extensions;
using Infrastructures.Mappers;
using Infrastructures.Persistence;
using Infrastructures.Repositories;
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
            services.AddSingleton<HealthService>();
            services.AddHealthCheck();

            #region Repositories
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<IClassStudentRepository, ClassStudentRepository>();
            services.AddScoped<IOutputStandardRepository, OutputStandardRepository>();
            services.AddScoped<ISyllabusRepository, SyllabusRepository>();
            services.AddScoped<ITestAssessmentRepository, TestAssessmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
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
            // this configuration just use in-memory for fast develop
            //services.AddDbContext<AppDbContext>(option => option.UseInMemoryDatabase("test"));
            #endregion

            #region Mapping
            //TODO: remove later when we have mapping each component
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddAutoMapper(typeof(TestAssessementProfile).Assembly);

            #endregion

            #region Identity
            #endregion
            return services;
        }
    }
}
