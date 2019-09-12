using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using System.Web.Configuration;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrOtEntitledController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult MappingOtTeam()
        {
            return PartialView();
        }        

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult BatchSave(HR_OT_ENTITLEDModel ob, List<HrDesigModel> ob1)
        {
            string vMsg = "";
            //bool vSuccess = false;

            //bool vIsOtApplicable = obEmpBLL.EmployeeListData(null,null,ob.EMPLOYEE_CODE,null).Any(m => m.HR_EMPLOYEE_ID==ob.HR_EMPLOYEE_ID && (m.OT_UNIT_FLAG == "D" || m.OT_UNIT_FLAG == "H"));
            //if (vIsOtApplicable!=true)
            //{
            //    ModelState.AddModelError("HR_EMPLOYEE_ID", "Sorry! OT not applicable for this employee");
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.BatchSave(ob1);

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

            return Json(new { success = true, vMsg });
        }

        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult Save(HR_OT_ENTITLEDModel ob)
        //{
        //    string vMsg = "";
        //    //bool vSuccess = false;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            vMsg = obBLL.Save(ob);

        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Json(new { success = false, errors });
        //    }

        //    return Json(new { success = true, vMsg });
        //}

        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult Update(HR_OT_ENTITLEDModel ob)
        //{
        //    string vMsg = "";
        //    //bool vSuccess = false;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            vMsg = obBLL.Update(ob);

        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Json(new { success = false, errors });
        //    }

        //    return Json(new { success = true, vMsg });
        //}


        //[HttpPost]
        ////[MyValidateAntiForgeryToken]
        //public JsonResult DeleteBatch(List<HR_OT_APPROVEModel> obList)
        //{
        //    string vMsg = "";
        //    //bool vSuccess = false;            
        //    //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        //    //DateTime ddate = json_serializer.Deserialize<DateTime>(@"""\/Date(1326038400000)\/""").ToLocalTime();
            
        //    //foreach(HR_OT_APPROVEModel ob in obList)
        //    //{
        //    //    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
        //    //    DateTime ddate = json_serializer.Deserialize<DateTime>(@"""\"+ob.OT_APRV_DATE+@"/""").ToLocalTime();
        //    //}


        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            vMsg = obBLL.DeleteBatch(obList);

        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Json(new { success = false, errors });
        //    }

        //    return Json(new { success = true, vMsg });
        //}

        public JsonResult OtTeamListData()
        {
            var ob = new HR_OT_TEAMModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NonEntitledDesigListData()
        {
            var ob = new HrDesigModel().NonEntitledDesigListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EntitledDesigListData(Int64 pHR_OT_TEAM_ID)
        {            
            var ob = new HrDesigModel().EntitledDesigListData(pHR_OT_TEAM_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
                

    }
}