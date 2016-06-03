namespace DDS.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DDS.Data.Models;

    public interface IDiplomasService
    {
        IQueryable<Diploma> GetRandomDiplomas(int count);

        Diploma GetById(int id);

        Task<Diploma> GetByIdFullObject(int id);

        IQueryable<Diploma> GetByTeacherId(int id);

        IQueryable<Diploma> GetAll();

        IQueryable<Diploma> GetAllByCreatedDate();

        void Delete(Diploma entity);

        void Create(Diploma entity);

        void Edit(Diploma entity);
    }
}
