namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class Teacher : BaseModel<int>
    {
        [Required]
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Diploma> Diplomas { get; set; }
    }
}
