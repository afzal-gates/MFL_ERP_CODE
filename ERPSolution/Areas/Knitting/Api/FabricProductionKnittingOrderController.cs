using ERP.Model;
using ERP.Model.Purchase;
using ERPSolution.Hubs;
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
    public class FabricProductionKnittingOrderController : ApiController
    {
        [Route("FabProdKnitOrder/SelectAll/{pageNo}/{pageSize}/{pMC_BYR_ACC_ID}/{pMC_STYLE_H_ID}/{pRF_FAB_PROD_CAT_ID}/{pORDER_NO}/{pFIRSTDATE}")]
        [HttpGet]
        [NoCache]
        // GET :  /api/purchase/SupplierProfile/SelectAll
        public IHttpActionResult SelectAll(
             int pageNo,
            int pageSize,
            int pMC_BYR_ACC_ID,
            int pMC_STYLE_H_ID,
            int pRF_FAB_PROD_CAT_ID,
            string pORDER_NO,
            DateTime? pFIRSTDATE,
            string pHAS_KP = "N",
            String pHAS_COL_CUFF = "N",
            string pLK_COL_TYPE_ID_LST = "",
            Int32? pRF_FAB_TYPE_ID = null
         )
        {
            try
            {
                var data = new MC_FAB_PROD_ORD_HModel().SelectAll(pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pRF_FAB_PROD_CAT_ID, pFIRSTDATE, pageNo, pageSize, pHAS_KP, pHAS_COL_CUFF, pLK_COL_TYPE_ID_LST, pRF_FAB_TYPE_ID);
                int total = 0;

                if (data.Count > 0)
                    total = Convert.ToInt32(data.FirstOrDefault().TOTAL_REC.ToString());
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("FabProdKnitOrder/SelectShipmentMonth/{pMC_BYR_ACC_ID}/{pMC_STYLE_H_ID}/{pRF_FAB_PROD_CAT_ID}")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/SelectShipmentMonth
        public IHttpActionResult SelectShipmentMonth(int pMC_BYR_ACC_ID, int pMC_STYLE_H_ID, int pRF_FAB_PROD_CAT_ID, int? pMC_BYR_ACC_GRP_ID = null)
        {
            try
            {
                var data = new MC_FAB_PROD_ORD_HModel().SelectShipmentMonth(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pRF_FAB_PROD_CAT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetShipmentMonth")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetShipmentMonth
        public IHttpActionResult GetShipmentMonth(int? pMC_BYR_ACC_GRP_ID = null, int? pMC_BYR_ACC_ID = null, int? pMC_STYLE_H_ID = null, int? pRF_FAB_PROD_CAT_ID = null)
        {
            try
            {
                var data = new MC_FAB_PROD_ORD_HModel().SelectShipmentMonth(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pRF_FAB_PROD_CAT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/Select")]
        [HttpGet]
        [NoCache]
        // GET :  /api/purchase/SupplierProfile/SelectAll
        public IHttpActionResult Select(Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var data = new MC_FAB_PROD_ORD_HModel().Select(pMC_FAB_PROD_ORD_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("FabProdKnitOrder/SelectForKnitProdOrder/{MC_FAB_PROD_ORD_H_ID}")]
        [HttpGet]
        // GET :  /api/knit/FabProdKnitOrder/SelectForKnitProdOrder
        public IHttpActionResult SelectForKnitProdOrder(Int64 MC_FAB_PROD_ORD_H_ID)
        {
            try
            {
                var data = new MC_STYLE_D_FABModel().SelectForKnitProdOrder(MC_FAB_PROD_ORD_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/SelectByID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/purchase/SupplierProfile/SelectAll
        public IHttpActionResult SelectByID(Int64? pMC_FAB_PROD_ORD_H_ID = null, String pONLY_COLLAR = null, Int32? pOption = 3001, string pLK_COL_TYPE_ID_LST = "", Int64? pMC_FAB_PROD_ORD_D_ID = null, string pHAS_YD = null, Int32? pRF_FAB_TYPE_ID = null)
        {
            try
            {
                var list = new MC_FAB_PROD_ORD_DModel().SelectByID(pMC_FAB_PROD_ORD_H_ID, pONLY_COLLAR, pOption, pLK_COL_TYPE_ID_LST, pMC_FAB_PROD_ORD_D_ID, pHAS_YD, pRF_FAB_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("FabProdKnitOrder/getFabOrderDataForBatchProgram")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/FabProdKnitOrder/getFabOrderDataForBatchProgram
        public IHttpActionResult getFabOrderDataForBatchProgram(
             Int64 pageNumber,
             Int64 pageSize,
             Int64? pMC_BYR_ACC_ID,
             string pRF_FAB_PROD_CAT_ID_LST = null,
             DateTime? pFIRSTDATE = null,
             DateTime? pLASTDATE = null,
             Int64? pMC_FAB_PROD_ORD_H_ID = null,
             Int64? pLK_COL_TYPE_ID = null,
            Int64? pMC_COLOR_ID = null
        )
        {
            try
            {
                var list = new MC_FAB_PROD_ORD_DModel().getFabOrderDataForBatchProgram(pageNumber, pageSize, pMC_BYR_ACC_ID, pRF_FAB_PROD_CAT_ID_LST, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID, pLK_COL_TYPE_ID, pMC_COLOR_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }


        [Route("FabProdKnitOrder/getFabOrderDataForSampleBatchProgram")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/FabProdKnitOrder/getFabOrderDataForSampleBatchProgram
        public IHttpActionResult getFabOrderDataForSampleBatchProgram(
            Int64 pageNumber,
            Int64 pageSize,
            Int64? pMC_BYR_ACC_ID,
            Int64? pMC_BYR_ACC_GRP_ID,
            string pRF_FAB_PROD_CAT_ID_LST = null,
            DateTime? pFIRSTDATE = null,
            DateTime? pLASTDATE = null,
            Int64? pMC_FAB_PROD_ORD_H_ID = null,
            Int64? pLK_COL_TYPE_ID = null,
            Int64? pMC_COLOR_ID = null,
            string pJOB_CRD_NO = null,
            Int64? pOption = null
        )
        {
            try
            {
                var list = new MC_FAB_PROD_ORD_DModel().getFabOrderDataForSampleBatchProgram(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_BYR_ACC_GRP_ID, pRF_FAB_PROD_CAT_ID_LST, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID, pLK_COL_TYPE_ID, pMC_COLOR_ID, pJOB_CRD_NO, pOption);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("FabProdKnitOrder/getFabOrderDataForScProgram")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/FabProdKnitOrder/getFabOrderDataForScProgram
        public IHttpActionResult getFabOrderDataForScProgram(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID, Int64? pMC_STYLE_H_EXT_ID = null, string pRF_FAB_PROD_CAT_ID_LST = null, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var list = new MC_FAB_PROD_ORD_DModel().getFabOrderDataForScProgram(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID, pRF_FAB_PROD_CAT_ID_LST, pFIRSTDATE, pLASTDATE, pMC_FAB_PROD_ORD_H_ID, pMC_COLOR_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("FabProdKnitOrder/GetColorListByProdOrdID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/mrc/Order/GetColorListByProdOrdID
        public IHttpActionResult GetColorListByProdOrdID(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            var obList = new MC_COLORModel().GetColorListByProdOrdID(pMC_FAB_PROD_ORD_H_ID);
            return Ok(obList);
        }


        [Route("FabProdKnitOrder/GetFabOrdListByID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetFabOrdListByID
        public IHttpActionResult GetFabOrdListByID(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            try
            {
                var obList = new MC_FAB_PROD_ORD_DModel().GetFabOrdListByID(pMC_FAB_PROD_ORD_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetFabProdOrdHdr")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetFabProdOrdHdr
        public IHttpActionResult GetFabProdOrdHdr(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID)
        {
            try
            {
                var obList = new MC_FAB_PROD_ORD_HModel().GetFabProdOrdHdr(pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_EXT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetFabProdOrdHdrByStyleId")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetFabProdOrdHdrByStyleId
        public IHttpActionResult GetFabProdOrdHdrByStyleId(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_ID)
        {
            try
            {
                var obList = new MC_FAB_PROD_ORD_HModel().GetFabProdOrdHdrByStyleId(pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/SaveFabDevKnitOrd")]
        [HttpPost]
        [NoCache]
        // GET :  api/knit/FabProdKnitOrder/SaveFabDevKnitOrd
        public IHttpActionResult SaveFabDevKnitOrd([FromBody] MC_FAB_PROD_ORD_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveFabDevKnitOrd();
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

        [Route("FabProdKnitOrder/SaveFabProdOrdMnul")]
        [HttpPost]
        [NoCache]
        // GET :  api/knit/FabProdKnitOrder/SaveFabProdOrdMnul
        public IHttpActionResult SaveFabProdOrdMnul([FromBody] MC_FAB_PROD_ORD_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveFabProdOrdMnul();
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

        [Route("FabProdKnitOrder/MoveToYDKP")]
        [HttpPost]
        [NoCache]
        // GET :  api/knit/FabProdKnitOrder/MoveToYDKP
        public IHttpActionResult MoveToYDKP([FromBody] MC_FAB_PROD_ORD_DModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MoveToYDKP();
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






        //=========================== Start Short Fabric Booking ===============
        [Route("FabProdKnitOrder/SaveFabProdOrdH")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/SaveFabProdOrdH
        public IHttpActionResult SaveFabProdOrdH([FromBody] KNT_SRT_FAB_REQ_HModel ob)
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

        [Route("FabProdKnitOrder/SubmitSrtFabProdOrdH")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/SubmitSrtFabProdOrdH
        public IHttpActionResult SubmitSrtFabProdOrdH([FromBody] KNT_SRT_FAB_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SubmitSrtFabProdOrdH();
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

        [Route("FabProdKnitOrder/ApproveSrtFabProdOrdH")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/ApproveSrtFabProdOrdH
        public IHttpActionResult ApproveSrtFabProdOrdH([FromBody] KNT_SRT_FAB_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ApproveSrtFabProdOrdH();
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

        [Route("FabProdKnitOrder/RevisionSrtFabProdOrdH")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/RevisionSrtFabProdOrdH
        public IHttpActionResult RevisionSrtFabProdOrdH([FromBody] KNT_SRT_FAB_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RevisionSrtFabProdOrdH();
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

        [Route("FabProdKnitOrder/SaveFabProdOrdD1")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/SaveFabProdOrdD1
        public IHttpActionResult SaveFabProdOrdD1([FromBody] KNT_SRT_FAB_REQ_D1Model ob)
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

        [Route("FabProdKnitOrder/SaveFabProdOrdD2")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/SaveFabProdOrdD2
        public IHttpActionResult SaveFabProdOrdD2([FromBody] KNT_SRT_FAB_REQ_D2Model ob)
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

        [Route("FabProdKnitOrder/SaveFabProdOrdD3")]
        [HttpPost]
        // GET :  api/knit/FabProdKnitOrder/SaveFabProdOrdD3
        public IHttpActionResult SaveFabProdOrdD3([FromBody] KNT_SRT_FAB_REQ_D3Model ob)
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


        [Route("FabProdKnitOrder/GetSrtFabProdOrdHdr")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabProdOrdHdr
        public IHttpActionResult GetSrtFabProdOrdHdr(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_HModel().GetSrtFabProdOrdHdr(pKNT_SRT_FAB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtFabBookingList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabBookingList
        public IHttpActionResult GetSrtFabBookingList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_HModel().GetSrtFabBookingList(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtFabBookingAprovList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabBookingAprovList
        public IHttpActionResult GetSrtFabBookingAprovList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pSFAB_REQ_NO = null)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_HModel().GetSrtFabBookingAprovList(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID, pSFAB_REQ_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetFinDiaByProdOrdId")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetFinDiaByProdOrdId
        public IHttpActionResult GetFinDiaByProdOrdId(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID)
        {
            try
            {
                var obList = new MC_FAB_PROD_ORD_DModel().GetFinDiaByProdOrdId(pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_EXT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("FabProdKnitOrder/GetSrtFabDtlByID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabDtlByID
        public IHttpActionResult GetSrtFabDtlByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_D1Model().GetSrtFabDtlByID(pKNT_SRT_FAB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtFabReasonByID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabReasonByID
        public IHttpActionResult GetSrtFabReasonByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_D2Model().GetSrtFabReasonByID(pKNT_SRT_FAB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtFabResponsibilityByID")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabResponsibilityByID
        public IHttpActionResult GetSrtFabResponsibilityByID(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_D3Model().GetSrtFabResponsibilityByID(pKNT_SRT_FAB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtCollarCuffReq")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtCollarCuffReq
        public IHttpActionResult GetSrtCollarCuffReq(Int64 pMC_STYLE_H_EXT_ID, Int64 pFAB_COLOR_ID, Int64 pKNT_SRT_FAB_REQ_D1_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_D11Model().GetSrtCollarCuffReq(pMC_STYLE_H_EXT_ID, pFAB_COLOR_ID, pKNT_SRT_FAB_REQ_D1_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("FabProdKnitOrder/GetSrtFabReqRpt")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabReqRpt
        public IHttpActionResult GetSrtFabReqRpt(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_RPTModel().GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FabProdKnitOrder/GetSrtFabReqUserLavel")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/FabProdKnitOrder/GetSrtFabReqUserLavel
        public IHttpActionResult GetSrtFabReqUserLavel()
        {
            try
            {
                var obList = new KNT_SRT_FAB_REQ_HModel().GetSrtFabReqUserLavel();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        //=========================== End Short Fabric Booking ===============

    }
}
