using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Planning.Api
{
    [RoutePrefix("api/pln")]
    [Authorize]
    public class PlnItemPartMapController : ApiController
    {        
        [Route("PlnItemPartMap/GetCategGmtPartMapList")]
        [HttpGet]
        // GET :  /api/pln/PlnItemPartMap/GetCategGmtPartMapList
        public IHttpActionResult GetCategGmtPartMapList(int pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new GMT_MAP_GARM_PARTModel().GetCategGmtPartMapList(pINV_ITEM_CAT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("PlnItemPartMap/BatchSave")]
        [HttpPost]
        // GET :  /api/pln/PlnItemPartMap/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_MAP_GARM_PARTModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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
