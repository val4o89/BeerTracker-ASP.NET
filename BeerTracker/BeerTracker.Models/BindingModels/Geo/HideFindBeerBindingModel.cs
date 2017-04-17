namespace BeerTracker.Models.BindingModels.Geo
{
    using System;

    public class HideFindBeerBindingModel
    {
        public string Manufacturer { get; set; }
        public int EndOfSerialNumber { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
