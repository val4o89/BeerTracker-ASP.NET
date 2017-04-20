namespace BeerTracker.Services
{
    using Contracts;
    using Microsoft.AspNet.Identity;
    using Models.BindingModels.Account;
    using Models.DataModels;
    using Models.DataModels.Enums;
    using Models.DataModels.UserModels;
    using System;
    using System.Data.Entity.Validation;
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

        public void RemoveFromRole(UserManager<ApplicationUser> userManager, string userId, string roleName)
        {
            var removeRoleResult = userManager.RemoveFromRole(userId, roleName);

            if (!removeRoleResult.Succeeded)
            {
                throw new Exception(string.Join(";", removeRoleResult.Errors));
            }
        }

        public void AddToRole(UserManager<ApplicationUser> userManager, string userId, string roleName)
        {
            var addRoleResult = userManager.AddToRole(userId, roleName);

            if (!addRoleResult.Succeeded)
            {
                throw new Exception(string.Join(";", addRoleResult.Errors));
            }
        }
    }
}
