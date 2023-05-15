using Application.Health;
using HealthChecks.UI.Client;
using Infrastructures.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructures.Extensions
{
    public static class HealthCheckExtension
    {
        public static void AddHealthCheck(this WebApplicationBuilder builder, string databaseConnection)
        {
            builder.Services.AddHealthChecks()
                .AddCheck<HealthCheck>(nameof(HealthCheck), tags: new[] { "TrainingManagementSystem" })
                .AddDbContextCheck<ApplicationDbContext>(tags: new[] { "db context" })
                .AddSqlServer(databaseConnection ?? throw new InvalidOperationException(), tags: new[] { "database" });

            builder.Services
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