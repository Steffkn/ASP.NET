namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class AdministrationService : IAdministrationService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IDbRepository<Student> students;

        public AdministrationService(IDbRepository<Student> students)
        {
            this.students = students;
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            var allUsers = this.db.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);

            return allUsers;
        }

        public ApplicationUser GetById(string userId)
        {
            var user = this.db.Users.Find(userId);

            return user;
        }

        public IQueryable<Student> GetAllStudents()
        {
            var allStudents = this.db.Students.OrderBy(s => s.User.FirstName).ThenBy(s => s.User.LastName);
            return allStudents;
        }

        public IQueryable<Student> GetAllStudentsByCreatedDate()
        {
            var allStudents = this.db.Students.OrderBy(s => s.CreatedOn);
            return allStudents;
        }

        public IQueryable<Teacher> GetAllTeachers()
        {
            var allTeachers = this.db.Teachers.OrderBy(t => t.User.FirstName).ThenBy(t => t.User.LastName);
            return allTeachers;
        }

        public IQueryable<Teacher> GetAllTeachersByCreatedDate()
        {
            var allTeachers = this.db.Teachers.OrderBy(t => t.CreatedOn);
            return allTeachers;
        }
    }
}
