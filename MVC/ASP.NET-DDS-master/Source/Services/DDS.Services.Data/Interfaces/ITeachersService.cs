namespace DDS.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using DDS.Data.Models;

    public interface ITeachersService : IBaseServices<Teacher>
    {
        IQueryable<Teacher> GetByUserId(string id);

        IEnumerable<Diploma> GetAllDiplomas(int id);

        void AddDiploma(int teacherID, Diploma entity);

        void AddStudent(int teacherID, Student entity);
    }
}
