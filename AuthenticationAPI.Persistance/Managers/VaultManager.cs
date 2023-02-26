using AuthenticationAPI.Domain.Interfaces;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace AuthenticationAPI.Infrastructure.Managers
{
    public class VaultManager : IVaultManager
    {
        private IVaultClient _vaultClient;
        public VaultManager(IVaultClient vaultClient)
        {
            _vaultClient = vaultClient;
        }

        public async Task<IDictionary<string, object>> GetKeysFromSecretAsync(string secretName)
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretName);

            return secret.Data.Data;
        }

        public async Task<object?> GetValueAsync(string secretName, string key)
        {
            var secret = await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretName);
            bool keyExists = secret.Data.Data.TryGetValue(key, out var value);

            if (!keyExists)
                return null;

            return value;
        }

        public async Task EnsureConnectionAsync()
        {
            try
            {
                var healthStatus = await _vaultClient.V1.System.GetHealthStatusAsync();
                if (healthStatus == null || !healthStatus.Initialized || healthStatus.Sealed)
                {
                    string? urlVaultSharp = Environment.GetEnvironmentVariable("URL_VAULTSHARP");
                    string? tokenVaultSharp = Environment.GetEnvironmentVariable("TOKEN_VAULTSHARP");

                    var vaultClientSettings = new VaultClientSettings(urlVaultSharp, new TokenAuthMethodInfo(tokenVaultSharp));
                    _vaultClient = new VaultClient(vaultClientSettings);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not connect to Vault: {ex.Message}");
            }
        }
    }
}
