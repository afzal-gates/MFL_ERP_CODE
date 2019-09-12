using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace ERPSolution.Common
{
    public class SignInCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // code involving this.Session // edited to simplify           
            
            string x = "";
            string vIsValidMenu = "";

            if (HttpContext.Current.Session["multiLoginId"] != null)
                x = HttpContext.Current.Session["multiLoginId"].ToString();

            if (HttpContext.Current.Session["multiValidMenuId"] != null)
                vIsValidMenu = HttpContext.Current.Session["multiValidMenuId"].ToString();

            //if (vIsValidMenu == "N")
            //{
            //    HttpContext.Current.Session["multiLoginId"] = "";
            //    x = "";
            //}

            if (x == null || x == "")
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = "SignIn",
                    controller = "ScUser",
                    area = "Security"
                }));

                //HttpContext.Current.Session.Clear();
                //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                //HttpContext.Current.Response.Cache.SetNoStore();
                //HttpContext.Current.Cache.Remove("MultiTEXCache");
                
                HttpContext.Current.Response.Redirect("/Security/ScUser/SignIn");
                
                return;
            }
            
            base.OnActionExecuting(filterContext); // re-added in edit
            
        }
    }
}