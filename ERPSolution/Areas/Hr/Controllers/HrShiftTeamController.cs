using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrShiftTeamController : BaseController
    {        
        // GET: /Hr/HrOffice/
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        [HttpPost]
        public string Update(HrShiftTeamModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
                vMsg = ob.Update();

            return vMsg;
        }

        [HttpPost]
        public string Save(HrShiftTeamModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
                vMsg = ob.Save();

            return vMsg;
        }


        //public JsonResult ShiftTypeListData()
        //{
        //    List<HrShiftTypeModel> ob = new List<HrShiftTypeModel>();
        //    ob = obBLL.ShiftTypeListData();

        //    return Json(ob, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult SelectAll()
        {
            var ob = new HrShiftTeamModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShiftTeamListData(long pHR_SHIFT_PLAN_ID)
        {
            var ob = new HrShiftTeamModel().ShiftTeamListData(pHR_SHIFT_PLAN_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


	}


}