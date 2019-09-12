using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    [Authorize]
    public class CollarCuffController : ApiController
    {
        // GET api/collarcuff
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/collarcuff/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/collarcuff
        public void Post([FromBody]string value)
        {
        }

        // PUT api/collarcuff/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/collarcuff/5
        public void Delete(int id)
        {
        }

        [Route("CollarCuff/Save")]
        [HttpPost]
        // GET :  /api/mrc/collarcuff/Save
        public IHttpActionResult Save([FromBody] MC_STYL_CLCF_MEASModel ob)
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
