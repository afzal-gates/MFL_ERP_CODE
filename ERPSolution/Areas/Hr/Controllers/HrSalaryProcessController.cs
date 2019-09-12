using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using System.Web.Hosting;
using System.IO;
using Postal;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrSalaryProcessController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); 

            string vMenuID = "";
            string[] vQryStrSplit = { };
            string vQryStr = HttpContext.Request.Params["a"];
            ViewBag.PageName = "";
            if (vQryStr != null && vQryStr != "")
            {
                vQryStrSplit = vQryStr.Split('/');
                vMenuID = vQryStrSplit[0];

                if(vMenuID=="14")
                    ViewBag.PageName = "Salary Process";
                else if(vMenuID=="15")
                    ViewBag.PageName = "Partial Salary Process";
            }

            return View();
        }
        public PartialViewResult SalaryProcess()
        {            
            return PartialView();
        }

        public PartialViewResult PartSalaryProcess()
        {            
            return PartialView();
        }
       
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult SalaryProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pHR_OFFICE_ID, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_SAL_TRAN_HModel().SalaryProcess(pHR_COMPANY_ID, pACC_PAY_PERIOD_ID, pHR_OFFICE_ID, pHR_DEPARTMENT_ID,
                                                pHR_DESIGNATION_ID, pHR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID, pHR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID, pLINE_NO, pCORE_DEPT_ID);
            }
            catch(Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult PartialSalaryProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pHR_OFFICE_ID, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID, Int64? pLK_RELIGION_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID, string pIS_INCLUDE_ADVANCE, string pIS_INCLUDE_OT,
            Int64? pROUND_AMOUNT, Int64? pROUND_TYPE_ID, Int64? pADDL_PRE_DAYS, string pHR_DESIGNATION_GRP_IDS, DateTime? pOT_START_DT, DateTime? pOT_END_DT)
        {
            string vMsg = "";

            try
            {
                vMsg = new HR_SAL_TRAN_HModel().PartialSalaryProcess(pHR_COMPANY_ID, pACC_PAY_PERIOD_ID, pHR_OFFICE_ID, pHR_DEPARTMENT_ID,
                                                pHR_DESIGNATION_ID, pHR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID, pLK_RELIGION_ID, pHR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID, pLINE_NO, pCORE_DEPT_ID, pIS_INCLUDE_ADVANCE, pIS_INCLUDE_OT,
                                                pROUND_AMOUNT, pROUND_TYPE_ID, pADDL_PRE_DAYS, pHR_DESIGNATION_GRP_IDS, pOT_START_DT, pOT_END_DT);
            }
            catch (Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        public static void SendJobCardNotificationMail(string ACC_PAY_PERIOD_NAME)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new JobCardCheckingMailService
            {
                ACC_PAY_PERIOD_NAME = ACC_PAY_PERIOD_NAME,
            };

            emailService.Send(email);
        }

        [HttpGet]
        public JsonResult FireJobCardNotificationMail(Int64? pACC_PAY_PERIOD_ID)
        {
            try
            {
                HR_SAL_TRAN_HModel obj = new HR_SAL_TRAN_HModel().getAccPayPeriodName(pACC_PAY_PERIOD_ID);
                if (obj.ACC_PAY_PERIOD_NAME!= String.Empty)
                {
                    SendJobCardNotificationMail(obj.ACC_PAY_PERIOD_NAME);
                }
                return Json(new { SUCCESS = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return View(ob);
                throw ex;
            }
        }
    }
}