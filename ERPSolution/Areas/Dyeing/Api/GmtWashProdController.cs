using ERP.Model;
using ERPSolution.Hubs;
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
    [NoCache]
    public class GmtWashProdController : ApiController
    {

        [Route("GmtWashProd/GetGmtWashProdListByID")]
        [HttpGet]
        // GET :  /api/Dye/GmtWashProd/GetGmtWashProdListByID?pGMT_DF_WASH_REQ_ID
        public IHttpActionResult GetGmtWashProdListByID(Int64? pGMT_DF_WASH_REQ_ID = null)
        {
            try
            {
                var obList = new DF_GMT_WASH_PRODModel().SelectAll(pGMT_DF_WASH_REQ_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtWashProd/GetGmtWashProdByID")]
        [HttpGet]
        // GET :  /api/Dye/GmtWashProd/GetGmtWashProdByID?pDF_GMT_WASH_PROD_ID
        public IHttpActionResult GetGmtWashProdByID(Int64? pDF_GMT_WASH_PROD_ID = null)
        {
            try
            {
                var obList = new DF_GMT_WASH_PRODModel().Select(pDF_GMT_WASH_PROD_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtWashProd/Save")]
        [HttpPost]
        // GET :  api/Dye/GmtWashProd/Save
        public IHttpActionResult Save([FromBody] DF_GMT_WASH_PRODModel ob)
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
