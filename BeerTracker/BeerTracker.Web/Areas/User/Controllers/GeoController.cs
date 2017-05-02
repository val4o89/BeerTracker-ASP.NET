using BeerTracker.Models.BindingModels.Geo;
using BeerTracker.Models.ViewModels.Geo;
using BeerTracker.Services;
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
            this.service.HideBeer(model, User.Identity.Name);

            return this.RedirectToAction("ShowAll");
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
            this.service.FindBeer(model, User.Identity.Name);
            return this.RedirectToAction("ShowAll");
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
            string userId = this.service.GetUserIdByUsername(User.Identity.Name);

            this.service.FindContestBeer(userId, model);

            return this.RedirectToAction("ShowContestBeers", new { Id = model.ContestId });
        }

    }
}