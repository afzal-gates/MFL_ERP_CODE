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
    public class TnaTemplateDtlController : ApiController
    {

        [Route("TnaTemplateDtl/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_TNA_TMPLT_DModel().SelectAll();
            return Ok(obList);
        }

        [Route("TnaTemplateDtl/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_TNA_TMPLT_DModel().Select(ID);
            return Ok(ob);
        }

        [Route("TnaTemplateDtl/treelistData/{MC_TNA_TMPLT_H_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TnaTemplateDtl/treelistData/1
        public IHttpActionResult treelistData(Int64 MC_TNA_TMPLT_H_ID)
        {
            var ob = new MC_TNA_TMPLT_DModel().treelistData(MC_TNA_TMPLT_H_ID);
            return Ok(ob);
        }

        [Route("TnaTemplateDtl/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Save
        public IHttpActionResult Save([FromBody] MC_TNA_TMPLT_DModel ob)
        {
            string jsonStr = "";
           
            try
            {
                jsonStr = ob.Save();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Ok(new { success = true, jsonStr });
        }


        [Route("TnaTemplateDtl/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Update
        public IHttpActionResult Update([FromBody] MC_TNA_TMPLT_DModel ob)
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


        [Route("TnaTemplateDtl/Delete")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Update
        public IHttpActionResult Delete([FromBody] MC_TNA_TMPLT_DModel ob)
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