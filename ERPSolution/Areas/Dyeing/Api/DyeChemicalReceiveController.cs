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
    public class DyeChemicalReceiveController : ApiController
    {
        [Route("DyeChemicalReceive/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalReceive/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pRF_REQ_TYPE_ID = null, string pDC_MRR_NO = null, string pDC_MRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pCHALAN_NO = null, string pIMP_LC_NO = null, string pREMARKS = null)
        {
            try
            {
                if (pDC_MRR_DT != null)
                    pDC_MRR_DT = Convert.ToDateTime(pDC_MRR_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DYE_DC_RCV_HModel().SelectAll(pageNo, pageSize, pRF_REQ_TYPE_ID, pDC_MRR_NO, pDC_MRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pCHALAN_NO, pIMP_LC_NO, pREMARKS);
                int total = 0;
                if (data.Count > 0)
                    total = data[0].TOTAL_REC;
                return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalReceive/GetDyeChemicalReceiveInfo")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfo
        public IHttpActionResult GetDyeChemicalReceiveInfo(Int64? pDYE_DC_RCV_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_RCV_HModel().Select(pDYE_DC_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalReceive/GetDyeChemicalReceiveInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DyeChemicalReceive/GetDyeChemicalReceiveInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDyeChemicalReceiveInfoDtlByID(Int64? pDYE_DC_RCV_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_RCV_DModel().Select(pDYE_DC_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeChemicalReceive/Save")]
        [HttpPost]
        // GET :  api/Dye/DyeChemicalReceive/Save
        public IHttpActionResult Save([FromBody] DYE_DC_RCV_HModel ob)
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

        [Route("DyeChemicalReceive/GetDYEOpeningBlance/{HR_COMPANY_ID}/{SCM_STORE_ID}/{INV_ITEM_CAT_ID}")]
        [HttpGet]
        // GET :  /api/dye/DyeChemicalReceive/GetDYEOpeningBlance
        public IHttpActionResult GetDYEOpeningBlance(Int64? HR_COMPANY_ID = null, Int64? SCM_STORE_ID = null, Int64? INV_ITEM_CAT_ID = null)
        {
            try
            {
                var list = new DYE_DC_RCV_OBModel().SelectAll(HR_COMPANY_ID, SCM_STORE_ID, INV_ITEM_CAT_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeChemicalReceive/SaveOB")]
        [HttpPost]
        // GET :  api/dye/DyeChemicalReceive/SaveOB
        public IHttpActionResult SaveOB([FromBody] DYE_DC_RCV_OBModel ob)
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

        [Route("DyeChemicalReceive/Finalize")]
        [HttpPost]
        // GET :  api/dye/DyeChemicalReceive/Finalize
        public IHttpActionResult Finalize([FromBody] DYE_DC_RCV_OBModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Finalize();
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
