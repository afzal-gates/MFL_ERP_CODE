using ERP.Model;
using ERPSolution.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/dye")]
    [NoCache]
    public class FabricHoldListController : ApiController
    {

        [Route("FabHold/FabricHoldList")]
        [HttpGet]
        // GET :  /api/Dye/FabHold/FabricHoldList
        public IHttpActionResult FabricHoldList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var data = new DYE_BT_CARD_HModel().FabricHoldList(pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID);
                //var items=data.ToList();
                //var objList = from x in items
                //              select new
                //              {
                //                  x.DYE_LOT_NO,
                //                  x.DYE_BATCH_NO,
                //                  x.BATCH_QTY,
                //                  x.ACT_BATCH_QTY,
                //                  x.BYR_ACC_GRP_NAME_EN,
                //                  x.ORDER_NO,
                //                  x.BUYER_NAME_EN,
                //                  x.COLOR_GRP_NAME_EN,
                //                  x.COLOR_NAME_EN,
                //                  x.RESP_DEPT_NAME,
                //                  x.STYLE_NO,
                //                  x.subLot=data.ToList().Where(m=>m.DYE_BATCH_NO==x.DYE_BATCH_NO).ToList()
                //              };
                
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
