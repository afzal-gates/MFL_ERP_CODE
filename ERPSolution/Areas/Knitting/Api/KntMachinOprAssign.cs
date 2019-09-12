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
    [Authorize]
    public class KntMachinOprAssignController : ApiController
    {

        [Route("KntMachinOprAssign/GetAssiPersonListByMachinId")]
        [HttpGet]
        // GET :  /api/knit/KntMachinOprAssign/GetAssiPersonListByMachinId
        public IHttpActionResult GetAssiPersonListByMachinId(Int64 pKNT_MACHINE_ID)
        {
            try
            {
                var obList = new KNT_MACHN_OPRModel().GetAssiPersonListByMachinId(pKNT_MACHINE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntMachinOprAssign/Save")]
        [HttpPost]
        // GET :  api/knit/KntMachinOprAssign/Save
        public IHttpActionResult Save([FromBody] KNT_MACHN_OPRModel ob)
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

        [Route("KntMachinOprAssign/GetKnitScheduleList")]
        [HttpGet]
        // GET :  /api/knit/KntMachinOprAssign/GetKnitScheduleList
        public IHttpActionResult GetKnitScheduleList()
        {
            try
            {
                var obList = new HR_SCHEDULE_HModel().GetKnitScheduleList();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
       
        
    }
}
