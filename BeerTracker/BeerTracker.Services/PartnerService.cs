namespace BeerTracker.Services
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

    public class PartnerService : BaseService, IPartnerService
    {
        public PartnerService(IUnitOfWork db) : base(db)
        {

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
    }
}
