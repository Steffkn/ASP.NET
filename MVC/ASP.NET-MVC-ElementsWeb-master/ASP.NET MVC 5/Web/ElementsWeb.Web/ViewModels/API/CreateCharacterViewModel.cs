using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElementsWeb.Web.ViewModels.API
{
    public class CreateCharacterViewModel
    {

        [Required]
        public string Name { get; set; }
    }
}