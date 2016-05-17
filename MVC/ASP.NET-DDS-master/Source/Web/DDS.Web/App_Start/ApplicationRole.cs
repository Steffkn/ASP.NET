﻿namespace DDS.Web.App_Start
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
            : base() { }

        public ApplicationRole(string name)
            : base(name) { }

        public string Description { get; set; }
    }
}
