namespace BeerTracker.Services.Contracts
{
    using System;
    using Models.BindingModels.Account;
    using Microsoft.AspNet.Identity;
    using Models.DataModels;

    public interface IAccountService
    {
        //void CreateRegularUser(string id);
        void RemoveFromRole(UserManager<ApplicationUser> userManager, string userId, string roleId);
        void AddToRole(UserManager<ApplicationUser> userManager, string userId, string roleId);
        string GetUsernameIfIsActive(ApplicationUser user);
        void ModifyUserAccess(string userId, bool isActive);
    }
}
