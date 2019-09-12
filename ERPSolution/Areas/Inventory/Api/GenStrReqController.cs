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
    public class GenStrReqController : ApiController
    {

        [Route("GenStrReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                var data = new SCM_STR_GEN_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pRF_REQ_TYPE_ID, pUSER_ID, pOption);
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


        [Route("GenStrReq/GetGenStrReqInfo")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrReqInfo
        public IHttpActionResult GetGenStrReqInfo(Int64? pSCM_STR_GEN_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_REQ_HModel().Select(pSCM_STR_GEN_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GenStrReq/GetGenStrReqDtlByID")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrReqDtlByID
        public IHttpActionResult GetGenStrReqDtlByID(Int64? pSCM_STR_GEN_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_REQ_DModel().SelectByID(pSCM_STR_GEN_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GenStrReq/GetGenStrIssueInfoByReqID")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrIssueInfoByReqID?pSCM_STR_GEN_REQ_H_ID
        public IHttpActionResult GetGenStrIssueInfoByReqID(Int64? pSCM_STR_GEN_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_HModel().SelectByReqID(pSCM_STR_GEN_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GenStrReq/GetGenStrIssueInfoByID")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrIssueInfoByID?pSCM_STR_GEN_REQ_H_ID
        public IHttpActionResult GetGenStrIssueInfoByID(Int64? pSCM_STR_GEN_ISS_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_HModel().Select(pSCM_STR_GEN_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GenStrReq/GetGenStrIssueDtlByID")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrIssueDtlByID?pSCM_STR_GEN_ISS_H_ID
        public IHttpActionResult GetGenStrIssueDtlByID(Int64? pSCM_STR_GEN_ISS_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_DModel().SelectByID(pSCM_STR_GEN_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GenStrReq/GetGenStrIssueDtlForRtn")]
        [HttpGet]
        // GET :  /api/Inv/GenStrReq/GetGenStrIssueDtlForRtn?pSTR_REQ_NO=&pISS_REF_NO=
        public IHttpActionResult GetGenStrIssueDtlForRtn(string pSTR_REQ_NO = null, string pISS_REF_NO = null)
        {
            try
            {
                var list = new SCM_STR_GEN_ISS_DModel().GetGenStrIssueDtlForRtn(pSTR_REQ_NO, pISS_REF_NO);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GenStrReq/Save")]
        [HttpPost]
        // GET :  api/Inv/GenStrReq/Save
        public IHttpActionResult Save([FromBody] SCM_STR_GEN_REQ_HModel ob)
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


        [Route("GenStrReq/SaveIssue")]
        [HttpPost]
        // GET :  api/Inv/GenStrReq/SaveIssue
        public IHttpActionResult SaveIssue([FromBody] SCM_STR_GEN_ISS_HModel ob)
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
