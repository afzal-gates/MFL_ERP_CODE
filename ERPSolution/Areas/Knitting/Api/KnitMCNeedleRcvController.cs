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
    public class KnitMCNeedleRcvController : ApiController
    {
        [Route("KnitMcNeedleRcv/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/KnitMcNeedleRcv/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pMRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            try
            {
                var data = new KNT_NDL_STR_RCV_HModel().SelectAll(pageNo, pageSize, pMRR_NO, pMRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pREMARKS);
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

        [Route("KnitMcNeedleRcv/GetKnitMcNeedleRcvInfo")]
        [HttpGet]
        // GET :  /api/knit/KnitMcNeedleRcv/GetKnitMcNeedleRcvInfo
        public IHttpActionResult GetKnitMcNeedleRcvInfo(Int64? pKNT_NDL_STR_RCV_H_ID = null)
        {
            try
            {
                var list = new KNT_NDL_STR_RCV_HModel().Select(pKNT_NDL_STR_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }        

        [Route("KnitMcNeedleRcv/GetKnitMcNeedleRcvInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitMcNeedleRcv/GetKnitMcNeedleRcvInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetKnitMcNeedleRcvInfoDtlByID(Int64? pKNT_NDL_STR_RCV_H_ID = null)
        {
            try
            {
                var list = new KNT_NDL_STR_RCV_DModel().SelectByID(pKNT_NDL_STR_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitMcNeedleRcv/Save")]
        [HttpPost]
        // GET :  api/knit/KnitMcNeedleRcv/Save
        public IHttpActionResult Save([FromBody] KNT_NDL_STR_RCV_HModel ob)
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
