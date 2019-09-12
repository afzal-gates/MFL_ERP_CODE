using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Threading;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrTaProcessDataController : BaseController
    {
       
        //private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
           
       
        [HttpPost]
        //[MTAThread]
        [MyValidateAntiForgeryToken]
        public JsonResult AttendanceProcess(DateTime? pFromDate, DateTime? pToDate, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pHR_DAY_TYPE_ID, Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID)
        {
            string vMsg = "";

            try
            {               
                vMsg = new HR_TA_PROCESS_DATAModel().AttendanceProcess(pFromDate, pToDate, pHR_DEPARTMENT_ID,
                                                pHR_DESIGNATION_ID, pHR_SHIFT_TEAM_ID, pHR_EMPLOYEE_ID, pHR_MANAGEMENT_TYPE_ID, pLK_FLOOR_ID, pLINE_NO, pHR_DAY_TYPE_ID,
                                                pHR_COMPANY_ID, pHR_OFFICE_ID);
            }
            catch(Exception e)
            {
                vMsg = e.Message;
            }

            
            return Json(new { success = true, vMsg });
        }

        //public JsonResult AttenProcessProgressBar()
        //{
        //    //List<HR_LEAVE_TRANSModel> data = new List<HR_LEAVE_TRANSModel>();
        //    string data = obBLL.AttenProcessProgressBar();
        //    Hub.Clients.All.AttenProcessProgressBar();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

       
        
    }
}