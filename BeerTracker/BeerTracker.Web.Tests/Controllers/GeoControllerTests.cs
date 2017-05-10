namespace BeerTracker.Web.Tests.Controllers
{
    using Areas.User.Controllers;
    using BeerTracker.Services;
    using BeerTracker.Services.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.BindingModels.Geo;
    using Models.ViewModels.Geo;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TestStack.FluentMVCTesting;

    [TestClass]
    public class GeoControllerTests
    {
        private Mock<IBeerService> mockedService;

        [TestInitialize]
        public void Initialize()
        {
            this.mockedService = new Mock<IBeerService>();
        }

        [TestMethod]
        public void ShowAllShouldReturnCollectionOfBeerLocationViewModelWithCorrectView()
        {
            this.mockedService.Setup(s => s.GetLocations()).Returns(new List<BeerLocationViewModel>()
            {
                new BeerLocationViewModel() {Latitude = 1.1m, Longitude = 1.1m }
            });

            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.ShowAll()).ShouldRenderDefaultView().WithModel<List<BeerLocationViewModel>>();
        }

        [TestMethod]
        public void HideBeerShouldReturnDefaultView()
        {
            var controller = new GeoController(this.mockedService.Object);
            controller.WithCallTo(c => c.HideBeer()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void HideBeerIfSuccessShouldRedirectToActionShowAll()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.HideBeer(beer, It.IsAny<string>())).Returns(true);


            var controller = new GeoController(this.mockedService.Object);
            controller.WithCallTo(c => c.HideBeer(beer))
                        .ShouldRedirectTo<GeoController>(typeof(GeoController).GetMethod("ShowAll"));
        }

        [TestMethod]
        public void HideBeerIfFailsShouldRedirectToActionHideBeer()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.HideBeer(beer, It.IsAny<string>())).Returns(false);


            var controller = new GeoController(this.mockedService.Object);
            var result = (RedirectToRouteResult)controller.HideBeer(beer);

            //fluentMVC fails if there are more than one methods with same names
            Assert.AreEqual("HideBeer", result.RouteValues["action"]);
        }

        [TestMethod]
        public void FindBeerShouldReturnDefaultView()
        {
            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.FindBeer()).ShouldRenderDefaultView();
        }

        [TestMethod]
        public void FindBeerIfSuccessShouldRedirectToActionShowAll()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.FindBeer(beer, It.IsAny<string>())).Returns(true);

            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.FindBeer(beer))
                .ShouldRedirectTo<GeoController>(typeof(GeoController).GetMethod("ShowAll"));
        }

        [TestMethod]
        public void FindBeerIfFailsShouldRedirectToActionFindBeer()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.FindBeer(beer, It.IsAny<string>())).Returns(false);

            var controller = new GeoController(this.mockedService.Object);

            var result = (RedirectToRouteResult)controller.FindBeer(beer);

            //fluentMVC fails if there are more than one methods with same names
            Assert.AreEqual("FindBeer", result.RouteValues["action"]);
        }

        [TestMethod]
        public void ShowContestBeersShouldRenderDefaultViewWithBeerLocationViewModel()
        {
            this.mockedService.Setup(s => s.GetBeersOfContest(It.IsAny<int>()))
                .Returns(new List<BeerLocationViewModel>()
            {
                new BeerLocationViewModel()
            });

            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.ShowContestBeers(1))
                .ShouldRenderDefaultView().WithModel<IEnumerable<BeerLocationViewModel>>();
        }

        [TestMethod]
        public void FindContestBeersShouldRenderPartialView()
        {
            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.FindContestBeers(1)).ShouldRenderPartialView("_FindContestBeer");
        }

        [TestMethod]
        public void FindContestBeerIfSuccessShouldRedirectToActionShowContestBeers()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.FindContestBeer(It.IsAny<string>(), beer)).Returns(true);

            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.FindContestBeer(beer))
                .ShouldRedirectTo<GeoController>(typeof(GeoController).GetMethod("ShowContestBeers"))
                .WithRouteValue("Id");
        }

        [TestMethod]
        public void FindContestBeerIfFailsShouldRedirectToActionShowContestBeers()
        {
            var beer = new HideFindBeerBindingModel
            {
                EndOfSerialNumber = "11111",
                Manufacturer = "Ariana",
                Latitude = 41.11111,
                Longitude = 41.11111
            };

            this.mockedService.Setup(s => s.FindContestBeer(It.IsAny<string>(), beer)).Returns(false);

            var controller = new GeoController(this.mockedService.Object);

            controller.WithCallTo(c => c.FindContestBeer(beer))
                .ShouldRedirectTo<GeoController>(typeof(GeoController).GetMethod("ShowContestBeers"))
                .WithRouteValue("Id");
        }
    }
}
