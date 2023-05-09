

using System.IO.Compression;
using Application;
using Infrastructures;
using Infrastructures.Persistence;
using Microsoft.AspNetCore.ResponseCompression;
using WebAPI;
using WebAPI.Middlewares;

namespace WebAPI.Extensions;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder, string DatabaseConnection, string UserApp)
    {
        builder.Services.AddInfrastructuresService(DatabaseConnection);
        builder.Services.AddApplicationService();
        builder.Services.AddWebAPIService();



        #region Compression
        builder.Services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
            options.Providers.Add<BrotliCompressionProvider>();
        });
        builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.SmallestSize;
        });
        #endregion
        // Cors
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "MyCors",
            policy =>
            {
                policy.AllowAnyHeader()
             .AllowAnyMethod()
             .WithOrigins(new string[] { UserApp });
            });
        });
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
        app.MapHealthChecks("/healthchecks");

        app.UseHttpsRedirection();
        // todo authentication
        app.UseAuthorization();

        app.MapControllers();



        return app;
    }
}
