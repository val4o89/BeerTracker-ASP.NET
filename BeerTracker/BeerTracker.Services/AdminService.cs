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
    using System.Linq.Expressions;
    using System.Text.RegularExpressions;

    public class AdminService : BaseService, IAdminService
    {
        public AdminService(IUnitOfWork db) : base(db)
        {

        }

        public int GetCorrectPage(int? page)
        {
            page = page ?? 1;
            page = page <= 0 ? 1 : page;
            return (int)page;
        }

        public EditUserRoleViewModel GetUserRoles(string userId)
        {
            var user = this.db.AppUsers.FindFirst(u => u.Id == userId);

            //var allRoles = this.db.Roles.Where(r => r.Name != UserRoles.Administrator.ToString());
            var allRoles = this.db.Roles.Where(ar => !user.Roles.Select(ur => ur.RoleId)
                    .Contains(ar.Id) && ar.Name != UserRoles.Administrator.ToString()).Select(r => r.Name);

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

        public IPagedList<UserViewModel> GetUsersToManage(int page, bool areActive, bool includeAdministrator, string keyword)
        {
            int elementsToTake = 10;
            string mainPattern = "\\w*";
            keyword = keyword ?? mainPattern;
            string adminRoleId = includeAdministrator ? null : this.db.Roles.FirstOrDefault(r => r.Name == UserRoles.Administrator.ToString()).Id;
            var regex = new Regex(keyword + mainPattern);
            
            return new PagedList<UserViewModel>(this.db.AppUsers.FindMany(u => u.IsActive == areActive)
                .Where(u => !u.Roles.Any(r => r.RoleId == adminRoleId))
                .OrderBy(u => u.UserName).ToList().Where(u => regex.IsMatch(u.UserName))
                .Select(mapper.Map<ApplicationUser, UserViewModel>),page, elementsToTake);

        }
    }
}
