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
    public class KnitYarnIssueChallanController : ApiController
    {
        [Route("YarnIssueChallan/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueChallan/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, string pCHALAN_DT = null, Int64? pUSER_ID = null)
        {
            if (pCHALAN_DT != null)
                pCHALAN_DT = Convert.ToDateTime(pCHALAN_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

            var data = new KNT_YRN_CHL_ISS_HModel().SelectAll(pageNo, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }


        [Route("YarnIssueChallan/GetYarnIssueChallanInfo")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueChallan/GetYarnIssueChallanInfo?pKNT_YRN_CHL_ISS_H_ID=
        public IHttpActionResult GetYarnIssueChallanInfo(Int64? pKNT_YRN_CHL_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_CHL_ISS_HModel().Select(pKNT_YRN_CHL_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("YarnIssueChallan/GetYarnIssueChallanInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/YarnIssueChallan/GetYarnIssueChallanInfoDtlByID?pKNT_YRN_CHL_ISS_H_ID=
        public IHttpActionResult GetYarnIssueChallanInfoDtlByID(Int64? pKNT_YRN_CHL_ISS_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_CHL_ISS_DModel().SelectByID(pKNT_YRN_CHL_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("YarnIssueChallan/Save")]
        [HttpPost]
        // GET :  api/knit/YarnIssueChallan/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_CHL_ISS_HModel ob)
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
