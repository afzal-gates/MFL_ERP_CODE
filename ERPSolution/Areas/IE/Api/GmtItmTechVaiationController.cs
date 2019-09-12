using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.IE.Api
{
    [RoutePrefix("api/ie")]
    [Authorize]
    public class GmtItmTechVaiationController : ApiController
    {
        [Route("GmtItemVariation/FindGmtTechSpecVariationData")]
        [HttpGet]
        // GET :  /api/ie/GmtItemVariation/FindGmtTechSpecVariationData
        public IHttpActionResult FindGmtTechSpecVariationData(int pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new GMT_IE_ITM_VARIATIONModel().FindGmtTechSpecVariationData(pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtItemVariation/Save")]
        [HttpPost]
        // GET :  /api/pln/GmtItemVariation/Save
        public IHttpActionResult updateEventForTuning([FromBody] GMT_IE_ITM_VARIATIONModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }

        
    }
}
