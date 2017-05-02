namespace BeerTracker.Services.Contracts
{
    using System;
    using Models.BindingModels.Account;
    using Microsoft.AspNet.Identity;
    using Models.DataModels;

    public interface IAccountService
    {
        //void CreateRegularUser(string id);
        void RemoveFromRole(UserManager<User> userManager, string userId, string roleId);
        void AddToRole(UserManager<User> userManager, string userId, string roleId);
        string GetUsernameIfIsActive(User user);
        void ModifyUserAccess(string userId, bool isActive);
    }
}
