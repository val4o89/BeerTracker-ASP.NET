namespace BeerTracker.Services.Contracts
{
    using System;
    using Models.ViewModels.Partner;
    using Models.BindingModels.Partner;
    using System.Collections.Generic;
    using PagedList;
    using Models.BindingModels.Geo;

    public interface IPartnerService
    {
        void AddContest(string name, AddContestBindingModel model);
        IPagedList<ContestViewModel> GetMyContests(string name, int page);
        int GetCorrectPage(int? page);
        ManageContestBindingModel GetContestToManage(string name, int contestId);
        void AddBeerToContest(string name, HideFindBeerBindingModel model);
    }
}
