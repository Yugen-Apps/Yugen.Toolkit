using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;

namespace Yugen.Toolkit.Uwp.CodeChallenge.UnitTest.Services
{
    public class TestKeyManager : IKeyManager
    {
        private string _key;

        public string GetEncryptionKey(bool isDemoMode)
        {
            return _key;
        }

        public void SetEncryptionKey(string key)
        {
            _key = key;
        }

        public bool DeleteEncryptionKey()
        {
            _key = null;
            return true;
        }

        public bool IsKeySet()
        {
            return string.IsNullOrEmpty(_key) == false;
        }
    }
}
