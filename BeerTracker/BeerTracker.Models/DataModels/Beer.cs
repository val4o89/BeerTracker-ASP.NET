﻿namespace BeerTracker.Models.DataModels
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

        public string EndOfSerialNumber { get; set; }

        public virtual Location Location { get; set; }

        public virtual RegularUser Hider { get; set; }

        public virtual RegularUser Founder { get; set; }

        public int? ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public bool IsFound { get; set; }

        public bool IsDeleted { get; set; }

    }
}
