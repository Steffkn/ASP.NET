namespace DDS.Services.Data
{
    using System;
    using System.Linq;
    using DDS.Data;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;

    public class AdministrationService : IAdministrationService
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IDbRepository<Student> students;
        private IDbRepository<Teacher> teachers;

        public AdministrationService()
        {
            this.students = new DbRepository<Student>(this.db);
            this.teachers = new DbRepository<Teacher>(this.db);
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

        public void DeleteTeacher(Teacher entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            this.db.SaveChanges();
        }

        public void CreateTeacher(Teacher entity)
        {
            this.db.Teachers.Add(entity);
            this.db.SaveChanges();
        }

        public void EditTeacher(Teacher entity)
        {
            this.db.Teachers.FirstOrDefault(t => t.Id == entity.Id).DeletedOn = entity.DeletedOn;
            this.db.Teachers.FirstOrDefault(t => t.Id == entity.Id).IsDeleted = entity.IsDeleted;
            this.db.Teachers.FirstOrDefault(t => t.Id == entity.Id).ModifiedOn = entity.ModifiedOn;
            this.db.SaveChanges();
        }

        public void DeleteStudent(Student entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            this.db.SaveChanges();
        }

        public void CreateStudent(Student entity)
        {
            this.db.Students.Add(entity);
            this.db.SaveChanges();
        }

        public void EditStudent(Student entity)
        {
            this.db.Students.FirstOrDefault(t => t.Id == entity.Id).ModifiedOn = DateTime.Now;
            this.db.SaveChanges();
        }
    }
}
