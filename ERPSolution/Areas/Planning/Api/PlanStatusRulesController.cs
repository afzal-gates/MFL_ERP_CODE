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
    public class PlanStatusRulesController : ApiController
    {
        [Route("PlanStatus/Areas")]
        [HttpGet]
        // GET :  /api/pln/PlanStatus/Areas
        public IHttpActionResult getGmtPlanStatusAreas()
        {
            try
            {
                var obList = new GMT_PLN_STS_GROUPModel().getPlanStatusAreas();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PlanStatus/Rules")]
        [HttpGet]
        // GET :  /api/pln/PlanStatus/Rules
        public IHttpActionResult getGmtPlanStatusRules()
        {
            try
            {
                var obList = new GMT_PLN_STS_GROUPModel().getPlanStatusRules();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("PlanStatus/Save")]
        [HttpPost]
        // GET :  /api/pln/PlanStatus/Save
        public IHttpActionResult BatchSave([FromBody] GMT_PLN_STS_GROUPModel ob)
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
