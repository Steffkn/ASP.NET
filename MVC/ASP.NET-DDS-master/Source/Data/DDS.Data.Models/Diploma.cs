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

        public int TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher Teacher { get; set; }

        public bool IsApprovedByLeader { get; set; }

        public bool IsSelectedByStudent { get; set; }

        public bool IsApprovedByHead { get; set; }
    }
}
