
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
    public class BuyerColorController : ApiController
    {

        [Route("BuyerColor/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/BuyerColor/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_BU_COL_REFModel().SelectAll();
            return Ok(obList);
        }

        [Route("BuyerColor/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/BuyerColor/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BU_COL_REFModel().Select(ID);
            return Ok(ob);
        }

        [Route("BuyerColor/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc//BuyerColor/Save
        public IHttpActionResult Save([FromBody] MC_BU_COL_REFModel ob)
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


        //[Route("BuyerColor/Update")]
        //[HttpPut]
        //[Authorize]

        // GET :  api/mrc/BuyerColor/Update
        //public IHttpActionResult Update([FromBody] MC_BU_COL_REFModel ob)
        //{
        //    string jsonStr = "";

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Update();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }

        //    return Ok(new { success = true, jsonStr });

        //}
    }
}