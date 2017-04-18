using BeerTracker.Models.ViewModels.Geo;
using BeerTracker.Models.ViewModels.User;
using BeerTracker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.User.Controllers
{

    [Authorize(Roles = "Administrator, RegularUser")]
    [RouteArea("User")]
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [Route("MyBeers")]
        [HttpGet]
        public ActionResult MyBeers()
        {
            MyBeersViewModel model = this.service.GetAllMineBeers(this.User.Identity.Name);
            return this.View(model);
        }
    }
}