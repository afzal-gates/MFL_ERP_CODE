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
    [RoutePrefix("api/Dye")]
    [NoCache]
    public class DfScProgramBillController : ApiController
    {
        [Route("DfScProgramBill/GetChallanList4Bill")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgramBill/GetChallanList4Bill
        public IHttpActionResult GetChallanList4Bill(int pageNo, int pageSize, Int64 pSCM_SUPPLIER_ID, string pCHALAN_NO = null)
        {
            try
            {
                var data = new DF_SCO_CHL_RCV_HModel().SelectScRcvForBill(pageNo, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO);
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

        [Route("DfScProgramBill/GetRcvList4AOP")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgramBill/GetRcvList4AOP
        public IHttpActionResult GetRcvList4AOP(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, Int64? pDYE_BT_CARD_H_ID = null, string pCHALAN_NO = null, string pIS_AOP = null)
        {
            try
            {
                var data = new DF_SCO_CHL_RCV_HModel().SelectScRcvForAOP(pageNo, pageSize, pSCM_SUPPLIER_ID, pDYE_BT_CARD_H_ID, pCHALAN_NO, pIS_AOP);
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

        [Route("DfScProgramBill/GetBillHdr")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgramBill/GetBillHdr
        public IHttpActionResult GetBillHdr(Int64? pDF_SCO_FP_BILL_H_ID = null)
        {
            try
            {
                var list = new DF_SCO_FP_BILL_HModel().Select(pDF_SCO_FP_BILL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfScProgramBill/GetBillDtl")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgramBill/GetBillDtl
        public IHttpActionResult GetBillDtl(Int64? pDF_SCO_FP_BILL_H_ID = null)
        {
            try
            {
                var list = new DF_SCO_FP_BILL_DModel().SelectByID(pDF_SCO_FP_BILL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfScProgramBill/GetBillList")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgramBill/GetBillList
        public IHttpActionResult GetBillList(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pBILL_NO = null, DateTime? pBILL_DT = null)
        {
            try
            {
                var data = new DF_SCO_FP_BILL_HModel().SelectAll(pageNumber, pageSize, pSCM_SUPPLIER_ID, pBILL_NO, pBILL_DT);
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

        [Route("DfScProgramBill/BatchSave")]
        [HttpPost]
        // GET :  api/Dye/DfScProgramBill/BatchSave
        public IHttpActionResult BatchSave([FromBody] DF_SCO_FP_BILL_HModel ob)
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

        [Route("DfScProgramBill/SaveRevision")]
        [HttpPost]
        // GET :  api/Dye/DfScProgramBill/SaveRevision
        public IHttpActionResult SaveRevision([FromBody] DF_SCO_FP_BILL_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveRevision();
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
