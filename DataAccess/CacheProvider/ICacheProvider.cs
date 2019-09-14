namespace DataAccess.CacheProvider
{
    public interface ICacheProvider
    {
        T Fetch<T>(string key);

        bool Save(string key, object value);
    }
}