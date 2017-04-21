namespace BeerTracker.Models.DataModels.UserModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Partner
    {
        public Partner()
        {
            this.Contests = new HashSet<Contest>();
        }

        public int Id { get; set; }

        public string AppUserId { get; set; }

        [Required]
        public virtual ApplicationUser AppUser { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Contest> Contests { get; set; }

        public decimal MoneyBallance { get; set; }

        public bool IsActive { get; set; }
    }
}
