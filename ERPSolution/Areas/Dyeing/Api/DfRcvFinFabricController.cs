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
    public class DfRcvFinFabricController : ApiController
    {

        [Route("DfRcvFinFabric/SelectRcvAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DfRcvFinFabric/SelectRcvAll
        public IHttpActionResult SelectRcvAll(int pageNo, int pageSize, string pRCV_REF_NO = null, string pRCV_DT = null, string pCHALAN_NO = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, string pDYE_BATCH_NO = null, Int64? pUSER_ID = null)
        {
            try
            {
                if (pRCV_DT != null)
                    pRCV_DT = Convert.ToDateTime(pRCV_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DF_SCO_FSTR_RCV_HModel().SelectAll(pageNo, pageSize, pRCV_REF_NO, pRCV_DT, pCHALAN_NO, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pDYE_BATCH_NO, pUSER_ID);
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

        [Route("DfRcvFinFabric/SelectForReceive")]
        [HttpGet]
        // GET :  /api/Dye/DfRcvFinFabric/SelectForReceive?pSCM_SUPPLIER_ID
        public IHttpActionResult SelectForReceive(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var fab = new DF_SCO_FSTR_RCV_D1Model().SelectForReceive(pSCM_SUPPLIER_ID);
                var CC = new DF_SCO_FSTR_RCV_D2Model().SelectForReceive(pSCM_SUPPLIER_ID);
                var Trim = new DF_SCO_FSTR_RCV_D3Model().SelectForReceive(pSCM_SUPPLIER_ID);
                return Ok(new { fab, CC, Trim });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfRcvFinFabric/SelectReceiveInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfRcvFinFabric/SelectReceiveInfo?pDF_SCO_FSTR_RCV_H_ID
        public IHttpActionResult SelectReceiveInfo(Int64? pDF_SCO_FSTR_RCV_H_ID = null)
        {
            try
            {
                var obj = new DF_SCO_FSTR_RCV_HModel().Select(pDF_SCO_FSTR_RCV_H_ID);
                return Ok(obj);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfRcvFinFabric/SelectReceiveInfoDtl")]
        [HttpGet]
        // GET :  /api/Dye/DfRcvFinFabric/SelectReceiveInfoDtl?pDF_SCO_FSTR_RCV_H_ID
        public IHttpActionResult SelectReceiveInfoDtl(Int64? pDF_SCO_FSTR_RCV_H_ID = null)
        {
            try
            {
                var fab = new DF_SCO_FSTR_RCV_D1Model().SelectAll(pDF_SCO_FSTR_RCV_H_ID);
                var CC = new DF_SCO_FSTR_RCV_D2Model().SelectAll(pDF_SCO_FSTR_RCV_H_ID);
                var Trim = new DF_SCO_FSTR_RCV_D3Model().SelectAll(pDF_SCO_FSTR_RCV_H_ID);
                return Ok(new { fab, CC, Trim });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfRcvFinFabric/Save")]
        [HttpPost]
        // GET :  api/Dye/DfRcvFinFabric/Save
        public IHttpActionResult Save([FromBody] DF_SCO_FSTR_RCV_HModel ob)
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
