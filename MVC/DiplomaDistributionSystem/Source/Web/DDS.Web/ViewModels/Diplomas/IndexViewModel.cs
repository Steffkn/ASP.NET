namespace DDS.Web.ViewModels.Diplomas
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<DiplomaViewModel> Diplomas { get; set; }

        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
