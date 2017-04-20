namespace BeerTracker.Models.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.Validation;

    public class ValidateRoleToRemoveAttribute : ValidationAttribute
    {
        public ValidateRoleToRemoveAttribute(string restrictedRole)
        {
            this.RestrictedRole = restrictedRole;
        }

        public override bool IsValid(object value)
        {
            string role = (string)value;

            if (role == "Administrator")
            {
                return false;
            }

            return true;
        }

        public string RestrictedRole { get; private set; }
    }
}
