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
    public class YarnIssueReqController : ApiController
    {
        [Route("YarnIssueReq/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null,
            string pBYR_ACC_NAME_EN = null, string pMC_ORDER_NO_LST = null, string pYR_COUNT_NO = null,
            string pUSER_NAME_EN = null, string pEVENT_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new KNT_YRN_STR_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pBYR_ACC_NAME_EN, pMC_ORDER_NO_LST, pYR_COUNT_NO, pUSER_NAME_EN, pEVENT_NAME, pRF_REQ_TYPE_ID, pUSER_ID, pOption);
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


        [Route("YarnIssueReq/GetCollarCuffScoReqList")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/GetCollarCuffScoReqList
        public IHttpActionResult GetCollarCuffScoReqList(Int64 pKNT_SCO_CLCF_PRG_H_ID)
        {
            try
            {
                var obList = new KNT_YRN_STR_REQ_HModel().GetCollarCuffScoReqList(pKNT_SCO_CLCF_PRG_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnIssueReq/GetYarnIssueReqInfo")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/GetYarnRequisitionDtlByID
        public IHttpActionResult GetYarnIssueReqInfo(Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            try
            {
                var data = new KNT_YRN_STR_REQ_HModel().Select(pKNT_YRN_STR_REQ_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReq/ItemLotStockByID/{YARN_ITEM_ID}/{SCM_STORE_ID}/{pRF_REQ_TYPE_ID}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/ItemLotStockByID
        public IHttpActionResult ItemLotStockByID(Int64 YARN_ITEM_ID, Int64 SCM_STORE_ID, Int64 pRF_REQ_TYPE_ID)
        {
            try
            {
                var data = new KNT_YRN_LOT_STKModel().ItemLotStockByID(YARN_ITEM_ID, SCM_STORE_ID, pRF_REQ_TYPE_ID);
                var bList = (from x in data
                             select new { x.BRAND_NAME_EN, x.RF_BRAND_ID }).ToList().Distinct().ToList();
                var cList = (from x in data
                             select new { x.YRN_COLR_GRP, x.LK_YRN_COLR_GRP_ID }).ToList().Distinct().ToList();
                return Ok(new { data, bList, cList });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReq/GetYarnRequisitionDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/GetYarnRequisitionDtlByID
        public IHttpActionResult GetYarnRequisitionDtlByID(Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            try
            {
                var data = new KNT_YRN_STR_REQ_DModel().Select(pKNT_YRN_STR_REQ_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnIssueReq/GetYarnAllcoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReq/GetYarnAllcoDtlByID?pKNT_YRN_LOT_ID=&pYARN_ITEM_ID=
        public IHttpActionResult GetYarnRequisitionDtlByID(Int64? pKNT_YRN_LOT_ID = null, Int64? pYARN_ITEM_ID = null)
        {
            try
            {
                var data = new KNT_YRN_STR_REQ_DModel().SelectForAllocDtl(pKNT_YRN_LOT_ID, pYARN_ITEM_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReq/Save")]
        [HttpPost]
        // GET :  api/knit/YarnIssueReq/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_STR_REQ_HModel ob)
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


        [Route("YarnIssueReq/Update")]
        [HttpPost]
        // GET :  api/knit/YarnIssueReq/Update
        public IHttpActionResult Update([FromBody] KNT_YRN_STR_REQ_HModel ob)
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
    }
}
