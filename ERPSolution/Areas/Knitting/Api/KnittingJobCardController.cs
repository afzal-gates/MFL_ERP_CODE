using ERP.Model;
using ERPSolution.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class KnittingJobCardController : ApiController
    {
        [Route("KnittingJobCard/JobCardDB/{pageNo}/{pageSize}")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnittingJobCard/JobCardDB/{pageNo}/{pageSize}
        public IHttpActionResult JobCardDB(
            int pageNo, 
            int pageSize,
            Int64? pMC_BYR_ACC_ID = null,
            Int64? pMC_BUYER_ID = null, 
            Int64? pMC_STYLE_H_EXT_ID = null,
            string pBUYER_NAME_EN = null,
            string pSTYLE_NO = null,
            string pORDER_NO = null,
            string pCOLOR_NAME_EN = null,
            string pFAB_TYPE_NAME = null
        )
        {
            try
            {
                var data = new KNT_PLAN_HModel().JobCardDB(pageNo, pageSize, pMC_BYR_ACC_ID, pMC_BUYER_ID, pMC_STYLE_H_EXT_ID, pBUYER_NAME_EN, pSTYLE_NO, pORDER_NO, pCOLOR_NAME_EN, pFAB_TYPE_NAME); ;
                int total = 0;
                if (data.Count>0)
                {
                    total = Convert.ToInt32(data.FirstOrDefault().TOTAL_REC.ToString());
                }

                //return Json Ok(,);
                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}
