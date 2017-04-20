namespace BeerTracker.Services
{
    using Contracts;
    using System;
    using Models.ViewModels.Geo;
    using System.Linq;
    using Models.DataModels;
    using System.Collections.Generic;
    using Models.ViewModels.User;
    using UnitOfWork.Contracts;

    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork db) : base(db)
        {

        }
        public MyBeersViewModel GetAllMineBeers(string username)
        {
            IEnumerable<Beer> myHiddenBeers = this.db.Beers.FindMany(b => b.Miner.AppUser.UserName == username).ToList();
            IEnumerable<Beer> myFoundBeers = this.db.Beers.FindMany(b => b.IsFound == true && b.Founder.AppUser.UserName == username).ToList();

            var myHiddenBeersViewModel = this.mapper.Map<IEnumerable<Beer>, IEnumerable<MyHiddenBeerViewModel>>(myHiddenBeers);
            var myFoundBeersViewModel =  this.mapper.Map<IEnumerable<Beer>, IEnumerable<MyFoundBeerViewModel>>(myFoundBeers);

            return new MyBeersViewModel()
            {
                FoundBeers = myFoundBeersViewModel,
                HiddenBeers = myHiddenBeersViewModel
            };
        }
    }
}
