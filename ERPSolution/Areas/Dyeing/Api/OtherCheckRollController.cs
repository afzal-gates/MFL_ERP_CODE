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
    public class OtherCheckRollController : ApiController
    {

        [Route("OtherCheckRoll/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/OtherCheckRoll/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pDYE_BATCH_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                var data = new DF_RIB_SHADE_RPT_HModel().SelectAll(pageNo, pageSize, pDYE_BATCH_NO, pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pMC_COLOR_ID);
                //return Ok(data);
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


        [Route("OtherCheckRoll/SelectForRibRequisition")]
        [HttpGet]
        // GET :  /api/Dye/OtherCheckRoll/SelectForRibRequisition
        public IHttpActionResult SelectForRibRequisition(string pDYE_BATCH_NO = null)
        {
            try
            {
                var data = new DF_RIB_SHADE_RPT_HModel().SelectForRibRequisition(pDYE_BATCH_NO);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("OtherCheckRoll/GetRibShadRptByID")]
        [HttpGet]
        // GET :  /api/Dye/OtherCheckRoll/GetRibShadRptByID
        public IHttpActionResult GetRibShadRptByID(Int64? pDF_RIB_SHADE_RPT_H_ID = null)
        {
            try
            {
                var data = new DF_RIB_SHADE_RPT_HModel().Select(pDF_RIB_SHADE_RPT_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("OtherCheckRoll/Save")]
        [HttpPost]
        // GET :  api/Dye/OtherCheckRoll/Save
        public IHttpActionResult Save([FromBody] DF_RIB_SHADE_RPT_HModel ob)
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
