namespace UnitOfWork.Contracts
{
    using BeerTracker.Models.DataModels;
    using BeerTracker.Models.DataModels.UserModels;
    using System;

    public interface IUnitOfWork
    {
        void SaveChanges();
        IRepository<ApplicationUser> AppUsers { get; }
        IRepository<RegularUser> RegularUsers { get; }
        IRepository<Location> Locations { get; }
        IRepository<Beer> Beers { get; }

    }
}
