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
    public class ExportLCPIController : ApiController
    {
        [Route("ExportLCPI/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCPI/SelectAll/{pageNo}/{pageSize}
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pMC_BUYER_ID = null,
            Int64? pRF_ACTN_STATUS_ID = null, string pPI_NO_EXP = null, string pSTYLE_ORDER_NO = null, string pPI_DT_EXP = null)
        {
            try
            {
                if (pPI_DT_EXP != null)
                    pPI_DT_EXP = Convert.ToDateTime(pPI_DT_EXP.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new CM_EXP_PI_HModel().SelectAll(pageNo, pageSize, pHR_COMPANY_ID, pMC_BUYER_ID, pRF_ACTN_STATUS_ID, pPI_NO_EXP, pSTYLE_ORDER_NO, pPI_DT_EXP);
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

        [Route("ExportLCPI/GetExportLcPIInfo")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCPI/GetExportLcPIInfo?pCM_EXP_PI_H_ID=
        public IHttpActionResult GetExportLcPIInfo(Int64? pCM_EXP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_PI_HModel().Select(pCM_EXP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ExportLCPI/GetOrderDtlInfo")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCPI/GetOrderDtlInfo?pMC_ORDER_H_ID=
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


        [Route("ExportLCPI/GetPOListByID")]
        [HttpGet]
        // GET :  /api/cmr/ExportLCPI/GetPOListByID?pCM_EXP_PI_H_ID=
        public IHttpActionResult GetPOListByID(Int64? pCM_EXP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_PI_D_POModel().GetPOListByID(pCM_EXP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ExportLCPI/Save")]
        [HttpPost]
        // GET :  api/Cmr/ExportLCPI/Save
        public IHttpActionResult Save([FromBody] CM_EXP_PI_HModel ob)
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


        [Route("ExportLCPI/Update")]
        [HttpPost]
        // GET :  api/Cmr/ExportLCPI/Update
        public IHttpActionResult Update([FromBody] CM_EXP_PI_HModel ob)
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
