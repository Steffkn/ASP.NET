namespace DDS.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class TeachersService : BaseService<Teacher>, ITeachersService
    {
        public TeachersService(IDbRepository<Teacher> teachers)
            : base(teachers)
        {
        }

        public Teacher GetFullObjectById(int id)
        {
            var teachers = this.Items.All().Where(d => d.Id == id);
            return teachers.Include(e => e.User).First();
        }

        public Teacher GetByUserId(string id)
        {
            var teacher = this.Items.All().FirstOrDefault(t => t.User.Id == id);
            return teacher;
        }

        public IEnumerable<Diploma> GetAllDiplomas(int id)
        {
            var teacher = this.Items.All()
                                .Where(d => d.Id == id)
                                .Include(e => e.Diplomas);
            return teacher.First().Diplomas;
        }

        public void AddDiploma(int teacherID, Diploma entity)
        {
            this.Items.GetById(teacherID).Diplomas.Add(entity);
            this.Items.Save();
        }

        public void AddStudent(int teacherID, Student entity)
        {
            this.Items.GetById(teacherID).Students.Add(entity);
            this.Items.Save();
        }
    }
}
