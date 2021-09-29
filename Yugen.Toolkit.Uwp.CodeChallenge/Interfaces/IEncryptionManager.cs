using System.Threading.Tasks;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public interface IEncryptionManager
    {
        Task<byte[]> DecryptV2(byte[] bytes, bool isDemoMode);
        Task<byte[]> EncryptV2(byte[] bytes, bool isDemoMode);
    }
}
