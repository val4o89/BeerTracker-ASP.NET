namespace BeerTracker.Web.Tests.Mocked
{
    using System;
    using System.Collections.Generic;
    using Models.DataModels;
    using Models.DataModels.UserModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using UnitOfWork.Contracts;

    public class FakeUnitOfWork : IUnitOfWork
    {
        private FakeRepository<User> users;
        private FakeRepository<RegularUser> regularUsers;
        private FakeRepository<Partner> partners;
        private FakeRepository<Location> locations;
        private FakeRepository<Beer> beers;
        private FakeRepository<Contest> contests;

        public IRepository<User> AppUsers
        {
            get { return this.users ?? (users = new FakeRepository<User>()); }
            set { this.users = (FakeRepository<User>)value; }
        }

        public IRepository<Beer> Beers
        {
            get { return this.beers ?? (beers = new FakeRepository<Beer>()); }
            set { this.beers = (FakeRepository<Beer>)value; }
        }

        public IRepository<Contest> Contests
        {
            get { return this.contests ?? (contests = new FakeRepository<Contest>()); }
            set { this.contests = (FakeRepository<Contest>)value; }
        }

        public IRepository<Location> Locations
        {
            get { return this.locations ?? (locations = new FakeRepository<Location>()); }
            set { this.locations = (FakeRepository<Location>)value; }
        }

        public IRepository<Partner> Partners
        {
            get { return this.partners ?? (partners = new FakeRepository<Partner>()); }
            set { this.partners = (FakeRepository<Partner>)value; }
        }

        public IRepository<RegularUser> RegularUsers
        {
            get { return this.regularUsers ?? (regularUsers = new FakeRepository<RegularUser>()); }
            set { this.regularUsers = (FakeRepository<RegularUser>)value; }
        }

        public ICollection<IdentityRole> Roles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void SaveChanges()
        {
            
        }
    }
}
