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
    using Models.BindingModels.User;
    using Models.DataModels.UserModels;

    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork db) : base(db)
        {

        }

        public void AddUserToContest(string userId, ParticipateContestBindingModel model)
        {
            int regularUserId = this.db.RegularUsers.FindFirst(u => u.AppUserId == userId).Id;

            this.db.Contests.FindFirst(c => c.Id == model.ContestId).Participants.Add(new ContestRegularUser
            {
                RegularUserId = regularUserId
            });

            this.db.SaveChanges();
        }

        public IEnumerable<ContestUserViewModel> GetAllContests(string username)
        {
            var participatedContests = this.db.Contests.FindMany(c => c.IsActive == true &&
                c.EndDate > DateTime.Now &&
                c.Participants.Any(p => p.RegularUser.AppUser.UserName == username));

            var notParticipatedContests = this.db.Contests.FindMany(c => c.IsActive == true &&
                c.EndDate > DateTime.Now &&
                !c.Participants.Any(p => p.RegularUser.AppUser.UserName == username));

            List<ContestUserViewModel> allContests = 
                this.mapper.Map<IEnumerable<Contest>, IEnumerable<ContestUserViewModel>>(participatedContests).ToList();

                allContests.ForEach(c => c.IsParticipant = true);

             allContests.AddRange(this.mapper.Map<IEnumerable<Contest>, IEnumerable<ContestUserViewModel>>(notParticipatedContests));

            return allContests.OrderBy(c => c.IsParticipant).ThenByDescending(c => c.EndDate);
        }

        public MyBeersViewModel GetAllMyBeers(string username)
        {
            IEnumerable<Beer> myHiddenBeers = this.db.Beers.FindMany(b => b.Hider.AppUser.UserName == username).ToList();
            IEnumerable<Beer> myFoundBeers = this.db.Beers.FindMany(b => b.IsFound == true && b.Founder.AppUser.UserName == username).ToList();

            var myHiddenBeersViewModel = this.mapper.Map<IEnumerable<Beer>, IEnumerable<MyHiddenBeerViewModel>>(myHiddenBeers);
            var myFoundBeersViewModel = this.mapper.Map<IEnumerable<Beer>, IEnumerable<MyFoundBeerViewModel>>(myFoundBeers);

            return new MyBeersViewModel()
            {
                FoundBeers = myFoundBeersViewModel,
                HiddenBeers = myHiddenBeersViewModel
            };
        }

        public string GetDescription(int id)
        {
            return this.db.Contests.FindFirst(c => c.Id == id).Description;
        }

        public string GetUserIdByName(string name)
        {
            return this.db.AppUsers.FindFirst(u => u.UserName == name).Id;
        }

        public void RemoveUserFromContest(string userId, ParticipateContestBindingModel model)
        {
            int regularUserId = this.db.RegularUsers.FindFirst(u => u.AppUserId == userId).Id;

            var contestUser = this.db.Contests.FindFirst(c => c.Id == model.ContestId).Participants
                .FirstOrDefault(c => c.RegularUserId == regularUserId);

            this.db.Contests.FindFirst(c => c.Id == model.ContestId).Participants.Remove(contestUser);

            this.db.SaveChanges();
        }
    }
}
