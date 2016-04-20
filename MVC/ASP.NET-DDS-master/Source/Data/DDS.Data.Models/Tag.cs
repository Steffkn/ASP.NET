namespace DDS.Data.Models
{
    using DDS.Data.Common.Models;
    using System.Collections.Generic;

    public class Tag : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Diploma> Diplomas { get; set; }
    }
}
