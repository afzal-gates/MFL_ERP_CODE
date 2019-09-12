using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Hubs;
using ERPSolution.Common;
using System.Collections;
using System.Drawing;
using System.IO;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using System.Web.Hosting;
using Postal;
using Hangfire;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrSalAdvanceController : BaseController
    {
        //HR_SAL_ADVANCEBLL obBLL = new HR_SAL_ADVANCEBLL();
        //CommonBLL obCommonBLL = new CommonBLL();
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        // GET: Test/Country
        public ActionResult Index(Int64? a)
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); 
            ViewBag.LK_ACTION_TYPE_ID = a;
            //SickLeaveMail("dev.aminul@multi-fabs.com", "Md. Aminul Islam", "dasfd", "IT-MIS");
           // eLoanMailAfterApproval("dev.aminul@multi-fabs.com", "Md. Aminul Islam", "Software Engineer", "IT-MIS", "GEN-02492", "ADV-oeee", "Loan From Salary", 50000,"Production Director",DateTime.Now,7);
            return View();
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_SAL_ADVANCEModel ob)
        {
            string vMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Update();

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
                return Json(new { success = false, errors, vMsg });
            }

            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult submitData(HR_SAL_ADVANCEModel ob)
        {
            HR_SAL_ADVANCEModel Obj = new HR_SAL_ADVANCEModel();
            if (ModelState.IsValid)
            {
                try
                 {
                     Obj = ob.submitData();


                     if (ob.APROVER_LVL_NO == 2 && ob.LK_ADV_STATUS_ID == 119 && Obj.APRV_EMAIL_LIST!=string.Empty && ob.ADV_RQST_AMT>=200000)
                     {
                         BackgroundJob.Enqueue(() => eLoanMailBeforeApproval(Obj.APRV_EMAIL_LIST,Obj.EMP_FULL_NAME_EN,Obj.DESIGNATION_NAME_EN,Obj.DEPARTMENT_NAME_EN,Obj.EMPLOYEE_CODE,Obj.ADV_REF_NO,Obj.LK_ADV_TYPE,Obj.ADV_RQST_AMT));
                     }

                     if (ob.APROVER_LVL_NO == 3 && ob.LK_ADV_STATUS_ID == 123 && Obj.APRV_EMAIL_LIST != string.Empty)
                     {
                         BackgroundJob.Enqueue(() => eLoanMailAfterApproval(Obj.APRV_EMAIL_LIST,Obj.EMP_FULL_NAME_EN,Obj.DESIGNATION_NAME_EN,Obj.DEPARTMENT_NAME_EN,Obj.EMPLOYEE_CODE,Obj.ADV_REF_NO,Obj.LK_ADV_TYPE,Obj.ADV_APRV_AMT,Obj.APPROVED_BY,Obj.DEDU_ST_DT,Obj.NO_OF_INSTL));
                     }
                   
                    Hub.Clients.All.SalaryAdvApproverNotif();
                    Hub.Clients.All.SalaryAdvReqesterNotif();
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
                return Json(new { success = false, errors, Obj.MSG });
            }

            return Json(new { success = true, Obj.MSG });
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult saveDataForSectionHead(HR_SAL_ADVANCEModel ob)
        {
            string vMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.saveDataForSectionHead();
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
                return Json(new { success = false, errors, vMsg });
            }

            return Json(new { success = true, vMsg });
        }


       [HttpPost]
       [MyValidateAntiForgeryToken]
       public JsonResult ClearFromDashboard(HR_SAL_ADVANCEModel obj, Int64 pOption)
        {
            string vMsg = "";       
            try
            {
                vMsg = obj.ClearFromDashboard(pOption);

                if (pOption == 2004)
                {
                    Hub.Clients.All.SalaryAdvReqesterNotif();
                }
                else if (pOption == 2005)
                {
                    Hub.Clients.All.SalaryAdvApproverNotif();
                }
               
                return Json(new { success = true, vMsg });
            }
            catch (Exception e)
            {
                return Json(new { success = false, e.Message });
            } 
        }


        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult submitDataForSectionHead(HR_SAL_ADVANCEModel ob)
        //{
        //    string vMsg = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            vMsg = obBLL.submitDataForSectionHead(ob);
        //            Hub.Clients.All.SalaryAdvApproverNotif();
        //            Hub.Clients.All.SalaryAdvReqesterNotif();

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
        //        return Json(new { success = false, errors, vMsg });
        //    }

        //    return Json(new { success = true, vMsg });
        //}

        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult ApprovedByHr(HR_SAL_ADVANCEModel ob)
        //{

        //    string vMsg = "";
        //    if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
        //    {
        //        //string extension = Path.GetExtension(ob.ATT_FILE.FileName);
        //        string extension = ".jpg";
        //        string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SALARY_ADVANCE"), ob.ADV_REF_NO + extension);
        //        ob.ATT_FILE.SaveAs(path);

        //    }

        //    try
        //    {
        //        vMsg = obBLL.ApprovedByHr(ob);
        //        Hub.Clients.All.SalaryAdvApproverNotif();
        //        Hub.Clients.All.SalaryAdvReqesterNotif();

        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", e.Message);
        //    }
        //    return Json(new { success = true, vMsg });
        //}


        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult AccSave(HR_SAL_ADVANCEModel ob)
        //{

        //    string vMsg = "";
        //    try
        //    {
        //        vMsg = obBLL.AccSave(ob);
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("", e.Message);
        //    }
        //    return Json(new { success = true, vMsg });
        //}


        [HttpPost]
        public JsonResult fileClose(HR_SAL_ADVANCEModel ob)
        {

            string vMsg = "";
            try
            {
                vMsg = ob.fileClose();
                Hub.Clients.All.SalaryAdvReqesterNotif();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Json(new { success = true, vMsg });
        }
        

        
        
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HR_SAL_ADVANCEModel SAL_ADV)
        {

            string vMsg = "";
            try
            {
                vMsg = SAL_ADV.Save();

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Json(new { success = true, vMsg });        
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult SaveAdvData(HR_SAL_ADVANCEModel SAL_ADV)
        {
            var obj = SAL_ADV.SaveAdvData();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult SaveManualAdvData(HR_SAL_ADVANCEModel SAL_ADV)
        {
            var obj = SAL_ADV.SaveManualAdvData();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        
        [HttpPost]
        public JsonResult submitHrData(HR_SAL_ADVANCEModel SAL_ADV)
        {
            var obj = SAL_ADV.submitHrData();
            Hub.Clients.All.SalaryAdvApproverNotif();
            Hub.Clients.All.SalaryAdvReqesterNotif();
            return Json(obj, JsonRequestBehavior.AllowGet);
           
        }

        public PartialViewResult SalaryAdvReq()
        {
            return PartialView();
        }

        public PartialViewResult ManualSalaryAdvReq()
        {
            return PartialView();
        }

        public PartialViewResult SalaruAdvList()
        {
            return PartialView();
        }

        



        public JsonResult getEmployeeByUserId()
        {
            var emplList = new HR_EMPLOYEE_VM().getEmployeeByUserId();
            return Json(emplList, JsonRequestBehavior.AllowGet);

        }


        public JsonResult SalaryAdvDataById(Int64 HR_SAL_ADVANCE_ID, Int64 HR_SAL_ADV_APRVL_ID)
        {
            var data = new HR_SAL_ADVANCEModel().SalaryAdvDataById(HR_SAL_ADVANCE_ID, HR_SAL_ADV_APRVL_ID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SalAdvNotif(Int64 HR_SAL_ADVANCE_ID)
        {
            var datas = new HR_SAL_ADV_APRVLModel().SalAdvNotif(HR_SAL_ADVANCE_ID);
            return Json(datas, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CurrentSalaryAdvanceList()
        {
            var datas = new HR_SAL_ADVANCEModel().CurrentSalaryAdvanceList();
            return Json(datas, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getBoardOfDirectors()
        {
            var data = new HR_SAL_ADVANCEModel().getBoardOfDirectors();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public FileResult SalaryAdvanceImage(string SalAdvance)
        {
            string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/SALARY_ADVANCE"), SalAdvance + ".jpg");
            return File(path, "image/jpeg");
        }


        public static void eLoanMailBeforeApproval(String To, String EMP_FULL_NAME_EN, String DESIGNATION_NAME_EN, String DEPARTMENT_NAME_EN, string EMPLOYEE_CODE, string ADV_REF_NO, string LK_ADV_TYPE, Decimal ADV_RQST_AMT)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new eLoanMailServiceBeforeApprove
            {
                To = To,
                EMP_FULL_NAME_EN = EMP_FULL_NAME_EN,
                DESIGNATION_NAME_EN = DESIGNATION_NAME_EN,
                DEPARTMENT_NAME_EN = DEPARTMENT_NAME_EN,
                EMPLOYEE_CODE = EMPLOYEE_CODE,
                ADV_REF_NO = ADV_REF_NO,
                LK_ADV_TYPE = LK_ADV_TYPE,
                ADV_RQST_AMT = ADV_RQST_AMT
            };

            emailService.Send(email);
        }


        public static void eLoanMailAfterApproval(String To, String EMP_FULL_NAME_EN, String DESIGNATION_NAME_EN, String DEPARTMENT_NAME_EN, string EMPLOYEE_CODE, string ADV_REF_NO, string LK_ADV_TYPE, Decimal ADV_APRV_AMT, string APPROVED_BY, DateTime DEDU_ST_DT,Int64 NO_OF_INSTL)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new eLoanMailServiceAfterApprove
            {
                To = To,
                EMP_FULL_NAME_EN = EMP_FULL_NAME_EN,
                DESIGNATION_NAME_EN = DESIGNATION_NAME_EN,
                DEPARTMENT_NAME_EN = DEPARTMENT_NAME_EN,
                EMPLOYEE_CODE = EMPLOYEE_CODE,
                ADV_REF_NO = ADV_REF_NO,
                LK_ADV_TYPE = LK_ADV_TYPE,
                ADV_APRV_AMT = ADV_APRV_AMT,
                APPROVED_BY=APPROVED_BY,
                DEDU_ST_DT = DEDU_ST_DT,
                NO_OF_INSTL = NO_OF_INSTL
            };

            emailService.Send(email);
        }        
    }
}