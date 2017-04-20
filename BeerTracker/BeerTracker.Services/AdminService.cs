namespace BeerTracker.Services
{
    using Contracts;
    using Models.ViewModels.Admin;
    using System.Linq;
    using Models.DataModels;
    using PagedList;
    using UnitOfWork.Contracts;
    using Models.DataModels.Enums;
    using System;
    using Microsoft.AspNet.Identity;

    public class AdminService : BaseService, IAdminService
    {
        public AdminService(IUnitOfWork db) : base(db)
        {

        }

        public EditUserRoleViewModel GetUserRoles(string userId)
        {
            var user = this.db.AppUsers.FindFirst(u => u.Id == userId);

            //var allRoles = this.db.Roles.Where(r => r.Name != UserRoles.Administrator.ToString());
            var allRoles = this.db.Roles.Where(ar => !user.Roles.Select(ur => ur.RoleId)
                    .Contains(ar.Id) /*&& ar.Name != UserRoles.Administrator.ToString()*/).Select(r => r.Name);

            var userRoles = this.db.Roles
            .Where(urn => user.Roles
            .Select(ur => ur.RoleId)
            .Contains(urn.Id)).Select(r => r.Name);



            return new EditUserRoleViewModel
            {
                AllRoles = allRoles,
                ImplementedRoles = userRoles,
                UserName = user.UserName,
                UserId = user.Id
            };
        }

        public IPagedList<UserViewModel> GetUsersToManage(int page)
        {
            int elementsToTake = 10;

            return new PagedList<UserViewModel>(this.db.AppUsers.GetAll()
                .OrderBy(u => u.UserName)
                .Select(mapper.Map<ApplicationUser, UserViewModel>),page, elementsToTake);

        }
    }
}
