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
    public class PurchaseRequisitionApiController : ApiController
    {
        [Route("PurchaseReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/purchase/PurchaseReq/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pPURC_REQ_NO = null, string pPURC_REQ_DT = null, string pCOMP_NAME_EN = null,
            string pDEPARTMENT_NAME_EN = null, string pREQ_STATUS_NAME_EN = null, string pREQ_PRIORITY_NAME_EN = null, string pREQ_SOURCE_NAME_EN = null,
            string pREMARKS = null, Int64? pRF_REQ_SRC_ID = null, Int32? pOption = 3003)
        {
            try
            {
                var list = new SCM_PURC_REQ_HModel().SelectAll(pageNo, pageSize, pPURC_REQ_NO, pPURC_REQ_DT, pCOMP_NAME_EN, pDEPARTMENT_NAME_EN, pREQ_STATUS_NAME_EN, pREQ_PRIORITY_NAME_EN, pREQ_SOURCE_NAME_EN, pREMARKS, pRF_REQ_SRC_ID, pOption);
                var data = list.Skip(pageNo - 1).Take(pageSize).ToList();
                int total = 0;
                if (list.Count() > 0)
                    total = list[0].TOTAL_REC;

                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PurchaseReq/GetPendingReqListForGPO")]
        [HttpGet]
        // GET :  /api/Purchase/PurchaseReq/GetPendingReqListForGPO
        public IHttpActionResult GetPendingReqListForGPO(Int32? pRF_REQ_SRC_ID = null, Int32? pSCM_PURC_REQ_H_ID = null, Int64? pLK_PURC_PROD_GRP_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_HModel().SelectForGPO(pRF_REQ_SRC_ID, pSCM_PURC_REQ_H_ID, pLK_PURC_PROD_GRP_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PurchaseReq/GetStockByID")]
        [HttpGet]
        // GET :  /api/Purchase/PurchaseReq/GetStockByID?pINV_ITEM_ID=&pSCM_STORE_ID=&pHR_COMPANY_ID=
        public IHttpActionResult GetStockByID(Int64? pINV_ITEM_ID = null, Int64? pSCM_STORE_ID = null, Int64? pHR_COMPANY_ID = null)//
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

        [Route("PurchaseReq/Save")]
        [HttpPost]
        // GET :  api/Purchase/PurchaseReq/Save
        public IHttpActionResult Save([FromBody] SCM_PURC_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveGR();
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
