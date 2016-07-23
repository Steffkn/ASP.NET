namespace DDS.Data.Models
{
    using DDS.Data.Common.Models;

    public class Student : BaseModel<int>
    {
        public int FNumber { get; set; }

        public string Address { get; set; }

        public ApplicationUser User { get; set; }

        public Diploma SelectedDiploma { get; set; }

        public bool IsGraduate { get; set; }
    }
}
