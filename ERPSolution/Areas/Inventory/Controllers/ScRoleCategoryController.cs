using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;

namespace ERPSolution.Areas.Inventory.Controllers
{
    [SignInCheck]
    public class ScRoleCategoryController : Controller
    {
        //        // GET: /Security/ScRoleCategory/
        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult Save(List<INV_USER_ITMCATModel> roleMenuNode)
        //{
        //    var x = HttpContext.Request.QueryString;
        //    string vMsg = "";
        //    var errors = new Hashtable();


        //    foreach (INV_USER_ITMCATModel item in roleMenuNode)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                INV_USER_ITMCATModel ob = new INV_USER_ITMCATModel();
        //                ob.SC_USER_ID = item.SC_USER_ID;
        //                ob.INV_ITEM_CAT_ID = item.INV_ITEM_CAT_ID;
        //                ob.IS_ACTIVE = item.IsChecked == true ? "Y" : "N";
        //                //ob.CREATED_BY = Convert.ToInt64(Session["multiScUserId"]);
        //                //ob.LAST_UPDATED_BY = Convert.ToInt64(Session["multiScUserId"]);

        //                vMsg = ob.Save();
        //            }
        //            catch (Exception e)
        //            {
        //                ModelState.AddModelError("", e.Message);
        //            }
        //        }
        //        else
        //        {                    
        //            foreach (var pair in ModelState)
        //            {
        //                if (pair.Value.Errors.Count > 0)
        //                {
        //                    errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //                }
        //            }
        //            return Json(new { success = false, errors });
        //        }
        //    }


        //    return Json(new { success = true, vMsg });
        //}
        
        public JsonResult ScRoleMenuData(INV_USER_ITMCATModel ob)
        {
            var obList = new INV_ITEM_CATModel().ScRoleMenuData( ob.SC_USER_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReportListData(Int64 pSC_MENU_ID)
        {

            var obList = new RF_REPORTModel().ReportListData(pSC_MENU_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }




	}
}