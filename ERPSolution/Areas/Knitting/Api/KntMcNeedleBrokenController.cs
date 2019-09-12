using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    [System.Web.Http.Authorize]
    public class KntMcNeedleBrokenController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("McNeedleBroken/GetNeedleBrokenList")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetNeedleBrokenList
        public IHttpActionResult GetNeedleBrokenList(Int64 pageNumber, Int64 pageSize)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetNeedleBrokenList(pageNumber, pageSize);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("McNeedleBroken/GetNeedleBrokenByHdrID")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetNeedleBrokenByHdrID
        public IHttpActionResult GetNeedleBrokenByHdrID(Int64 pKNT_MCN_NDL_BRK_H_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetNeedleBrokenByHdrID(pKNT_MCN_NDL_BRK_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("McNeedleBroken/GetNeedleBrokenDtlByID")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetNeedleBrokenDtlByID
        public IHttpActionResult GetNeedleBrokenDtlByID(long pKNT_MCN_NDL_BRK_H_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetNeedleBrokenDtlByID(pKNT_MCN_NDL_BRK_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McNeedleBroken/GetItemStockByID")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetItemStockByID
        public IHttpActionResult GetItemStockByID(Int64? pSCM_STORE_ID = null, Int64? pINV_ITEM_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetItemStockByID(pSCM_STORE_ID, pINV_ITEM_ID, pHR_OFFICE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("McNeedleBroken/GetAssignNdleItemListByMc")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetAssignNdleItemListByMc
        public IHttpActionResult GetAssignNdleItemListByMc(Int64 pKNT_MACHINE_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_ASSIGNModel().GetAssignNdleItemListByMc(pKNT_MACHINE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("McNeedleBroken/BatchSave")]
        [HttpPost]
        // GET :  api/knit/McNeedleBroken/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_MCN_NDL_BRK_HModel ob)
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


        [Route("McNeedleBroken/BatchRevise")]
        [HttpPost]
        // GET :  api/knit/McNeedleBroken/BatchRevise
        public IHttpActionResult BatchRevise([FromBody] KNT_MCN_NDL_BRK_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchRevise();
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
