namespace Yugen.Toolkit.Standard.Data.Interfaces
{
    /// <summary>
    /// IRepositoryFactory
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// GetRepository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IBaseRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}