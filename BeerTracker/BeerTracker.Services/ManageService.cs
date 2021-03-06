﻿namespace BeerTracker.Services
{
    using Contracts;
    using System;
    using System.Linq;
    using System.IO;
    using System.Web;
    using System.Runtime.InteropServices.ComTypes;
    using UnitOfWork.Contracts;

    public class ManageService : BaseService, IManageService
    {
        public ManageService(IUnitOfWork db) : base(db)
        {

        }

        public void UploadProfilePicture(MemoryStream target, HttpPostedFileBase file, string loggedUsername)
        {

            var loggedUser = this.db.RegularUsers.FindFirst(u => u.AppUser.UserName == loggedUsername);

            if (loggedUser != null)
            {
                file.InputStream.CopyTo(target);

                loggedUser.ProfilePicture = target.ToArray();

                //Code porn
                loggedUser.AppUser = loggedUser.AppUser;

                this.db.SaveChanges();
            }
        }
    }
}
