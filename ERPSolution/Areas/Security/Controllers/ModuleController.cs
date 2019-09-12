using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;


namespace ERPSolution.Areas.Security.Controllers
{
    [SignInCheck]
    public class ModuleController : BaseController
    {
        //
        // GET: /Security/Module/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModuleCreation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(ScSystemModel ob)
        {
            if (ob.TranMode == 1)
                Session["vMsg"] = ob.Save();
            else
                Session["vMsg"] = ob.Update();

            return RedirectToAction("ModuleCreation");
        }

        public JsonResult SystemModuleData()
        {
            var obList = new ScSystemModel().ScSystemModuleData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }
	}
}