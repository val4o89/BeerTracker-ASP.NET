namespace BeerTracker.Services.Contracts
{
    using System;
    using Models.ViewModels.Geo;
    using Models.ViewModels.User;

    public interface IUserService
    {
        MyBeersViewModel GetAllMineBeers(string name);
    }
}
