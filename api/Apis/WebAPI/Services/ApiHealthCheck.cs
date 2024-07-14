using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace WebAPI.Services
{
    public class ApiHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var url = "https://airport-info.p.rapidapi.com/airport";
            var client = new RestClient();
            var request = new RestRequest(url, Method.Get);

            request.AddHeader("X-RapidAPI-Key", "SIGN-UP-FOR-KEY");
            request.AddHeader("X-RapidAPI-Host", "airport-info.p.rapidapi.com");

            var response = client.Execute(request);
            if (response.IsSuccessful)
                return Task.FromResult(HealthCheckResult.Healthy());
            else
                return Task.FromResult(HealthCheckResult.Unhealthy());
        }
    }
}
