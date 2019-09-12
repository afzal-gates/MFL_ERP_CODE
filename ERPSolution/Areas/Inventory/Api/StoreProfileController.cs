using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Inventory.Api
{
    [RoutePrefix("api/inv")]
    [Authorize]
    public class StoreProfileController : ApiController
    {

        [Route("StoreProfile/SelectAll")]
        [HttpGet]
        // GET :  /api/inv/StoreProfile/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var obList = new SCM_STOREModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("StoreProfile/Save")]
        [HttpPost]
        // GET :  api/inv/StoreProfile/Save
        public IHttpActionResult Save([FromBody] SCM_STOREModel ob)
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

        [Route("StoreProfile/Delete")]
        [HttpPost]
        // GET :  api/inv/StoreProfile/Delete
        public IHttpActionResult Delete([FromBody] SCM_STOREModel ob)
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
