

using Application;
using Infrastructures;
using Infrastructures.Persistence;
using WebAPI;
using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder, string DatabaseConnection)
    {
        //builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructuresService(DatabaseConnection);
        builder.Services.AddApplicationService();
        builder.Services.AddWebAPIService();


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

        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<PerformanceMiddleware>();
        app.MapHealthChecks("/healthchecks");
        app.UseHttpsRedirection();
        // todo authentication
        app.UseAuthorization();

        app.MapControllers();



        return app;
    }
}
