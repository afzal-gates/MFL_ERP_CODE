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
    public class DFProcessTypeController : ApiController
    {
        [Route("DFProcessType/SelectAll")]
        [HttpGet]
        // GET :  /api/Dye/DFProcessType/SelectAll
        public IHttpActionResult SelectAll(Int64? pMC_FAB_PROC_GRP_ID = null)
        {
            try
            {
                var data = new DF_PROC_TYPEModel().SelectAll(pMC_FAB_PROC_GRP_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProcessType/GetFabProcGrpList")]
        [HttpGet]
        // GET :  /api/Dye/DFProcessType/GetFabProcGrpList
        public IHttpActionResult GetFabProcGrpList()
        {
            try
            {
                var data = new MC_FAB_PROC_GRPModel().Select();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DFProcessType/Save")]
        [HttpPost]
        // GET :  api/Dye/DFProcessType/Save
        public IHttpActionResult Save([FromBody] DF_PROC_TYPEModel ob)
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

        [Route("DFProcessType/Delete")]
        [HttpPost]
        // GET :  api/Dye/DFProcessType/Delete
        public IHttpActionResult Delete([FromBody] DF_PROC_TYPEModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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
