namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DDS.Data.Common.Models;

    [Table("Diplomas")]
    public class Diploma : BaseModel<int>
    {
        [Required(ErrorMessage = "Заглавието е задължително")]
        [MinLength(40, ErrorMessage = "Заглавието трябва да бъде поне 40 символа.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описанието е задължително")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Експерименталната част е задължителна")]
        public string ExperimentalPart { get; set; }

        public string ContentCSV { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public int TeacherID { get; set; }

        [ForeignKey("TeacherID")]
        public virtual Teacher Teacher { get; set; }

        public bool IsApprovedByLeader { get; set; }

        public bool IsApprovedByHead { get; set; }

        public bool IsSelectedByStudent { get; set; }
    }
}
