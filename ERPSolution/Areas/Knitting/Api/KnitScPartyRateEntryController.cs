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
    public class KnitScPartyRateEntryController : ApiController
    {
        [Route("KnitScPartyRate/GetScPartyRate")]
        [HttpGet]
        // GET :  /api/knit/KnitScPartyRate/GetScPartyRate?pSCM_SUPPLIER_ID=&pMC_FAB_PROC_GRP_ID=
        public IHttpActionResult GetScPartyRate(Int64? pSCM_SUPPLIER_ID = null, Int64? pMC_FAB_PROC_GRP_ID = null)
        {
            try
            {
                var list = new SCM_SC_QUOT_RATE_HModel().SelectByID(pSCM_SUPPLIER_ID, pMC_FAB_PROC_GRP_ID);                
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //[Route("KnitScPartyRate/GetScPartyQuatationList")]
        //[HttpGet]
        //// GET :  /api/knit/KnitScPartyRate/GetScPartyQuatationList?pSCM_SUPPLIER_ID=
        //public IHttpActionResult GetScPartyRate(Int64? pSCM_SUPPLIER_ID = null)
        //{
        //    try
        //    {
        //        var list = new SCM_SC_QUOT_RATE_HModel().SelectByID(pSCM_SUPPLIER_ID, pMC_FAB_PROC_GRP_ID);
        //        return Ok(list);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        
        [Route("KnitScPartyRate/GetScPartyRateDtl")]
        [HttpGet]
        // GET :  /api/knit/KnitScPartyRate/GetScPartyRateDtl?pSCM_SUPPLIER_ID=
        public IHttpActionResult GetScPartyRateDtl(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new SCM_SC_QUOT_RATE_DModel().SelectByID(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitScPartyRate/Save")]
        [HttpPost]
        // GET :  api/knit/KnitScPartyRate/Save
        public IHttpActionResult Save([FromBody] SCM_SC_QUOT_RATE_HModel ob)
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

        //SCM_SC_QUOT_RATE_DModel,SCM_SC_QUOT_RATE_DModel
    }
}
