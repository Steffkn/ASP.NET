namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DDS.Data.Common.Models;

    [Table("Diplomas")]
    public class Diploma : BaseModel<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ExperimentalPart { get; set; }

        [Required]
        public IList<string> Content { get; set; }

        [Required]
        public virtual ICollection<Tag> Tags { get; set; }

        [Required]
        public virtual Teacher Leader { get; set; }

        public bool ApprovedByLeader { get; set; }

        public bool ApprovedByHead { get; set; }
    }
}
