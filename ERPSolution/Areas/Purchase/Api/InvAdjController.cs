using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/purchase")]
    [Authorize]
    public class InvAdjController : ApiController
    {
        [Route("InvAdj/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/purchase/InvAdj/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pADJ_REQ_NO = null, string pADJ_REQ_DT = null, Int64? pSCM_STORE_ID = null, Int64? pRF_REQ_TYPE_ID = null, Int32? pOption = 3000)
        {
            try
            {
                var data = new SCM_STR_ITM_ADJ_HModel().SelectAll(pageNo, pageSize, pADJ_REQ_NO, pADJ_REQ_DT, pSCM_STORE_ID, pRF_REQ_TYPE_ID, pOption);
                int total = 0;
                if (data.Count() > 0)
                    total = data[0].TOTAL_REC;

                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("InvAdj/GetInvAdjInfoByID")]
        [HttpGet]
        // GET :  /api/Purchase/InvAdj/GetInvAdjInfoByID
        public IHttpActionResult GetInvAdjInfoByID(Int32? pSCM_STR_ITM_ADJ_H_ID = null)//
        {
            try
            {
                var list = new SCM_STR_ITM_ADJ_HModel().Select(pSCM_STR_ITM_ADJ_H_ID); //
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("InvAdj/GetInvAdjDtlByID")]
        [HttpGet]
        // GET :  /api/Purchase/InvAdj/GetInvAdjDtlByID
        public IHttpActionResult GetInvAdjDtlByID(Int32? pSCM_STR_ITM_ADJ_H_ID = null)//
        {
            try
            {
                var list = new SCM_STR_ITM_ADJ_DModel().SelectByID(pSCM_STR_ITM_ADJ_H_ID); //
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("InvAdj/GetGenItemStockByID")]
        [HttpGet]
        // GET :  /api/Purchase/InvAdj/GetGenItemStockByID?pINV_ITEM_ID=&pSCM_STORE_ID=&pHR_COMPANY_ID=
        public IHttpActionResult GetGenItemStockByID(Int64? pINV_ITEM_ID = null, Int64? pSCM_STORE_ID = null, Int64? pHR_COMPANY_ID = null)//
        {
            try
            {
                var list = new INV_GEN_ITEM_STKModel().getStockByID(pINV_ITEM_ID, pSCM_STORE_ID, pHR_COMPANY_ID); //
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("InvAdj/GetDcItemStockByID")]
        [HttpGet]
        // GET :  /api/Purchase/InvAdj/GetDcItemStockByID?pINV_ITEM_ID=&pSCM_STORE_ID=&pINV_ITEM_CAT_LST=
        public IHttpActionResult GetDcItemStockByID(Int64? pINV_ITEM_ID = null, Int64? pSCM_STORE_ID = null, string pINV_ITEM_CAT_LST = null)
        {
            try
            {
                //var data = new DYE_STK_CONSModel().SelectByID(pINV_ITEM_ID, pSCM_STORE_ID, pINV_ITEM_CAT_LST);
                var list = new DYE_ITEM_STKModel().SelectByID(pINV_ITEM_ID, pSCM_STORE_ID, pINV_ITEM_CAT_LST); //
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("InvAdj/Save")]
        [HttpPost]
        // GET :  api/Purchase/InvAdj/Save
        public IHttpActionResult Save([FromBody] SCM_STR_ITM_ADJ_HModel ob)
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
            //Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }

    }
}
