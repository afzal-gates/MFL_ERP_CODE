
using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class StyleController : ApiController
    {

        [Route("Style/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/Style/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_STYLEModel().SelectAll();
            return Ok(obList);
        }

        [Route("Style/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/Style/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_STYLEModel().Select(ID);
            return Ok(ob);
        }

        [Route("Style/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc//Style/Save
        public IHttpActionResult Save([FromBody] MC_STYLEModel ob)
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


        [Route("Style/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/Style/Update
        public IHttpActionResult Update([FromBody] MC_STYLEModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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