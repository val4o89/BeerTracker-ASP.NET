namespace BeerTracker.Models.ViewModels.User
{
    using System;

    public class MyHiddenBeerViewModel
    {
        public string Manufacturer { get; set; }

        public int EndOfSerialNumber { get; set; }

        public string Founder { get; set; }

        public bool IsFound { get; set; }
    }
}
