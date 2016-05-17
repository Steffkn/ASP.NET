namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using DDS.Data.Common.Models;

    [Table("Diplomas")]
    public class Diploma : BaseModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ExperimentalPart { get; set; }

        public string ContentCSV { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public Teacher Leader { get; set; }

        public bool ApprovedByLeader { get; set; }

        public bool ApprovedByHead { get; set; }
    }
}
