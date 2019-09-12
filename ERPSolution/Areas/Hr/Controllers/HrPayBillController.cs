using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrPayBillController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
           
        public PartialViewResult OtherBillProcess()
        {
            return PartialView();
        }
       
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult OtherBillProcessData(DateTime? pFromDate, DateTime? pToDate, string pPAY_ELEMENT_CODE, string pOT_TYPES,
                                            Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID, Int64 pHR_PAY_ELEMENT_ID, string pHR_EMPLOYEE_IDS, Int64? pCORE_DEPT_ID,
                                            Int64? pHR_DEPARTMENT_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pEMPLOYEE_TYPE_ID,
                                            Int64? pHR_OFFICE_ID = null)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_PAY_BILLModel().OtherBillProcess(pFromDate, pToDate, pPAY_ELEMENT_CODE, pOT_TYPES, pHR_COMPANY_ID, pACC_PAY_PERIOD_ID,
                                                                pHR_PAY_ELEMENT_ID, pHR_EMPLOYEE_IDS, pCORE_DEPT_ID, pHR_DEPARTMENT_ID, pHR_SHIFT_TEAM_ID,
                                                                pLK_FLOOR_ID, pLINE_NO, pEMPLOYEE_TYPE_ID, pHR_OFFICE_ID);
            }
            catch(Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult OTBillProcessData(DateTime? pFromDate, DateTime? pToDate, string pPAY_ELEMENT_CODE, string pOT_TYPES,
                                            Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID, string pHR_EMPLOYEE_IDS, Int64? pCORE_DEPT_ID,
                                            Int64? pHR_DEPARTMENT_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pEMPLOYEE_TYPE_ID)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_PAY_BILLModel().OTBillProcessData(pFromDate, pToDate, pPAY_ELEMENT_CODE, pOT_TYPES, pHR_COMPANY_ID, pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_IDS, 
                                                                pCORE_DEPT_ID, pHR_DEPARTMENT_ID, pHR_SHIFT_TEAM_ID, pLK_FLOOR_ID, pLINE_NO, pEMPLOYEE_TYPE_ID);
            }
            catch (Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult NightBillProcessData(DateTime? pFromDate, DateTime? pToDate, string pPAY_ELEMENT_CODE, string pOT_TYPES,
        //                                    Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID)
        //{
        //    string vMsg = "";

        //    try
        //    {
        //        vMsg = obBLL.NightBillProcessData(pFromDate, pToDate, pPAY_ELEMENT_CODE, pOT_TYPES, pHR_COMPANY_ID, pACC_PAY_PERIOD_ID);
        //    }
        //    catch (Exception e)
        //    {
        //        vMsg = e.Message;
        //    }

        //    return Json(new { success = true, vMsg });
        //}


        public JsonResult OtTypeListData()
        {            
            var ob = new HR_OT_TYPEModel().OtTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        
        public PartialViewResult _ElProcess()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult ElEncashmentProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pACC_PAY_MONTH_ID, Int64? pHR_DEPARTMENT_ID, Int64? pHR_DESIGNATION_ID,
            Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID, Int64? pEMPLOYEE_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID,
            string pHR_DESIGNATION_GRP_IDS, DateTime? pJOINING_DT, decimal? pPAY_PCT)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_EL_BILLModel().ElEncashmentProcess(pHR_COMPANY_ID, pACC_PAY_PERIOD_ID, pACC_PAY_MONTH_ID, pHR_DEPARTMENT_ID,
                                                pHR_DESIGNATION_ID, pHR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID, pEMPLOYEE_TYPE_ID, pLK_FLOOR_ID, pLINE_NO, pCORE_DEPT_ID,
                                                pHR_DESIGNATION_GRP_IDS, pJOINING_DT, pPAY_PCT);
            }
            catch (Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        
    }
}