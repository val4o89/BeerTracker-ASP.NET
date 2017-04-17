using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerTracker.Models.DataModels
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public virtual Beer Beer { get; set; }

    }
}