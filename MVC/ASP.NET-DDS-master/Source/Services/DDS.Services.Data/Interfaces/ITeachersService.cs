namespace DDS.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using DDS.Data.Models;

    public interface ITeachersService : IBaseServices<Teacher>
    {
        Teacher GetByUserId(string id);

        Teacher GetFullObjectById(int id);

        IEnumerable<Diploma> GetAllDiplomas(int id);

        void AddDiploma(int teacherID, Diploma entity);

        void AddStudent(int teacherID, Student entity);
    }
}
