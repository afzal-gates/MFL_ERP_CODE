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
    [RoutePrefix("api/Dye")]
    [NoCache]
    public class AopBatchController : ApiController
    {
        [Route("AopBatch/SelectAopBatchList/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/AopBatch/SelectAopBatchList
        public IHttpActionResult SelectAopBatchList(int pageNo, int pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, 
            Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null, string pDYE_BATCH_NO = null)
        {
            try
            {
                var data = new DYE_BT_CARD_HModel().SelectAopBatchList(pageNo, pageSize, pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID, pDYE_BATCH_NO);
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


        [Route("AopBatch/GetAopBatchInfo")]
        [HttpGet]
        // GET :  /api/Dye/AopBatch/GetAopBatchInfo
        public IHttpActionResult GetAopBatchInfo(Int64? pDYE_BT_CARD_H_ID = null)
        {
            try
            {
                var list = new DF_AOP_BATCH_FABModel().SelectByID(pDYE_BT_CARD_H_ID)[0];
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AopBatch/GetAopBatchFabrics")]
        [HttpGet]
        // GET :  /api/Dye/AopBatch/GetAopBatchFabrics
        public IHttpActionResult GetAopBatchFabrics(Int64? pDYE_BT_CARD_H_ID = null)
        {
            try
            {
                var list = new DF_AOP_BATCH_FABModel().SelectByID(pDYE_BT_CARD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("AopBatch/BatchSave")]
        [HttpPost]
        // GET :  api/Dye/AopBatch/BatchSave
        public IHttpActionResult BatchSave([FromBody] DF_AOP_BATCH_FABModel ob)
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
