using BeerTracker.Models.ViewModels.Admin;
using BeerTracker.Services.Contracts;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        [Route("ManageUserRoles/{page:int?}")]
        [HttpGet]
        public ActionResult ManageUserRoles(int? page)
        {
            page = page ?? 1;
            page = page <= 0 ? 1 : page;

            IPagedList<UserViewModel> model = this.service.GetUsersToManage((int)page);

            return View(model);
        }


    }
}