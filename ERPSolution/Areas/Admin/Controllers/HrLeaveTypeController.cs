using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class HrLeaveTypeController : BaseController
    {        
        public ActionResult Index()
        {
            var obList = new HR_LEAVE_TYPEModel().SelectAll();
            return View(obList);
        }

        public JsonResult PeriodTypeData()
        {
            var objList = new HrPeriodTypeModel().PeriodTypeListData();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListData()
        {
            var obList = new HR_LEAVE_TYPEModel().SelectAll();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcessableLeaveType()
        {
            var obList = new HR_LEAVE_TYPEModel().ProcessableLeaveType();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_LEAVE_TYPEModel ob)
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
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, vMsg });

        }


        [HttpPost]
        public JsonResult Save(HR_LEAVE_TYPEModel ob)
        {
            string vMsg = "";
    
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Save();

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

        public JsonResult LeaveTypeData(Int64 Id)
        {

            var ob = new HR_LEAVE_TYPEModel().Select(Id);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }





        public PartialViewResult LeaveTypeEntry()
        {
            return PartialView();
        }

        public PartialViewResult LeaveTypeList()
        {
            return PartialView();
        }
      
        //[HttpGet]
        //public ActionResult AddNew()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult AddNew(HR_LEAVE_TYPEModel ob)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Session["vMsg"] = ob.Save();
        //        return RedirectToAction("Index");
        //    }
            
        //    return View(ob);
        //}

        //[HttpGet]
        //public ActionResult Edit(long id)
        //{
        //    var ob = new HR_LEAVE_TYPEModel().Select(id);
        //    return View(ob);
        //}

        //[HttpPost]
        //public ActionResult Edit(HR_LEAVE_TYPEModel ob)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        Session["vMsg"] = ob.Update();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ob);
        //}
	}
}