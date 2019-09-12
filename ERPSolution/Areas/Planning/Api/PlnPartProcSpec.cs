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
    public class PlnPartProcSpecController : ApiController
    {
        [Route("PlnPartProcSpec/GetGmtPartProcSpecList")]
        [HttpGet]
        // GET :  /api/pln/PlnPartProcSpec/GetGmtPartProcSpecList
        public IHttpActionResult GetGmtPartProcSpecList(int pRF_GARM_PART_ID)
        {
            try
            {
                var obList = new GMT_PART_OPR_SPECModel().GetGmtPartProcSpecList(pRF_GARM_PART_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("PlnPartProcSpec/BatchSave")]
        [HttpPost]
        // GET :  /api/pln/PlnPartProcSpec/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_PART_OPR_SPECModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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
