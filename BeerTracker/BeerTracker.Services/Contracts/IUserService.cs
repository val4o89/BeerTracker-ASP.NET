namespace BeerTracker.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Models.BindingModels.User;
    using Models.ViewModels.Geo;
    using Models.ViewModels.User;

    public interface IUserService
    {
        MyBeersViewModel GetAllMyBeers(string name);
        IEnumerable<ContestUserViewModel> GetAllContests(string name);
        string GetDescription(int id);
        void AddUserToContest(string name, ParticipateContestBindingModel model);
        string GetUserIdByName(string name);
        void RemoveUserFromContest(string userId, ParticipateContestBindingModel model);
    }
}
