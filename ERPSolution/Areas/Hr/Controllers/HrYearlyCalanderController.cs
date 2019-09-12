using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrYearlyCalanderController : BaseController
    {
        //public ActionResult 

        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult YearlyCalendarView()
        {
            return PartialView();
        }

        public PartialViewResult EditCalenderDate()
        {
            return PartialView();
        }

        public PartialViewResult YearlyCalendarParamView()
        {
            return PartialView();
        }
    
        [HttpPut]
        public void  Update(HrYrlyCalndrModel ob)
        {
            //ob.HR_COMPANY_ID = obCommonBLL.CompanyID();
            //ob.RF_FISCAL_YEAR_ID = obCommonBLL.FiscalYearID();

            ob.Update();
        }

        //[HttpPost]
        public JsonResult Save(HrYrlyCalndrModel ob)
        {
            var ob1 = HttpContext.Request.Params["dtpFromDate"];

            if (ModelState.IsValid)
            {
                Session["vMsg"] = ob.Update();
                //return RedirectToAction("HolidayCalendar");
            }

            return Json(ob1,JsonRequestBehavior.AllowGet);
        }

        public JsonResult DayTypeListData()
        {
            var ob = new HrDayTypeModel().DayTypeList();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HolidayListData()
        {
            var ob = new HrHolidayModel().HolidayList();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult CompanyListData()
        //{
        //    List<HrCompanyModel> objData = new List<HrCompanyModel>();
        //    objData = obBLL.CompanyList();

        //    return Json(objData, JsonRequestBehavior.AllowGet);
        //}

        //
        // GET: /Admin/HrYearlyCalander/


        public JsonResult HolidayCalendarData(Int64 pHR_COMPANY_ID, Int64 pRF_FISCAL_YEAR_ID)
        {
            var objData = new ViewCalendarModel().SelectViewCalender(pHR_COMPANY_ID, pRF_FISCAL_YEAR_ID);
            return Json(objData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HolidayCalendarListData()
        {
            DateTime showMonth = Convert.ToDateTime(HttpContext.Request.Params["pShowMonth"]);

            List<HrYrlyCalndrModel> objData = new List<HrYrlyCalndrModel>();
            if (showMonth != null)
            {
                objData = new HrYrlyCalndrModel().SelectYrCalender(showMonth);
            }
            return Json(objData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult HolidayCalendarProcess(Int64 pHR_COMPANY_ID, Int64 pRF_FISCAL_YEAR_ID)
        {
            string vMsg = "";
            vMsg = new HrYrlyCalndrModel().HolidayCalendarProcess(pHR_COMPANY_ID, pRF_FISCAL_YEAR_ID);
            return Json(new { success = true, vMsg });
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult UpdateDateRange(HrYrlyCalndrModel ob)
        {
            string vMsg = "";
            var errors = new Hashtable();
            
            if (ob.HR_DAY_TYPE_ID == 3 && ob.IS_GOVT_HOLIDAY == "Y" && ob.HR_HOLIDAY_ID == null)
            {
                ModelState.AddModelError("HR_HOLIDAY_ID", "Please select holiday, it's a govt. holiday also");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.UpdateDateRange();
                }
                catch (Exception e)
                {
                    vMsg = e.Message;
                }
            }
            else
            {
                
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                var x =new  {ErrorMessage="hell"};
                //x = new { ErrorMessage = "te" };
                
                //errors["HR_HOLIDAY_ID"] = x;
                //errors["HR_HOLIDAY_ID"] = "test";

                return Json(new { success = false, errors });
            }

            return Json(new { success = true, vMsg });
            
        }
        
	}
}