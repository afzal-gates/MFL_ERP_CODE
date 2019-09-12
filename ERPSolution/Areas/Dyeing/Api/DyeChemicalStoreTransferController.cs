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
    public class DyeChemicalStoreTransferController : ApiController
    {
        [Route("DyeChemicalStoreTransfer/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_TR_REQ_NO = null, string pSTR_TR_REQ_DT = null, string pCOMP_NAME_EN = null, 
            string pDEPARTMENT_NAME_EN = null, string pREQ_TYPE_NAME = null, string pFROM_ST_NAME = null, string pTO_ST_NAME = null,
            string pEVENT_NAME = null, string pREQ_REMARKS = null, Int64? pUSER_ID = null)
        {
            try
            {
                var data = new DYE_STR_TR_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_TR_REQ_NO, pSTR_TR_REQ_DT, pCOMP_NAME_EN, pDEPARTMENT_NAME_EN, pREQ_TYPE_NAME, pFROM_ST_NAME, pTO_ST_NAME, pEVENT_NAME, pREQ_REMARKS, pUSER_ID);
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

        


        [Route("DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo
        public IHttpActionResult GetDyeChemicalStoreTransferInfo(Int64? pDYE_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_HModel().Select(pDYE_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDyeChemicalStoreTransferInfoDtlByID(Int64? pDYE_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_DModel().Select(pDYE_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalStoreTransfer/GetStockInfo/{pDC_ITEM_ID}/{pFRM_STORE_ID}/{pTO_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo
        public IHttpActionResult GetStockInfo(Int64 pDC_ITEM_ID, Int64 pFRM_STORE_ID, Int64 pTO_STORE_ID)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_DModel().GetStockInfo(pDC_ITEM_ID, pFRM_STORE_ID, pTO_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalStoreTransfer/GetStockInfoForIssue/{pDC_ITEM_ID}/{pFRM_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetDyeChemicalStoreTransferInfo
        public IHttpActionResult GetStockInfoForIssue(Int64 pDC_ITEM_ID, Int64 pFRM_STORE_ID)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_DModel().GetStockInfoForIssue(pDC_ITEM_ID, pFRM_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeChemicalStoreTransfer/GetStockInfoForIssueAnyStore/{pDC_ITEM_ID}/{pFRM_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetStockInfoForIssueAnyStore
        public IHttpActionResult GetStockInfoForIssueAnyStore(Int64 pDC_ITEM_ID, Int64 pFRM_STORE_ID)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_DModel().GetStockInfoForIssueAnyStore(pDC_ITEM_ID, pFRM_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("DyeChemicalStoreTransfer/Save")]
        [HttpPost]
        // GET :  api/Dye/DyeChemicalStoreTransfer/Save
        public IHttpActionResult Save([FromBody] DYE_STR_TR_REQ_HModel ob)
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

        [Route("DyeChemicalStoreTransfer/Update")]
        [HttpPost]
        // GET :  api/Dye/DyeChemicalStoreTransfer/Update
        public IHttpActionResult Update([FromBody] DYE_STR_TR_REQ_HModel ob)
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


        [Route("DyeChemicalStoreTransfer/GetDyeStrTrIssue")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetDyeStrTrIssue
        public IHttpActionResult GetDyeStrTrIssue(Int64? pDYE_STR_TR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_TR_ISS_HModel().GetDyeStrTrIssue(pDYE_STR_TR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalStoreTransfer/getStoreIssueListByRefNo/{pISS_REF_NO}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/getStoreIssueListByRefNo
        public IHttpActionResult getStoreIssueListByRefNo(string pISS_REF_NO)
        {
            try
            {
                var list = new DYE_STR_TR_ISS_HModel().getStoreIssueListByRefNo(pISS_REF_NO);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("DyeChemicalStoreTransfer/GetIssueByID")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetIssueByID
        public IHttpActionResult GetIssueByID(Int64? pDYE_STR_TR_ISS_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_TR_ISS_HModel().Select(pDYE_STR_TR_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalStoreTransfer/GetIssueDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalStoreTransfer/GetIssueDtlByID
        public IHttpActionResult GetIssueDtlByID(Int64? pDYE_STR_TR_ISS_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_TR_ISS_DModel().Select(pDYE_STR_TR_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeChemicalStoreTransfer/SaveIssue")]
        [HttpPost]
        // GET :  api/Dye/DyeChemicalStoreTransfer/SaveIssue
        public IHttpActionResult SaveIssue([FromBody] DYE_STR_TR_ISS_HModel ob)
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
