namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using DDS.Data.Models;

    public interface IStudentsService
    {
        Student GetById(int id);

        Student GetByFNumber(int number);

        void Delete(Student entity);

        void Create(Student entity);

        void Edit(Student entity);
    }
}
