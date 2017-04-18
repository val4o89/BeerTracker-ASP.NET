using BeerTracker.Models.ViewModels.Admin;
using PagedList;

namespace BeerTracker.Services.Contracts
{
    public interface IAdminService
    {
        IPagedList<UserViewModel> GetUsersToManage(int page);
    }
}
