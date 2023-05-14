namespace Application.Cache
{
    public interface ICacheService
    {
        bool TryGet<T>(string key, out T value);
        void Set<T>(string key, T value);
        void Remove(string key);
    }
}
