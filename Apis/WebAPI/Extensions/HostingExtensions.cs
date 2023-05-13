using Application;
using HealthChecks.UI.Client;
using Infrastructures;
using Infrastructures.Extensions.Logging.Serilog;
using Infrastructures.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder,
        string databaseConnection,
        string userApp,
        string key,
        string issuer,
        string audience,
        string loggingPath,
        string loggingTemplate)
    {
        builder.Services.AddInfrastructuresService(databaseConnection);
        builder.Services.AddApplicationService();
        builder.Services.AddWebAPIService(userApp, key, issuer, audience);

        builder.AddSerilog(loggingPath, loggingTemplate);

        return builder.Build();
    }
    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();

            using var scope = app.Services.CreateScope();
            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
            await initialiser.InitialiseAsync();
            await initialiser.SeedAsync();
        }

        app.UseCors("MyCors");
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<PerformanceMiddleware>();
        // app.MapHealthChecks("/health");
        app.MapHealthChecks("/health", new HealthCheckOptions()
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
        app.UseResponseCompression();
        app.UseHttpsRedirection();
        // todo authentication
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
