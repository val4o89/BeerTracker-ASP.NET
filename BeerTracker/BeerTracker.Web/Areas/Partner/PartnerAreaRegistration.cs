using System.Web.Mvc;

namespace BeerTracker.Web.Areas.Partner
{
    public class PartnerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Partner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.Routes.MapMvcAttributeRoutes();

            context.MapRoute(
                "Partner_default",
                "Partner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}