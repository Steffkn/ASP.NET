namespace DDS.Services.Data.Interfaces
{
    using DDS.Data.Models;

    public interface ITagsService : IBaseServices<Tag>
    {
        Tag EnsureCategory(string name);
    }
}
