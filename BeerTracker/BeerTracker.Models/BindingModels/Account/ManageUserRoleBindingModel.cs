namespace BeerTracker.Models.BindingModels.Account
{
    using Attributes;
    using DataModels.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManageUserRoleBindingModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [ValidateRoleToRemove(restrictedRole: "Administrator", ErrorMessage = "You cant remove role from this user!")]
        public string RoleName { get; set; }
    }
}
