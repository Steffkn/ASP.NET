namespace DDS.Services.Data
{
    using System.Data.Entity;
    using System.Linq;
    using Data.Interfaces;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class StudentsService : BaseService<Student>, IStudentsService
    {
        public StudentsService(IDbRepository<Student> students)
            : base(students)
        {
        }

        public IQueryable<Student> GetByFNumber(int number)
        {
            return this.Items.All().Where(s => s.FNumber == number);
        }

        public IQueryable<Student> GetByUserId(string userId)
        {
            return this.Items.All().Where(t => t.User.Id == userId);
        }

        //public void AddDiploma(int studentId, Diploma entity)
        //{
        //    this.Items.GetById(studentId).SelectedDiploma = entity;
        //    this.Items.GetById(studentId).SelectedDiploma.Id = entity.Id;
        //}

        //public void RemoveDiploma(int studentId)
        //{
        //    this.Items.GetById(studentId).SelectedDiploma = null;
        //}

        public IQueryable<Student> GetStudentWithSelectedDiplomaByUserID(string userId)
        {
            return this.Items.All().Include(s => s.SelectedDiploma).Where(t => t.User.Id == userId);
        }

        public IQueryable<Student> GetStudentWithSelectedDiploma(int studentId)
        {
            return this.Items.All().Include(s => s.SelectedDiploma).Where(t => t.Id == studentId);
        }
    }
}
