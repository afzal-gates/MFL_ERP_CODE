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
    public class KntYdRecvChalnController : ApiController
    {
        [Route("KntYdRecvChaln/GetYdRecvChalnList")]
        [HttpGet]
        // GET :  /api/knit/KntYdRecvChaln/GetYdRecvChalnList
        public IHttpActionResult GetYdRecvChalnList(Int32 pageNumber, Int32 pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, DateTime? pCHALAN_DT = null)
        {
            try
            {
                var obList = new KNT_YD_RCV_CHL_HModel().GetYdRecvChalnList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntYdRecvChaln/GetYdRecvChalnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntYdRecvChaln/GetYdRecvChalnHdr
        public IHttpActionResult GetYdRecvChalnHdr(Int64 pKNT_YD_RCV_CHL_H_ID)
        {
            try
            {
                var list = new KNT_YD_RCV_CHL_HModel().GetYdRecvChalnHdr(pKNT_YD_RCV_CHL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntYdRecvChaln/GetYdRecvChalnDtl")]
        [HttpGet]
        // GET :  /api/knit/KntYdRecvChaln/GetYdRecvChalnDtl
        public IHttpActionResult GetYdRecvChalnDtl(Int64? pKNT_YD_PRG_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pKNT_YD_RCV_CHL_H_ID = null)
        {
            try
            {
                var list = new KNT_YD_RCV_CHL_DModel().GetYdRecvChalnDtl(pKNT_YD_PRG_H_ID, pMC_FAB_PROD_ORD_H_ID, pKNT_YD_RCV_CHL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
          
        [Route("KntYdRecvChaln/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntYdRecvChaln/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_YD_RCV_CHL_HModel ob)
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

        [Route("KntYdRecvChaln/GetYdRecvChalnList4TrChaln")]
        [HttpGet]
        // GET :  /api/knit/KntYdRecvChaln/GetYdRecvChalnList4TrChaln
        public IHttpActionResult GetYdRecvChalnList4TrChaln(Int64 pSCM_SUPPLIER_ID)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_HModel().GetYdRecvChalnList4TrChaln(pSCM_SUPPLIER_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntYdRecvChaln/GetYdList4TrChaln")]
        [HttpGet]
        // GET :  /api/knit/KntYdRecvChaln/GetYdList4TrChaln
        public IHttpActionResult GetYdList4TrChaln(string pKNT_YD_RCV_CHL_H_LST)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_HModel().GetYdList4TrChaln(pKNT_YD_RCV_CHL_H_LST);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoYdTrChaln/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntScoYdTrChaln/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_SCO_YD_TR_CHL_HModel ob)
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

        [Route("KntScoYdTrChaln/GetScoYdTrChalnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntScoYdTrChaln/GetScoYdTrChalnHdr
        public IHttpActionResult GetScoYdTrChalnHdr(Int64 pKNT_SCO_YD_TR_CHL_H_ID)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_HModel().GetScoYdTrChalnHdr(pKNT_SCO_YD_TR_CHL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoYdTrChaln/GetScoYdTrChalnDtl")]
        [HttpGet]
        // GET :  /api/knit/KntScoYdTrChaln/GetScoYdTrChalnDtl
        public IHttpActionResult GetScoYdTrChalnDtl(Int64 pKNT_SCO_YD_TR_CHL_H_ID)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_DModel().GetScoYdTrChalnDtl(pKNT_SCO_YD_TR_CHL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoYdTrChaln/GetScoYdTrChlPrgList")]
        [HttpGet]
        // GET :  /api/knit/KntScoYdTrChaln/GetScoYdTrChlPrgList
        public IHttpActionResult GetScoYdTrChlPrgList(Int64 pSCM_SUPPLIER_ID, Int64? pKNT_SCO_YD_TR_CHL_H_ID = null)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_DModel().GetScoYdTrChlPrgList(pSCM_SUPPLIER_ID, pKNT_SCO_YD_TR_CHL_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoYdTrChaln/GetScoYdTrChalnList")]
        [HttpGet]
        // GET :  /api/knit/KntScoYdTrChaln/GetScoYdTrChalnList
        public IHttpActionResult GetScoYdTrChalnList(Int32 pageNumber, Int32 pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, DateTime? pCHALAN_DT = null)
        {
            try
            {
                var list = new KNT_SCO_YD_TR_CHL_HModel().GetScoYdTrChalnList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
