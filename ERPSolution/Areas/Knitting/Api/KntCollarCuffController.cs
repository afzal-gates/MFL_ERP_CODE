using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    [System.Web.Http.Authorize]
    public class KntCollarCuffController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("KntCollarCuff/GetCollarCuffOrdReq")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuffOrdReq
        public IHttpActionResult GetCollarCuffOrdReq(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pORDER_NO = null,
            Int64? pRF_FAB_PROD_CAT_ID = null, string pFIRSTDATE = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var obList = new MC_CLCF_ORD_REQModel().GetCollarCuffOrdReq(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID, pORDER_NO, pRF_FAB_PROD_CAT_ID,
                    pFIRSTDATE, pMC_FAB_PROD_ORD_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntCollarCuff/GetCollarCuffOrdReqDtl")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuffOrdReqDtl
        public IHttpActionResult GetCollarCuffOrdReqDtl(Int64 pMC_FAB_PROD_ORD_H_ID, Int64 pMC_STYLE_H_ID)
        {
            try
            {
                var obList = new MC_CLCF_ORD_REQModel().GetCollarCuffOrdReqDtl(pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetCollarCuffProd")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuffProd
        public IHttpActionResult GetCollarCuffProd(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID, Int64 pMC_COLOR_ID, DateTime pPROD_DT)
        {
            try
            {
                var obList = new KNT_CLCF_ORD_PRDModel().GetCollarCuffProd(pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID, pPROD_DT);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetCollarCuffDesigType")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuffDesigType
        public IHttpActionResult GetCollarCuffDesigType(Int64 pRF_FAB_PROD_CAT_ID, Int64 pMC_STYLE_H_EXT_ID, Int64 pMC_COLOR_ID)
        {
            try
            {
                var obList = new KNT_CLCF_DESIG_TYPE_VM().GetCollarCuffDesigType(pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntCollarCuff/GetCollarCuffYarnList")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuffYarnList
        public IHttpActionResult GetCollarCuffYarnList(Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var obList = new KNT_CLCF_YARN_VM().GetCollarCuffYarnList(pMC_FAB_PROD_ORD_H_ID, pRF_FAB_PROD_CAT_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/BatchSaveCollarCuffProd")]
        [HttpPost]
        // GET :  api/knit/KntCollarCuff/BatchSaveCollarCuffProd
        public IHttpActionResult BatchSaveCollarCuffProd([FromBody] KNT_CLCF_ORD_PRDModel ob)
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


        [Route("KntCollarCuff/GetOrdCol4StrRcv")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetOrdCol4StrRcv
        public IHttpActionResult GetOrdCol4StrRcv(Int32? pLK_CLCF_CHL_TYP_ID = null, Int32? pSCM_SUPPLIER_ID = null, string pSTYLE_NO = null, string pWORK_STYLE_NO = null, string pMC_ORDER_NO_LST = null, string pCOLOR_NAME_EN = null, string pSCO_PRG_NO = null, string pRF_FAB_PROD_CAT_ID_LST = null)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetOrdCol4StrRcv(pLK_CLCF_CHL_TYP_ID, pSCM_SUPPLIER_ID, pSTYLE_NO, pWORK_STYLE_NO, pMC_ORDER_NO_LST, pCOLOR_NAME_EN, pSCO_PRG_NO, pRF_FAB_PROD_CAT_ID_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetCollarCuff4StrRcv")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetCollarCuff4StrRcv
        public IHttpActionResult GetCollarCuff4StrRcv(Int32? pLK_CLCF_CHL_TYP_ID = null, DateTime? pRCV_DT = null, Int32? pSCM_STORE_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetCollarCuff4StrRcv(pLK_CLCF_CHL_TYP_ID, pRCV_DT, pSCM_STORE_ID, pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetClcf4StrRcvByScoProg")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetClcf4StrRcvByScoProg
        public IHttpActionResult GetClcf4StrRcvByScoProg(Int32? pLK_CLCF_CHL_TYP_ID = null, DateTime? pRCV_DT = null, Int32? pSCM_STORE_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetClcf4StrRcvByScoProg(pLK_CLCF_CHL_TYP_ID, pRCV_DT, pSCM_STORE_ID, pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID, pKNT_SCO_CLCF_PRG_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetClcfChlnType")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetClcfChlnType
        public IHttpActionResult GetClcfChlnType()
        {
            try
            {
                var obList = new LookupDataModel().GetClcfChlnType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/GetClcfInternalChlnHdr")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetClcfInternalChlnHdr
        public IHttpActionResult GetClcfInternalChlnHdr(Int64 pKNT_SCO_CHL_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetClcfInternalChlnHdr(pKNT_SCO_CHL_RCV_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntCollarCuff/GetChlnDtl4ClcfStrRcv")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetChlnDtl4ClcfStrRcv
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


        [Route("KntCollarCuff/GetClcfInternalChlnList")]
        [HttpGet]
        // GET :  /api/knit/KntCollarCuff/GetClcfInternalChlnList
        public IHttpActionResult GetClcfInternalChlnList(Int64 pageNumber, Int64 pageSize, int pCLCF_SRC_PROD_CAT_ID)
        {
            try
            {
                var obList = new KNT_SCO_CHL_RCV_HModel().GetClcfInternalChlnList(pageNumber, pageSize, pCLCF_SRC_PROD_CAT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntCollarCuff/BatchSaveClcfInternalRcv")]
        [HttpPost]
        // GET :  api/knit/KntCollarCuff/BatchSaveClcfInternalRcv
        public IHttpActionResult BatchSaveClcfInternalRcv([FromBody] KNT_SCO_CHL_RCV_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveClcfInternalRcv();
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


        [Route("KntCollarCuff/SubmitClcfInternalChlnRcv")]
        [HttpPost]
        // GET :  api/knit/KntCollarCuff/SubmitClcfInternalChlnRcv
        public IHttpActionResult SubmitClcfInternalChlnRcv([FromBody] KNT_SCO_CHL_RCV_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SubmitClcfInternalChlnRcv();
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
            Hub.Clients.All.broadcastMcOilReq4SubStrList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("KntCollarCuff/FinalizeClcfInternalChlnRcv")]
        [HttpPost]
        // GET :  api/knit/KntCollarCuff/FinalizeClcfInternalChlnRcv
        public IHttpActionResult FinalizeClcfInternalChlnRcv([FromBody] KNT_SCO_CHL_RCV_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.FinalizeClcfInternalChlnRcv();
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
            Hub.Clients.All.broadcastMcOilReq4SubStrList();
            return Ok(new { success = true, jsonStr });
        }
        

        //================ Start Collar/Cuff S/C Out-house ========================
        [Route("ScoCollarCuff/GetScoCollarCuffProgList")]
        [HttpGet]
        // GET :  /api/knit/ScoCollarCuff/GetScoCollarCuffProgList
        public IHttpActionResult GetScoCollarCuffProgList(Int64 pageNumber, Int64 pageSize, Int64? pSCM_SUPPLIER_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null, string pSCO_PRG_NO = null)
        {
            try
            {
                var obList = new KNT_SCO_CLCF_PRG_HModel().GetScoCollarCuffProgList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pKNT_SCO_CLCF_PRG_H_ID, pSCO_PRG_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ScoCollarCuff/GetScoCollarCuffHdr")]
        [HttpGet]
        // GET :  /api/knit/ScoCollarCuff/GetScoCollarCuffHdr
        public IHttpActionResult GetScoCollarCuffHdr(Int64 pKNT_SCO_CLCF_PRG_H_ID, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var obList = new KNT_SCO_CLCF_PRG_HModel().GetScoCollarCuffHdr(pKNT_SCO_CLCF_PRG_H_ID, pMC_FAB_PROD_ORD_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ScoCollarCuff/GetScoCollarCuffDtl")]
        [HttpGet]
        // GET :  /api/knit/ScoCollarCuff/GetScoCollarCuffDtl
        public IHttpActionResult GetScoCollarCuffDtl(Int64 pKNT_SCO_CLCF_PRG_H_ID, Int64? pMC_FAB_PROD_ORD_H_ID = null, string pRF_GARM_PART_LST = null)
        {
            try
            {
                var obList = new KNT_SCO_CLCF_PRG_DModel().GetScoCollarCuffDtl(pKNT_SCO_CLCF_PRG_H_ID, pMC_FAB_PROD_ORD_H_ID, pRF_GARM_PART_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ScoCollarCuff/GetScoClCfProgRateAssign")]
        [HttpGet]
        // GET :  /api/knit/ScoCollarCuff/GetScoClCfProgRateAssign
        public IHttpActionResult GetScoClCfProgRateAssign(string pRF_GARM_PART_LST = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pKNT_SCO_CLCF_PRG_H_ID = null)
        {
            var obList = new RF_GARM_PARTModel().GetScoClCfProgRateAssign(pRF_GARM_PART_LST, pMC_FAB_PROD_ORD_H_ID, pKNT_SCO_CLCF_PRG_H_ID);
            return Ok(obList);
        }

        [Route("ScoCollarCuff/ScoCollarCuffBatchSave")]
        [HttpPost]
        // GET :  /api/knit/ScoCollarCuff/ScoCollarCuffBatchSave
        public IHttpActionResult ScoCollarCuffBatchSave([FromBody] KNT_SCO_CLCF_PRG_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ScoCollarCuffBatchSave();
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

        [Route("ScoCollarCuff/ScoCollarCuffProgFinalize")]
        [HttpPost]
        // GET :  /api/knit/ScoCollarCuff/ScoCollarCuffProgFinalize
        public IHttpActionResult ScoCollarCuffProgFinalize([FromBody] KNT_SCO_CLCF_PRG_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ScoCollarCuffProgFinalize();
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

        [Route("ScoCollarCuff/ScoCollarCuffProgCancel")]
        [HttpPost]
        // GET :  /api/knit/ScoCollarCuff/ScoCollarCuffProgCancel
        public IHttpActionResult ScoCollarCuffProgCancel([FromBody] KNT_SCO_CLCF_PRG_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ScoCollarCuffProgCancel();
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
        //================ End Collar/Cuff S/C Out-house ========================

        
    }
}
