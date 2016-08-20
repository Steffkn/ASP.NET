namespace DDS.Services.Data
{
    using System.Linq;
    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using System.Data.Entity;

    public class StudentsService : BaseService<Student>, IStudentsService
    {
        public StudentsService(IDbRepository<Student> students)
            : base(students)
        {
        }

        public override void Edit(Student entity)
        {
            this.Items.GetById(entity.Id).FNumber = entity.FNumber;
            this.Items.GetById(entity.Id).SelectedDiploma = entity.SelectedDiploma;
            base.Edit(entity);
        }

        public Student GetByFNumber(int number)
        {
            var student = this.Items.All().First(s => s.FNumber == number);
            return student;
        }

        //public Student GetByUserId(string userId)
        //{
        //    return this.Items.All().FirstOrDefault(t => t.User.Id == userId);
        //}

        public IQueryable<Student> GetByUserId(string userId)
        {
            return this.Items.All().Where(t => t.User.Id == userId);
        }

        public void AddDiploma(int studentId, Diploma entity)
        {
            this.Items.GetById(studentId).SelectedDiploma = entity;
            this.Items.GetById(studentId).SelectedDiploma.Id = entity.Id;
        }

        public void RemoveDiploma(int studentId)
        {
            this.Items.GetById(studentId).SelectedDiploma = null;
        }

        public Student GetSelectedDiplomaByUser(string userId)
        {
            return this.Items.All().Include(s => s.SelectedDiploma).FirstOrDefault(t => t.User.Id == userId);
        }

        public Student GetSelectedDiploma(int studentId)
        {
            return this.Items.All().Include(s => s.SelectedDiploma).FirstOrDefault(t => t.Id == studentId);
        }

    }
}
