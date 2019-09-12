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
    public class KnitUserReqMapController : ApiController
    {
        [Route("KnitUserReqMap/GetMppingInfoByUserID")]
        [HttpGet]
        // GET :  /api/knit/KnitUserReqMap/GetMppingInfoByUserID?pSC_USER_ID
        public IHttpActionResult GetMppingInfoByUserID(Int64? pSC_USER_ID = null)
        {
            try
            {
                var list = new SC_MAP_USR_RQSTYPModel().SelectByID(pSC_USER_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitUserReqMap/Save")]
        [HttpPost]
        // GET :  api/knit/KnitUserReqMap/Save
        public IHttpActionResult Save([FromBody] SC_MAP_USR_RQSTYPModel ob)
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
