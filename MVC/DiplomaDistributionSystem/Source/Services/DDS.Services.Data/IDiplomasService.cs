namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data.Models;

    public interface IDiplomasService
    {
        IQueryable<Diploma> GetRandomDiplomas(int count);

        Diploma GetById(int id);

        IQueryable<Diploma> GetAll();

        IQueryable<Diploma> GetAllByCreatedDate();

        void Delete(Diploma entity);

        void Create(Diploma entity);

        void Edit(Diploma entity);
    }
}
