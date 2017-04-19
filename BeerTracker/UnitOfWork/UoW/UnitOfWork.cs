namespace UnitOfWork.UoW
{
    using Contracts;
    using BeerTracker.Data;
    using Repository;
    using System;
    using BeerTracker.Models.DataModels;
    using BeerTracker.Models.DataModels.UserModels;

    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;

        private IRepository<ApplicationUser> appUsers;
        private IRepository<RegularUser> regularUsers;
        private IRepository<Location> locations;
        private IRepository<Beer> beers;

        public UnitOfWork()
        {
            this.context = new ApplicationDbContext();
        }

        public IRepository<ApplicationUser> AppUsers
        {
            get { return this.appUsers ?? (appUsers = new Repository<ApplicationUser>(this.context)); }
            set { this.appUsers = value; }
        }

        public IRepository<RegularUser> RegularUsers
        {
            get { return this.regularUsers ?? (regularUsers = new Repository<RegularUser>(this.context)); }
            set { this.regularUsers = value; }
        }

        public IRepository<Location> Locations
        {
            get { return this.locations ?? (locations = new Repository<Location>(this.context)); }
            set { this.locations = value; }
        }

        public IRepository<Beer> Beers
        {
            get { return this.beers ?? (beers = new Repository<Beer>(this.context)); }
            set { this.beers = value; }
        }



        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
