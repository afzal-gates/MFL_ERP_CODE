
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
    public class LabdipStatusController : ApiController
    {
        [Route("LabdipStatus/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/LabdipStatus/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_LD_REQ_STATUSModel().SelectAll();
            return Ok(obList);
        }

        [Route("LabdipStatus/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/LabdipStatus/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_LD_REQ_STATUSModel().Select(ID);
            return Ok(ob);
        }

        [Route("LabdipStatus/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/LabdipStatus/Save
        public IHttpActionResult Save(MC_LD_REQ_STATUSModel ob)
        {
            string jsonStr = "";
            jsonStr = ob.Save();
            return Ok(new { success = true, jsonStr });

        }


        [Route("LabdipStatus/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/LabdipStatus/Update
        public IHttpActionResult Update([FromBody] MC_LD_REQ_STATUSModel ob)
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