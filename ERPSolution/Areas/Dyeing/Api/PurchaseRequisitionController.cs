using ERP.Model.Purchase;
using ERPSolution.Hubs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/dye")]
    [Authorize]
    [NoCache]
    public class PurchaseRequisitionController : ApiController
    {
        [Route("PurcReq/GetRequisitionList")]
        [HttpGet]
        // GET :  /api/dye/PurcReq/GetRequisitionList
        public IHttpActionResult GetRequisitionList()
        {
            try
            {
                var obList = new SCM_PURC_REQ_HModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }
        [Route("PurcReq/GetPurcReqInfo")]
        [HttpGet]
        // GET :  /api/dye/PurcReq/GetPurcReqInfo?pSCM_PURC_REQ_H_ID
        public IHttpActionResult GetPurcReqInfo(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var obList = new SCM_PURC_REQ_HModel().Select(pSCM_PURC_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("PurcReq/GetPurcReqDtlInfo")]
        [HttpGet]
        // GET :  /api/dye/PurcReq/GetPurcReqDtlInfo?pSCM_PURC_REQ_H_ID
        public IHttpActionResult GetPurcReqDtlInfo(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var obList = new SCM_PURC_REQ_DModel().SelectByReqID(pSCM_PURC_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("PurcReq/Save")]
        [HttpPost]
        // GET :  api/dye/PurcReq/SaveSupplierData
        public IHttpActionResult Save([FromBody] SCM_PURC_REQ_HModel ob)
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
