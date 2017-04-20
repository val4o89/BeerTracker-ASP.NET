namespace BeerTracker.Models.ViewModels.Admin
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;

    public class EditUserRoleViewModel
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public IEnumerable<string> ImplementedRoles { get; set; }

        public IEnumerable<string> AllRoles { get; set; }
    }
}
