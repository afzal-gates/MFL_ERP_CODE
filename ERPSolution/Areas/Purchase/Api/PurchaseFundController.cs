using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/purchase")]
    public class PurchaseFundController : ApiController
    {

        [Route("fund/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Purchase/fund/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null)
        {
            try
            {
                var data = new SCM_FUND_REQ_HModel().SelectAll();                
                int total = 0;
                if (data.Count() > 0)
                    total = data[0].TOTAL_REC;

                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("fund/PendingReqForFund")]
        [HttpGet]
        // GET :  /api/Purchase/fund/PendingReqForFund
        public IHttpActionResult PendingReqForFund()
        {
            try
            {
                var list = new SCM_PURC_REQ_HModel().SelectForFund();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
                

        [Route("fund/GetFundInfoByID")]
        [HttpGet]
        // GET :  /api/Purchase/fund/GetFundInfoByID?pSCM_FUND_REQ_H_ID=
        public IHttpActionResult GetFundInfoByID(Int64? pSCM_FUND_REQ_H_ID = null)//
        {
            try
            {
                var list = new SCM_FUND_REQ_HModel().Select(pSCM_FUND_REQ_H_ID); //
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("fund/GetFundDtlByID")]
        [HttpGet]
        // GET :  /api/Purchase/fund/GetFundDtlByID?pSCM_FUND_REQ_H_ID=
        public IHttpActionResult GetFundDtlByID(Int64? pSCM_FUND_REQ_H_ID = null)//
        {
            try
            {
                var list = new SCM_FUND_REQ_DModel().SelectByID(pSCM_FUND_REQ_H_ID); //
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("fund/GetFundDtlItemByID")]
        [HttpGet]
        // GET :  /api/Purchase/fund/GetFundDtlItemByID?pSCM_FUND_REQ_H_ID=
        public IHttpActionResult GetFundDtlItemByID(Int64? pSCM_FUND_REQ_H_ID = null)//
        {
            try
            {
                var list = new SCM_FUND_REQ_D_ITMModel().SelectByID(pSCM_FUND_REQ_H_ID); //
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("fund/Save")]
        [HttpPost]
        // GET :  api/Purchase/fund/Save
        public IHttpActionResult Save([FromBody] SCM_FUND_REQ_HModel ob)
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
            //Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }
    }
}
