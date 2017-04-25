namespace BeerTracker.Models.ViewModels.Admin
{
    using System;

    public class ManageBeerViewModel
    {
        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public int EndOfSerialNumber { get; set; }

        public string HidersUsername { get; set; }


        public bool IsDeleted { get; set; }
    }
}
