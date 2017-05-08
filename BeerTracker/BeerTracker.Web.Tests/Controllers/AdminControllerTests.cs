namespace BeerTracker.Web.Tests.Controllers
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using Models.ViewModels.Admin;
    using Areas.Admin.Controllers;
    using TestStack.FluentMVCTesting;
    using PagedList;
    using System.Collections.Generic;
    using System.Linq;
    using Models.DataModels;
    using BeerTracker.Services.Contracts;

    [TestClass]
    public class AdminControllerTests
    {
        private Mock<IAdminService> mockedService;

        [TestInitialize]
        public void Initialize()
        {
            mockedService = new Mock<IAdminService>();
        }

        [TestMethod]
        public void ManageBeerShouldReturnCorrectViewAndModel()
        {

            var hider = "Ivan";
            var manufacturer = "Ariana";
            var endOfSerialNumber = "11111";

            mockedService.Setup(x => x.GetBeerById(It.IsInRange<int>(1, int.MaxValue, Range.Inclusive))).Returns(new ManageBeerViewModel
            {
                EndOfSerialNumber = endOfSerialNumber,
                HidersUsername = hider,
                IsDeleted = false,
                Manufacturer = manufacturer
            });

            var controller = new AdminController(mockedService.Object);

            controller.WithCallTo(c => c.ManageBeer(10))
                .ShouldRenderView("ManageBeer")
                .WithModel<ManageBeerViewModel>();
        }

        [TestMethod]
        public void UpdateBeerShouldRedirectToActionManageBeers()
        {
            var controller = new AdminController(mockedService.Object);
            var model = new ManageBeerViewModel();

            controller.WithCallTo(c => c.UpdateBeer(model))
                .ShouldRedirectTo<AdminController>(typeof(AdminController).GetMethod("ManageBeers"));
        }

        [TestMethod]
        public void ManageBeersShouldReturnDefaultView()
        {
            var controller = new AdminController(mockedService.Object);

            controller.WithCallTo(c => c.ManageBeers(1, ""))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void AllowUserAccessShouldRedirectToDefaultViewWithPagedListModel()
        {
            var users = new List<UserViewModel>
            {
                new UserViewModel { Username = "Ivan", Id = "abcde123456" }
            };

            mockedService.Setup(s => s.GetUsersToManage(1, false, false, ""))
                .Returns(new PagedList<UserViewModel>(users, 1, 1));

            var controller = new AdminController(mockedService.Object);

            controller.WithCallTo(c => c.AllowUserAccess(1, ""))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void DenyUserAccessShouldRedirectToDefaultViewWithPagedListModel()
        {
            var users = new List<UserViewModel>
            {
                new UserViewModel { Username = "Ivan", Id = "abcde123456" }
            };

            mockedService.Setup(s => s.GetUsersToManage(1, false, false, ""))
                .Returns(new PagedList<UserViewModel>(users, 1, 1));

            var controller = new AdminController(mockedService.Object);

            controller.WithCallTo(c => c.DenyUserAccess(1, ""))
                .ShouldRenderDefaultView();
        }

        [TestMethod]
        public void EditRoleShouldReturnDefaultViewWithEditUserRoleViewModel()
        {
            string userId = "abcde12345";
            mockedService.Setup(s => s.GetUserRoles(userId))
                .Returns(new EditUserRoleViewModel());

            var controller = new AdminController(mockedService.Object);

            controller.WithCallTo(c => c.EditRole(userId))
                .ShouldRenderDefaultView().WithModel<EditUserRoleViewModel>();
        }
    }
}
