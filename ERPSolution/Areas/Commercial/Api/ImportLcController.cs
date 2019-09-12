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
    public class ImportLcController : ApiController
    {
        [Route("ImportLC/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/cmr/ImportLC/SelectAll/{pageNo}/{pageSize}
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null,
                                            Int64? pLK_LC_STS_ID = null, string pIMP_LC_NO = null, string pLC_PI_NO = null, string pISSUE_DT = null, Int64? pRF_ACTN_STATUS_ID = null)
        {
            try
            {
                if (pISSUE_DT != null)
                    pISSUE_DT = Convert.ToDateTime(pISSUE_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new CM_IMP_LC_HModel().SelectAll(pageNo, pageSize, pHR_COMPANY_ID, pSCM_SUPPLIER_ID, pLK_LC_STS_ID, pIMP_LC_NO, pLC_PI_NO, pISSUE_DT, pRF_ACTN_STATUS_ID);
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

        [Route("ImportLC/GetImportLc")]
        [HttpGet]
        // GET :  /api/cmr/ImportLC/GetImportLc?pCM_IMP_LC_H_ID=
        public IHttpActionResult GetImportLc(Int64? pCM_IMP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_LC_HModel().Select(pCM_IMP_LC_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ImportLC/GetImpLcDtlInfo")]
        [HttpGet]
        // GET :  /api/cmr/ImportLC/GetImpLcDtlInfo?pCM_IMP_LC_H_ID=
        public IHttpActionResult GetImpLcDtlInfo(Int64? pCM_IMP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_LC_DModel().SelectByID(pCM_IMP_LC_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ImportLC/GetImportLcByID")]
        [HttpGet]
        // GET :  /api/cmr/ImportLC/GetImportLcByID?pMC_BUYER_ID=&pCM_EXP_LC_H_ID=
        public IHttpActionResult GetImportLcByID(Int64? pMC_BUYER_ID = null, Int64? pCM_EXP_LC_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_LC_HModel().SelectByID(pMC_BUYER_ID,pCM_EXP_LC_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("ImportLC/Save")]
        [HttpPost]
        // GET :  api/Cmr/ImportLC/Save
        public IHttpActionResult Save([FromBody] CM_IMP_LC_HModel ob)
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


        [Route("ImportLC/Update")]
        [HttpPost]
        // GET :  api/Cmr/ImportLC/Update
        public IHttpActionResult Update([FromBody] CM_IMP_LC_HModel ob)
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
