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
    public class KntMcOilController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("Req4SubStr/GetReqList")]
        [HttpGet]
        // GET :  /api/knit/Req4SubStr/GetReqList
        public IHttpActionResult GetReqList(Int64 pageNumber, Int64 pageSize)
        {
            try
            {
                var obList = new SCM_STR_OIL_REQ_HModel().GetReqList(pageNumber, pageSize);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("Req4SubStr/GetItemStockByID")]
        [HttpGet]
        // GET :  /api/knit/Req4SubStr/GetItemStockByID
        public IHttpActionResult GetItemStockByID(Int64 pISS_STORE_ID, Int64 pRCV_STORE_ID, Int64 pINV_ITEM_ID)
        {
            try
            {
                var obList = new SCM_STR_OIL_REQ_HModel().GetItemStockByID(pISS_STORE_ID, pRCV_STORE_ID, pINV_ITEM_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("Req4SubStr/GetReqByHdrID")]
        [HttpGet]
        // GET :  /api/knit/Req4SubStr/GetReqByHdrID
        public IHttpActionResult GetReqByHdrID(Int64 pSCM_STR_OIL_REQ_H_ID)
        {
            try
            {
                var obList = new SCM_STR_OIL_REQ_HModel().GetReqByHdrID(pSCM_STR_OIL_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("Req4SubStr/GetReqDtlByID")]
        [HttpGet]
        // GET :  /api/knit/Req4SubStr/GetReqDtlByID
        public IHttpActionResult GetReqDtlByID(Int64 pSCM_STR_OIL_REQ_H_ID)
        {
            try
            {
                var obList = new SCM_STR_OIL_REQ_DModel().GetReqDtlByID(pSCM_STR_OIL_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("Req4SubStr/BatchSave")]
        [HttpPost]
        // GET :  api/knit/Req4SubStr/BatchSave
        public IHttpActionResult BatchSave([FromBody] SCM_STR_OIL_REQ_HModel ob)
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

        [Route("Req4SubStr/SubmitRequisition")]
        [HttpPost]
        // GET :  api/knit/Req4SubStr/SubmitRequisition
        public IHttpActionResult SubmitRequisition([FromBody] SCM_STR_OIL_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SubmitRequisition();
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

            Hub.Clients.All.broadcastMcOilReq4SubStrList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("Req4SubStr/IssueRequisition")]
        [HttpPost]
        // GET :  api/knit/Req4SubStr/IssueRequisition
        public IHttpActionResult IssueRequisition([FromBody] SCM_STR_OIL_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IssueRequisition();
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
            Hub.Clients.All.broadcastMcOilReq4SubStrList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("McOil/GetOpeningBlanceList")]
        [HttpGet]
        // GET :  /api/knit/McOil/GetOpeningBlanceList
        public IHttpActionResult GetOpeningBlanceList(Int64 pageNumber, Int64 pageSize,Int64? pHR_COMPANY_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var list = new KNT_MCN_OIL_OBModel().SelectAll(pageNumber, pageSize, pHR_COMPANY_ID, pSCM_STORE_ID);
                var data = list.ToList();
                int total = 0;
                if (list.Count() > 0)
                    total = list[0].TOTAL_REC;

                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McOil/SaveOB")]
        [HttpPost]
        // GET :  api/knit/McOil/SaveOB
        public IHttpActionResult SaveOB([FromBody] KNT_MCN_OIL_OBModel ob)
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

            return Ok(new { success = true, jsonStr });
        }

    }
}
