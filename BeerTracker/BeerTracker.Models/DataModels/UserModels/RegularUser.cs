using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Remoting.Metadata;

namespace BeerTracker.Models.DataModels.UserModels
{
    public class RegularUser
    {
        public RegularUser()
        {
            this.BeersFound = new HashSet<Beer>();
            this.BeersHidden = new HashSet<Beer>();
        }

        public int Id { get; set; }

        public string AppUserId { get; set; }

        [Required]
        public virtual ApplicationUser AppUser { get; set; }


        public byte[] ProfilePicture { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public int Points { get; set; }

        [InverseProperty("Hider")]
        public virtual ICollection<Beer> BeersHidden { get; set; }

        [InverseProperty("Founder")]
        public virtual ICollection<Beer> BeersFound { get; set; }

        public bool IsActive { get; set; }
    }
}