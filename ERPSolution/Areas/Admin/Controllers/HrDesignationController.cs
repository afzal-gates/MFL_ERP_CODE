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
    public class HrDesignationController : BaseController
    {
        //
        // GET: /Admin/HrDesignation/
        public ActionResult IndexG()
        {
            Int64 showDepartment; 

            if(Session["DepartmentID"]==null)
                showDepartment=0;
            else
                showDepartment = Convert.ToInt64(Session["DepartmentID"]);

            var obList = new HrDesigModel().DesignationListData(showDepartment, 1);
            return View(obList);
            
        }

        public ActionResult Index()
        {
            Int64 showDepartment;

            if (Session["DepartmentID"] == null)
                showDepartment = 0;
            else
                showDepartment = Convert.ToInt64(Session["DepartmentID"]);

            var obList = new HrDesigModel().DesignationListData(showDepartment, 1);
            return View(obList);

        }

        public ActionResult AddNew()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult AddNew(HrDesigModel ob)
        {
            Session["DepartmentID"] = ob.HR_DEPARTMENT_ID;

            Session["vMsg"] = ob.Save();
            return RedirectToAction("Index");            
        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {
            var ob = new HrDesigModel().Select(id);
            return View(ob);
        }

        [HttpPost]
        public ActionResult Edit(HrDesigModel ob)
        {
            Session["DepartmentID"] = ob.HR_DEPARTMENT_ID;
            Session["vMsg"] = ob.Update();
            return RedirectToAction("Index");
        }




        ////////////////////////////////////////////////////
        public ActionResult GIndex()
        {
            Int64 showDesigGrpId;

            if (Session["DesigGrpID"] == null)
                showDesigGrpId = 0;
            else
                showDesigGrpId = Convert.ToInt64(Session["DesigGrpID"]);

            var obList = new HrDesigModel().DesignationListData(showDesigGrpId, 2);
            return View(obList);
        }

        public ActionResult GAddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GAddNew(HrDesigModel ob)
        {
            Session["DesigGrpID"] = ob.HR_DESIGNATION_GRP_ID;

            Session["vMsg"] = ob.Save();
            return RedirectToAction("GIndex");
        }

        [HttpGet]
        public ActionResult GEdit(Int64 id)
        {
            var ob = new HrDesigModel().Select(id);
            return View(ob);
        }

        [HttpPost]
        public ActionResult GEdit(HrDesigModel ob)
        {
            Session["DesigGrpID"] = ob.HR_DESIGNATION_GRP_ID;
            Session["vMsg"] = ob.Update();
            return RedirectToAction("GIndex");
        }

        /////////////////////////////////////////
      
        
        

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public ActionResult Save(HrDesigModel ob)
        {
            //string x = HttpContext.Request.Params["HR_DEPARTMENT_ID"];
            Session["DepartmentID"] = ob.HR_DEPARTMENT_ID;
            
            if (ob.TRAN_MODE == 1)
                Session["vMsg"] = ob.Save();
            else if (ob.TRAN_MODE == 2)
                Session["vMsg"] = ob.Update();
            else
                Session["vMsg"] = "MULTI-002Transection problem";

            return RedirectToAction("Index");
            
        }
        
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult SaveD(HrDesigModel ob)
        {
            string vMsg = "";
            //bool vSuccess = false;

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
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult UpdateD(HrDesigModel ob)
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

        public JsonResult DepartmentListData()
        {
            var ob = new HrDepartmentModel().DepartmentListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DesignationGrpListData(string pPARAM_TYPE)
        {
            var ob = new HrDesigGroupModel().DesignationGrpListData(pPARAM_TYPE);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DesignationListData()
        {
            Int64 showDepartment=0;
            int vTypeId = 0;
            var vDeptId = HttpContext.Request.Params["pShowDepartment"];
            var vType = HttpContext.Request.Params["pType"];

            if (vType != "")
            { 
                vTypeId = Convert.ToInt32(vType); 
            }

            if (vDeptId != "")
            {
                showDepartment = Convert.ToInt64(vDeptId);
                Session["DepartmentID"] = showDepartment;
            }
            var ob = new HrDesigModel().DesignationListData(showDepartment, Convert.ToInt32(vType));
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ManagementTypeListData()
        {
            var ob = new HrManagementTypeModel().ManagementTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeeTypeListData()
        {
            var ob = new HrManagementTypeModel().EmployeeTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GradeListData()
        {
            var ob = new HrGradeModel().GradeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

	}
}