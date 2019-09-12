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
    public class KntScChlnController : ApiController
    {
        [Route("KntScChlnRcv/GetScoOrdStyleListByHasKnitCard")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetScoOrdStyleListByHasKnitCard
        public IHttpActionResult GetScoOrdStyleListByHasKnitCard(Int64 pSCM_SUPPLIER_ID, string pSTYLE_NO = null)
        {
            try
            {
                var obList = new MC_STYLE_H_EXTModel().GetScoOrdStyleListByHasKnitCard(pSCM_SUPPLIER_ID, pSTYLE_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRcv/GetScPrgIssList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetScPrgIssList
        public IHttpActionResult GetScPrgIssList(Int64? pSCM_SUPPLIER_ID = null, Int64? pKNT_SCO_CHL_RCV_H_ID = null, string pSEARCH_STR = null)
        {
            try
            {
                var obList = new KNT_SC_PRG_ISS_VModel().GetScPrgIssList(pSCM_SUPPLIER_ID, pKNT_SCO_CHL_RCV_H_ID, pSEARCH_STR);                
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRcv/GetQcStatusTypeList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetQcStatusTypeList
        public IHttpActionResult GetQcStatusTypeList(string pKNT_QC_STS_TYPE_ID_LST = null)
        {
            try
            {
                var obList = new KNT_QC_STS_TYPEModel().GetQcStatusTypeList(pKNT_QC_STS_TYPE_ID_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRcv/GetRcvChallanList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetRcvChallanList
        public IHttpActionResult GetRcvChallanList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, Int32? pSCM_STORE_ID = null, DateTime? pRCV_DT = null,
            string pCHALAN_NO = null, DateTime? pCHALAN_DT = null, string pBYR_ACC_NAME_EN = null, string pSTYLE_NO = null, string pORDER_NO_LST = null,
            string pIS_TRANSFER = null, string pIS_FINALIZED = null)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetRcvChallanList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pRCV_DT,
                    pCHALAN_NO, pCHALAN_DT, pBYR_ACC_NAME_EN, pSTYLE_NO, pORDER_NO_LST, pIS_TRANSFER, pIS_FINALIZED);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntScChlnRcv/GetRcvChallanHdr")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetRcvChallanHdr
        public IHttpActionResult GetRcvChallanHdr(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetRcvChallanHdr(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnRcv/GetScoFabRcvList")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnRcv/GetScoFabRcvList
        public IHttpActionResult GetScoFabRcvList(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_DModel().GetScoFabRcvList(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnRcv/GetChlnDtl4ClcfStrRcv")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnRcv/GetChlnDtl4ClcfStrRcv
        public IHttpActionResult GetChlnDtl4ClcfStrRcv(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_CLCF_RCV_DModel().GetChlnDtl4ClcfStrRcv(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntScChlnRcv/GetYrnRcvList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRcv/GetFabYrnList
        public IHttpActionResult GetYrnRcvList(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_YRN_RET_DModel().GetYrnRcvList(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRcv/GetYarnListByProdOrdHdr")]
        [HttpGet]
        // GET :  /api/Knit/KntScChlnRcv/GetYarnListByProdOrdHdr
        public IHttpActionResult GetYarnListByProdOrdHdr(Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var obList = new YRN_SPEC_DtlModel().GetYarnListByProdOrdHdr(pMC_FAB_PROD_ORD_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("KntScChlnRcv/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntScChlnRcv/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_SCO_CHL_RCV_HModel ob)
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


        [Route("KntScChlnRtn/GetScoRtnChalnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRtn/GetScoRtnChalnHdr
        public IHttpActionResult GetScoRtnChalnHdr(Int64 pKNT_SCO_GFAB_RET_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_GFAB_RET_HModel().GetScoRtnChalnHdr(pKNT_SCO_GFAB_RET_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRtn/GetScoQcRejFabList4Rtn")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRtn/GetScoQcRejFabList4Rtn
        public IHttpActionResult GetScoQcRejFabList4Rtn(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new KNT_SC_PRG_ISS_VModel().GetScoQcRejFabList4Rtn(pSCM_SUPPLIER_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRtn/GetFabRtnList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRtn/GetFabRtnList
        public IHttpActionResult GetFabRtnList(Int64 pKNT_SCO_GFAB_RET_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_GFAB_RET_DModel().GetFabRtnList(pKNT_SCO_GFAB_RET_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRtn/BatchSave4ScoFabRtn")]
        [HttpPost]
        // GET :  api/knit/KntScChlnRtn/BatchSave4ScoFabRtn
        public IHttpActionResult BatchSave4ScoFabRtn([FromBody] KNT_SCO_GFAB_RET_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave4ScoFabRtn();
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

        [Route("KntScChlnRtn/GetScoRtnChallanList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRtn/GetScoRtnChallanList
        public IHttpActionResult GetScoRtnChallanList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, Int32? pSCM_STORE_ID = null, 
            string pRET_CHALAN_NO = null, DateTime? pRET_CHALAN_DT = null, string pSTYLE_NO = null, string pORDER_NO_LST = null)
        {
            try
            {
                var obList = new KNT_SCO_GFAB_RET_HModel().GetScoRtnChallanList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pSCM_STORE_ID, 
                    pRET_CHALAN_NO, pRET_CHALAN_DT, pSTYLE_NO, pORDER_NO_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //=================
        [Route("KntSciChlnRtn/GetSciRtnChalnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntSciChlnRtn/GetSciRtnChalnHdr
        public IHttpActionResult GetSciRtnChalnHdr(Int64 pKNT_SCI_GFAB_RET_H_ID)
        {
            try
            {
                var obList = new KNT_SCI_GFAB_RET_HModel().GetSciRtnChalnHdr(pKNT_SCI_GFAB_RET_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciChlnRtn/GetSciQcRejFabList4Rtn")]
        [HttpGet]
        // GET :  /api/knit/KntSciChlnRtn/GetSciQcRejFabList4Rtn
        public IHttpActionResult GetSciQcRejFabList4Rtn(Int64? pSCM_SUPPLIER_ID = null, string pSEARCH_STR = null)
        {
            try
            {
                var obList = new KNT_SC_PRG_ISS_VModel().GetSciQcRejFabList4Rtn(pSCM_SUPPLIER_ID, pSEARCH_STR);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntSciChlnRtn/GetSciFabRtnList")]
        [HttpGet]
        // GET :  /api/knit/KntSciChlnRtn/GetSciFabRtnList
        public IHttpActionResult GetSciFabRtnList(Int64 pKNT_SCI_GFAB_RET_H_ID)
        {
            try
            {
                var obList = new KNT_SCI_GFAB_RET_DModel().GetSciFabRtnList(pKNT_SCI_GFAB_RET_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScChlnRtn/BatchSave4SciFabRtn")]
        [HttpPost]
        // GET :  api/knit/KntScChlnRtn/BatchSave4SciFabRtn
        public IHttpActionResult BatchSave4SciFabRtn([FromBody] KNT_SCI_GFAB_RET_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave4SciFabRtn();
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

        [Route("KntScChlnRtn/GetSciRtnChallanList")]
        [HttpGet]
        // GET :  /api/knit/KntScChlnRtn/GetSciRtnChallanList
        public IHttpActionResult GetSciRtnChallanList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, Int32? pSCM_STORE_ID = null,
            string pRET_CHALAN_NO = null, DateTime? pRET_CHALAN_DT = null, string pSTYLE_NO = null, string pORDER_NO_LST = null)
        {
            try
            {
                var obList = new KNT_SCI_GFAB_RET_HModel().GetSciRtnChallanList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pSCM_STORE_ID,
                    pRET_CHALAN_NO, pRET_CHALAN_DT, pSTYLE_NO, pORDER_NO_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        //=================



        [Route("KntScoChlnTrans/GetTransChlnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnTrans/GetTransChlnHdr
        public IHttpActionResult GetTransChlnHdr(Int64 pKNT_SCO_YRN_TR_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_YRN_TR_HModel().GetTransChlnHdr(pKNT_SCO_YRN_TR_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnTrans/GetTransChlnYrnDtl")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnTrans/GetTransChlnYrnDtl
        public IHttpActionResult GetTransChlnYrnDtl(Int64 pKNT_SCO_YRN_TR_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_YRN_TR_DModel().GetTransChlnYrnDtl(pKNT_SCO_YRN_TR_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnTrans/GetChlnWiseTransferedYrn")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnTrans/GetChlnWiseTransferedYrn
        public IHttpActionResult GetChlnWiseTransferedYrn(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_YRN_RET_DModel().GetChlnWiseTransferedYrn(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnTrans/GetTransChlnList")]
        [HttpGet]
        // GET :  /api/knit/KntScoChlnTrans/GetTransChlnList
        public IHttpActionResult GetTransChlnList(int pageNumber, int pageSize, Int32? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, DateTime? pCHALAN_DT = null, 
            string pSTYLE_NO = null, string pORDER_NO_LST = null)
        {
            try
            {
                var obList = new KNT_SCO_YRN_TR_HModel().GetTransChlnList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT, pSTYLE_NO, pORDER_NO_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntScoChlnTrans/BatchSave4ScoYrnTransfer")]
        [HttpPost]
        // GET :  api/knit/KntScoChlnTrans/BatchSave4ScoYrnTransfer
        public IHttpActionResult BatchSave4ScoYrnTransfer([FromBody] KNT_SCO_YRN_TR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave4ScoYrnTransfer();
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
