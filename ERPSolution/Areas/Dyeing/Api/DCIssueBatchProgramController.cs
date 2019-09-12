using ERP.Model;
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
    [NoCache]
    public class DCIssueBatchProgramController : ApiController
    {
        [Route("DCIssueBatchProgram/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueBatchProgram/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null)
        {
            try
            {
                var data = new DYE_STR_REQ_HModel().SelectAllBatch(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, "REQUISITION FOR BATCH PRODUCTION", pUSER_NAME_EN, pEVENT_NAME);
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


        [Route("DCIssueBatchProgram/GetDCBatchProgramIssueInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueBatchProgram/GetDCBatchProgramIssueInfo
        public IHttpActionResult GetDCBatchProgramIssueInfo(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_ISS_HModel().SelectByReqID(pDYE_STR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssueBatchProgram/GetDCIssueBatchProgramInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueBatchProgram/GetDCIssueBatchProgramInfo
        public IHttpActionResult GetDCIssueBatchProgramInfo(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D1Model().Select(pDYE_STR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssueBatchProgram/GetDCIssueBatchProgramInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueBatchProgram/GetDCIssueBatchProgramInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCIssueBatchProgramInfoDtlByID(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D2Model().Select(pDYE_STR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssueBatchProgram/Save")]
        [HttpPost]
        // GET :  api/Dye/DCIssueBatchProgram/Save
        public IHttpActionResult Save([FromBody] DYE_BT_DC_ISS_HModel ob)
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

        [Route("DCIssueBatchProgram/UpdateRequisition")]
        [HttpPost]
        // GET :  api/Dye/DCIssueBatchProgram/UpdateRequisition
        public IHttpActionResult UpdateRequisition([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.UpdateRequisition();
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


        [Route("DCIssueBatchProgram/DeleteItem")]
        [HttpPost]
        // GET :  api/Dye/DCIssueBatchProgram/DeleteItem
        public IHttpActionResult DeleteItem([FromBody] DYE_BT_DC_ISS_DModel ob)
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

    }
}
