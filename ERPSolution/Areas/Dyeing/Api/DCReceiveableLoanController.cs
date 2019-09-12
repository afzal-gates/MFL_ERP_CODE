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
    public class DCReceiveableLoanController : ApiController
    {
        [Route("DCReceiveableLoan/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCReceiveableLoan/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pRET_CHALAN_NO = null, string pRET_CHALAN_DT = null, string pCOMP_NAME_EN = null,
            string pSTORE_NAME_EN = null, string pSUP_TRD_NAME_EN = null, string pACTN_STATUS_NAME = null, string pITEM_RECV_BY_NAME = null)
        {
            try
            {
                var data = new DYE_DC_LRT_HModel().SelectAll(pageNo, pageSize, pRET_CHALAN_NO, pRET_CHALAN_DT, pCOMP_NAME_EN, pSTORE_NAME_EN, pSUP_TRD_NAME_EN, pACTN_STATUS_NAME, pITEM_RECV_BY_NAME);
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

        [Route("DCReceiveableLoan/GetDCReceiveableLoanInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCReceiveableLoan/GetDCReceiveableLoanInfo
        public IHttpActionResult GetDCReceiveableLoanInfo(Int64? pDYE_DC_LRT_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_LRT_HModel().Select(pDYE_DC_LRT_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCReceiveableLoan/GetDCReceiveableLoanInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCReceiveableLoan/GetDCReceiveableLoanInfoDtlByID?pDYE_DC_LRT_H_ID
        public IHttpActionResult GetDCReceiveableLoanInfoDtlByID(Int64? pDYE_DC_LRT_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_LRT_DModel().Select(pDYE_DC_LRT_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCReceiveableLoan/Save")]
        [HttpPost]
        // GET :  api/Dye/DCReceiveableLoan/Save
        public IHttpActionResult Save([FromBody] DYE_DC_LRT_HModel ob)
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
