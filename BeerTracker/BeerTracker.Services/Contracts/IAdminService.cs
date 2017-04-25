using System.Security.Principal;
using BeerTracker.Models.ViewModels.Admin;
using PagedList;
using BeerTracker.Models.DataModels;
using Microsoft.AspNet.Identity;

namespace BeerTracker.Services.Contracts
{
    public interface IAdminService
    {
        IPagedList<UserViewModel> GetUsersToManage(int page, bool areActive, bool includeAdmin, string keyword);
        EditUserRoleViewModel GetUserRoles(string userId);
        int GetCorrectPage(int? page);
        IPagedList<ManageBeerViewModel> GetAllBeers(int requestedPage, string keyword);
        ManageBeerViewModel GetBeerById(int id);
        void UpdateBeer(ManageBeerViewModel model);
    }
}
