using Windows.Security.Credentials;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Services
{
    public class KeyManager : IKeyManager
    {
        private const string KeyVaultPasscodeResource = "KeyVaultPasscodeResource";
        private const string KeyVaultPasscodeUserName = "KeyVaultPasscode";
        private readonly PasswordVault _passwordVault = new PasswordVault();

        public string GetEncryptionKey(bool isDemoMode)
        {
            string databaseEncryptionKey = null;

            try
            {
                var passwordCredential = _passwordVault.Retrieve(KeyVaultPasscodeResource, KeyVaultPasscodeUserName);

                passwordCredential.RetrievePassword();
                databaseEncryptionKey = passwordCredential.Password;
            }
            catch
            {
                // Let's return an empty key when an issue arises while retrieving the encrypted key from the Vault.
            }

            return databaseEncryptionKey;
        }

        public void SetEncryptionKey(string key)
        {
            var passwordCredential = new PasswordCredential(KeyVaultPasscodeResource, KeyVaultPasscodeUserName, key);
            _passwordVault.Add(passwordCredential);
        }

        public bool DeleteEncryptionKey()
        {
            bool result;

            try
            {
                var passwordCredential = _passwordVault.Retrieve(KeyVaultPasscodeResource, KeyVaultPasscodeUserName);
                _passwordVault.Remove(passwordCredential);
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public bool IsKeySet()
        {
            var isKeySet = false;

            try
            {
                isKeySet = _passwordVault.Retrieve(KeyVaultPasscodeResource, KeyVaultPasscodeUserName) != null;
            }
            catch
            {
                // This is by design that an exception is thrown if a resource is not found. Many years ago, I asked Microsoft why and they answered, it was designed like this :)
            }

            return isKeySet;
        }
    }
}