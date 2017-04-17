using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BeerTracker.Services.Contracts
{
    public interface IManageService
    {
        void UploadProfilePicture(MemoryStream target, HttpPostedFileBase file, string loggedUsername);
    }
}
