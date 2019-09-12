using ERP.Model;
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
    [Authorize]
    public class KntOrdWiseGreyTrnReqController : ApiController
    {
        [Route("KntOrdWiseGreyTrnReq/GetOrdFabBkingList")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetOrdFabBkingList
        public IHttpActionResult GetOrdFabBking(Int64 pMC_FAB_PROD_ORD_H_ID, Int64? pFAB_COLOR_ID=null)
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_HModel().GetOrdFabBkingList(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntOrdWiseGreyTrnReq/GetTransactionTypeList")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetTransactionTypeList
        public IHttpActionResult GetTransactionTypeList()
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_HModel().GetTransactionTypeList();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqHdr")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqHdr
        public IHttpActionResult GetOrdWiseGreyTrnReqHdr(Int64 pKNT_ORD_TRN_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_HModel().GetOrdWiseGreyTrnReqHdr(pKNT_ORD_TRN_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqList")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetOrdWiseGreyTrnReqList
        public IHttpActionResult GetOrdWiseGreyTrnReqList(int pageNumber, int pageSize, int? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null,
            Int64? pKNT_ORD_TRN_REQ_H_ID = null, string pTRN_REQ_NO = null, DateTime? pTRN_REQ_DT = null)
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_HModel().GetOrdWiseGreyTrnReqList(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pKNT_ORD_TRN_REQ_H_ID, pTRN_REQ_NO, pTRN_REQ_DT);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/GetTransData4AutoSearch")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetTransData4AutoSearch
        public IHttpActionResult GetTransData4AutoSearch(string pTRN_REQ_NO = null)
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_HModel().GetTransData4AutoSearch(pTRN_REQ_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/GetProdBalQty4TransList")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetProdBalQty4TransList
        public IHttpActionResult GetProdBalQty4TransList(Int64 pMC_FAB_PROD_ORD_H_ID, Int64? pFAB_COLOR_ID = null)
        {
            try
            {
                var obList = new KNT_PROD_BAL4TRANS_Model().GetProdBalQty4TransList(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/GetFabTransDtl")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetFabTransDtl
        public IHttpActionResult GetFabTransDtl(Int64 pKNT_ORD_TRN_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_ORD_TRN_REQ_DModel().GetFabTransDtl(pKNT_ORD_TRN_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/GetFabTransUserLavel")]
        [HttpGet]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/GetFabTransUserLavel
        public IHttpActionResult GetFabTransUserLavel()
        {
            try
            {
                var list = new KNT_ORD_TRN_REQ_USR_LAVELModel().GetFabTransUserLavel();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntOrdWiseGreyTrnReq/BatchSave")]
        [HttpPost]
        // GET :  /api/knit/KntOrdWiseGreyTrnReq/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_ORD_TRN_REQ_HModel ob)
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


        
        
        
    }
}
