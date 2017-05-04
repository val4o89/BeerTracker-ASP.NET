namespace BeerTracker.Models.BindingModels.Partner
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ManageContestBindingModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

    }
}
