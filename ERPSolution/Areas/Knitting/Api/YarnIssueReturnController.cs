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
    public class YarnIssueReturnController : ApiController
    {
        [Route("YarnIssueReturn/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pRET_CHALAN_NO = null, string pRET_CHALAN_DT = null, string pCOMP_NAME_EN = null, 
            string pSTORE_NAME_EN = null, string pREQ_STATUS = null, string pREMARKS = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                var data = new KNT_YRN_ISS_RET_HModel().SelectAll(pageNo, pageSize, pRET_CHALAN_NO, pRET_CHALAN_DT, pCOMP_NAME_EN, pSTORE_NAME_EN, pREQ_STATUS, pREMARKS, pRF_REQ_TYPE_ID, pUSER_ID, pOption);
                //var data = new KNT_YRN_ISS_RET_HModel().SelectAll();
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

        [Route("YarnIssueReturn/GetYarnStoreReq")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/GetYarnStoreReq
        public IHttpActionResult GetYarnStoreReq(Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                //var list = new KNT_YRN_STR_REQ_HModel().GetYarnTestReq(pRF_REQ_TYPE_ID);
                //return Ok(list);
                var list = new KNT_YRN_STR_REQ_HModel().SelectAll(1, 500, null, null, null, null,null,null, null, null, pRF_REQ_TYPE_ID, pUSER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReturn/GetIssuedYarnList/{pKNT_YRN_STR_REQ_H_ID}/{pYARN_ITEM_ID}/{pRF_BRAND_ID}/{pKNT_YRN_LOT_ID}/{pYRN_LOT_NO}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/GetIssueInfoForReturn
        public IHttpActionResult GetIssuedYarnList(Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pRF_BRAND_ID = null, Int64? pKNT_YRN_LOT_ID = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                if (pYRN_LOT_NO == "null")
                    pYRN_LOT_NO = null;
                var list = new KNT_YRN_ISS_RET_DModel().GetIssuedYarnList(pKNT_YRN_STR_REQ_H_ID, pYARN_ITEM_ID, pRF_BRAND_ID, pKNT_YRN_LOT_ID, pYRN_LOT_NO, pRF_REQ_TYPE_ID);

                var iList = (from x in list
                             select new { x.ITEM_NAME_EN, x.YARN_ITEM_ID }
                            ).ToList().Distinct().ToList();

                var lList = (from x in list
                             select new { x.YRN_LOT_NO, x.KNT_YRN_LOT_ID }
                            ).ToList().Distinct().ToList();

                var bList = (from x in list
                             select new { x.BRAND_NAME_EN, x.RF_BRAND_ID }
                            ).ToList().Distinct().ToList();

                return Ok(new { iList, lList, bList });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("YarnIssueReturn/GetIssueInfoForReturn/{pKNT_YRN_STR_REQ_H_ID}/{pYARN_ITEM_ID}/{pRF_BRAND_ID}/{pKNT_YRN_LOT_ID}/{pYRN_LOT_NO}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/GetIssueInfoForReturn
        public IHttpActionResult GetIssueInfoForReturn(Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pRF_BRAND_ID = null, Int64? pKNT_YRN_LOT_ID = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                if (pYRN_LOT_NO == "null")
                    pYRN_LOT_NO = null;
                var list = new KNT_YRN_ISS_RET_DModel().GetIssueInfoForReturn(pKNT_YRN_STR_REQ_H_ID, pYARN_ITEM_ID, pRF_BRAND_ID, pKNT_YRN_LOT_ID, pYRN_LOT_NO, pRF_REQ_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnIssueReturn/GetYarnIssueRtnReqInfo")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/GetYarnIssueRtnReqInfo
        public IHttpActionResult GetYarnIssueRtnReqInfo(Int64? pKNT_YRN_ISS_RET_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_RET_HModel().Select(pKNT_YRN_ISS_RET_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReturn/GetYarnRtnDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueReturn/GetYarnRtnDtlByID?pKNT_YRN_ISS_RET_H_ID
        public IHttpActionResult GetYarnRtnDtlByID(Int64? pKNT_YRN_ISS_RET_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_RET_DModel().Select(pKNT_YRN_ISS_RET_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueReturn/Save")]
        [HttpPost]
        // GET :  api/knit/YarnIssueReturn/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_ISS_RET_HModel ob)
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
