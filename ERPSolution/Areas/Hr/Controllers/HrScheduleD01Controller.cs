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
    [MyValidateAntiForgeryToken]
    public class HrScheduleD01Controller : BaseController
    {        
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }


        [HttpPost]
        public void Update(HrScheduleD01Model ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
            {
                vMsg = ob.Update();
                //return Json(new { success = true, vMsg });
            }
            
        }

        

        [HttpPost]
        //[AllowAnonymous]        
        public JsonResult Save(HrScheduleD01Model ob)
        {
            string vMsg = "";
            //bool vSuccess = false;
            
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Save();
                    return Json(new { success = true, vMsg, ob });
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return Json(new { success = false, vMsg, ob });
            
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

        public JsonResult ScheduleWiseWeekGrpListData(Int64 ScheduleId)
        {            
            var ob = new HrScheduleD01Model().ScheduleWiseWeekGrpListData(ScheduleId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        
    }
}