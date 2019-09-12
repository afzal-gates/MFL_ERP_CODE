using ERP.Model;
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
    public class DFMachineController : ApiController
    {
        [Route("DFMachine/SelectAll")]
        [HttpGet]
        // GET :  /api/Dye/DFMachine/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var data = new DF_MACHINEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFMachine/GetMachineTypeList")]
        [HttpGet]
        // GET :  /api/Dye/DFMachine/GetMachineTypeList
        public IHttpActionResult GetMachineTypeList()
        {
            try
            {
                var data = new DF_MC_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFMachine/GetMachineProdStatusList")]
        [HttpGet]
        // GET :  /api/Dye/DFMachine/GetMachineProdStatusList
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

        [Route("DFMachine/GetBatchStatusType")]
        [HttpGet]
        // GET :  /api/Dye/DFMachine/GetBatchStatusType
        public IHttpActionResult GetBatchStatusType()
        {
            try
            {
                var list = new DF_BT_STS_TYPEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("DFMachine/Save")]
        [HttpPost]
        // GET :  api/Dye/DFMachine/Save
        public IHttpActionResult Save([FromBody] DF_MACHINEModel ob)
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

        [Route("DFMachine/Delete")]
        [HttpPost]
        // GET :  api/Dye/DFMachine/Delete
        public IHttpActionResult Delete([FromBody] DF_MACHINEModel ob)
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
