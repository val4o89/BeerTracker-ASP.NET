namespace BeerTracker.Models.BindingModels.Account
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccessUserBindingModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string RedirectToAction { get; set; }

        public int Page { get; set; }
    }
}
