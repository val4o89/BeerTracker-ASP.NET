using BeerTracker.Models.BindingModels.Geo;
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

    }
}