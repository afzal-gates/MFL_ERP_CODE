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
    public class HrSalaryTranController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult SalaryTran()
        {
            return PartialView();
        }

        public JsonResult SalaryTranHdrData(Int64 pACC_PAY_PERIOD_ID, Int64 pHR_EMPLOYEE_ID)
        {            
            var ob = new HR_SAL_TRAN_HModel().SalaryTranHdrData(pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalaryPayListData(Int64 pHR_SAL_TRAN_H_ID)
        {
            var ob = new HR_SAL_TRAN_DModel().SalaryPayListData(pHR_SAL_TRAN_H_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AttenSummeryData(Int64 pACC_PAY_PERIOD_ID, Int64 pHR_EMPLOYEE_ID)
        {           
            var ob = new HR_SAL_TRAN_HModel().AttenSummeryData(pACC_PAY_PERIOD_ID, pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DesignationGrpListData(string pPARAM_TYPE)
        {
            var obList = new HrDesigGroupModel().DesignationGrpListData(pPARAM_TYPE);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult BatchSave(HR_SAL_TRAN_HModel obHDR, List<HR_SAL_TRAN_DModel> obList)
        {            
            string vMsg = "";           

            if (ModelState.IsValid)
            {
                try
                {
                    var x = obHDR.BatchSave(obList);
                    return Json(new { x });
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

       
        
    }
}