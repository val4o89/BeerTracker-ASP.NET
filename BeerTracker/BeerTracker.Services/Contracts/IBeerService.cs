namespace BeerTracker.Services.Contracts
{
    using Models.ViewModels.Geo;
    using System;
    using System.Collections.Generic;
    using Models.BindingModels.Geo;

    public interface IBeerService
    {
        IEnumerable<BeerLocationViewModel> GetLocations();
        void HideBeer(HideFindBeerBindingModel model, string name);
        void FindBeer(HideFindBeerBindingModel model, string name);
    }
}
