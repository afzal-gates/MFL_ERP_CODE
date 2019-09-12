using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class HrGradeController : BaseController
    {
        // Start Temp Block =====================================================

        public ActionResult Index1()
        {
            var obList = new HrGradeModel().SelectAll();
            return View(obList);
        }

        // GET: /Admin/HrGrade/
        public ActionResult Index()
        {
            var obList = new HrGradeModel().SelectAll();
            return View(obList);
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(HrGradeModel ob)
        {
            if (ModelState.IsValid)
            {
                Session["vMsg"] = ob.Save();
                return RedirectToAction("Index");
            }
            
            return View(ob);
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var ob = new HrGradeModel().Select(id);
            return View(ob);
        }

        [HttpPost]
        public ActionResult Edit(HrGradeModel ob)
        {
            if(ModelState.IsValid)
            {
                Session["vMsg"] = ob.Update();
                return RedirectToAction("Index");
            }
            return View(ob);
        }
	}
}