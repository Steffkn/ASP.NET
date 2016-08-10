namespace DDS.Web.ViewModels.Shared
{
    using System.Collections.Generic;
    using DDS.Data.Models;

    public class TeacherDiplomasViewModel
    {
        public TeacherViewModel TeacherDetails { get; set; }

        public IEnumerable<Diploma> Diplomas { get; set; }
    }
}
