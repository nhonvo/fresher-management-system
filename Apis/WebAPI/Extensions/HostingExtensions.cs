using Application;
using Infrastructures;
using Infrastructures.Extensions;
using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder,
        string databaseConnection,
        string userApp,
        string userAppDev,
        string key,
        string issuer,
        string audience,
        string loggingPath,
        string loggingTemplate)
    {
        builder.Services.AddInfrastructuresService(databaseConnection);
        builder.Services.AddApplicationService();
        builder.Services.AddWebAPIService(userApp, userAppDev, key, issuer, audience);
        builder.AddSerilog(loggingPath, loggingTemplate);

        return builder.Build();
    }
    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || true)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
            app.UseCors("MyCors");
            using var scope = app.Services.CreateScope();
        }
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("MyCors");
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<PerformanceMiddleware>();
        app.MapHealthCheck();
        app.UseResponseCompression();
        app.UseHttpsRedirection();
        // todo authentication
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
