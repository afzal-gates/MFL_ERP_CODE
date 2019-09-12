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
    public class YarnReturnToSupplierController : ApiController
    {
        [Route("YarnReturnToSupplier/SelectAllReq/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnReturnToSupplier/SelectAllReq
        public IHttpActionResult SelectAllReq(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null,
            Int64? pSCM_SUPPLIER_ID = null, string pIMP_LC_NO = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var data = new KNT_YRN_STR_RPL_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pSCM_SUPPLIER_ID, pIMP_LC_NO, pYRN_LOT_NO, pRF_REQ_TYPE_ID);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetReplacementReqByID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetReplacementReqByID
        public IHttpActionResult GetReplacementReqByID(Int64? pKNT_YRN_STR_RPL_REQ_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_RPL_REQ_HModel().Select(pKNT_YRN_STR_RPL_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetReplacementReqDtlByID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetReplacementReqDtlByID
        public IHttpActionResult GetReplacementReqDtlByID(Int64? pKNT_YRN_STR_RPL_REQ_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_RPL_REQ_DModel().SelectByID(pKNT_YRN_STR_RPL_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetYarnSupRtnReqDtlBySupID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetYarnSupRtnReqDtlBySupID?pSCM_SUPPLIER_ID=
        public IHttpActionResult GetYarnSupRtnReqDtlBySupID(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_RPL_REQ_DModel().SelectBySupplierID(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        

        [Route("YarnReturnToSupplier/SaveReq")]
        [HttpPost]
        // GET :  api/knit/YarnReturnToSupplier/SaveReq
        public IHttpActionResult SaveReq([FromBody] KNT_YRN_STR_RPL_REQ_HModel ob)
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


        [Route("YarnReturnToSupplier/SelectAllIssue/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnReturnToSupplier/SelectAllIssue
        public IHttpActionResult SelectAllIssue(int pageNo, int pageSize, string pISS_CHALAN_NO = null, string pISS_CHALAN_DT = null,
            Int64? pSCM_SUPPLIER_ID = null, string pIMP_LC_NO = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var data = new KNT_YRN_RPL_ISS_HModel().SelectAll(pageNo, pageSize, pISS_CHALAN_NO, pISS_CHALAN_DT, pSCM_SUPPLIER_ID, pIMP_LC_NO, pYRN_LOT_NO, pRF_REQ_TYPE_ID);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetStockForSupplierReturn")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetStockForSupplierReturn
        public IHttpActionResult GetStockForSupplierReturn(Int64? pSCM_SUPPLIER_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pRF_BRAND_ID = null, string pIMP_LC_NO = null, string pYRN_LOT_NO = null)
        {
            try
            {
                var list = new KNT_YRN_RPL_ISS_HModel().GetStockForSupplierReturn(pSCM_SUPPLIER_ID, pYARN_ITEM_ID, pRF_BRAND_ID, pIMP_LC_NO, pYRN_LOT_NO);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetReplacementIssueByID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetReplacementIssueByID
        public IHttpActionResult GetReplacementIssueByID(Int64? pKNT_YRN_RPL_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_RPL_ISS_HModel().Select(pKNT_YRN_RPL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetReplacementIssueDtlByID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetReplacementIssueDtlByID
        public IHttpActionResult GetReplacementIssueDtlByID(Int64? pKNT_YRN_RPL_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_RPL_ISS_DModel().Select(pKNT_YRN_RPL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReturnToSupplier/GetReplacementReceiveDtlByID")]
        [HttpGet]
        // GET :  /api/Knit/YarnReturnToSupplier/GetReplacementReceiveDtlByID
        public IHttpActionResult GetReplacementReceiveDtlByID(Int64? pKNT_YRN_RPL_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_RPL_ISS_DModel().Select(pKNT_YRN_RPL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnReturnToSupplier/SaveIssue")]
        [HttpPost]
        // GET :  api/knit/YarnReturnToSupplier/SaveIssue
        public IHttpActionResult SaveIssue([FromBody] KNT_YRN_RPL_ISS_HModel ob)
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

        [Route("YarnReturnToSupplier/ReceiveConfirm")]
        [HttpPost]
        // GET :  api/knit/YarnReturnToSupplier/ReceiveConfirm
        public IHttpActionResult ReceiveConfirm([FromBody] KNT_YRN_RPL_ISS_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ReceiveSave();
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


        [Route("YarnReturnToSupplier/ReceiveSave")]
        [HttpPost]
        // GET :  api/knit/YarnReturnToSupplier/ReceiveSave
        public IHttpActionResult ReceiveSave([FromBody] KNT_YRN_RPL_ISS_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ReceiveSave();
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
