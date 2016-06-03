namespace DDS.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using System.Data.Entity;

    public class TeachersService : ITeachersService
    {
        private readonly IDbRepository<Teacher> teachers;

        public TeachersService(IDbRepository<Teacher> students)
        {
            this.teachers = students;
        }

        public void Delete(Teacher entity)
        {
            this.teachers.Delete(entity);
            this.teachers.Save();
        }

        public void Create(Teacher entity)
        {
            this.teachers.Add(entity);
            this.teachers.Save();
        }

        public void Edit(Teacher entity)
        {
            this.teachers.GetById(entity.Id).Diplomas = entity.Diplomas;
            this.teachers.Save();
        }

        public Teacher GetById(int id)
        {
            var teachers = this.teachers.GetById(id);
            return teachers;
        }

        public IEnumerable<Teacher> GetByIdFullObject(int id)
        {
            var teachers = this.teachers.All().Where(d => d.Id == id);
            return teachers.Include(e => e.User);
        }

        public Teacher GetByUserId(string id)
        {
            var teacher = this.teachers.All().FirstOrDefault(t => t.User.Id == id);
            return teacher;
        }

        public IEnumerable<Diploma> GetAllDiplomas(int id)
        {
            var teacher = this.teachers.GetById(id);
            return teacher.Diplomas;
        }

        public void AddDiploma(Diploma entity)
        {
            this.teachers.GetById(entity.Teacher.Id).Diplomas.Add(entity);
            this.teachers.Save();
        }
    }
}
