using Serilog;

namespace WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // write to console 
                Console.WriteLine("GlobalExceptionMiddleware");
                Console.WriteLine(ex.Message);
                await context.Response.WriteAsync(ex.ToString());
                //writing log
                Log.Information("GlobalExceptionMiddleware");
                Log.Information(ex.Message.ToString());
                Log.Information(ex.ToString());
            }
        }
    }
}
