namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;
    using DDS.Data.Models;

    public interface IDiplomasService : IBaseServices<Diploma>
    {
        Diploma GetFullObjectById(int id);

        IQueryable<Diploma> GetByTeacherId(int id);

        IQueryable<Diploma> GetRandomDiplomas(int count);

        IQueryable<Diploma> GetWithID(int id);
    }
}
