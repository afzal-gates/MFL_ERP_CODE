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
    public class PlnLearningCurvController : ApiController
    {
        [Route("LearningCurv/GetLearningCurvList")]
        [HttpGet]
        // GET :  /api/pln/LearningCurv/GetLearningCurvList
        public IHttpActionResult GetLearningCurvList(Int64 pRF_FAB_CLASS_ID)
        {
            try
            {
                var obList = new GMT_LRN_CURVE_VMModel().GetLearningCurvList(pRF_FAB_CLASS_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("LearningCurv/BatchSave")]
        [HttpPost]
        // GET :  /api/pln/LearningCurv/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_LRN_CURVEModel ob)
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
