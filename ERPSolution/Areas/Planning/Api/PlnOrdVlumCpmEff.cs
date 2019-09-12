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
    public class PlnOrdVlumCpmEffController : ApiController
    {
        [Route("PlnOrdVlumCpmEff/GetOrdCpmEffList")]
        [HttpGet]
        // GET :  /api/pln/PlnOrdVlumCpmEff/GetOrdCpmEffList
        public IHttpActionResult GetOrdCpmEffList()
        {
            try
            {
                var obList = new GMT_CPM_EFF_ORD_VOLModel().GetOrdCpmEffList();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("PlnOrdVlumCpmEff/BatchSave")]
        [HttpPost]
        // GET :  /api/pln/PlnOrdVlumCpmEff/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_CPM_EFF_ORD_VOLModel ob)
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
