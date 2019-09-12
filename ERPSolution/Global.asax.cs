using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ERPSolution
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
        }

        //protected void Application_AuthenticateRequest()
        //{

        //    string url = HttpContext.Current.Request.RawUrl;
        //    string currentUser = "";

        //    if (url.Contains("ApprovalRequest"))
        //    {
        //        HttpContext.Current.SkipAuthorization = true;
        //    }
        //    else if (HttpContext.Current.Session == null)
        //    {
        //        Response.Redirect("/Security/ScUser/SignIn");
        //    }
        //    else
        //    {
        //        currentUser = HttpContext.Current.Session["multiScUserId"].ToString();
        //    }

        //    if (!url.Contains(".css") && !url.Contains(".jpg") && !url.Contains(".gif") && !url.Contains(".bmp") && !url.Contains(".png") && !url.Contains(".js") && !url.Contains(".pdf") && !url.Contains(".xls") && !url.Contains(".xlsx") && !url.Contains("/Security/") && !url.Contains("/PMS/Account"))
        //    {
        //    }
        //    else
        //    {
        //        HttpContext.Current.SkipAuthorization = true;
        //    }
        //}
        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);


        }

    }
}
