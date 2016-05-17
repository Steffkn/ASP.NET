﻿namespace DDS.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Teacher : BaseModel<int>
    {
        public Teacher()
        {
            this.Students = new HashSet<Student>();
            this.Diplomas = new HashSet<Diploma>();
        }

        public ApplicationUser User { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Diploma> Diplomas { get; set; }
    }
}
