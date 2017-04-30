using BeerTracker.Models.BindingModels.User;
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
            MyBeersViewModel model = this.service.GetAllMyBeers(this.User.Identity.Name);
            return this.View(model);
        }

        [Route("Contests")]
        [HttpGet]
        public ActionResult Contests()
        {
            IEnumerable<ContestUserViewModel> model = this.service.GetAllContests(User.Identity.Name);

            return this.View(model);
        }

        [Route("GetDescription/{id:int}")]
        [HttpGet]
        public ActionResult GetDescription(int id)
        {
            string description = this.service.GetDescription(id);

            return this.PartialView("_Description", description);
        }

        [Route("Participate")]
        [HttpPost]
        public ActionResult Participate(ParticipateContestBindingModel model)
        {
            string userId = this.service.GetUserIdByName(User.Identity.Name);

            this.service.AddUserToContest(userId, model);

            return RedirectToAction("Contests");
        }

        [Route("Unparticipate")]
        [HttpPost]
        public ActionResult Unparticipate(ParticipateContestBindingModel model)
        {
            string userId = this.service.GetUserIdByName(User.Identity.Name);

            this.service.RemoveUserFromContest(userId, model);

            return RedirectToAction("Contests");
        }
    }
}