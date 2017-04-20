using System.Security.Principal;
using BeerTracker.Models.ViewModels.Admin;
using PagedList;
using BeerTracker.Models.DataModels;
using Microsoft.AspNet.Identity;

namespace BeerTracker.Services.Contracts
{
    public interface IAdminService
    {
        IPagedList<UserViewModel> GetUsersToManage(int page);
        EditUserRoleViewModel GetUserRoles(string userId);
    }
}
