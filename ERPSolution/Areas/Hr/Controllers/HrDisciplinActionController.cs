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
    public class HrDisciplinActionController : BaseController
    {
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        
        public PartialViewResult DisciplinAction()
        {
            return PartialView();
        }

        public PartialViewResult DisciplinSearch()
        {
            return PartialView();
        }

        public PartialViewResult DisciplinList()
        {
            return PartialView();
        }

       
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HR_DSPLN_ACTModel ob1)
        {
            string vMsg = "";
            //bool vSuccess = false;

            if ((ob1.HR_DSPLN_ACT_TYPE_ID == 9 /*|| ob1.HR_DSPLN_ACT_TYPE_ID == 10 || ob1.HR_DSPLN_ACT_TYPE_ID == 11*/) && ob1.IS_DEDUCT_SALARY4FS == "N")
                ob1.HR_DSPLN_ACT_TYPE_ID = 10;
                //ModelState.AddModelError("IS_DEDUCT_SALARY4FS", "Sorry! Fair shop sales found for this employee");


            if ((ob1.HR_DSPLN_ACT_TYPE_ID == 1 || ob1.HR_DSPLN_ACT_TYPE_ID == 2) && (ob1.DEDU_AMT == null || ob1.DEDU_AMT < 1))
                ModelState.AddModelError("DEDU_AMT", "Amount must be grater than 0 for this action type.");

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob1.Save();

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


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_DSPLN_ACTModel ob1)
        {
            string vMsg = "";
            //bool vSuccess = false;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob1.Update();

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


        public JsonResult DeductAmountData(HR_DSPLN_ACTModel ob)
        {
            Int64 deductAmount = ob.DeductAmountData();
            return Json(new { deductAmount = deductAmount });
        }


        public JsonResult ActionTypeListData()
        {
            var ob = new HR_DSPLN_ACT_TYPEModel().ActionTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        } 

        public JsonResult LookupListData(int pLookupTableId)
        {            
            var ob = new LookupDataModel().LookupListData(pLookupTableId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyAllPeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {            
            var ob = new ACC_PAY_PERIODModel().AllPeriodListData(pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyOpenPeriodListData(Int64 pHR_COMPANY_ID, Int64? pHR_PERIOD_TYPE_ID)
        {            
            var ob = new ACC_PAY_PERIODModel().OpenPeriodListData(pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisciplinActionListData(Int64? pHR_COMPANY_ID, Int64? pRF_FISCAL_YEAR_ID, Int64? pRF_CAL_MONTH_ID, 
                            Int64? pHR_EMPLOYEE_ID, string pDISP_ACT_REF_NO)
        {            
            var ob = new HR_DSPLN_ACTModel().DisciplinActionListData(pHR_COMPANY_ID, pRF_FISCAL_YEAR_ID, pRF_CAL_MONTH_ID, pHR_EMPLOYEE_ID, pDISP_ACT_REF_NO);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoSearchDisciplinActionListData(string pDISP_ACT_REF_NO)
        {            
            var ob = new HR_DSPLN_ACTModel().AutoSearchDisciplinActionListData(pDISP_ACT_REF_NO);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        

    }
}