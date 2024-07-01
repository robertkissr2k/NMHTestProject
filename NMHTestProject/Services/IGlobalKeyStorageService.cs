namespace NMHTestProject.Services
{
    public interface IGlobalKeyStorageService
    {
        TValue? Get<TValue>(string key);

        void Set<TValue>(string key, TValue value);
    }
}