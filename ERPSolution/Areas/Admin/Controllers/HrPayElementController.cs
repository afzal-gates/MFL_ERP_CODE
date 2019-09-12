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
    public class HrPayElementController : BaseController
    {

        //
        // GET: /Admin/HrDepartment/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult BillTypeListData()
        {
            var ob = new HR_PAY_ELEMENTModel().BillTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        // End For department

	}
}