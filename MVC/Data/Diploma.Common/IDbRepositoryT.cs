namespace Diploma.Common
{
    using Models;
    using System.Linq;

    public interface IDbRepositoryT<T> : IDbRepositoryT<T, int>
        where T : BaseModel<int>
    {
    }

    public interface IDbRepositoryT<T, in TKey>
        where T : BaseModel<TKey>
    {
        IQueryable<T> All();

        IQueryable<T> AllWithDeleted();

        T GetById(TKey id);

        void Add(T entity);

        void Delete(T entity);

        void HardDelete(T entity);

        void Save();
    }
}
