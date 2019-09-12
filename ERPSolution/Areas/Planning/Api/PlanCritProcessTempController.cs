using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Planning.Api
{
    [RoutePrefix("api/pln")]
    [Authorize]
    public class PlanCritProcessTempController : ApiController
    {
        [Route("CritProcess/getGmtPlanCpm")]
        [HttpGet]
        // GET :  /api/pln/CritProcess/getGmtPlanCpm?pMC_BUYER_ID
        public IHttpActionResult getGmtPlanCpm(int? pMC_BUYER_ID = null, Int32? pPARENT_ID = null, string pMC_TNA_TMPLT_H_LST = null)
        {
            try
            {
                var ob = new GMT_PLN_TNA_BUYER().getGmtPlanCpm(pMC_BUYER_ID, pPARENT_ID, pMC_TNA_TMPLT_H_LST);
                return Ok(ob);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("CritProcess/getTnaTasksDs")]
        [HttpGet]
        // GET :  /api/pln/CritProcess/getTnaTasksDs
        public IHttpActionResult getTnaTasksDs()
        {
            try
            {
                var ob = new GMT_PLN_TNA_TMPLTModel().getTnaTasksDs();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("CritProcess/Save")]
        [HttpPost]
        // GET :  /api/pln/CritProcess/Save
        public IHttpActionResult CritProcessSave([FromBody] GMT_PLN_TNA_BUYER ob)
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
