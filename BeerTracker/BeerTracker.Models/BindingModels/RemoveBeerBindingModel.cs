namespace BeerTracker.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RemoveBeerBindingModel
    {
        [Required]
        public int Id { get; set; }
    }
}
