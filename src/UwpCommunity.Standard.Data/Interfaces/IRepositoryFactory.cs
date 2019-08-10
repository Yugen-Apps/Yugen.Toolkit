using UwpCommunity.Standard.Data.Models;

namespace UwpCommunity.Standard.Data.Interfaces
{
    public interface IRepositoryFactory
    {
        IBaseRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}