namespace BeerTracker.Models.BindingModels.Geo
{
    using Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HideFindBeerBindingModel
    {
        public int? ContestId { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        [ValidateSerialNumber]
        public string EndOfSerialNumber { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
