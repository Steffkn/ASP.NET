namespace DDS.Data.Models
{
    using System.Collections.Generic;

    using Common.Models;

    public class Teacher : BaseModel<int>
    {
        public Teacher()
        {
            this.Students = new HashSet<Student>();
            this.Diplomas = new HashSet<Diploma>();
            this.Tags = new HashSet<Tag>();
        }

        public ApplicationUser User { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Diploma> Diplomas { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
