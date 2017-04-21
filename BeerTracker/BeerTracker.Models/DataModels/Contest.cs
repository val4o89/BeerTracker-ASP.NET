﻿namespace BeerTracker.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using UserModels;

    public class Contest
    {
        public Contest()
        {
            this.Beers = new HashSet<Beer>();
            this.Participants = new HashSet<RegularUser>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public virtual Partner Owner { get; set; }

        public virtual ICollection<Beer> Beers { get; set; }

        public virtual ICollection<RegularUser> Participants { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}