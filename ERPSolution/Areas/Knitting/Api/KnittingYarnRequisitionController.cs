using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class KnittingYarnRequisitionController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("KnitYarnReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pPURC_REQ_NO = null, string pPURC_REQ_DT = null, string pCOMP_NAME_EN = null,
            string pDEPARTMENT_NAME_EN = null, string pREQ_STATUS_NAME_EN = null, string pREQ_PRIORITY_NAME_EN = null, string pREQ_SOURCE_NAME_EN = null,
            string pREMARKS = null, Int64? pRF_REQ_SRC_ID = null)
        {
            try
            {
                var data = new SCM_PURC_REQ_HModel().SelectAll(pageNo, pageSize, pPURC_REQ_NO, pPURC_REQ_DT, pCOMP_NAME_EN, pDEPARTMENT_NAME_EN, pREQ_STATUS_NAME_EN, pREQ_PRIORITY_NAME_EN, pREQ_SOURCE_NAME_EN, pREMARKS, pRF_REQ_SRC_ID);
                //var data = list.Skip(pageNo - 1).Take(pageSize).ToList();
                int total = 0;
                total = data[0].TOTAL_REC;
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/GetYarnBuffReqInfo")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetYarnBuffReqInfo
        public IHttpActionResult GetYarnBuffReqInfo(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_HModel().Select(pSCM_PURC_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/GetPurchasePlan")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetPurchasePlan
        public IHttpActionResult GetPurchasePlan()
        {
            try
            {
                var list = new SCM_PURC_REQ_HModel().SelectPlan();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/ReqDtlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/ReqDtlByID?pSCM_PURC_REQ_H_ID
        public IHttpActionResult ReqDtlByID(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_DModel().SelectByID(pSCM_PURC_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/YarnReqDtlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/YarnReqDtlByID?pSCM_PURC_REQ_H_ID
        public IHttpActionResult YarnReqDtlByID(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRNModel().SelectByID(pSCM_PURC_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/CartDtlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/CartDtlByID
        public IHttpActionResult CartDtlByID()
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRNModel().CartDtlByID();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/YarnReqDtlForPlan")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/YarnReqDtlForPlan?pSCM_PURC_REQ_H_ID
        public IHttpActionResult YarnReqDtlForPlan(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRNModel().SelectForPlanByID(pSCM_PURC_REQ_H_ID);
                var Col = new SCM_PURC_YRN_PRICEModel().SelectForColumn(pSCM_PURC_REQ_H_ID);
                if (Col.Count == 0)
                {
                    var obj = new SCM_PURC_YRN_PRICEModel();
                    Col.Add(obj);
                }

                return Ok(new { list, Col });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYarnReq/Save")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/Save
        public IHttpActionResult Save([FromBody] SCM_PURC_REQ_HModel ob)
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
            Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }



        [Route("KnitYarnReq/AddCart")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/AddCart
        public IHttpActionResult AddCart([FromBody] SCM_PURC_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.AddCart();
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

        [Route("KnitYarnReq/GetColorWiseFabOrdListByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetColorWiseFabOrdListByID?pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult GetColorWiseFabOrdListByID(Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var list = new MC_FAB_PROD_ORD_DModel().GetColorWiseFabOrdListByID(pMC_FAB_PROD_ORD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/BufferStockByID")]
        [HttpPost]
        // GET :  /api/knit/KnitYarnReq/BufferStockByID?pMC_BUYER_ID=&pINV_ITEM_ID=&pLK_YRN_COLR_GRP_ID
        public IHttpActionResult BufferStockByID(SCM_PURC_REQ_D_YRNModel ob)
        {
            try
            {
                var lst = new List<SCM_PURC_REQ_D_YRNModel>();
                foreach (var obj in ob.dataList)
                {
                    var list = new SCM_PURC_REQ_D_YRNModel().Select(obj.MC_BUYER_ID, obj.INV_ITEM_ID, obj.LK_YRN_COLR_GRP_ID);
                    obj.BFR_QTY = list.BFR_QTY;
                    lst.Add(obj);
                }
                return Ok(lst);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/BufferStockDtlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/BufferStockDtlByID?pMC_BUYER_ID=&pINV_ITEM_ID=&pLK_YRN_COLR_GRP_ID
        public IHttpActionResult BufferStockDtlByID(Int64? pMC_BUYER_ID = null, Int64? pINV_ITEM_ID = null, Int64? pLK_YRN_COLR_GRP_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRNModel().SelectBufferDtlByID(pMC_BUYER_ID, pINV_ITEM_ID, pLK_YRN_COLR_GRP_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYarnReq/SelectTnaByOrderList")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/SelectTnaByOrderList?pMC_ORDER_H_ID_LST
        public IHttpActionResult SelectTnaByOrderList(string pMC_ORDER_H_ID_LST = null)
        {
            try
            {
                var list = new MC_ORD_TNAModel().SelectByOrderList(pMC_ORDER_H_ID_LST);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYarnReq/SaveCS")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/SaveCS
        public IHttpActionResult SaveCS([FromBody] SCM_PURC_YRN_PRICEModel ob)
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
            Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("KnitYarnReq/GetPOList")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetPOList
        public IHttpActionResult GetPOList(string pIS_LOCAL_CASH_P = null, Int64? pLK_PURC_PROD_GRP_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_HModel().SelectAll(pIS_LOCAL_CASH_P, pLK_PURC_PROD_GRP_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYarnReq/GetPOByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetPOByID?pCM_IMP_PO_H_ID=
        public IHttpActionResult GetPOByID(Int64? pCM_IMP_PO_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_HModel().Select(pCM_IMP_PO_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYarnReq/PendingSupplierList")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/PendingSupplierList
        public IHttpActionResult PendingSupplierList()
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRN1Model().SelectPendingSupplier();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/PendingRequisitionBySupplierID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/PendingRequisitionBySupplierID?pSCM_SUPPLIER_ID=
        public IHttpActionResult PendingRequisitionBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new SCM_PURC_REQ_D_YRNModel().PendingRequisitionBySupplierID(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/GetPoDetlByID")]
        [HttpGet]
        // GET :  /api/knit/KnitYarnReq/GetPoDetlByID?pCM_IMP_PO_H_ID=
        public IHttpActionResult GetPoDetlByID(Int64? pCM_IMP_PO_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_D_YRNModel().SelectByID(pCM_IMP_PO_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYarnReq/SavePO")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/SavePO
        public IHttpActionResult SavePO([FromBody] CM_IMP_PO_HModel ob)
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
            Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("KnitYarnReq/RevisePO")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/RevisePO
        public IHttpActionResult RevisePO([FromBody] CM_IMP_PO_HModel ob)
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
            Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }




        [Route("KnitYarnReq/Delete")]
        [HttpPost]
        // GET :  api/knit/KnitYarnReq/Delete
        public IHttpActionResult Delete([FromBody] SCM_PURC_REQ_D_TMPModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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
