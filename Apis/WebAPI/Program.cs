using Domain;
using Serilog;
using WebAPI.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // parse the configuration in appsettings
    var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
    builder.Services.AddSingleton(configuration);

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(configuration.LoggingPath, rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.File(configuration.LoggingPath, rollingInterval: RollingInterval.Day)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    var app = await builder
        .ConfigureServices(
            configuration.ConnectionStrings.DatabaseConnection,
            configuration.MyAllowSpecificOrigins.UserApp,
            configuration.Jwt.Key,
            configuration.Jwt.Issuer,
            configuration.Jwt.Audience)
        .ConfigurePipelineAsync();
    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled Exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }
