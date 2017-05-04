namespace BeerTracker.Services.Contracts
{
    using System;
    using Models.BindingModels.Account;
    using Microsoft.AspNet.Identity;
    using Models.DataModels;

    public interface IAccountService
    {
        //void CreateRegularUser(string id);
        bool RemoveFromRole(UserManager<User> userManager, string userId, string roleId);
        bool AddToRole(UserManager<User> userManager, string userId, string roleId);
        string GetUsernameIfIsActive(User user);
        bool ModifyUserAccess(string userId, bool isActive);
    }
}
