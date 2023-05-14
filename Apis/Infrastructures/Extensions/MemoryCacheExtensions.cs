using Application.Cache;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructures.Extensions
{
    public static class MemoryCacheExtensions
    {
        public static void AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();
        }
    }

}