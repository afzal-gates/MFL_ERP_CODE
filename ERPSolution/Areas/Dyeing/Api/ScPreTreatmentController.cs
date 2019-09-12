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
    public class ScPreTreatmentController : ApiController
    {

        [Route("ScPreTreatment/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/ScPreTreatment/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSC_PRG_NO = null, string pPRG_ISS_DT = null, string pEXP_DELV_DT = null, Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, Int64? pDYE_GSTR_REQ_H_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                if (pPRG_ISS_DT != null)
                    pPRG_ISS_DT = Convert.ToDateTime(pPRG_ISS_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                if (pEXP_DELV_DT != null)
                    pEXP_DELV_DT = Convert.ToDateTime(pEXP_DELV_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DF_SC_PT_ISS_HModel().SelectAll(pageNo, pageSize, pSC_PRG_NO, pPRG_ISS_DT, pEXP_DELV_DT, pHR_COMPANY_ID, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pDYE_GSTR_REQ_H_ID, pUSER_ID);
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

        [Route("ScPreTreatment/ScDfParamList")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScDfParamList
        public IHttpActionResult ScDfParamList()
        {
            var obList = new RF_DF_PARAM_TYPEModel().SelectAll();
            return Ok(obList);
        }


        [Route("ScPreTreatment/GetScPreTreatmentInfo")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/GetScPreTreatmentInfo
        public IHttpActionResult GetScPreTreatmentInfo(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_ISS_HModel().Select(pDF_SC_PT_ISS_H_ID);
            return Ok(obList);
        }


        [Route("ScPreTreatment/ScChallanBySupplierID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScChallanBySupplierID
        public IHttpActionResult ScChallanBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            var obList = new DF_SC_PT_ISS_HModel().ScChallanBySupplierID(pSCM_SUPPLIER_ID);
            return Ok(obList);
        }


        [Route("ScPreTreatment/ScPtChallanBySupplierID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScPtChallanBySupplierID
        public IHttpActionResult ScPtChallanBySupplierID(Int64? pSCM_SUPPLIER_ID = null, string pSC_PRG_NO = null)
        {
            var obList = new DF_SC_PT_CHL_ISS_HModel().SelectBySupplierID(pSCM_SUPPLIER_ID, pSC_PRG_NO);
            return Ok(obList);
        }

        [Route("ScPreTreatment/GetScPtIssueChallanByID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/GetScPTIssueD1ByID
        public IHttpActionResult GetScPtIssueChallanByID(Int64? pDF_SC_PT_CHL_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_CHL_ISS_DModel().SelectByID(pDF_SC_PT_CHL_ISS_H_ID);
            return Ok(obList);
        }



        [Route("ScPreTreatment/ScProgramBySupplierID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScProgramBySupplierID
        public IHttpActionResult ScProgramBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            var obList = new DF_SC_PT_ISS_HModel().ScProgramBySupplierID(pSCM_SUPPLIER_ID);
            return Ok(obList);
        }

        [Route("ScPreTreatment/SelectBatchFabForPT")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/SelectBatchFabForPT?pDYE_BATCH_NO=
        public IHttpActionResult SelectBatchFabForPT(string pDYE_BATCH_NO = null)
        {
            var obList = new DF_SC_PT_ISS_D1Model().SelectBatchFabForPT(pDYE_BATCH_NO);
            return Ok(obList);
        }


        [Route("ScPreTreatment/ScPtChallanGetAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScPtChallanGetAll
        public IHttpActionResult ScPtChallanGetAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, string pCHALAN_DT = null, Int64? pUSER_ID = null)
        {
            if (pCHALAN_DT != null)
                pCHALAN_DT = Convert.ToDateTime(pCHALAN_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

            var data = new DF_SC_PT_CHL_ISS_HModel().SelectAll(pageNo, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }


        [Route("ScPreTreatment/ScPtChallanInfoByID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScPtChallanInfoByID
        public IHttpActionResult ScPtChallanInfoByID(Int64? pDF_SC_PT_CHL_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_CHL_ISS_HModel().Select(pDF_SC_PT_CHL_ISS_H_ID);
            return Ok(obList);
        }


        [Route("ScPreTreatment/ScPtChallanDtlByID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/ScPtChallanDtlByID
        public IHttpActionResult ScPtChallanDtlByID(Int64? pDF_SC_PT_CHL_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_CHL_ISS_DModel().SelectByID(pDF_SC_PT_CHL_ISS_H_ID);
            return Ok(obList);
        }

        [Route("ScPreTreatment/GetScPTIssueD1ByID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/GetScPTIssueD1ByID
        public IHttpActionResult GetScPTIssueD1ByID(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_ISS_D1Model().SelectAll(pDF_SC_PT_ISS_H_ID);
            return Ok(obList);
        }

        [Route("ScPreTreatment/GetScPTIssueD1ForDF")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/GetScPTIssueD1ForDF?pSC_PRG_NO
        public IHttpActionResult GetScPTIssueD1ForDF(string pSC_PRG_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST=null)
        {
            var obList = new DF_SC_PT_ISS_D1Model().SelectForDF(pSC_PRG_NO, pLK_DIA_TYPE_ID, pLK_FBR_GRP_LST);
            return Ok(obList);
        }


        [Route("ScPreTreatment/GetScPTIssueD2ByID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/GetScPTIssueD1ByID
        public IHttpActionResult GetScPTIssueD2ByID(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            var obList = new DF_SC_PT_ISS_D2Model().SelectAll(pDF_SC_PT_ISS_H_ID);
            return Ok(obList);
        }

        [Route("ScPreTreatment/Save")]
        [HttpPost]
        // GET :  api/Dye/ScPreTreatment/Save
        public IHttpActionResult Save([FromBody] DF_SC_PT_ISS_HModel ob)
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


        [Route("ScPreTreatment/SaveChallan")]
        [HttpPost]
        // GET :  api/Dye/ScPreTreatment/SaveChallan
        public IHttpActionResult SaveChallan([FromBody] DF_SC_PT_CHL_ISS_HModel ob)
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
