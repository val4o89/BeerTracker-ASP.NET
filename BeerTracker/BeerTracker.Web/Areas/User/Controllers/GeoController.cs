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
    [RouteArea("User", AreaPrefix = "")]
    [RoutePrefix("Geo")]
    public class GeoController : Controller
    {
        private IBeerService service;

        public GeoController(IBeerService service)
        {
            this.service = service;
        }

        [Route("Beers")]
        [HttpGet]
        public ActionResult ShowAll()
        {
            IEnumerable<BeerLocationViewModel> model = this.service.GetLocations();

            return this.View(model);
        }

        [Authorize(Roles = "RegularUser")]
        [Route("HideBeer")]
        [HttpGet]
        public ActionResult HideBeer()
        {
            return this.View();
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RegularUser")]
        [Route("HideBeer")]
        [HttpPost]
        public ActionResult HideBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {

                bool isCreated = this.service.HideBeer(model, User?.Identity.Name);

                if (isCreated)
                {
                    this.AddNotification("A Beer has been hidden!", NotificationType.SUCCESS);
                    return this.RedirectToAction("ShowAll");
                }
            }

            this.AddNotification("Beer has not been hidden!", NotificationType.ERROR);
            return this.RedirectToAction("HideBeer");
        }


        [Authorize(Roles = "RegularUser")]
        [Route("FindBeer")]
        [HttpGet]
        public ActionResult FindBeer()
        {
            return this.View();
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RegularUser")]
        [Route("FindBeer")]
        [HttpPost]
        public ActionResult FindBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool isFound = this.service.FindBeer(model, User?.Identity.Name);

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

        [Authorize(Roles = "RegularUser")]
        [Route("FindContestBeers/{id:int}")]
        [HttpGet]
        public ActionResult FindContestBeers(int id)
        {
            ViewBag.ContestId = id;

            return this.PartialView("_FindContestBeer");
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "RegularUser")]
        [Route("FindContestBeer")]
        [HttpPost]
        public ActionResult FindContestBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = this.service.GetUserIdByUsername(User?.Identity.Name);

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