namespace DDS.Services.Data
{
    using System.Linq;
    using DDS.Data.Common;
    using DDS.Data.Models;
    using Interfaces;

    public class TagsService : BaseService<Tag>, ITagsService
    {
        public TagsService(IDbRepository<Tag> tags)
            : base(tags)
        {
        }

        public Tag EnsureCategory(string name)
        {
            var tag = this.Items.All().FirstOrDefault(x => x.Name == name);
            if (tag != null)
            {
                return tag;
            }

            tag = new Tag { Name = name };
            this.Items.Add(tag);
            this.Items.Save();
            return tag;
        }
    }
}
