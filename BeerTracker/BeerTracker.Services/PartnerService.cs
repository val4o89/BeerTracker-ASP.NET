﻿namespace BeerTracker.Services
{
    using Contracts;
    using System;
    using UnitOfWork.Contracts;
    using Models.BindingModels.Partner;
    using Models.DataModels.UserModels;
    using Models.DataModels;
    using Models.ViewModels.Partner;
    using System.Collections.Generic;
    using System.Linq;
    using PagedList;
    using Models.BindingModels.Geo;
    using Models.DataModels.Enums;
    using EntityFramework.Extensions;

    public class PartnerService : BaseService, IPartnerService
    {
        public PartnerService(IUnitOfWork db) : base(db)
        {

        }

        public void AddBeerToContest(string name, HideFindBeerBindingModel model)
        {
            Beer beer = new Beer
            {
                EndOfSerialNumber = model.EndOfSerialNumber,
                Manufacturer = (BeerMake)Enum.Parse(typeof(BeerMake), model.Manufacturer),
                Location = new Location
                {
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                }
            };

            this.db.Contests.FindFirst(c => c.Owner.AppUser.UserName == name && c.Id == model.ContestId).Beers.Add(beer);

            this.db.SaveChanges();
        }

        public void AddContest(string partnerName, AddContestBindingModel model)
        {
            Partner partner = this.db.Partners.FindFirst(p => p.AppUser.UserName == partnerName);

            Contest contest = new Contest
            {
                Owner = partner,
                Description = model.Description,
                Title = model.Title
            };
            this.db.Contests.Add(contest);
            this.db.SaveChanges();
        }

        public IEnumerable<ContestBeerViewModel> GetContestBeers(int id)
        {
            var contestBeers = this.db.Beers.FindMany(b => b.ContestId == id && b.IsDeleted == false);

            return this.mapper.Map<IEnumerable<Beer>, IEnumerable<ContestBeerViewModel>>(contestBeers);
        }

        public int GetContestByBeerId(string username, int id)
        {
            return this.db.Contests.FindFirst(c => c.Owner.AppUser.UserName == username && c.Beers.Any(b => b.Id == id)).Id;
        }

        public dynamic GetContestName(int id)
        {
            return this.db.Contests.FindFirst(c => c.Id == id).Title;
        }

        public ManageContestBindingModel GetContestToManage(string name, int contestId)
        {
            return this.mapper.Map<Contest, ManageContestBindingModel>(this.db.Contests
                .FindFirst(c => c.Id == contestId && c.Owner.AppUser.UserName == name));
        }

        public int GetCorrectPage(int? page)
        {
            page = page ?? 1;
            page = page <= 0 ? 1 : page;
            return (int)page;
        }

        public IPagedList<ContestViewModel> GetMyContests(string name, int page)
        {
            int itemsOnPage = 10;
            return new PagedList<ContestViewModel>(this.mapper.Map<IEnumerable<Contest>, IEnumerable<ContestViewModel>>
                (this.db.Contests.FindMany(c => c.Owner.AppUser.UserName == name)
                .OrderBy(c => c.StartDate)), page, itemsOnPage);
        }

        public void RemoveBeer(string name, int id)
        {
            this.db.Contests.FindFirst(c => c.Owner.AppUser.UserName == name).Beers.FirstOrDefault(b => b.Id == id).IsDeleted = true;
            this.db.SaveChanges();
        }

        public void UpdateContest(string name, ManageContestBindingModel model)
        {
            Contest contest = this.db.Contests.FindFirst(c => c.Owner.AppUser.UserName == name && c.Id == model.Id);

            if (model.IsActive)
            {
                contest.EndDate = model.EndDate;
                contest.Description = model.Description;
                contest.Title = model.Title;
                contest.StartDate = DateTime.Now;
            }

            contest.IsActive = model.IsActive;

            this.db.SaveChanges();
        }
    }
}
