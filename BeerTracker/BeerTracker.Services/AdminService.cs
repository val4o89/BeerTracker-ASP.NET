namespace BeerTracker.Services
{
    using Contracts;
    using System;
    using Models.ViewModels.Admin;
    using System.Collections.Generic;
    using System.Linq;
    using Models.DataModels;
    using PagedList;

    public class AdminService : BaseService, IAdminService
    {
        public IPagedList<UserViewModel> GetUsersToManage(int page)
        {
            int elementsToTake = 10;

            var users = this.db.Users.OrderBy(u => u.UserName);
            return mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(users).ToPagedList(page, elementsToTake);
        }
    }
}
