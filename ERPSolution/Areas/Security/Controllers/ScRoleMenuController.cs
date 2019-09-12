using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;

namespace ERPSolution.Areas.Security.Controllers
{
    [SignInCheck]
    public class ScRoleMenuController : BaseController
    {
        //        // GET: /Security/ScRole/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(List<SC_ROLE_MENUModel> roleMenuNode)
        {
            var x = HttpContext.Request.QueryString;
            string vMsg = "";
            var errors = new Hashtable();


            foreach (SC_ROLE_MENUModel item in roleMenuNode)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        SC_ROLE_MENUModel ob = new SC_ROLE_MENUModel();
                        ob.SC_MENU_ID = item.SC_MENU_ID;
                        ob.SC_ROLE_ID = item.SC_ROLE_ID;
                        ob.IS_ACTIVE = item.IsChecked == true ? "Y" : "N";
                        ob.CREATED_BY = Convert.ToInt64(Session["multiScUserId"]);
                        ob.LAST_UPDATED_BY = Convert.ToInt64(Session["multiScUserId"]);

                        vMsg = ob.Save();
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
                else
                {                    
                    foreach (var pair in ModelState)
                    {
                        if (pair.Value.Errors.Count > 0)
                        {
                            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                        }
                    }
                    return Json(new { success = false, errors });
                }
            }


            return Json(new { success = true, vMsg });
        }

        public JsonResult getRptGrpDataList()
        {
            var obList = new RF_REPORT_GRPModel().getRptGrpDataList();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getRoleRptDataList(int pRF_REPORT_GRP_ID, int pSC_ROLE_ID)
        {
            var obList = new RF_REPORTModel().getRoleRptDataList(pRF_REPORT_GRP_ID, pSC_ROLE_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult SaveRoleReport(RF_REPORTModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveRoleReport();

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                var errors = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, jsonStr });
        }

        //public string Update(ScRoleModel ob)
        //{
        //    string vMsg = "";
        //    if (ModelState.IsValid)
        //        vMsg = obBLL.Update(ob);

        //    return vMsg;
        //}

        //public JsonResult RoleListData()
        //{
        //    List<SC_ROLE_MENUModel> ob = new List<SC_ROLE_MENUModel>();

        //    ob = obBLL.SelectAll();

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult MenuListData()
        //{
        //    List<SC_ROLE_MENUModel> ob = new List<ScMenuModel>();

        //    ob = obMenuBLL.SelectAll();

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult RoleMenuListData()
        //{
        //    List<ScMenuModel> ob = new List<ScMenuModel>();

        //    ob = obMenuBLL.SelectAll();

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult IsUniqueNameE(ScRoleModel ob)
        //{
        //    try
        //    {
        //        //obBLL.SignIn().Where(m => m.LOGIN_ID == ob.LOGIN_ID && m.PASSWORD_HASH == ob.PASSWORD_HASH).ToList()[0]
        //        ScRoleModel obUser = obBLL.SelectAll().Where(m => m.ROLE_NAME_EN.ToLower() == ob.ROLE_NAME_EN.ToLower()).ToList()[0];
        //        return Json(false, JsonRequestBehavior.AllowGet);
        //    }
        //    catch 
        //    {
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult ScRoleMenuData(SC_ROLE_MENUModel ob)
        {
            var obList = new ScMenuModel().ScRoleMenuData(ob.SC_SYSTEM_MODULE_ID, ob.SC_ROLE_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReportListData(Int64 pSC_MENU_ID)
        {

            var obList =new RF_REPORTModel().ReportListData(pSC_MENU_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }




	}
}