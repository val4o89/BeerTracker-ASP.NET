namespace BeerTracker.Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using UserModels;

    public class ContestRegularUser
    {
        [Key, Column(Order = 0)]
        public int ContestId { get; set; }

        [Key, Column(Order = 1)]
        public int RegularUserId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual RegularUser RegularUser { get; set; }

        public int UserScores { get; set; }
    }
}
