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
    public class YarnIssueController : ApiController
    {
        [Route("YarnIssue/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pYRN_MRR_NO = null, string pYRN_MRR_DT = null, string pCOMP_NAME_EN = null,
            string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var data = new KNT_YRN_ISS_HModel().SelectAll(pageNo, pageSize, pYRN_MRR_NO, pYRN_MRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pREMARKS, pRF_REQ_TYPE_ID);
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

        [Route("YarnIssue/GetYarnStoreReq")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnStoreReq
        public IHttpActionResult GetYarnStoreReq(Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_REQ_HModel().PendingRequisiotion(pRF_REQ_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/GetYarnTestReq")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnTestReq
        public IHttpActionResult GetYarnTestReq(Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_REQ_HModel().GetYarnTestReq(pRF_REQ_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/GetYarnIssueReq")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnIssueReq
        public IHttpActionResult GetYarnIssueReq(Int64? pRF_REQ_TYPE_ID = null)
        {
            try
            {
                var list = new KNT_YRN_STR_REQ_HModel().GetYarnIssueReq(pRF_REQ_TYPE_ID);
                if (pRF_REQ_TYPE_ID == 14)
                {
                    var sList = (from x in list
                                 select new { x.SCM_SUPPLIER_ID, x.SUP_TRD_NAME_EN }
                                ).ToList().Distinct().ToList();

                    var oList = (from x in list
                                 select new { x.ORDER_LIST, x.KNT_JOB_CRD_H_ID }
                                ).ToList().Distinct().ToList();

                    return Ok(new { list, sList, oList });
                }
                else
                    return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/GetYarnJCDtl/{pKNT_YRN_STR_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnJCDtl
        public IHttpActionResult GetYarnJCDtl(Int64 pKNT_YRN_STR_REQ_H_ID, Int32? pOption = null)
        {
            try
            {
                var list = new KNT_YRN_STR_REQ_HModel().GetYarnStoreReq(pKNT_YRN_STR_REQ_H_ID, pOption);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnIssue/GetYarnIssueInfo")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnIssueInfo
        public IHttpActionResult GetYarnIssueInfo(Int64? pKNT_YRN_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_HModel().Select(pKNT_YRN_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/GetYarnIssueInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/GetYarnIssueInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetYarnIssueInfoDtlByID(Int64? pKNT_YRN_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_DModel().Select(pKNT_YRN_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/SelectByReqID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/SelectByReqID
        public IHttpActionResult SelectByReqID(Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            try
            {
                var data = new KNT_YRN_ISS_HModel().SelectByReqID(pKNT_YRN_STR_REQ_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/Save")]
        [HttpPost]
        // GET :  api/knit/YarnIssue/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_ISS_HModel ob)
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

        [Route("YarnIssue/SelectForTest")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/SelectForTest?pKNT_YRN_STR_REQ_H_ID
        public IHttpActionResult SelectForTest(Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_DModel().SelectForTest(pKNT_YRN_STR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("YarnIssue/SelectForChallan")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/SelectForChallan?pSCM_SUPPLIER_ID=
        public IHttpActionResult SelectForChallan(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new KNT_YRN_ISS_DModel().SelectForChallan(pSCM_SUPPLIER_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/YarnTestResult")]
        [HttpPost]
        // GET :  api/knit/YarnIssue/YarnTestResult
        public IHttpActionResult YarnTestResult([FromBody] KNT_YRN_LOTModel ob)
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

        [Route("YarnIssue/YarnIssueChallanAuto")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/YarnIssueChallanAuto
        public IHttpActionResult YarnIssueChallanAuto(string pCHALAN_NO)
        {
            try
            {
                var obList = new KNT_YRN_ISS_HModel().YarnIssueChallanAuto(pCHALAN_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssue/getRequisitionDataByJobCardH")]
        [HttpGet]
        // GET :  /api/knit/YarnIssue/getRequisitionDataByJobCardH?pKNT_JOB_CRD_H_ID=1&pKNT_PLAN_H_ID=1
        public IHttpActionResult getRequisitionDataByJobCardH(Int64? pKNT_JOB_CRD_H_ID = null, Int64? pKNT_PLAN_H_ID=null)
        {
            try
            {
                var obList = new KNT_YRN_STR_REQ_HModel().getRequisitionDataByJobCardH(pKNT_JOB_CRD_H_ID, pKNT_PLAN_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



    }
}
