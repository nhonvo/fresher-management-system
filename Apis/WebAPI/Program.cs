using Domain;
using Serilog;
using WebAPI.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // parse the configuration in appsettings
    var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
    builder.Services.AddSingleton(configuration);

    var app = await builder
        .ConfigureServices(
            configuration.ConnectionStrings.DatabaseConnection,
            configuration.MyAllowSpecificOrigins.UserApp,
            configuration.MyAllowSpecificOrigins.UserAppDev,
            configuration.Jwt.Key,
            configuration.Jwt.Issuer,
            configuration.Jwt.Audience,
            configuration.LoggingPath,
            configuration.LoggingTemplate)
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
