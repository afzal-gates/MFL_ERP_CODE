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
    public class McLDPortController : ApiController
    {

        [Route("McLDPort/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/McLDPort/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_LD_PORTModel().SelectAll();
            return Ok(obList);
        }

        [Route("McLDPort/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/McLDPort/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_LD_PORTModel().Select(ID);
            return Ok(ob);
        }

        [Route("McLDPort/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/McLDPort/Save
        public IHttpActionResult Save(MC_LD_PORTModel ob)
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


        [Route("McLDPort/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/McLDPort/Update
        public IHttpActionResult Update([FromBody] MC_LD_PORTModel ob)
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