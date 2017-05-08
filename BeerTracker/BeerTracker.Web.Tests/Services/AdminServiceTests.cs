namespace BeerTracker.Web.Tests.Services
{
    using BeerTracker.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mocked;
    using Models.DataModels;
    using Models.DataModels.UserModels;
    using Models.ViewModels.Admin;
    using Moq;
    using Services;
    using System;
    using UnitOfWork.Contracts;
    using UnitOfWork.Repository;
    using UnitOfWork.UoW;

    [TestClass]
    public class AdminServiceTests
    {
        private FakeUnitOfWork db;
        private AdminService adminService;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new FakeUnitOfWork();
            this.adminService = new AdminService(db);

            int id = 1;

            db.Beers.Add(new Beer
            {
                Id = id,
                Hider = new RegularUser
                {
                    AppUser = new User
                    {
                        Email = "abc@abc.bg",
                        UserName = "gencho"
                    }
                }
            });
        }

        [TestMethod]
        public void GetBeerByIdShouldReturnManageBeerViewModelWithCorrectId()
        {
            int id = 1;

            var beerViewModel = adminService.GetBeerById(1);

            Assert.AreEqual(id, beerViewModel.Id);
        }

        [TestMethod]
        public void UpdateBeerShouldMakeChanges()
        {
            string modifiedSerialNumber = "11111";
            int beerId = 1;

            ManageBeerViewModel model = new ManageBeerViewModel
            {
                Id = beerId,
                EndOfSerialNumber = modifiedSerialNumber,
                HidersUsername = "goshko",
                IsDeleted = false,
                Manufacturer = "Ariana"
            };

            this.adminService.UpdateBeer(model);

            Assert.AreEqual(modifiedSerialNumber, this.db.Beers.FindFirst(b => b.Id == beerId).EndOfSerialNumber);
        }

        [TestMethod]
        public void GetAllBeersShouldReturnBeersCount()
        {
            var beersCount = this.adminService.GetAllBeers(1, "").Count;
            var expectedCount = 1;

            Assert.AreEqual(expectedCount, beersCount);
        }

        [TestMethod]
        public void GetCorrectPageShouldReturnPositiveNumber()
        {
            int number = this.adminService.GetCorrectPage(-1);

            Assert.IsTrue(number > 0);
        }
    }
}
