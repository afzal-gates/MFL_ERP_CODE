using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Inventory.Api
{
    [RoutePrefix("api/Inv")]
    public class StoreTransferController : ApiController
    {
        [Route("StoreTransfer/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_TR_REQ_NO = null, string pSTR_TR_REQ_DT = null, string pCOMP_NAME_EN = null,
            string pDEPARTMENT_NAME_EN = null, string pREQ_TYPE_NAME = null, string pFROM_ST_NAME = null, string pTO_ST_NAME = null,
            string pEVENT_NAME = null, string pREQ_REMARKS = null, Int64? pUSER_ID = null)
        {
            try
            {
                var data = new INV_STR_TR_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_TR_REQ_NO, pSTR_TR_REQ_DT, pCOMP_NAME_EN, pDEPARTMENT_NAME_EN, pREQ_TYPE_NAME, pFROM_ST_NAME, pTO_ST_NAME, pEVENT_NAME, pREQ_REMARKS, pUSER_ID);
                int total = 0;
                if (data.Count > 0)
                    total = data[0].TOTAL_REC;
                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("StoreTransfer/GetStoreTransferInfo")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetStoreTransferInfo
        public IHttpActionResult GetStoreTransferInfo(Int64? pINV_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new INV_STR_TR_REQ_HModel().Select(pINV_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreTransfer/GetStoreTransferInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetStoreTransferInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetStoreTransferInfoDtlByID(Int64? pINV_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new INV_STR_TR_REQ_DModel().SelectByID(pINV_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreTransfer/GetStockInfo/{pINV_ITEM_ID}/{pFRM_STORE_ID}/{pTO_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetStoreTransferInfo
        public IHttpActionResult GetStockInfo(Int64 pINV_ITEM_ID, Int64 pFRM_STORE_ID, Int64 pTO_STORE_ID)
        {
            try
            {
                var list = new INV_STR_TR_REQ_DModel().GetStockInfo(pINV_ITEM_ID, pFRM_STORE_ID, pTO_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreTransfer/Save")]
        [HttpPost]
        // GET :  api/Inv/StoreTransfer/Save
        public IHttpActionResult Save([FromBody] INV_STR_TR_REQ_HModel ob)
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

        [Route("StoreTransfer/Update")]
        [HttpPost]
        // GET :  api/Inv/StoreTransfer/Update
        public IHttpActionResult Update([FromBody] INV_STR_TR_REQ_HModel ob)
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
            return Ok(new { success = true, jsonStr });
        }



        [Route("StoreTransfer/GetInvStrTrIssue")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetInvStrTrIssue
        public IHttpActionResult GetInvStrTrIssue(Int64? pINV_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new INV_STR_TR_ISS_HModel().GetInvStrTrIssue(pINV_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreTransfer/getStoreIssueListByRefNo/{pISS_REF_NO}")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/getStoreIssueListByRefNo
        public IHttpActionResult getStoreIssueListByRefNo(string pISS_REF_NO)
        {
            try
            {
                var list = new INV_STR_TR_ISS_HModel().getStoreIssueListByRefNo(pISS_REF_NO);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreTransfer/GetIssueByID")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetIssueByID
        public IHttpActionResult GetIssueByID(Int32? pINV_STR_TR_ISS_H_ID = null)
        {
            try
            {
                var list = new INV_STR_TR_ISS_HModel().Select(pINV_STR_TR_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreTransfer/GetIssueDtlByID")]
        [HttpGet]
        // GET :  /api/Inv/StoreTransfer/GetIssueDtlByID
        public IHttpActionResult GetIssueDtlByID(Int32? pINV_STR_TR_ISS_H_ID = null)
        {
            try
            {
                var list = new INV_STR_TR_ISS_DModel().Select(pINV_STR_TR_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreTransfer/SaveIssue")]
        [HttpPost]
        // GET :  api/Inv/StoreTransfer/SaveIssue
        public IHttpActionResult SaveIssue([FromBody] INV_STR_TR_ISS_HModel ob)
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
