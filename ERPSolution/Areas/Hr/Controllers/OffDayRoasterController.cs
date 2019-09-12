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
    public class OffDayRoasterController : BaseController
    {
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }


        public JsonResult ParentDeptListData()
        {

            var ob = new HrDepartmentModel().ParentDeptListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeptListData(Int64 pPARENT_ID)
        {
            var ob = new HrDepartmentModel().DeptListData(pPARENT_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getDeptByCompanyOffice(Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID)
        {
            var ob = new HrDepartmentModel().getDeptByCompanyOffice(pHR_COMPANY_ID, pHR_OFFICE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getEmployeeDataByDept(Int64? HR_COMPANY_ID, Int64? HR_DEPARTMENT_ID, Int64? HR_EMPLOYEE_ID)
        {
            var objList = new HR_EMP_OFF_DAYModel().getEmployeeDataByDept(HR_COMPANY_ID, HR_DEPARTMENT_ID, HR_EMPLOYEE_ID);
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Submit(List<HR_EMP_OFF_DAYModel> obList,Int64 pOption)
        {
            string vMsg = "";
            try
            {
                vMsg = new HR_EMP_OFF_DAYModel().Submit(obList, pOption);

            }
            catch (Exception ex)
            {
                throw ex;

            }
     


            return Json(new { success = true, vMsg });

        }

        public PartialViewResult OffDayRoaster()
        {
            return PartialView();
        }


        public PartialViewResult OffDayRoasterData()
        {
            return PartialView();
        }

	}
}