using BeerTracker.Models.BindingModels.User;
using BeerTracker.Models.ViewModels.Geo;
using BeerTracker.Models.ViewModels.User;
using BeerTracker.Services.Contracts;
using BeerTracker.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.User.Controllers
{

    [RouteArea("User", AreaPrefix = "")]
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [Authorize(Roles = "RegularUser")]
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

        [Authorize(Roles = "RegularUser")]
        [Route("Participate")]
        [HttpPost]
        public ActionResult Participate(ParticipateContestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = this.service.GetUserIdByName(User.Identity.Name);

                bool isSucceeded = this.service.AddUserToContest(userId, model);

                if (isSucceeded)
                {
                    this.AddNotification($"{User.Identity.Name} has been added successfuly!", NotificationType.SUCCESS);
                    return RedirectToAction("Contests");
                }
            }
            this.AddNotification($"{ User.Identity.Name} has NOT been added!", NotificationType.ERROR);
            return RedirectToAction("Contests");
        }

        [Authorize(Roles = "RegularUser")]
        [Route("Unparticipate")]
        [HttpPost]
        public ActionResult Unparticipate(ParticipateContestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = this.service.GetUserIdByName(User.Identity.Name);

                bool isRemoved = this.service.RemoveUserFromContest(userId, model);

                if (isRemoved)
                {
                    this.AddNotification($"{User.Identity.Name} has been removed from this contest", NotificationType.SUCCESS);
                    return RedirectToAction("Contests");
                }
            }

            this.AddNotification($"{User.Identity.Name} has NOT been removed from this contest", NotificationType.ERROR);
            return RedirectToAction("Contests");
        }

        [Route("Ranking/{id:int}")]
        [HttpGet]
        public ActionResult ShowRanking(int id)
        {
            IEnumerable<UserRankViewModel> model = this.service.GetContestRanking(id);

            return this.PartialView("_Ranking", model);
        }

        [Route("MainRanking")]
        [HttpGet]
        public ActionResult MainRanking()
        {
            IEnumerable<UserRankViewModel> model = this.service.GetRanking();

            return this.View(model);
        }
    }
}