using BeerTracker.Models.BindingModels.Geo;
using BeerTracker.Models.ViewModels.Geo;
using BeerTracker.Services;
using BeerTracker.Services.Contracts;
using BeerTracker.Web.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.User.Controllers
{
    [Authorize(Roles = "Administrator, RegularUser")]
    [RouteArea("User", AreaPrefix = "")]
    [RoutePrefix("Geo")]
    public class GeoController : Controller
    {
        private IBeerService service;

        public GeoController(IBeerService service)
        {
            this.service = service;
        }

        [Route("ShowAll")]
        [HttpGet]
        public ActionResult ShowAll()
        {
            var model = this.service.GetLocations();

            return this.View(model);
        }

        [Route("HideBeer")]
        [HttpGet]
        public ActionResult HideBeer()
        {
            return this.View();
        }

        [Route("HideBeer")]
        [HttpPost]
        public ActionResult HideBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool isCreated = this.service.HideBeer(model, User.Identity.Name);

                if (isCreated)
                {
                    this.AddNotification("Beer created!", NotificationType.SUCCESS);
                    return this.RedirectToAction("ShowAll");
                }
            }

            this.AddNotification("Beer was not created!", NotificationType.ERROR);
            return this.RedirectToAction("HideBeer");
        }

        [Route("FindBeer")]
        [HttpGet]
        public ActionResult FindBeer()
        {
            return this.View();
        }

        [Route("FindBeer")]
        [HttpPost]
        public ActionResult FindBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool isFound = this.service.FindBeer(model, User.Identity.Name);

                if (isFound)
                {
                    this.AddNotification("You have found a beer!", NotificationType.SUCCESS);
                    return this.RedirectToAction("ShowAll");
                }
            }

            this.AddNotification("Beer was not found!!!", NotificationType.ERROR);
            return this.RedirectToAction("FindBeer");
        }

        [Route("ShowContestBeers/{id:int}")]
        [HttpGet]
        public ActionResult ShowContestBeers(int id)
        {
            IEnumerable<BeerLocationViewModel> model = this.service.GetBeersOfContest(id);

            ViewBag.ContestId = id;
            return this.View(model);
        }

        [Route("FindContestBeers/{id:int}")]
        [HttpGet]
        public ActionResult FindContestBeers(int id)
        {
            ViewBag.ContestId = id;

            return this.PartialView("_FindContestBeer");
        }

        [Route("FindContestBeer")]
        [HttpPost]
        public ActionResult FindContestBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = this.service.GetUserIdByUsername(User.Identity.Name);

                bool isFound = this.service.FindContestBeer(userId, model);

                if (isFound)
                {
                    this.AddNotification("You have found a beer!!!", NotificationType.SUCCESS);
                    return this.RedirectToAction("ShowContestBeers", new { Id = model.ContestId });
                }
            }

            this.AddNotification("Beer was not found!!!", NotificationType.ERROR);
            return this.RedirectToAction("ShowContestBeers", new { Id = model.ContestId });
        }
    }
}