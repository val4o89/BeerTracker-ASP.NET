using BeerTracker.Models.ViewModels.Admin;
using BeerTracker.Services.Contracts;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.Admin.Controllers
{
    [RouteArea("Admin")]
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        private IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [Route("ManageUserRoles/{page:int?}/{keyword?}")]
        [HttpGet]
        public ActionResult ManageUserRoles(int? page, string keyword)
        {
            int requestedPage = this.service.GetCorrectPage(page);

            IPagedList<UserViewModel> model = this.service.GetUsersToManage(requestedPage, true, true, keyword);

            ViewBag.Keyword = keyword;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ManageUserRoles", model);
            }

            return View(model);
        }

        [Route("EditRole/{userId}")]
        [HttpGet]
        public ActionResult EditRole(string userId)
        {
            EditUserRoleViewModel model = this.service.GetUserRoles(userId);

            return this.View(model);
        }

        [Route("DenyUserAccess/{page:int?}/{keyword?}")]
        [HttpGet]
        public ActionResult DenyUserAccess(int? page, string keyword)
        {
            int requestedPage = this.service.GetCorrectPage(page);

            IPagedList<UserViewModel> model = this.service.GetUsersToManage(requestedPage, true, false, keyword);
            ViewBag.Keyword = keyword;

           if (Request.IsAjaxRequest())
            {
                return PartialView("_DenyUserAccess", model);
            }
            return this.View(model);
        }


        [Route("AllowUserAccess/{page:int?}/{keyword?}")]
        [HttpGet]
        public ActionResult AllowUserAccess(int? page, string keyword)
        {
            int requestedPage = this.service.GetCorrectPage(page);
            
            IPagedList<UserViewModel> model = this.service.GetUsersToManage(requestedPage, false, false, keyword);
            ViewBag.Keyword = keyword;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_AllowUserAccess", model);
            }
            return this.View(model);
        }
    }
}