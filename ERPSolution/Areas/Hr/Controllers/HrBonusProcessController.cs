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
    public class HrBonusProcessController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/"));

            ViewBag.PageName = "Bonus Process";
            return View();
        }
        public PartialViewResult BonusProcess()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult BonusProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID, Int64? pLK_FB_TYPE_ID,
            DateTime? pFB_LIMIT_DT)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_FB_TRANModel().BonusProcess(pHR_COMPANY_ID, pACC_PAY_PERIOD_ID, pHR_DEPARTMENT_ID,
                                                pHR_DESIGNATION_ID, pHR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID, pHR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID, pLINE_NO, pCORE_DEPT_ID, pLK_FB_TYPE_ID, pFB_LIMIT_DT);
            }
            catch (Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }
        
       
        
    }
}