namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data.Models;

    public interface ITagsService
    {
        IQueryable<Tag> GetAll();

        Tag EnsureCategory(string name);
    }
}
