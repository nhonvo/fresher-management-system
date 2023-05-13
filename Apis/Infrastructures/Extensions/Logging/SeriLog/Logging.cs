using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Infrastructures.Extensions.Logging.Serilog
{
    public static class Logging
    {
        public static void AddSerilog(this WebApplicationBuilder builder, string loggingPath, string logTemplate)
        {
            Log.Logger = new LoggerConfiguration()
                      .MinimumLevel.Information()
                      .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                      .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                      .Enrich.FromLogContext()
                      .WriteTo.Console(outputTemplate: logTemplate, theme: AnsiConsoleTheme.Grayscale)
                      .WriteTo.File(
                          loggingPath,
                          rollingInterval: RollingInterval.Day,
                          outputTemplate: logTemplate)
                      .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);
        }
    }
}