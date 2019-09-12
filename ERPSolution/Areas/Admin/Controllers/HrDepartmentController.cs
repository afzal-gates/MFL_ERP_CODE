using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class HrDepartmentController : BaseController
    {
        //
        // GET: /Admin/HrDepartment/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View1()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNew(HrDepartmentModel ob)
        {
            if (ob.TranMode == 1)
                Session["vMsg"] = ob.Save();
            else
                Session["vMsg"] = ob.Update();

            return RedirectToAction("Index");
        }

        // Start For department

        public JsonResult SelectAll()
        {
            var ob = new HrDepartmentModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeptListData()
        {
            var ob = new HrDepartmentModel().DeptListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        // End For department

	}
}