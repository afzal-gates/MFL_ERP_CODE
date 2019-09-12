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
    public class HrScheduleController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_SCHEDULE_HModel ob)
        {
            string vMsg = "";
            //bool vSuccess = false;

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


        //[HttpPost]        
        //[MyValidateAntiForgeryToken]
        //public JsonResult Save(HR_SCHEDULE_HModel ob)
        //{
        //    string vMsg = "";
        //    //bool vSuccess = false;

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            vMsg = obBLL.Save(ob);
                    
        //        }
        //        catch(Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Json(new { success = false, errors });
        //    }

        //    return Json(new { success = true, vMsg });

        //    //return Json(new
        //    //{
        //    //    success = false,
        //    //    errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
        //    //                    .Select(m => m.ErrorMessage).ToArray()
        //    //});           
        //}


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult DataSave(HR_SCHEDULE_HModel ob)
        {
            string vMsg = "";
            object objReturn = new { };
            //bool vSuccess = false;

            if (ModelState.IsValid)
            {
                try
                {
                    objReturn = ob.DataSave();

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

            return Json(new { success = true, objReturn });

            //return Json(new
            //{
            //    success = false,
            //    errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
            //                    .Select(m => m.ErrorMessage).ToArray()
            //});           
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult DataBatchSave(HrScheduleD01Model obWeek, List<HrScheduleD011Model> obDay, List<HrScheduleD012Model> obClock)
        {
            //object obWeek, object obDay, object obClock
            string vMsg = "";
            object objReturn = new { };
            //bool vSuccess = false;

            foreach(var item in obDay)
            {
                bool ob = new RfCalendarDayModel().ScheduleDayData(obWeek.HR_SCHEDULE_H_ID, null).Any(m => m.CALENDAR_DAY_NAME_EN == item.CALENDAR_DAY_NAME_EN 
                                                                                                        && m.HR_SCHEDULE_D01_ID!=null
                                                                                                        && m.HR_SCHEDULE_D01_ID!=obWeek.HR_SCHEDULE_D01_ID
                                                                                                        && m.IS_ACTIVE=="Y" && item.IS_ACTIVE=="Y");

                if (ob == true)
                {
                    ModelState.AddModelError("", item.CALENDAR_DAY_NAME_EN + " already asign in another week group of same schedule pattern");
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                                        .Select(m => m.ErrorMessage).ToArray()
                    });
                }
            }

            objReturn = obWeek.DataBatchSave(obDay, obClock);

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        objReturn = obBLL.DataSave(ob);

            //    }
            //    catch (Exception e)
            //    {
            //        ModelState.AddModelError("", e.Message);
            //    }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Json(new { success = false, errors });
            //}

            return Json(new { success = true, objReturn });

            //return Json(new
            //{
            //    success = false,
            //    errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
            //                    .Select(m => m.ErrorMessage).ToArray()
            //});           
        }


        //[HttpPost]
        //public JsonResult Save(HR_SCHEDULE_HModel ob)
        //{
        //    string vMsg = "";

        //    if (ModelState.IsValid)
        //        vMsg = obBLL.Save(ob);

        //    return vMsg;
        //}

        //public JsonResult CompanyGroupListData()
        //{
        //    List<HR_SCHEDULE_HModel> ob = new List<HR_SCHEDULE_HModel>();
        //    ob = obCommonBLL.CompanyGroupListData();

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //} 

        //public JsonResult LookupListData()
        //{            
        //    Int64 lookupTableId = Convert.ToInt64(HttpContext.Request.Params["pLookupTableId"]);

        //    List<LookupDataModel> ob = new List<LookupDataModel>();
        //    ob = obCommonBLL.LookupListData(lookupTableId);

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult ScheduleListData()
        {
            var ob = new HR_SCHEDULE_HModel().ScheduleListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ScheduleClockData(Int64 scheduleD01Id)
        {
            var obList = new HrScheduleD012Model().ScheduleClockData(scheduleD01Id);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ScheduleDayData(Int64 scheduleId, Int64 scheduleD01Id)
        {
            var obList = new RfCalendarDayModel().ScheduleDayData(scheduleId, scheduleD01Id);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult WeekGroupData(long pHrScheduleD01Id)
        {
            var ob = new HrScheduleD01Model().Select(pHrScheduleD01Id);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        
        public PartialViewResult ScheduleEntry()
        {
            return PartialView();
        }

        public PartialViewResult ScheduleList()
        {
            return PartialView();
        }

        public PartialViewResult WeekGroup()
        {
            return PartialView();
        }

        public PartialViewResult ScheduleClock()
        {
            return PartialView();
        }
    }
}