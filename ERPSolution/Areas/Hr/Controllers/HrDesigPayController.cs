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
    public class HrDesigPayController : BaseController
    {       

        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult DesigPaySetup()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult BatchSave(List<HR_DESIG_PAYModel> obList)
        {
            string vMsg = "";
            
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = new HR_DESIG_PAYModel().BatchSave(obList);

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



        public JsonResult DesigPayListData(Int64? pHR_DEPARTMENT_ID, Int64? pHR_PAY_ELEMENT_ID)
        {            
            var ob = new HR_DESIG_PAYModel().DesigPayListData(pHR_DEPARTMENT_ID, pHR_PAY_ELEMENT_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


       
        
    }
}