namespace DDS.Services.Data.Interfaces
{
    using DDS.Data.Models;
    using System.Linq;

    public interface IStudentsService : IBaseServices<Student>
    {
        Student GetByFNumber(int number);

        IQueryable<Student> GetByUserId(string userId);

        void AddDiploma(int studentId, Diploma entity);

        void RemoveDiploma(int studentId);

        Student GetSelectedDiplomaByUser(string userId);

        Student GetSelectedDiploma(int studentId);
    }
}
