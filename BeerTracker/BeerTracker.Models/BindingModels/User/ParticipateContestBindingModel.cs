namespace BeerTracker.Models.BindingModels.User
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ParticipateContestBindingModel
    {
        [Required]
        public int ContestId { get; set; }
    }
}
