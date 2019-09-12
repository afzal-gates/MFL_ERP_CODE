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
    public class ScPreTreatmentReceiveController : ApiController
    {
        [Route("ScPtReceive/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/ScPtReceive/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pRCV_DT = null, string pCHALAN_DT = null, string pCHALAN_NO = null,
            Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, Int64? pUSER_ID = null, string pSC_PRG_NO = null)
        {
            try
            {
                if (pRCV_DT != null)
                    pRCV_DT = Convert.ToDateTime(pRCV_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                if (pCHALAN_DT != null)
                    pCHALAN_DT = Convert.ToDateTime(pCHALAN_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DF_SC_PT_RCV_HModel().SelectAll(pageNo, pageSize, pMRR_NO, pRCV_DT, pCHALAN_DT, pCHALAN_NO, pHR_COMPANY_ID, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pUSER_ID, pSC_PRG_NO);
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

        [Route("ScPtReceive/GetScPtReceiveInfo")]
        [HttpGet]
        // GET :  /api/dye/ScPtReceive/GetScPtReceiveInfo
        public IHttpActionResult GetScPtReceiveInfo(Int64? pDF_SC_PT_RCV_H_ID = null)
        {
            var obList = new DF_SC_PT_RCV_HModel().Select(pDF_SC_PT_RCV_H_ID);
            return Ok(obList);
        }



        [Route("ScPtReceive/GetScPtRcvDtlByID")]
        [HttpGet]
        // GET :  /api/dye/ScPtReceive/GetScPtRcvDtlByID?pDF_SC_PT_RCV_H_ID=
        public IHttpActionResult GetScPtRcvDtlByID(Int64? pDF_SC_PT_RCV_H_ID = null, string pIS_TRANSFER = null)
        {
            if (pIS_TRANSFER == "X")
            {
                var obList = new DF_SC_PT_RTN_DModel().SelectAll(pDF_SC_PT_RCV_H_ID);
                return Ok(obList);
            }
            else
            {
                var obList = new DF_SC_PT_RCV_DModel().SelectAll(pDF_SC_PT_RCV_H_ID);
                return Ok(obList);
            }
        }



        [Route("ScPtReceive/GetScPtRtnRollInfo")]
        [HttpGet]
        // GET :  /api/dye/ScPtReceive/GetScPtRtnRollInfo?pKNT_STYL_FAB_ITEM_ID=&pFAB_ROLL_NO=
        public IHttpActionResult GetScPtRtnRollInfo(Int64? pKNT_STYL_FAB_ITEM_ID = null, string pFAB_ROLL_NO = null)
        {
            var obList = new DF_SC_PT_ISS_ROLLModel().SelectForScRtn(pKNT_STYL_FAB_ITEM_ID, pFAB_ROLL_NO);
            return Ok(obList);

        }


        [Route("ScPtReceive/Save")]
        [HttpPost]
        // GET :  api/Dye/ScPtReceive/Save
        public IHttpActionResult Save([FromBody] DF_SC_PT_RCV_HModel ob)
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
