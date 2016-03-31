namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data.Common;
    using DDS.Data.Models;

    public class TagsService : ITagsService
    {
        private readonly IDbRepository<Tag> tags;

        public TagsService(IDbRepository<Tag> tags)
        {
            this.tags = tags;
        }

        public Tag EnsureCategory(string name)
        {
            var tag = this.tags.All().FirstOrDefault(x => x.Name == name);
            if (tag != null)
            {
                return tag;
            }

            tag = new Tag { Name = name };
            this.tags.Add(tag);
            this.tags.Save();
            return tag;
        }

        public IQueryable<Tag> GetAll()
        {
            return this.tags.All().OrderBy(x => x.Name);
        }
    }
}
