using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/cmr")]
    public class ExportLCSalesContactController : ApiController
    {

        [Route("ExportLCSalesContact/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCSalesContact/SelectAll/{pageNo}/{pageSize}
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pMC_BUYER_ID = null,
                                            Int64? pLK_LC_STS_ID = null, string pEXP_LCSC_NO = null, string pSTYLE_ORDER_NO = null, string pISSUE_DT = null)
        {
            try
            {
                if (pISSUE_DT != null)
                    pISSUE_DT = Convert.ToDateTime(pISSUE_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new CM_EXP_LC_HModel().SelectAll(pageNo, pageSize, pHR_COMPANY_ID, pMC_BUYER_ID, pLK_LC_STS_ID, pEXP_LCSC_NO, pSTYLE_ORDER_NO, pISSUE_DT);
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

        [Route("ExportLCSalesContact/GetExportLcContactInfo")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCSalesContact/GetExportLcContactInfo?pCM_EXP_LC_H_ID=
        public IHttpActionResult GetExportLcContactInfo(Int64? pCM_EXP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_LC_HModel().Select(pCM_EXP_LC_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ExportLCSalesContact/GetExportLcByBuyerID")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCSalesContact/GetExportLcByBuyerID?pMC_BUYER_ID=&pCM_EXP_LC_H_ID=
        public IHttpActionResult GetExportLcByBuyerID(Int64? pMC_BUYER_ID = null, Int64? pCM_EXP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_LC_HModel().SelectByBuyerID(pMC_BUYER_ID, pCM_EXP_LC_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ExportLCSalesContact/GetOrderDtlInfo")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCSalesContact/GetOrderDtlInfo?pMC_ORDER_H_ID=
        public IHttpActionResult GetOrderDtlInfo(Int64? pMC_ORDER_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_LC_D_POModel().SelectByOrderID(pMC_ORDER_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ExportLCSalesContact/GetPOListByID")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCSalesContact/GetPOListByID?pCM_EXP_LC_H_ID=
        public IHttpActionResult GetPOListByID(Int64? pCM_EXP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_LC_D_POModel().GetPOListByID(pCM_EXP_LC_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ExportLCSalesContact/Save")]
        [HttpPost]
        // GET :  api/Cmr/ExportLCSalesContact/Save
        public IHttpActionResult Save([FromBody] CM_EXP_LC_HModel ob)
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


        [Route("ExportLCSalesContact/Update")]
        [HttpPost]
        // GET :  api/Cmr/ExportLCSalesContact/Update
        public IHttpActionResult Update([FromBody] CM_EXP_LC_HModel ob)
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
