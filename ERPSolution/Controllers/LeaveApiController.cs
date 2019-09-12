using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Controllers
{
    public class LeaveApiController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [ResponseType(typeof(LeaveModelApi))]
        public IHttpActionResult Get(string LOGIN_ID)
        {
            try
            {
                var obList = new LeaveModelApi().GetRequesterData(LOGIN_ID);
                return Ok(obList);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [ResponseType(typeof(LeaveModelApi))]
        public IHttpActionResult Get(string LOGIN_ID_ReqNoti, Int64? Option)
        {
            try
            {
                var obList = new LeaveModelApi().getReqNotiData(LOGIN_ID_ReqNoti);
                return Ok(obList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        //[ResponseType(typeof(HR_LEAVE_BALModel))]
        public IHttpActionResult Get(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID)
        {
            try
            {
                var obList = new HR_LEAVE_BALModel().LeaveBalance(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_EMPLOYEE_ID);
                return Ok(obList);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[System.Web.Http.Authorize]
        public IHttpActionResult Get(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID, DateTime FROM_DATE, DateTime TO_DATE, Int64? HR_EMPLOYEE_ID)
        {
            try
            {
                Int64 DateDiff = new LeaveModelApi().DateDiff(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, HR_LEAVE_TYPE_ID, FROM_DATE, TO_DATE, HR_EMPLOYEE_ID);
                return Ok(new { DateDiff = DateDiff });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //[System.Web.Http.Authorize]
        public IHttpActionResult Get(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, DateTime TO_DATE, Int64? HR_EMPLOYEE_ID)
        {
            try
            {
                DateTime NextJoinDate = new LeaveModelApi().findNextWorkingDay(HR_COMPANY_ID, RF_FISCAL_YEAR_ID, TO_DATE, HR_EMPLOYEE_ID);
                return Ok(new { NextJoinDate = NextJoinDate });

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //[System.Web.Http.Authorize]
        public IHttpActionResult Get(Int64 HR_LEAVE_TRANS_ID, Int64? SC_USER_ID)
        {
            try
            {
                var obj = new HR_LEAVE_TRANSModel().LeaveDataById(HR_LEAVE_TRANS_ID, null, SC_USER_ID);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[System.Web.Http.Authorize]
        [ResponseType(typeof(HR_LEAVE_TRANSModel))]
        public IHttpActionResult Post([FromBody]HR_LEAVE_TRANSModel ob, Int64 pOption)
        {
            try
            {
                if (ob == null)
                {
                    return BadRequest("Leave Data cannot be null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var obj = ob.saveDataForEleave(ob, pOption);

                if (obj == null)
                {
                    return Conflict();
                }

                Hub.Clients.All.OnlineLeaveApproverNotif();
                Hub.Clients.All.OnlineLeaveReqesterNotif();
                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.showNotification();
                Hub.Clients.All.getMessageData();

                return Created<HR_LEAVE_TRANSModel>("sdddd", obj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
