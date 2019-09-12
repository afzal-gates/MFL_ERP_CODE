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
    [Authorize]
    public class KntScRcvController : ApiController
    {
        [Route("KntScRcv/GetFabOrdListByKntScPrgID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetFabOrdListByKntScPrgID
        public IHttpActionResult GetFabOrdListByKntScPrgID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            try
            {
                var obList = new KNT_SC_PRG_RCVModel().GetFabOrdListByKntScPrgID(pKNT_SC_PRG_RCV_ID);                
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScYrnRcvDtlListByHrdID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScYrnRcvDtlListByHrdID
        public IHttpActionResult GetScYrnRcvDtlListByHrdID(Int64 pKNT_SC_YRN_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SC_YRN_RCV_DModel().GetScYrnRcvDtlListByHrdID(pKNT_SC_YRN_RCV_H_ID);                
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScYrnRcvHdrListByPrgID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScYrnRcvHdrListByPrgID
        public IHttpActionResult GetScYrnRcvHdrListByPrgID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            try
            {
                var obList = new KNT_SC_YRN_RCV_HModel().GetScYrnRcvHdrListByPrgID(pKNT_SC_PRG_RCV_ID);
                //var data = list.Skip(pageNo - 1).Take(pageSize).ToList();
                //int total = list.Count();
                //return Ok(new { total, data });
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScRcvProgramByID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScRcvProgramByID
        public IHttpActionResult GetScRcvProgramByID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            try
            {
                var ob = new KNT_SC_PRG_RCVModel().GetScRcvProgramByID(pKNT_SC_PRG_RCV_ID);                
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScRcvProgramByPartyID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScRcvProgramByPartyID
        public IHttpActionResult GetScRcvProgramByPartyID(Int64? pSCM_SUPPLIER_ID, Int64 pageNumber, Int64 pageSize, string pSUP_TRD_NAME_EN = null, string pSC_STYLE_NO = null, string pSC_ORDER_NO = null, string pLK_SC_PRG_STATUS_ID = null)
        {
            try
            {
                var ob = new KNT_SC_PRG_RCVModel().GetScRcvProgramByPartyID(pSCM_SUPPLIER_ID, pageNumber, pageSize, pSUP_TRD_NAME_EN, pSC_STYLE_NO, pSC_ORDER_NO, pLK_SC_PRG_STATUS_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScRcvProgramList")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScRcvProgramList
        public IHttpActionResult GetScRcvProgramList(Int32? pSCM_SUPPLIER_ID=null, Int64? pKNT_SC_PRG_RCV_ID = null, string pPRG_RCV_NO = null)        
        {
            var obList = new KNT_SC_PRG_RCVModel().GetScRcvProgram(pSCM_SUPPLIER_ID, pKNT_SC_PRG_RCV_ID, pPRG_RCV_NO);
            return Ok(obList);
        }

        //[Route("KntScRcv/GetYarnRcvDtlList")]
        //[HttpGet]
        //// GET :  /api/knit/KntScRcv/GetYarnRcvDtlList?pKNT_SC_YRN_RCV_H_ID=1
        //public IHttpActionResult GetYarnRcvDtlList(Int64 pKNT_SC_YRN_RCV_H_ID)
        //{
        //    try
        //    {
        //        var ob = new KNT_SC_YRN_RCV_DModel().GetYarnRcvDtlList(pKNT_SC_YRN_RCV_H_ID);
        //        return Ok(ob);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

        [Route("KntScRcv/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntScRcv/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_SC_PRG_RCVModel ob)
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


        [Route("KntScRcv/BatchSaveSciChallan")]
        [HttpPost]
        // GET :  api/knit/KntScRcv/BatchSaveSciChallan
        public IHttpActionResult BatchSaveSciChallan([FromBody] KNT_SC_PRG_RCVModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveSciChallan();
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

        [Route("KntScRcv/FinalizeSciChallan")]
        [HttpPost]
        // GET :  api/knit/KntScRcv/FinalizeSciChallan
        public IHttpActionResult FinalizeSciChallan([FromBody] KNT_SC_YRN_RCV_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.FinalizeSciChallan();
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


        [Route("KntScRcv/GetScOrdRefByPartyID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScOrdRefByPartyID
        public IHttpActionResult GetScOrdRefByPartyID(Int64 pageNumber, Int64 pageSize, Int64? pSCM_SUPPLIER_ID, Int64? pKNT_SC_PRG_RCV_ID = null, string pSC_WO_REF_NO = null)
        {
            try
            {
                var ob = new SCM_SC_WO_REFModel().GetScOrdRefByPartyID(pageNumber, pageSize, pSCM_SUPPLIER_ID, pKNT_SC_PRG_RCV_ID, pSC_WO_REF_NO);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/SaveScOrdRef")]
        [HttpPost]
        // GET :  api/knit/KntScRcv/SaveScOrdRef
        public IHttpActionResult SaveScOrdRef([FromBody] SCM_SC_WO_REFModel ob)
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

        [Route("KntScRcv/GetYrnListByFabOrd")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetYrnListByFabOrd?pMC_FAB_PROD_ORD_D_ID=1
        public IHttpActionResult GetYrnListByFabOrd(Int64 pMC_FAB_PROD_ORD_D_ID)
        {
            try
            {
                var ob = new MC_FAB_PROD_D_YRNModel().GetYrnListByFabOrd(pMC_FAB_PROD_ORD_D_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetScOrdRefByPrgID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetScOrdRefByPrgID
        public IHttpActionResult GetScOrdRefByPrgID(Int64 pKNT_SC_PRG_RCV_ID)
        {
            try
            {
                var obList = new SCM_SC_WO_REFModel().GetScOrdRefByPrgID(pKNT_SC_PRG_RCV_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScRcv/GetYrnByScOrdRefID")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetYrnByScOrdRefID
        public IHttpActionResult GetYrnByScOrdRefID(Int64 pSCM_SC_WO_REF_ID)
        {
            try
            {
                var obList = new MC_FAB_PROD_D_YRNModel().GetYrnByScOrdRefID(pSCM_SC_WO_REF_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntScRcv/GetYrnSummery")]
        [HttpGet]
        // GET :  /api/knit/KntScRcv/GetYrnSummery
        public IHttpActionResult GetYrnSummery(Int64? pSCM_SC_WO_REF_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pKNT_YRN_LOT_ID = null,
            Int64? pLK_YFAB_PART_ID = null, Int64? pKNT_SC_YRN_RCV_D_ID = null, DateTime? pCHALAN_DT = null)
        {
            try
            {
                var obList = new KNT_SC_YRN_RCV_DModel().GetYrnSummery(pSCM_SC_WO_REF_ID, pYARN_ITEM_ID, pKNT_YRN_LOT_ID, pLK_YFAB_PART_ID, pKNT_SC_YRN_RCV_D_ID, pCHALAN_DT);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
    }
}
