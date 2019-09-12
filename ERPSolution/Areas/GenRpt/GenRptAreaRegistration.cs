using System.Web.Mvc;

namespace ERPSolution.Areas.GenRpt
{
    public class GenRptAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GenRpt";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GenRpt_default",
                "GenRpt/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}