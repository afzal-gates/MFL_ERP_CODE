using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/cmr")]
    public class BuyerNotifyPartyController : ApiController
    {
        [Route("BuyerNotifyParty/GetBuyerNotifyPartyInfo")]
        [HttpGet]
        // GET :  /api/cmr/BuyerNotifyParty/GetBuyerNotifyPartyInfo?pMC_BUYER_ID=
        public IHttpActionResult GetBuyerNotifyPartyInfo(Int64? pMC_BUYER_ID = null)
        {
            try
            {
                var list = new CM_NOTIFY_PARTYModel().SelectByID(pMC_BUYER_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("BuyerNotifyParty/Save")]
        [HttpPost]
        // GET :  api/Cmr/BuyerNotifyParty/Save
        public IHttpActionResult Save([FromBody] CM_NOTIFY_PARTYModel ob)
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
