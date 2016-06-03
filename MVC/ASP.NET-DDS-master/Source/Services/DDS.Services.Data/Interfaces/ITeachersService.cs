namespace DDS.Services.Data.Interfaces
{
    using System.Collections.Generic;

    using DDS.Data.Models;

    public interface ITeachersService
    {
        Teacher GetById(int id);

        Teacher GetByUserId(string id);

        IEnumerable<Teacher> GetByIdFullObject(int id);

        IEnumerable<Diploma> GetAllDiplomas(int id);

        void AddDiploma(Diploma entity);

        void Delete(Teacher entity);

        void Create(Teacher entity);

        void Edit(Teacher entity);
    }
}
