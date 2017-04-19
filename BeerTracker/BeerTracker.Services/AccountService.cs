namespace BeerTracker.Services
{
    using Contracts;
    using Models.DataModels.UserModels;
    using System;
    using System.Linq;
    using UnitOfWork.Contracts;

    public class AccountService : BaseService, IAccountService
    {
        public AccountService(IUnitOfWork db) : base(db)
        {
        }

        public void CreateRegularUser(string id)
        {
            var applicationUser = this.db.AppUsers.FindFirst(u => u.Id == id);
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
