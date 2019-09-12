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
    public class HrAssignEmpAccountController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); 

            ViewBag.PageName = "Assign Employee Bank A/C";
            return View();
        }
        public PartialViewResult AssignEmpBkAcc()
        {
            return PartialView();
        }
       
       
        
    }
}