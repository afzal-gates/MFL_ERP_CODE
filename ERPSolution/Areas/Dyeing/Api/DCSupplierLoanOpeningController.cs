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
    [RoutePrefix("api/dye")]
    public class DCSupplierLoanOpeningController : ApiController
    {
        [Route("DCSupLNSTK/SelectOpeningLN")]
        [HttpGet]
        // GET :  /api/Dye/DCSupLNSTK/SelectOpeningLN
        public IHttpActionResult SelectOpeningLN(Int64? SCM_SUPPLIER_ID=null)
        {
            try
            {
                var data = new DYE_SUP_LN_OBModel().Select(SCM_SUPPLIER_ID);
                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCSupLNSTK/SaveLNOB")]
        [HttpPost]
        // GET :  api/Dye/DCSupLNSTK/SaveLNOB
        public IHttpActionResult SaveLNOB([FromBody] DYE_SUP_LN_OBModel ob)
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

        [Route("DCSupLNSTK/Save")]
        [HttpPost]
        // GET :  api/Dye/DCSupLNSTK/Save
        public IHttpActionResult Save([FromBody] DYE_SUP_LN_STKModel ob)
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
