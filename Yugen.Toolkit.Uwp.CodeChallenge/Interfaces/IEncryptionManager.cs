using System.Threading.Tasks;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface IEncryptionManager
    {
        Task<byte[]> DecryptV2Async(byte[] bytes, bool isDemoMode);
        byte[] EncryptV2(byte[] bytes, bool isDemoMode);
    }
}
