using System.Web.Mvc;

namespace ERPSolution.Areas.Security
{
    public class SecurityAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Security";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Security_default_Index",
                "Security/{controller}/{action}/{id}",
                new { controller = "Users", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Security_default",
                "Security/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}