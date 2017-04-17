namespace BeerTracker.Models.DataModels
{
    using Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using UserModels;

    public class Beer
    {
        [Key]
        public int Id { get; set; }

        public BeerMake? Manufacturer { get; set; }

        public int EndOfSerialNumber { get; set; }

        public virtual Location Location { get; set; }

        public virtual RegularUser Miner { get; set; }

        public virtual RegularUser Founder { get; set; }

        public bool IsFound { get; set; }


    }
}
