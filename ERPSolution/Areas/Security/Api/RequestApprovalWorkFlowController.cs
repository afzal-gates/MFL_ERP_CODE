using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Security.Api
{
    [RoutePrefix("api/security")]
    public class RequestApprovalWorkFlowController : ApiController
    {
        [Route("RequestApprovalWorkFlow/ActionTypeAll")]
        [HttpGet]
        // GET :  /api/security/RequestApprovalWorkFlow/ActionTypeAll
        public IHttpActionResult ActionTypeAll()
        {
            try
            {
                var data = new RF_ACTN_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("RequestApprovalWorkFlow/GetActionStatusByID")]
        [HttpGet]
        // GET :  /api/security/RequestApprovalWorkFlow/GetActionStatusByID
        public IHttpActionResult GetActionStatusByID(Int64? pRF_ACTN_TYPE_ID = null)
        {
            try
            {
                var list = new RF_ACTN_STATUSModel().Select(pRF_ACTN_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("RequestApprovalWorkFlow/SaveAT")]
        [HttpPost]
        // GET :  /api/security/RequestApprovalWorkFlow/SaveAT
        public IHttpActionResult SaveAT([FromBody] RF_ACTN_TYPEModel ob)
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

        [Route("RequestApprovalWorkFlow/DeleteAT")]
        [HttpPost]
        // GET :  /api/security/RequestApprovalWorkFlow/DeleteAT
        public IHttpActionResult DeleteAT([FromBody] RF_ACTN_TYPEModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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

        [Route("RequestApprovalWorkFlow/SaveAS")]
        [HttpPost]
        // GET :  /api/security/RequestApprovalWorkFlow/SaveAS
        public IHttpActionResult SaveAS([FromBody] RF_ACTN_STATUSModel ob)
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
