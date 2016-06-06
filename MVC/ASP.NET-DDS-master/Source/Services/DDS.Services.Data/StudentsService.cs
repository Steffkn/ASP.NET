namespace DDS.Services.Data
{
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
    }
}
