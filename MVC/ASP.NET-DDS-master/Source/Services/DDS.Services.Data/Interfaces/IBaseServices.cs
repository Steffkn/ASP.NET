namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using DDS.Data.Common.Models;

    public interface IBaseServices<T>
        where T : BaseModel<int>
    {
        void Create(T entity);

        void Delete(T entity);

        void Edit(T entity);

        T GetById(int id);

        IQueryable<T> GetAll();

        IQueryable<T> GetDeleted();
    }
}
