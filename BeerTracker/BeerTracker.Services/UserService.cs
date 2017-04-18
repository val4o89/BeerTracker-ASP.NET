namespace BeerTracker.Services
{
    using Contracts;
    using System;
    using Models.ViewModels.Geo;
    using System.Linq;
    using Models.DataModels;
    using System.Collections.Generic;
    using Models.ViewModels.User;

    public class UserService : BaseService, IUserService
    {
        public MyBeersViewModel GetAllMineBeers(string username)
        {
            var myHiddenBeers = this.db.Beers.Where(b => b.Miner.AppUser.UserName == username).ToList();
            var myFoundBeers = this.db.Beers.Where(b => b.IsFound == true && b.Founder.AppUser.UserName == username).ToList();

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
