namespace BeerTracker.Models.ViewModels.Geo
{
    using DataModels.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class LocateBeerViewModel
    {
        //[Required]
        //public decimal Latitude { get; set; }

        //[Required]
        //public decimal Longitude { get; set; }

        [Display(Name = "Enter last 5 digits of serial number")]
        public int EndOfSerialNumber { get; set; }

        [Display(Name = "Select ")]
        public BeerMake Manufacturer { get; set; }

    }
}
