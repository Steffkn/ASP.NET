namespace DDS.Data.Models
{
    using System.Collections.Generic;

    using DDS.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Diploma> Diplomas { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
