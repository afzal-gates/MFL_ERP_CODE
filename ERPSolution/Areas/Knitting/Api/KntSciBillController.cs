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
    public class KntSciBillController : ApiController
    {
        [Route("KntSciBill/GetChallanList4Bill")]
        [HttpGet]
        // GET :  /api/knit/KntSciBill/GetChallanList4Bill
        public IHttpActionResult GetChallanList4Bill(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pCHALAN_NO = null)
        {
            try
            {
                var obList = new KNT_SC_GFAB_DLV_HModel().GetChallanList4Bill(pageNumber, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciBill/GetBillHdr")]
        [HttpGet]
        // GET :  /api/knit/KntSciBill/GetBillHdr
        public IHttpActionResult GetBillHdr(Int64 pKNT_SCI_BILL_H_ID)
        {
            try
            {
                var list = new KNT_SCI_BILL_HModel().GetBillHdr(pKNT_SCI_BILL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciBill/GetBillDtl")]
        [HttpGet]
        // GET :  /api/knit/KntSciBill/GetBillDtl
        public IHttpActionResult GetBillDtl(Int64 pKNT_SCI_BILL_H_ID)
        {
            try
            {
                var list = new KNT_SCI_BILL_DModel().GetBillDtl(pKNT_SCI_BILL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciBill/GetBillList")]
        [HttpGet]
        // GET :  /api/knit/KntSciBill/GetBillList
        public IHttpActionResult GetBillList(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pBILL_NO = null, DateTime? pBILL_DT=null)
        {
            try
            {
                var obList = new KNT_SCI_BILL_HModel().GetBillList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pBILL_NO, pBILL_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciBill/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntSciBill/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_SCI_BILL_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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

        [Route("KntSciBill/SaveRevision")]
        [HttpPost]
        // GET :  api/knit/KntSciBill/SaveRevision
        public IHttpActionResult SaveRevision([FromBody] KNT_SCI_BILL_HModel ob)
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

        [Route("KntSciBill/GetSciBillProcUserLavel")]
        [HttpGet]
        // GET :  /api/knit/KntSciBill/GetSciBillProcUserLavel
        public IHttpActionResult GetSciBillProcUserLavel()
        {
            try
            {
                var list = new KNT_SCI_BILL_USR_LAVELModel().GetSciBillProcUserLavel();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
