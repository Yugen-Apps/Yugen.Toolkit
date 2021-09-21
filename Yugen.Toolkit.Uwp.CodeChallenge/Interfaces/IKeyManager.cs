namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface IKeyManager
    {
        string GetEncryptionKey(bool isDemoMode);
        void SetEncryptionKey(string key);
        bool DeleteEncryptionKey();
        bool IsKeySet();
    }
}
