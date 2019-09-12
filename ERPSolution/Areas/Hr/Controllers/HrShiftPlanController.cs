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
    public class HrShiftPlanController : BaseController
    {                       
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HrShiftPlanModel ob)
        {
            string vMsg = "";
            var errors = new Hashtable();

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Update();
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
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
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HrShiftPlanModel ob)
        {
            string vMsg = "";
            var errors = new Hashtable();

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


        public JsonResult ShiftTypeListData()
        {
            var ob = new HrShiftTypeModel().ShiftTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        public JsonResult PeriodTypeListData()
        {            
            var ob = new HrPeriodTypeModel().PeriodTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DayListData()
        {
            var ob = new RfCalendarDayModel().DayListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectAll()
        {
            var ob = new HrShiftPlanModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ShiftTeam()
        {
            //int x = 65;
            //List<SelectListItem> letterList = new List<SelectListItem>();
            //letterList.Add(new SelectListItem { Text = "Select", Value = "" });
            //while (x <= 90)
            //{
            //    char c = (char)x;

            //    letterList.Add(new SelectListItem { Text = c.ToString(), Value = c.ToString() });
            //    x = x + 1;
            //}
            //ViewData["letterListData"] = letterList;


            return PartialView();
        }

        


	}


}