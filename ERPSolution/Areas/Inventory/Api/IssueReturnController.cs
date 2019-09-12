using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Inventory.Api
{
    [RoutePrefix("api/Inv")]
    public class IssueReturnController : ApiController
    {
        [Route("IssueReturn/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Inv/IssueReturn/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                var data = new SCM_STR_GEN_ISS_RTN_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pRF_REQ_TYPE_ID, pUSER_ID, pOption);
                int total = 0;
                if (data.Count > 0)
                    total = data[0].TOTAL_REC;
                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("IssueReturn/GetIssueReturnInfo")]
        [HttpGet]
        // GET :  /api/Inv/IssueReturn/GetIssueReturnInfo
        public IHttpActionResult GetIssueReturnInfo(Int64? pSCM_STR_GEN_ISS_RTN_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_RTN_HModel().Select(pSCM_STR_GEN_ISS_RTN_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("IssueReturn/GetIssueReturnDtlByID")]
        [HttpGet]
        // GET :  /api/Inv/IssueReturn/GetIssueReturnDtlByID
        public IHttpActionResult GetIssueReturnDtlByID(Int64? pSCM_STR_GEN_ISS_RTN_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_RTN_DModel().SelectByID(pSCM_STR_GEN_ISS_RTN_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("IssueReturn/Save")]
        [HttpPost]
        // GET :  api/Inv/IssueReturn/Save
        public IHttpActionResult Save([FromBody] SCM_STR_GEN_ISS_RTN_HModel ob)
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
