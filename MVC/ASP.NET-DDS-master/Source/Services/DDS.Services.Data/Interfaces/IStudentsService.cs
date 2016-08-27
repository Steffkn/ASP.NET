namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using DDS.Data.Models;

    public interface IStudentsService : IBaseServices<Student>
    {
        IQueryable<Student> GetByFNumber(int number);

        IQueryable<Student> GetByUserId(string userId);

        IQueryable<Student> GetStudentWithSelectedDiplomaByUserID(string userId);

        IQueryable<Student> GetStudentWithSelectedDiploma(int studentId);

        void AddDiploma(int studentId, Diploma entity);

        void RemoveDiploma(int studentId);
    }
}
