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

        //public void CreateRegularUser(string id)
        //{
        //    var applicationUser = this.db.AppUsers.FindFirst(u => u.Id == id);

        //    this.db.RegularUsers.Add(new RegularUser
        //    {
        //        Points = 0,
        //        RegistrationDate = DateTime.Now,
        //        AppUser = applicationUser
        //    });

        //    this.db.SaveChanges();
        //}

        public bool RemoveFromRole(UserManager<User> userManager, string userId, string roleName)
        {
            var removeRoleResult = userManager.RemoveFromRole(userId, roleName);

            if (removeRoleResult.Succeeded)
            {
                this.RetrieveInstanceForRole(userId, roleName, false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddToRole(UserManager<User> userManager, string userId, string roleName)
        {
            var addRoleResult = userManager.AddToRole(userId, roleName);

            if (addRoleResult.Succeeded)
            {
                this.RetrieveInstanceForRole(userId, roleName, true);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RetrieveInstanceForRole(string userId, string roleName, bool isActive)
        {
            if (roleName == UserRoles.RegularUser.ToString())
            {
                var user = this.db.RegularUsers.FindFirst(ru => ru.AppUserId == userId);

                if (user != null)
                {
                    user.AppUser = user.AppUser;
                    user.IsActive = isActive;
                }
                else
                {
                    this.db.RegularUsers.Add(new RegularUser
                    {
                        AppUser = this.db.AppUsers.FindFirst(u => u.Id == userId),
                        RegistrationDate = DateTime.Now,
                        IsActive = isActive
                    });
                }
            }

            if (roleName == UserRoles.Partner.ToString())
            {
                var user = this.db.Partners.FindFirst(ru => ru.AppUserId == userId);

                if (user != null)
                {
                    user.AppUser = user.AppUser;
                    user.IsActive = isActive;
                }
                else
                {
                    this.db.Partners.Add(new Partner
                    {
                        AppUser = this.db.AppUsers.FindFirst(u => u.Id == userId),
                        RegistrationDate = DateTime.Now,
                        IsActive = isActive
                    });
                }
            }

            try
            {
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                return false;
            }

            return true;
        }

        public string GetUsernameIfIsActive(User user)
        {
            if (user != null)
            {
                return user.IsActive ? user.UserName : string.Empty;
            }

            return string.Empty;
        }

        public bool ModifyUserAccess(string userId, bool isActive)
        {
            this.db.AppUsers.FindFirst(u => u.Id == userId).IsActive = isActive;
            try
            {
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException)
            {
                return false;
            }

            return true;
        }
    }
}
