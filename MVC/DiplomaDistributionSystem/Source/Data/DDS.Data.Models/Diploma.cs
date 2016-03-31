namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using DDS.Data.Common.Models;

    public class Diploma : BaseModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
