namespace DDS.Services.Data.Interfaces
{
    using DDS.Data.Models;

    public interface IStudentsService : IBaseServices<Student>
    {
        Student GetByFNumber(int number);
    }
}
