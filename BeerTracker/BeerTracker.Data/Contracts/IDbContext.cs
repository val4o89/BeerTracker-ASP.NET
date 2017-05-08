using BeerTracker.Models.DataModels;
using BeerTracker.Models.DataModels.UserModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerTracker.Data.Contracts
{
    public interface IDbContext
    {
        DbSet<Location> Locations { get; set; }

        DbSet<RegularUser> RegularUsers { get; set; }

        DbSet<Beer> Beers { get; set; }
    }
}
