using System.Web.Mvc;

namespace BeerTracker.Web.Areas.User
{
    public class UserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "User_default",
                "User/{controller}/{action}/{id}",
                new { action = "HideBeer", controller = "Geo", id = UrlParameter.Optional }
            );
        }
    }
}