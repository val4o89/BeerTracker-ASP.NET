using BeerTracker.Models.BindingModels;
using BeerTracker.Models.BindingModels.Geo;
using BeerTracker.Models.BindingModels.Partner;
using BeerTracker.Models.ViewModels.Partner;
using BeerTracker.Services.Contracts;
using BeerTracker.Web.Extensions;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Areas.Partner.Controllers
{
    [RouteArea("Partner", AreaPrefix = "")]
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
            if (ModelState.IsValid)
            {
                bool isAdded = this.service.AddContest(User.Identity.Name, model);

                if (isAdded)
                {
                    this.AddNotification($"Contest {model.Title} has been added!", NotificationType.SUCCESS);
                    return RedirectToAction("MyContests");
                }
            }

            this.AddNotification($"Contest {model.Title} has NOT been added!", NotificationType.ERROR);
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
            ViewBag.ContestTitle = this.service.GetContestName(id);

            ViewBag.ContestId = id;
            return this.View();
        }

        [HttpPost]
        [Route("AddBeer")]
        public ActionResult AddBeer(HideFindBeerBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = this.service.AddBeerToContest(User.Identity.Name, model);

                if (isAdded)
                {
                    this.AddNotification("A beer has been added!", NotificationType.SUCCESS);
                    return RedirectToAction("ManageContest", new { model.ContestId });
                }
            }

            this.AddNotification("A beer has NOT been added!", NotificationType.ERROR);
            return RedirectToAction("ManageContest", new { model.ContestId });
        }

        [HttpPost]
        [Route("RemoveBeer")]
        public ActionResult RemoveBeer(RemoveBeerBindingModel model)
        {
            int contestId;

            if (ModelState.IsValid)
            {
                bool isRemoved = this.service.RemoveBeer(User.Identity.Name, model.Id);

                if (isRemoved)
                {
                    this.AddNotification("Beer has been removed", NotificationType.SUCCESS);
                    contestId = this.service.GetContestByBeerId(User.Identity.Name, model.Id);
                    return RedirectToAction("ManageContest", new { contestId });
                }
            }

            this.AddNotification("Beer has NOT been removed", NotificationType.ERROR);
            contestId = this.service.GetContestByBeerId(User.Identity.Name, model.Id);
            return RedirectToAction("ManageContest", new { contestId });
        }

        [HttpGet]
        [ChildActionOnly]
        [Route("AllContestBeers/{id:int}")]
        public ActionResult AllContestBeers(int id)
        {
            IEnumerable<ContestBeerViewModel> model = this.service.GetContestBeers(id);

            return this.PartialView("_AllContestBeers", model);
        }

        [HttpPost]
        [Route("ManageContestStatus")]
        public ActionResult ManageContestStatus(ManageContestBindingModel model)
        {
            if (ModelState.IsValid)
            {
                bool isUpdated = this.service.UpdateContest(User.Identity.Name, model);

                if (isUpdated)
                {
                    string status = model.IsActive ? "activated" : "deactivated";
                    this.AddNotification($"{model.Title} contest has been {status}!", NotificationType.SUCCESS);
                    return RedirectToAction("ManageContest", new { Id = model.Id });
                }
            }

            this.AddNotification($"{model.Title} contest has NOT been updated!", NotificationType.ERROR);
            return RedirectToAction("ManageContest", new { Id = model.Id });
        }
    }
}