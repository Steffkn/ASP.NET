namespace DDS.Web.ViewModels.ManageDiplomas
{
    using System.Collections.Generic;
    using DDS.Web.ViewModels.Shared;

    public class StudentDiplomaViewModel
    {
        public DisplayDiplomaViewModel Diploma { get; set; }

        public SimpleStudentViewModel Student { get; set; }

        public IList<MessageViewModel> MessageBox { get; set; }

        public string CurrentUserId { get; set; }
    }
}
