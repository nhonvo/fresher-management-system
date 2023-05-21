using Application.Health;
using HealthChecks.UI.Client;
using Infrastructures.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructures.Extensions
{
    public static class HealthCheckExtension
    {
        public static void AddHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<HealthCheck>(nameof(HealthCheck), tags: new[] { "TrainingManagementSystem" })
                .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "db context" })
                .AddSqlServer("workstation id=mockprojectDb.mssql.somee.com;packet size=4096;user id=binhdeptrai;pwd=123456789;data source=mockprojectDb.mssql.somee.com;persist security info=False;initial catalog=mockprojectDb ;TrustServerCertificate=True", tags: new[] { "database" });

            services
                .AddHealthChecksUI(options =>
                {
                    options.AddHealthCheckEndpoint("Health Check API", "/hc");
                    options.SetEvaluationTimeInSeconds(10);
                })
                .AddInMemoryStorage();
        }

        public static IApplicationBuilder MapHealthCheck(this WebApplication app)
        {
            app.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false,
                ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
            });
            app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");

            return app;
        }
    }

}