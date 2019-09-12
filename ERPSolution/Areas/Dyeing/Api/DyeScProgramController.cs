using ERP.Model;
using ERP.Model.Purchase;
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
    [RoutePrefix("api/dye")]
    [Authorize]
    [NoCache]
    public class DyeScProgramController : ApiController
    {

        [Route("DyeScProgram/SaveDyeScPrgIss")]
        [HttpPost]
        // GET :  /api/dye/DyeScProgram/SaveDyeScPrgIss
        public IHttpActionResult SaveDyeScPrgIss([FromBody]DYE_SC_PRG_ISSModel ob)
        {
            string json = "";
            if (ModelState.IsValid)
            {
                try
                {
                    json = ob.Save();
                    return Ok(new { success = true, json });
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.InternalServerError, e.Message);
                }
            }
            else
            {
                var errors = new List<string>();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors.Add(pair.Value.Errors.Select(error => error.ErrorMessage).ToList()[0]);
                    }
                }
                return Ok(new { success = false, errors });
            }
        }

        
        [Route("DyeScProgram/SelectDyeScPrgIss")]
        [HttpGet]
        // GET :  /api/dye/DyeScProgram/SelectDyeScPrgIss?pDYE_SC_PRG_ISS_ID=1&pOption=3001
        public IHttpActionResult SelectDyeScPrgIss(Int64 pDYE_SC_PRG_ISS_ID, int pOption)
        {
            try
            {
                var ob = new DYE_SC_PRG_ISSModel().Select(pDYE_SC_PRG_ISS_ID, pOption);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeScProgram/QueryDyeScPrgIss")]
        [HttpGet]
        // GET :  /api/dye/DyeScProgram/QueryDyeScPrgIss?pPRG_ISS_NO&pDYE_SC_PRG_ISS_ID&pOption=3000
        public IHttpActionResult QueryDyeScPrgIss(String pPRG_ISS_NO, Int64? pDYE_SC_PRG_ISS_ID, int pOption)
        {
            try
            {
                var ob = new DYE_SC_PRG_ISSModel().Query(pPRG_ISS_NO, pDYE_SC_PRG_ISS_ID,pOption);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
