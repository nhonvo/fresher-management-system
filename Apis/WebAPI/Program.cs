using Application.Commons;
using Domain.Commons;
using Serilog;
using WebAPI.Extensions;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(@"D:\SRS_FA TRAINING MANAGEMENT SYSTEM\Logging\logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.File(@"D:\SRS_FA TRAINING MANAGEMENT SYSTEM\Logging\logs.txt", rollingInterval: RollingInterval.Day)
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    // parse the configuration in appsettings
    var configuration = builder.Configuration.Get<AppConfiguration>();
    builder.Services.AddSingleton(configuration);

    /*
        register with singleton life time
        now we can use dependency injection for AppConfiguration
    */

    var app = await builder
           .ConfigureServices(configuration.DatabaseConnection)
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


// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }
