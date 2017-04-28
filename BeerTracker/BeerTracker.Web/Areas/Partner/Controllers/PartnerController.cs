using BeerTracker.Models.BindingModels.Geo;
using BeerTracker.Models.BindingModels.Partner;
using BeerTracker.Models.ViewModels.Partner;
using BeerTracker.Services.Contracts;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.Partner.Controllers
{
    [RouteArea("Partner")]
    [RoutePrefix("Partner")]
    public class PartnerController : Controller
    {
        private IPartnerService service;

        public PartnerController(IPartnerService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("MyContests/{page:int?}")]
        public ActionResult MyContests(int? page)
        {
            int requestedPage = this.service.GetCorrectPage(page);

            IPagedList<ContestViewModel> model = this.service.GetMyContests(User.Identity.Name, requestedPage);

            if (Request.IsAjaxRequest())
            {
                return this.PartialView("_MyContests", model);
            }
            return this.View(model);
        }

        [HttpGet]
        [Route("AddContest")]
        public ActionResult AddContest()
        {
            return View();
        }

        [HttpPost]
        [Route("AddContest")]
        public ActionResult AddContest(AddContestBindingModel model)
        {
            this.service.AddContest(User.Identity.Name, model);

            return RedirectToAction("MyContests");
        }

        [HttpGet]
        [Route("ManageContest/{contestId:int}")]
        public ActionResult ManageContest(int contestId)
        {
            ManageContestBindingModel model = this.service.GetContestToManage(User.Identity.Name, contestId);

            return this.View(model);
        }

        [Route("AddBeers/{id:int}")]
        [HttpGet]
        public ActionResult AddBeers(int id)
        {
            ViewBag.ContestId = id;
            return this.View();
        }

        [HttpPost]
        [Route("AddBeer")]
        public ActionResult AddBeer(HideFindBeerBindingModel model)
        {
            this.service.AddBeerToContest(User.Identity.Name, model);

            return RedirectToAction("ManageContest", new { model.ContestId });
        }
    }
}