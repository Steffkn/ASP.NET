namespace DDS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using DDS.Data.Common.Models;

    public class Student : BaseModel<int>
    {
        [Required]
        public virtual ApplicationUser User { get; set; }

        public string FNumber { get; set; }

        public Diploma SelectedDiploma { get; set; }
    }
}
