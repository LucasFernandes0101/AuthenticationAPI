namespace AuthenticationAPI.Domain.Interfaces
{
    public interface IVaultManager
    {
        Task EnsureConnectionAsync();
        Task<IDictionary<string, object>> GetKeysFromSecretAsync(string secretName);
        Task<object?> GetValueAsync(string secretName, string key);
    }
}
