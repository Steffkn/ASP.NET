namespace DDS.Services.Data
{
    using System.Linq;
    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class StudentsService : IStudentsService
    {
        private readonly IDbRepository<Student> students;

        public StudentsService(IDbRepository<Student> students)
        {
            this.students = students;
        }

        public void Delete(Student entity)
        {
            this.students.Delete(entity);
            this.students.Save();
        }

        public void Create(Student entity)
        {
            this.students.Add(entity);
            this.students.Save();
        }

        public void Edit(Student entity)
        {
            this.students.GetById(entity.Id).FNumber = entity.FNumber;
            this.students.GetById(entity.Id).SelectedDiploma = entity.SelectedDiploma;
            this.students.Save();
        }

        public Student GetById(int id)
        {
            var students = this.students.GetById(id);
            return students;
        }

        public Student GetByFNumber(int number)
        {
            var student = this.students.All().First(s => s.FNumber == number);
            return student;
        }
    }
}
