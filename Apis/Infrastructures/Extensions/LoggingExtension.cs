using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Globalization;

namespace Infrastructures.Extensions
{
    public static class SeriLogExtension
    {
        public static void AddSerilog(this WebApplicationBuilder builder, string loggingPath, string logTemplate)
        {
            Log.Logger = new LoggerConfiguration()
                      .MinimumLevel.Information()
                      .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                      .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                      .Enrich.FromLogContext()
                      .WriteTo.Console(outputTemplate: logTemplate,
                                       restrictedToMinimumLevel: LogEventLevel.Information,
                                       formatProvider: CultureInfo.InvariantCulture,
                                       standardErrorFromLevel: LogEventLevel.Error,
                                       theme: AnsiConsoleTheme.Literate)
                    .WriteTo.File(
                          loggingPath,
                          rollingInterval: RollingInterval.Day,
                          outputTemplate: logTemplate)
                      .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);
        }
    }

}