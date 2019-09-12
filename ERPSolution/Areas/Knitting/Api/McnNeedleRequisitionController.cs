using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class McnNeedleRequisitionController : ApiController
    {
        [Route("McnNeedleReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/SelectAll
        //, string pMRR_NO = null, string pMRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pOption = null)
        {
            try
            {
                var data = new SCM_STR_NDL_REQ_HModel().SelectAll(pageNo, pageSize, pOption);
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
        [Route("McnNeedleReq/GetNeedlReqReason")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedlReqReason
        public IHttpActionResult GetNeedlReqReason()
        {
            try
            {
                var list = new RF_NDL_REQ_TYPEModel().SelectAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("McnNeedleReq/GetStockInfo")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetStockInfo
        public IHttpActionResult GetStockInfo(Int64? pINV_ITEM_ID = null, Int64? pFRM_REQ_STR_ID = null, Int64? pTO_REQ_STR_ID = null, Int64? pRF_NDL_REQ_TYPE_ID = null, Int64? pKNT_MACHINE_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_REQ_DModel().SelectStock(pINV_ITEM_ID, pFRM_REQ_STR_ID, pTO_REQ_STR_ID, pRF_NDL_REQ_TYPE_ID, pKNT_MACHINE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McnNeedleReq/GetPendingBrokenItem")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetPendingBrokenItem
        public IHttpActionResult GetPendingBrokenItem(Int64? pFRM_REQ_STR_ID = null, Int64? pTO_REQ_STR_ID = null, Int64? pRF_NDL_REQ_TYPE_ID = null, Int64? pKNT_MC_TYPE_ID = null, DateTime? pFROM_DATE = null, DateTime? pTO_DATE = null)
        {
            try
            {
                var list = new SCM_STR_NDL_REQ_DModel().SelectPendingBrokenItem(pFRM_REQ_STR_ID, pTO_REQ_STR_ID, pRF_NDL_REQ_TYPE_ID, pKNT_MC_TYPE_ID, pFROM_DATE, pTO_DATE);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("McnNeedleReq/GetNeedleReqInfo")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedleReqInfo
        public IHttpActionResult GetNeedleReqInfo(Int64? pSCM_STR_NDL_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_REQ_HModel().Select(pSCM_STR_NDL_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("McnNeedleReq/GetNeedlReqDtl")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedlReqDtl
        public IHttpActionResult GetNeedlReqDtl(Int64? pSCM_STR_NDL_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var list = new SCM_STR_NDL_REQ_DModel().SelectAll(pSCM_STR_NDL_REQ_H_ID, pOption);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McnNeedleReq/GetNeedlReqDtlList")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedlReqDtlList
        public IHttpActionResult GetNeedlReqDtlList(Int64? pSCM_STR_NDL_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_REQ_D_LSTModel().SelectByID(pSCM_STR_NDL_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        

        [Route("McnNeedleReq/Save")]
        [HttpPost]
        // GET :  api/knit/McnNeedleReq/Save
        public IHttpActionResult Save([FromBody] SCM_STR_NDL_REQ_HModel ob)
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


        [Route("McnNeedleReq/GetNeedleIssueByID")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedleIssueByID
        public IHttpActionResult GetNeedleIssueByID(Int64? pSCM_STR_NDL_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_ISS_HModel().SelectAll(pSCM_STR_NDL_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("McnNeedleReq/GetNeedleIssueInfo")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedleIssueInfo?pSCM_STR_NDL_ISS_H_ID
        public IHttpActionResult GetNeedleIssueInfo(Int64? pSCM_STR_NDL_ISS_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_ISS_HModel().Select(pSCM_STR_NDL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("McnNeedleReq/GetNeedleIssueDtl")]
        [HttpGet]
        // GET :  /api/knit/McnNeedleReq/GetNeedleIssueDtl?pSCM_STR_NDL_ISS_H_ID
        public IHttpActionResult GetNeedleIssueDtl(Int64? pSCM_STR_NDL_ISS_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_NDL_ISS_DModel().SelectAll(pSCM_STR_NDL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McnNeedleReq/Issue")]
        [HttpPost]
        // GET :  api/knit/McnNeedleReq/Issue
        public IHttpActionResult Issue([FromBody] SCM_STR_NDL_ISS_HModel ob)
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


        [Route("McnNeedleReq/Back2Req")]
        [HttpPost]
        // GET :  api/knit/McnNeedleReq/Back2Req
        public IHttpActionResult Back2Req([FromBody] SCM_STR_NDL_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Back2Req();
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


        [Route("McnNeedleReq/Close")]
        [HttpPost]
        // GET :  api/knit/McnNeedleReq/Close
        public IHttpActionResult Close([FromBody] SCM_STR_NDL_ISS_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Close();
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
