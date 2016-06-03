namespace DDS.Services.Data.Interfaces
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

        void DeleteTeacher(Teacher entity);

        void CreateTeacher(Teacher entity);

        void EditTeacher(Teacher entity);

        void DeleteStudent(Student entity);

        void CreateStudent(Student entity);

        void EditStudent(Student entity);
    }
}
