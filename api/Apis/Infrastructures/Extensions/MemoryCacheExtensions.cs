using Application.Cache;
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