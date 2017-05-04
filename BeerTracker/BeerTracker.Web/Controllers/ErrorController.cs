using BeerTracker.Models.ViewModels.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeerTracker.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Base


        public ActionResult BadRequest()
        {
            Response.StatusCode = 400;

            var model = new ErrorViewModel
            {
                Message = "Bad Request",
                StatusCode = 400
            };

            return View("Error", model);
        }

        public ActionResult Forbidden()
        {
            Response.StatusCode = 403;

            var model = new ErrorViewModel
            {
                Message = "Forbidden",
                StatusCode = 403
            };

            return View("Error", model);
        }

        public ActionResult NotFound()
        {
            Response.StatusCode = 404;
            var model = new ErrorViewModel
            {
                Message = "Not Found",
                StatusCode = 404
            };

            return View("Error");
        }
        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;

            var model = new ErrorViewModel
            {
                Message = "Internal Server Error",
                StatusCode = 500
            };

            return View("Error", model);
        }

        public ActionResult NotImplemented()
        {
            Response.StatusCode = 501;

            var model = new ErrorViewModel
            {
                Message = "Not Implemented",
                StatusCode = 501
            };

            return View("Error", model);
        }

        public ActionResult BadGateway()
        {
            Response.StatusCode = 502;

            var model = new ErrorViewModel
            {
                Message = "Bad Gateway",
                StatusCode = 502
            };

            return View("Error", model);
        }

        public ActionResult ServiceUnavailable()
        {
            Response.StatusCode = 503;

            var model = new ErrorViewModel
            {
                Message = "Service Unavailable",
                StatusCode = 503
            };

            return View("Error", model);
        }
    }
}