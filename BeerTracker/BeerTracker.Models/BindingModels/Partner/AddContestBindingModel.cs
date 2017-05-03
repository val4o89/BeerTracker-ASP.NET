namespace BeerTracker.Models.BindingModels.Partner
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddContestBindingModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
