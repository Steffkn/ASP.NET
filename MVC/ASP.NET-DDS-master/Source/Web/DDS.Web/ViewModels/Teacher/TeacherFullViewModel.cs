namespace DDS.Web.ViewModels.Teacher
{
    using System.Collections.Generic;
    using DDS.Data.Models;

    public class TeacherFullViewModel
    {
        public ApplicationUser User { get; set; }

        public IEnumerable<Diploma> Diplomas { get; set; }
    }
}