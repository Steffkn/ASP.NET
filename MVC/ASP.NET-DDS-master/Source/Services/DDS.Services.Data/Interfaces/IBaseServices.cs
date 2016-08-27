namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using DDS.Data.Common.Models;

    public interface IBaseServices<T>
        where T : BaseModel<int>
    {
        void Create(T entity);

        void Delete(T entity);

        void UnDelete(T entity);

        void HardDelete(T entity);

        void Edit(T entity);

        void Save();

        T GetObjectById(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetDeleted();

        IQueryable<T> GetById(int id);
    }
}
