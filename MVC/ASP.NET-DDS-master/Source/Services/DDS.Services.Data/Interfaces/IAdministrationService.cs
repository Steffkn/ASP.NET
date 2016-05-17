namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data.Models;

    public interface IAdministrationService
    {
        IQueryable<ApplicationUser> GetAll();

        ApplicationUser GetById(string userId);

        IQueryable<Student> GetAllStudents();

        IQueryable<Student> GetAllStudentsByCreatedDate();

        IQueryable<Teacher> GetAllTeachers();

        IQueryable<Teacher> GetAllTeachersByCreatedDate();
    }
}
