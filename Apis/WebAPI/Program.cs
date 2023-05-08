using Application;
using Application.Commons;
using Infrastructures;
using WebAPI;
using WebAPI.Extensions;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// parse the configuration in appsettings
var configuration = builder.Configuration.Get<AppConfiguration>();
builder.Services.AddSingleton(configuration);
// builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
// builder.Services.AddApplicationService();
// builder.Services.AddWebAPIService();

/*
    register with singleton life time
    now we can use dependency injection for AppConfiguration
*/

 var app = await builder
        .ConfigureServices(configuration.DatabaseConnection)
        .ConfigurePipelineAsync();

app.Run();

// this line tell intergrasion test
// https://stackoverflow.com/questions/69991983/deps-file-missing-for-dotnet-6-integration-tests
public partial class Program { }