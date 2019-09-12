using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.IE.Api
{
    [RoutePrefix("api/ie")]
    [Authorize]
    public class GmtWashReqController : ApiController
    {
        [Route("GmtWashReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/ie/GmtWashReq/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pWASH_REQ_NO = null, string pWASH_REQ_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_ORDER_H_ID = null, Int64? pUSER_ID = null, string pMC_ORDER_H_ID_LST = null)
        {
            var data = new GMT_DF_WASH_REQModel().SelectAll(pageNo, pageSize, pWASH_REQ_NO, pWASH_REQ_DT, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID, pMC_ORDER_H_ID, pUSER_ID, pMC_ORDER_H_ID_LST);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }

        [Route("GmtWashReq/GetGmtWashReqInfoByID")]
        [HttpGet]
        // GET :  /api/ie/GmtWashReq/GetGmtWashReqInfoByID?pGMT_DF_WASH_REQ_ID
        public IHttpActionResult GetGmtWashReqInfoByID(Int64? pGMT_DF_WASH_REQ_ID = null)
        {
            try
            {
                var obList = new GMT_DF_WASH_REQModel().SelectByID(pGMT_DF_WASH_REQ_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtWashReq/Save")]
        [HttpPost]
        // GET :  /api/ie/GmtWashReq/Save
        public IHttpActionResult Save([FromBody] GMT_DF_WASH_REQModel ob)
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
