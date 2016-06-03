namespace DDS.Services.Data.Interfaces
{
    using System.Linq;
    using DDS.Data.Models;

    public interface ITagsService
    {
        IQueryable<Tag> GetAll();

        Tag GetById(int id);

        void Delete(Tag entity);

        Tag EnsureCategory(string name);
    }
}
