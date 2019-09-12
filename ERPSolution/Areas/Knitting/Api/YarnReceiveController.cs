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
    public class YarnReceiveController : ApiController
    {
        [Route("YarnReceive/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pYRN_MRR_NO = null, string pYRN_MRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            try
            {
                var data = new KNT_YRN_RCV_HModel().SelectAll(pageNo, pageSize, pYRN_MRR_NO, pYRN_MRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pREMARKS);
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

        [Route("YarnReceive/GetYarnReceiveInfo")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetYarnReceiveInfo
        public IHttpActionResult GetYarnReceiveInfo(Int64? pKNT_YRN_RCV_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_RCV_HModel().Select(pKNT_YRN_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }        

        [Route("YarnReceive/GetYarnReceiveInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetYarnReceiveInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetYarnReceiveInfoDtlByID(Int64? pKNT_YRN_RCV_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_RCV_DModel().Select(pKNT_YRN_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReceive/Save")]
        [HttpPost]
        // GET :  api/knit/YarnReceive/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_RCV_HModel ob)
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


        [Route("YarnReceive/UpdateLC")]
        [HttpPost]
        // GET :  api/knit/YarnReceive/UpdateLC
        public IHttpActionResult UpdateLC([FromBody] KNT_YRN_RCV_HModel ob)
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

        [Route("YarnReceive/GetYarnOpeningBlance")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetYarnOpeningBlance
        public IHttpActionResult GetYarnOpeningBlance()
        {
            try
            {
                var list = new KNT_YRN_RCV_OBModel().SelectAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnReceive/SaveOB")]
        [HttpPost]
        // GET :  api/knit/YarnReceive/SaveOB
        public IHttpActionResult SaveOB([FromBody] KNT_YRN_RCV_OBModel ob)
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

        [Route("YarnReceive/DeleteOB")]
        [HttpPost]
        // GET :  api/knit/YarnReceive/DeleteOB
        public IHttpActionResult DeleteOB([FromBody] KNT_YRN_RCV_OBModel ob)
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


        [Route("YarnReceive/GetYarnForTest/{pRF_BRAND_ID}/{pRF_YRN_CNT_ID}")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetYarnForTest
        public IHttpActionResult GetYarnForTest(Int64? pRF_BRAND_ID = null, Int64? pRF_YRN_CNT_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_STKModel().GetYarnForTest(pRF_BRAND_ID, pRF_YRN_CNT_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnReceive/GetKnitSTKItemListByCatID")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetKnitSTKItemListByCatID
        public IHttpActionResult GetKnitSTKItemListByCatID(Int64? pINV_ITEM_CAT_ID = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            var obList = new INV_ITEMModel().GetKnitSTKItemListByCatID(Convert.ToInt32(pINV_ITEM_CAT_ID.ToString()), pRF_REQ_TYPE_ID);
            return Ok(obList);
        }



        [Route("YarnReceive/GetYarnForTestReq")]
        [HttpGet]
        // GET :  /api/knit/YarnReceive/GetYarnForTestReq
        public IHttpActionResult GetYarnForTestReq(string pFROM_DATE = null, string pTO_DATE = null, string pIMP_LC_NO = null, string pYRN_LOT_NO = null, Int64? pRF_BRAND_ID = null, Int64? pRF_YRN_CNT_ID = null)
        {
            try
            {
                if (pFROM_DATE == "null")
                    pFROM_DATE = null;
                else if (pFROM_DATE != null)
                    pFROM_DATE = Convert.ToDateTime(pFROM_DATE.ToString()).ToString("yyyy-MMM-dd");

                if (pTO_DATE == "null")
                    pTO_DATE = null;
                else if (pTO_DATE != null)
                    pTO_DATE = Convert.ToDateTime(pTO_DATE.ToString()).ToString("yyyy-MMM-dd");
                var list = new KNT_YRN_LOT_STKModel().GetYarnForTestReq(pFROM_DATE, pTO_DATE, pIMP_LC_NO, pYRN_LOT_NO, pRF_BRAND_ID, pRF_YRN_CNT_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
    }
}
