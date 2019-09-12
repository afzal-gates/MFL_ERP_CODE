using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/Dye")]
    [NoCache]
    public class DyeMcMaintenanceController : ApiController
    {

        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("DyeMcMaintenance/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DyeMcMaintenance/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize)
        {
            try
            {
                var data = new DYE_MCN_STOP_TRANModel().SelectAll(pageNo, pageSize);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeMcMaintenance/DyeMcStopTranGetByID")]
        [HttpGet]
        // GET :  /api/Dye/DyeMcMaintenance/DyeMcStopTranGetByID
        public IHttpActionResult DyeMcStopTranGetByID(Int64 pDYE_MCN_STOP_TRAN_ID)
        {
            try
            {
                var list = new DYE_MCN_STOP_TRANModel().Select(pDYE_MCN_STOP_TRAN_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeMcMaintenance/DyeMcDefectTypeList")]
        [HttpGet]
        // GET :  /api/Dye/DyeMcMaintenance/DyeMcDefectTypeList
        public IHttpActionResult DyeMcDefectTypeList(string pRF_RESP_DEPT_LST = null)
        {
            try
            {
                var list = new RF_MCN_DFCT_TYPEModel().SelectAll(pRF_RESP_DEPT_LST);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeMcMaintenance/Save")]
        [HttpPost]
        // GET :  api/Dye/DyeMcMaintenance/Save
        public IHttpActionResult Save([FromBody] DYE_MCN_STOP_TRANModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Save();
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
                return Ok(new { success = false, errors });
            }

            Hub.Clients.All.dyeMcMaintenanceList();
            return Ok(new { success = true, jsonStr });
        }
    }
}
