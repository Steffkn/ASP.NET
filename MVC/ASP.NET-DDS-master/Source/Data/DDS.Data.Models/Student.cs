namespace DDS.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using DDS.Data.Common.Models;

    public class Student : BaseModel<int>
    {
        public int FNumber { get; set; }

        public ApplicationUser User { get; set; }

        public Diploma SelectedDiploma { get; set; }
    }
}
