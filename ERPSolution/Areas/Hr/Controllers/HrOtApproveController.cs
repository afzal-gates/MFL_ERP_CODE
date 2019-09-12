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
    public class HrOtApproveController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult OtApprove()
        {            
            return PartialView();
        }

        public PartialViewResult OtApproveList()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HR_OT_APPROVEModel ob, HR_TA_PROCESS_DATAModel ob1)
        {
            string vMsg = "";
            //bool vSuccess = false;

            bool vIsOtApplicable = new HR_EMPLOYEEModel().EmployeeListData(null,null,ob.EMPLOYEE_CODE,null).Any(m => m.HR_EMPLOYEE_ID==ob.HR_EMPLOYEE_ID && (m.OT_UNIT_FLAG == "D" || m.OT_UNIT_FLAG == "H"));
            if (vIsOtApplicable!=true)
            {
                ModelState.AddModelError("HR_EMPLOYEE_ID", "Sorry! OT not applicable for this employee");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Save(ob1);

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
        //public JsonResult Update(HR_OT_APPROVEModel ob)
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


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult DeleteBatch(List<HR_OT_APPROVEModel> obList)
        {
            string vMsg = "";
            //bool vSuccess = false;            
            //JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //DateTime ddate = json_serializer.Deserialize<DateTime>(@"""\/Date(1326038400000)\/""").ToLocalTime();
            
            //foreach(HR_OT_APPROVEModel ob in obList)
            //{
            //    JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //    DateTime ddate = json_serializer.Deserialize<DateTime>(@"""\"+ob.OT_APRV_DATE+@"/""").ToLocalTime();
            //}


            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = new HR_OT_APPROVEModel().DeleteBatch(obList);

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

        public JsonResult OtApproveListData(DateTime pOT_APRV_DATE)
        {                        
            var ob = new HR_OT_APPROVEModel().OtApproveListData(pOT_APRV_DATE);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OtApproveSearchListData(DateTime? pOT_FROM_DATE, DateTime? pOT_TO_DATE, Int64? pHR_EMPLOYEE_ID)
        {
            var ob = new HR_OT_APPROVEModel().OtApproveSearchListData(pOT_FROM_DATE, pOT_TO_DATE, pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AttenDataList(HR_TA_PROCESS_DATAModel ob)
        {
            var obList = ob.AttenDataList();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }
     

    }
}