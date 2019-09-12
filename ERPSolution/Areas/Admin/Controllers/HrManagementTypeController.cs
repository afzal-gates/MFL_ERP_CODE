using ERP.Model;
using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERPSolution.Common;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class HrManagementTypeController : BaseController
    {
        public ActionResult Index()
        {            
            return View();            
        }

        public JsonResult HrManagementTypeData()
        {
            var obList = new HrManagementTypeModel().HrManagementTypeData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(HrManagementTypeModel ob)
        {
            if (ModelState.IsValid)
            {
                if (ob.TranMode == 1)
                    Session["vMsg"] = ob.Save();
                else
                    Session["vMsg"] = ob.Update();

                return RedirectToAction("Index");                                
            }

            return View("Index",ob);
        }



	}
}