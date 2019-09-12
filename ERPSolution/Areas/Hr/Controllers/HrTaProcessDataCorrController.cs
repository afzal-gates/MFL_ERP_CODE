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
    public class HrTaProcessDataCorrController : BaseController
    {
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public JsonResult MonthListData()
        {
            var obList = new RF_CAL_MONTHModel().MonthListData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }
        //[MyValidateAntiForgeryToken]
        public JsonResult LoadTaData(ATTN_CORR_VM ob)
        {
            var obList = new ATTN_CORR_DATA_VM().LoadTaData(ob);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UupdateAttnCorrData(List<ATTN_CORR_DATA_VM> obList)
        {
            string vMsg = "";
            vMsg = new ATTN_CORR_DATA_VM().UupdateAttnCorrData(obList);
            return Json(new { success = true, vMsg });
        }
        [HttpPost]
        public JsonResult saveBatchData(List<ATTN_CORR_DATA_VM> obList)
        {
            string vMsg = "";

            vMsg = new ATTN_CORR_DATA_VM().saveBatchData(obList);

            return Json(new { success = true, vMsg });
        }
        public PartialViewResult HrTaProcessDataCorr()
        {
            return PartialView();
        }
        public PartialViewResult HrTaProcessDataCorrDtl()
        {
            return PartialView();
        }
    }
}