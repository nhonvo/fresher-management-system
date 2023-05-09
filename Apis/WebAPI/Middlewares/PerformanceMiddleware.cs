using System.Diagnostics;
using Infrastructures;
using Serilog;

namespace WebAPI.Middlewares
{
    public class PerformanceMiddleware : IMiddleware
    {
        private readonly Stopwatch _stopwatch;

        public PerformanceMiddleware(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Restart();
            _stopwatch.Start();
            Log.Information("Start performance record");

            await next(context);

            Log.Information("End performance record");
            _stopwatch.Stop();

            TimeSpan timeTaken = _stopwatch.Elapsed;
            Log.Information("Time taken: " + timeTaken.ToString(@"m\:ss\.fff"));

        }
    }
}
