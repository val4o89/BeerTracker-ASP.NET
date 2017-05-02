namespace UnitOfWork.UoW
{
    using Contracts;
    using BeerTracker.Data;
    using Repository;
    using System;
    using BeerTracker.Models.DataModels;
    using BeerTracker.Models.DataModels.UserModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext context;

        private IRepository<User> appUsers;
        private IRepository<RegularUser> regularUsers;
        private IRepository<Partner> partners;
        private IRepository<Location> locations;
        private IRepository<Contest> contests;
        private IRepository<Beer> beers;

        public UnitOfWork()
        {
            this.context = new ApplicationDbContext();
        }

        public IRepository<User> AppUsers
        {
            get { return this.appUsers ?? (appUsers = new Repository<User>(this.context)); }
        }


        public IRepository<RegularUser> RegularUsers
        {
            get { return this.regularUsers ?? (regularUsers = new Repository<RegularUser>(this.context)); }
        }
        public IRepository<Partner> Partners
        {
            get { return this.partners ?? (partners = new Repository<Partner>(this.context)); }
        }

        public IRepository<Location> Locations
        {
            get { return this.locations ?? (locations = new Repository<Location>(this.context)); }
        }

        public IRepository<Contest> Contests
        {
            get { return this.contests ?? (contests = new Repository<Contest>(this.context)); }
        }

        public IRepository<Beer> Beers
        {
            get { return this.beers ?? (beers = new Repository<Beer>(this.context)); }
        }

        public ICollection<IdentityRole> Roles
        {
            get
            {
                var roleStore = new RoleStore<IdentityRole>(this.context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                return roleManager.Roles.ToList();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
