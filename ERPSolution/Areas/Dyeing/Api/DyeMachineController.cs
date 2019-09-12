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

    [RoutePrefix("api/dye")]
    [NoCache]
    public class DyeMachineController : ApiController
    {
        [Route("DyeMachine/SelectAll")]
        [HttpGet]
        // GET :  /api/Dye/DyeMachine/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var data = new DYE_MACHINEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeMachine/GetMachineTypeList")]
        [HttpGet]
        // GET :  /api/Dye/DyeMachine/GetMachineTypeList
        public IHttpActionResult GetMachineTypeList()
        {
            try
            {
                var data = new DYE_MC_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeMachine/GetMachineProdStatusList")]
        [HttpGet]
        // GET :  /api/Dye/DyeMachine/GetMachineProdStatusList
        public IHttpActionResult GetMachineProdStatusList()
        {
            try
            {
                var data = new RF_MAC_PROD_STSModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeMachine/GetMachineListByCompany")]
        [HttpGet]
        // GET :  /api/Dye/DyeMachine/GetMachineListByCompany?pHR_COMPANY_ID=
        public IHttpActionResult GetMachineListByCompany(Int64? pHR_COMPANY_ID = null)
        {
            try
            {
                var data = new DYE_MACHINEModel().SelectByCompany(pHR_COMPANY_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeMachine/Save")]
        [HttpPost]
        // GET :  api/Dye/DyeMachine/Save
        public IHttpActionResult Save([FromBody] DYE_MACHINEModel ob)
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

        [Route("DyeMachine/Delete")]
        [HttpPost]
        // GET :  api/Dye/DyeMachine/Delete
        public IHttpActionResult Delete([FromBody] DYE_MACHINEModel ob)
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
