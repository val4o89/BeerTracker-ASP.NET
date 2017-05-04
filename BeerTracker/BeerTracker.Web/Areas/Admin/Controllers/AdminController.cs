using BeerTracker.Models.ViewModels.Admin;
using BeerTracker.Services.Contracts;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    [RouteArea("Admin", AreaPrefix = "")]
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        private IAdminService service;
        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [Route("UserRoles/{page:int?}/{keyword?}")]
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
                return this.PartialView("_AllowUserAccess", model);
            }
            return this.View(model);
        }

        [Route("Beers/{page:int?}/{keyword?}")]
        [HttpGet]
        public ActionResult ManageBeers(int? page, string keyword)
        {
            int requestedPage = this.service.GetCorrectPage(page);

            IPagedList<ManageBeerViewModel> model = this.service.GetAllBeers(requestedPage, keyword);

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_ManageBeers", model);
            }
            return this.View(model);
        }

        [Route("Beer/{id:int}")]
        [HttpGet]
        public ActionResult ManageBeer(int id)
        {
            ManageBeerViewModel model = this.service.GetBeerById(id);
            return this.View(model);
        }

        [Route("UpdateBeer")]
        [HttpPost]
        public ActionResult UpdateBeer(ManageBeerViewModel model)
        {
            this.service.UpdateBeer(model);
            return this.RedirectToAction("ManageBeers");
        }
    }
}