namespace UnitOfWork.Contracts
{
    using BeerTracker.Models.DataModels;
    using BeerTracker.Models.DataModels.UserModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public interface IUnitOfWork
    {
        void SaveChanges();
        IRepository<ApplicationUser> AppUsers { get; }
        IRepository<RegularUser> RegularUsers { get; }
        IRepository<Partner> Partners { get; }
        IRepository<Location> Locations { get; }
        IRepository<Beer> Beers { get; }

        IRepository<Contest> Contests { get; }
        ICollection<IdentityRole> Roles { get; }

    }
}
