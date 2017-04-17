namespace BeerTracker.Services
{
    using Contracts;
    using Models.DataModels.UserModels;
    using System;
    using System.Linq;

    public class AccountService : BaseService, IAccountService
    {
        public AccountService() : base()
        {
        }

        public void CreateRegularUser(string id)
        {
            var applicationUser = this.db.Users.FirstOrDefault(u => u.Id == id);
            this.db.RegularUsers.Add(new RegularUser
            {
                Points = 0,
                RegistrationDate = DateTime.Now,
                AppUser = applicationUser
            });

            this.db.SaveChanges();
        }
    }
}
