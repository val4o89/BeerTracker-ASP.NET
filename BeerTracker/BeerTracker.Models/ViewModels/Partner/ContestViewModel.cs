namespace BeerTracker.Models.ViewModels.Partner
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ContestViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
