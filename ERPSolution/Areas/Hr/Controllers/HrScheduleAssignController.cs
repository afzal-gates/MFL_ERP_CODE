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
    public class HrScheduleAssignController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult ScheduleAssignByTeam()
        {
            return PartialView();
        }

        public PartialViewResult ScheduleAssignByPerson()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HrScheduleAssignModel ob)
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
        public JsonResult Update(HrScheduleAssignModel ob)
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

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Delete(HrScheduleAssignModel ob)
        {
            string vMsg = "";
            //bool vSuccess = false;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Delete();

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
        public JsonResult BatchSave4ScheduleAssignByTeam(HrScheduleAssignModel ob)
        {
            string jsonStr = "";
            var errors = new Hashtable();
            
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        vMsg = ob.BatchSave4ScheduleAssignByTeam();
            //    }
            //    catch(Exception e)
            //    {
            //        ModelState.AddModelError("", e.Message);
            //    }
            //}
            //else
            //{
                
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Json(new { success = false, errors });
            //}

            try
            {
                jsonStr = ob.BatchSave4ScheduleAssignByTeam();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Json(new { success = true, jsonStr });
                     
        }


        [MyValidateAntiForgeryToken]
        public JsonResult ScheduleWithPlanListData()
        {
            var ob = new HR_SCHEDULE_HModel().ScheduleWithPlanListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult ShiftTeamListData(Int64 pShiftPlanId)
        {
            var ob = new HrScheduleAssignModel().ShiftTeamListData(pShiftPlanId);            
            return Json(ob, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult ScheduleWiseAssign(Int64 shiftPlaneId)
        {
            var ob = new HrScheduleAssignModel().ScheduleWiseAssign(shiftPlaneId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult ScheduleWiseAssignByPerson(Int64 pHR_EMPLOYEE_ID)
        {
            var ob = new HrScheduleAssignModel().ScheduleWiseAssignByPerson(pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ScheduleListData(Int64 schedulePlanId)
        {
            var ob = new HR_SCHEDULE_HModel().ScheduleListData(schedulePlanId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult PersonScheduleAssignListData(Int64 pHR_EMPLOYEE_ID)
        {
            var ob = new HrScheduleAssignModel().PersonScheduleAssignListData(pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }



    }
}