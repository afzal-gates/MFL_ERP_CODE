using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using ERPSolution.Models;
using ERP.Model;

namespace ERPSolution.Controllers
{
    public class BaseController : Controller
    {
        public bool IsMenuPermission()
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            Int64 ScUserId = Convert.ToInt64(Session["multiScUserId"]);
            List<ScMenuModel> obList = new List<ScMenuModel>();

            obList = new ScMenuModel().checkMenuPermission(ScUserId, controllerName, actionName);
            if (obList.Count > 0 || ScUserId == 1)
                return true;
            else
                return false;
        }

        public BaseController()
        {
            //List<ScMenuModel> obList = new List<ScMenuModel>();
            //obList = obBLL.ScMenuData();

            //ViewData["sideMenuList"] = obList;




            //ScMenuModel currentObj =           
            //            from p in obList
            //            where p.MENU_URL==CurrentUrl
            //            select p;

            //if (CurrentURL == "/") {
            //  CurrentMenu = obBLL.ScMenuData().Where(m => m.MENU_URL == CurrentURL).ToList()[0];
            //}
            // CurrentMenu = obBLL.ScMenuData().Where(m => m.MENU_URL == CurrentURL).ToList()[0];


            //List<MenuModel> listMenu = new List<MenuModel>();

            //if (Session != null)
            //{
            //    Int64 Role = Convert.ToInt64(SessionHandler.GetRole());

            //    MenuModel obMenu = new MenuModel();
            //    obMenu.ID = 1;
            //    obMenu.Name = "Test";
            //    obMenu.Application = "Test";
            //    obMenu.Functionality = "Country";
            //    obMenu.Page = "Index";

            //if (Role == 1 || Role == 2)
            //{
            //listMenu.Add(obMenu);
            //}

            //obMenu = new MenuModel();
            //obMenu.ID = 1;
            //obMenu.Name = "Security";
            //obMenu.Application = "Security";
            //obMenu.Functionality = "Users";
            //obMenu.Page = "Index";

            //if (Role == 1)
            //{
            //listMenu.Add(obMenu);
            //}
            //}

            //ViewData["MenuList"] = listMenu;
        }

        [OutputCache(Duration = 10, VaryByParam = "MultiTEXCache")]
        public PartialViewResult _SideMenu()
        {
            Int64 ScUserId = Convert.ToInt64(Session["multiScUserId"]);
            string UserType = Session["multiUserType"].ToString();
            List<ScMenuModel> obList = new List<ScMenuModel>();

            if (UserType == "B")
            {
                obList = new ScMenuModel().ScMenuData();
            }
            else
            {
                //obList = obBLL.ScMenuData();
                obList = new ScMenuModel().ScSideMenuData(ScUserId);

                if (Session["multiValidMenuId"].ToString() == "Y")
                {
                    if (HttpContext.Request.RawUrl != "/" && HttpContext.Request.RawUrl.ToLower() != "/Home/Index/UserDashBoard".ToLower())
                    {
                        List<ScMenuModel> obUserMenuList = new List<ScMenuModel>();
                        obUserMenuList = (List<ScMenuModel>)HttpContext.Session["multiUserMenuList"];
                        string vMenuID = "";
                        string[] vQryStrSplit = { };
                        string vQryStr = HttpContext.Request.Params["a"];
                        if (vQryStr != null && vQryStr != "")
                        {
                            vQryStrSplit = vQryStr.Split('/');
                            vMenuID = vQryStrSplit[0];
                        }

                        bool vIsUser = false;
                        if (vMenuID != null && vMenuID != "")
                        {
                            vIsUser = obUserMenuList.Any(m => m.SC_MENU_ID == Convert.ToInt64(vMenuID));
                        }
                        if (vIsUser == false)
                        {
                            Session["multiValidMenuId"] = "N";
                            //return RedirectToAction("SignIn", "ScUser", new { area = "Security" });
                        }
                    }
                }
                if (Session["multiValidMenuId"].ToString() == "")
                {
                    Session["multiValidMenuId"] = "Y";
                }


            }
            ViewData["sideMenuList"] = obList;
            return PartialView();
        }


        //public JsonResult MenuData()
        //{
        //    List<ScMenuModel> obList = new List<ScMenuModel>();
        //    obList = obBLL.ScMenuData();
        //    return Json(obList, JsonRequestBehavior.AllowGet);
        //}


        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = 999999999 //Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }



    }
}