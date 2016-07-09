namespace DDS.Web.ViewModels.Tags
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DDS.Data.Models;

    public class DiplomaTestViewModel
    {
        public string[] TagsArray { get; set; }

        public IEnumerable<SelectListItem> Tags { get; set; }
    }
}
