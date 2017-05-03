namespace BeerTracker.Services.Contracts
{
    using Models.ViewModels.Geo;
    using System;
    using System.Collections.Generic;
    using Models.BindingModels.Geo;

    public interface IBeerService
    {
        IEnumerable<BeerLocationViewModel> GetLocations();
        bool HideBeer(HideFindBeerBindingModel model, string name);
        bool FindBeer(HideFindBeerBindingModel model, string name);
        IEnumerable<BeerLocationViewModel> GetBeersOfContest(int id);
        string GetUserIdByUsername(string name);
        bool FindContestBeer(string userId, HideFindBeerBindingModel model);
    }
}
