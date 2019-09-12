using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    [System.Web.Http.Authorize]
    public class KntMcnNeedleAssignController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("McNeedleAssign/GetNeedleAssignList")]
        [HttpGet]
        // GET :  /api/knit/McNeedleAssign/GetNeedleAssignList
        public IHttpActionResult GetNeedleAssignList(Int64 pHR_OFFICE_ID, Int64 pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_ASSIGNModel().GetNeedleAssignList(pHR_OFFICE_ID, pINV_ITEM_CAT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("McNeedleAssign/BatchSave")]
        [HttpPost]
        // GET :  api/knit/McNeedleAssign/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_MCN_NDL_ASSIGNModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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
            return Ok(new { success = true, jsonStr });
        }

       


    }
}
