namespace BeerTracker.Models.ViewModels.User
{
    using System;
    using System.Collections.Generic;

    public class MyBeersViewModel
    {
        public IEnumerable<MyHiddenBeerViewModel> HiddenBeers { get; set; }

        public IEnumerable<MyFoundBeerViewModel> FoundBeers { get; set; }
    }
}
