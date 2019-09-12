
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
    public class SizeMasterController : ApiController
    {

        [Route("SizeMaster/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_SIZEModel().SelectAll();
            return Ok(obList);
        }

        [Route("SizeMaster/GetStyleSizeList/{pMC_STYLE_H_ID:int}/{pMC_SIZE_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SizeMaster/GetStyleSizeList/0/0
        public IHttpActionResult GetStyleSizeList(Int64? pMC_STYLE_H_ID=null, string pMC_SIZE_ID=null)
        {
            var obList = new MC_STYL_CLCF_MEASModel().GetStyleSizeList(pMC_STYLE_H_ID, pMC_SIZE_ID);
            return Ok(obList);
        }


        [Route("SizeMaster/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_SIZEModel().Select(ID);
            return Ok(ob);
        }

        [Route("SizeMaster/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Save
        public IHttpActionResult Save(MC_SIZEModel ob)
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


        [Route("SizeMaster/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Update
        public IHttpActionResult Update([FromBody] MC_SIZEModel ob)
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