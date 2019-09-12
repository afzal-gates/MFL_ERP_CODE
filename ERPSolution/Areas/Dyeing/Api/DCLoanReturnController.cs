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
    public class DCLoanReturnController : ApiController
    {
        [Route("DCLoanReturn/LoanSelectAll/{pageNo}/{pageSize}/{pRF_REQ_TYPE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DCLoanReturn/LoanSelectAll
        public IHttpActionResult LoanSelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                var data = new DYE_STR_REQ_HModel().LoanSelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pUSER_NAME_EN, pEVENT_NAME, pRF_REQ_TYPE_ID, pUSER_ID);
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

        [Route("DCLoanReturn/GetSupplierLoanDtlByID/{SCM_SUPPLIER_ID}/{REQ_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DCLoanReturn/GetSupplierLoanDtlByID?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierLoanDtlByID(Int64? SCM_SUPPLIER_ID = null, Int64? REQ_STORE_ID=null)
        {
            try
            {
                var list = new DYE_SUP_LN_STKModel().SelectBySuppID(SCM_SUPPLIER_ID, REQ_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        

        [Route("DCLoanReturn/GetDCLoanReturnInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCLoanReturn/GetDCLoanReturnInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCLoanReturnInfoDtlByID(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_REQ_DModel().SelectForLoan(pDYE_STR_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCLoanReturn/Save")]
        [HttpPost]
        // GET :  api/Dye/DCLoanReturn/Save
        public IHttpActionResult Save([FromBody] DYE_STR_REQ_HModel ob)
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



        [Route("DCLoanReturn/SelectAllLRT/{pageNo}/{pageSize}/{pRF_REQ_TYPE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DCLoanReturn/SelectAllLRT
        public IHttpActionResult SelectAllLRT(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                var data = new DYE_DC_LRT_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pUSER_NAME_EN, pEVENT_NAME);
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


    }
}
