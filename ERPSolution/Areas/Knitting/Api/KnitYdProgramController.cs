using ERP.Model;
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
    [Authorize]
    public class KnitYdProgramController : ApiController
    {
        [Route("KnitYdProgram/getBaseColorsByYdCol")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getBaseColorsByYdCol?pMC_FAB_PROD_ORD_H_LST=1&pKNT_YD_PRG_H_ID=1&pPARENT_ID
        public IHttpActionResult getBaseColorsByYdCol(string pMC_FAB_PROD_ORD_H_LST, Int64? pKNT_YD_PRG_H_ID = null, Int64? pPARENT_ID = null)
        {
            try
            {
                var obList = new KNT_COMBO_ColModel().getColorList(pMC_FAB_PROD_ORD_H_LST, pKNT_YD_PRG_H_ID,pPARENT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/SearchApprovedBatchNo")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/SearchApprovedBatchNo?pYD_BATCH_NO&pOption
        public IHttpActionResult SearchApprovedBatchNo(string pYD_BATCH_NO, Int32? pOption = null)
        {
            try
            {
                var obList = new KNT_YD_RCV_DModel().SearchApprovedBatchNo(pYD_BATCH_NO, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYdProgram/getOrderListByProgram")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getOrderListByProgram&pMC_FAB_PROD_ORD_H_LST
        public IHttpActionResult getOrderListByProgram(string pMC_FAB_PROD_ORD_H_LST)
        {
            try
            {
                var obList = new KNT_YD_RCV_HModel().getOrderListByProgram(pMC_FAB_PROD_ORD_H_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitYdProgram/getYdProgramDropDownData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getYdProgramDropDownData?pPRG_REF_NO&pKNT_YD_PRG_H_ID=1
        public IHttpActionResult getYdProgramDropDownData(string pPRG_REF_NO, Int64? pKNT_YD_PRG_H_ID = null)
        {
            try
            {
                var obList = new KNT_YD_PRG_HModel().getYdProgramDropDownData(pPRG_REF_NO, pKNT_YD_PRG_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/getYdYarnRecvData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getYdYarnRecvData?pKNT_YD_RCV_H_ID
        public IHttpActionResult getYdYarnRecvData(Int64 pKNT_YD_RCV_H_ID)
        {
            try
            {
                var obList = new KNT_YD_RCV_HModel().Select(pKNT_YD_RCV_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/getYdYrnRecvD")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getYdYrnRecvD?pKNT_YD_RCV_H_ID
        public IHttpActionResult getYdYrnRecvD(Int64 KNT_YD_PRG_H_ID, Int64? pKNT_YD_RCV_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var obList = new KNT_YD_RCV_DModel().SelectAll(KNT_YD_PRG_H_ID, pKNT_YD_RCV_H_ID, pMC_FAB_PROD_ORD_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/getFabricTypeByFabOrderH")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getFabricTypeByFabOrderH?pMC_FAB_PROD_ORD_H_LST=1
        public IHttpActionResult getFabricTypeByFabOrderH(string pMC_FAB_PROD_ORD_H_LST)
        {
            try
            {
                var obList = new RF_FAB_TYPEModel().getFabTypeByFabProdOrder(pMC_FAB_PROD_ORD_H_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitYdProgram/getColorListbyFabOrder")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getColorListbyFabOrder?pMC_FAB_PROD_ORD_H_LST =1
        public IHttpActionResult getColorListbyFabOrder(string pMC_FAB_PROD_ORD_H_LST, Int64? pKNT_YD_PRG_H_ID = null, Int64? pPARENT_ID=null)
        {
            try
            {
                var obList = new KNT_YDP_D_COLModel().getColorSummaryList(pMC_FAB_PROD_ORD_H_LST, pKNT_YD_PRG_H_ID, pPARENT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitYdProgram/getProgamList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getProgamList?pageNumber&pageSize&pSCM_SUPPLIER_ID&pPRG_REF_NO&pPRG_ISS_DT
        public IHttpActionResult getProgamList(Int64 pageNumber, Int64 pageSize, 
            Int64? pSCM_SUPPLIER_ID = null, 
            string pPRG_REF_NO = null, 
            string pPRG_ISS_DT = null, 
            string pDELV_TGT_DT=null,
            Int64? pMC_BYR_ACC_ID = null,
            Int64? pMC_FAB_PROD_ORD_H_ID=null,
            string pIS_PL_ADJ = null)
        {
            try
            {
                var obList = new KNT_YD_PRG_HModel().getProgamList(
                        pageNumber, 
                        pageSize, 
                        pSCM_SUPPLIER_ID, 
                        pPRG_REF_NO,
                        pPRG_ISS_DT,
                        pDELV_TGT_DT,
                        pMC_BYR_ACC_ID,
                        pMC_FAB_PROD_ORD_H_ID,
                        pIS_PL_ADJ
                );
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitYdProgram/getYdProgramListDs")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getYdProgramListDs?pPRG_REF_NO
        public IHttpActionResult getYdProgramListDs(
            string pPRG_REF_NO = null)
        {
            try
            {
                var obList = new KNT_YD_PRG_HModel().getYdProgramListDs(pPRG_REF_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }



        [Route("KnitYdProgram/getYdYrnRecvList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getYdYrnRecvList?pageNumber&pageSize&pSCM_SUPPLIER_ID&pPRG_REF_NO&pPRG_ISS_DT
        public IHttpActionResult getYdYrnRecvList(
             Int64 pageNumber, 
             Int64 pageSize, 
             Int64? pSCM_SUPPLIER_ID = null, 
             string pPRG_REF_NO = null,
             string pCHALAN_NO = null,
             Int64? pMC_BYR_ACC_ID = null,
             Int64? pMC_FAB_PROD_ORD_H_ID = null,
             string pIS_TRANSFER=null,
             Int64? pRF_ACTN_STATUS_ID=null
        )
        {
            try
            {
                var obList = new KNT_YD_PRG_HModel().getYdYrnRecvList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pPRG_REF_NO, pCHALAN_NO, pMC_BYR_ACC_ID, pMC_FAB_PROD_ORD_H_ID, pIS_TRANSFER, pRF_ACTN_STATUS_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("KnitYdProgram/GetYrnRqdList4Yd")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/GetYrnRqdList4Yd?pKNT_YD_PRG_H_ID=1
        public IHttpActionResult GetYrnRqdList4Yd(Int64 pKNT_YD_PRG_H_ID, Int64? pKNT_YRN_STR_REQ_H_ID = null)
        {
            try
            {
                var obList = new KNT_YDP_YRN_REQModel().GetYrnRqdList4Yd(pKNT_YD_PRG_H_ID, pKNT_YRN_STR_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitYdProgram/GetYrnReqList4YD")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitYdProgram/GetYrnReqList4YD
        public IHttpActionResult GetYrnReqList4YD(Int64 pKNT_YD_PRG_H_ID)
        {
            try
            {
                var obList = new KNT_YRN_STR_REQ_HModel().GetYrnReqList4YD(pKNT_YD_PRG_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYdProgram/Select")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/Select?pKNT_YD_PRG_H_ID =1&pPARENT_ID
        public IHttpActionResult Select(Int64? pKNT_YD_PRG_H_ID = null, Int64? pPARENT_ID = null)
        {
            try
            {
                var obList = new KNT_YD_PRG_HModel().Select(pKNT_YD_PRG_H_ID, pPARENT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitYdProgram/getProgramNoAuto")]
        [HttpGet]
        // GET :  /api/Knit/KnitYdProgram/getProgramNoAuto
        public IHttpActionResult getProgramNoAuto()
        {
            try
            {
                var ob = new KNT_YD_PRG_HModel().getProgramNoAuto();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("KnitYdProgram/Save")]
        [HttpPost]
        // GET :  api/knit/KnitYdProgram/Save
        public IHttpActionResult Save([FromBody] KNT_YD_PRG_HModel ob)
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

        [Route("KnitYdProgram/SaveYdYrnRecv")]
        [HttpPost]
        // GET :  api/knit/KnitYdProgram/SaveYdYrnRecv
        public IHttpActionResult SaveYdYrnRecv([FromBody] KNT_YD_RCV_HModel ob)
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

        [Route("KnitYdProgram/CreateRequisition")]
        [HttpPost]
        // GET :  api/knit/KnitYdProgram/CreateRequisition
        public IHttpActionResult CreateRequisition([FromBody] KNT_YD_PRG_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.CreateRequisition();
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

        [Route("KnitYdProgram/RemoveRequisition4YD")]
        [HttpPost]
        // GET :  api/knit/KnitYdProgram/RemoveRequisition4YD
        public IHttpActionResult RemoveRequisition4YD([FromBody] KNT_YRN_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RemoveRequisition4YD();
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

        [Route("KnitYdProgram/getKntStatementData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/getKntStatementData?pageNumber =1&pageSize=10&pRF_FAB_PROD_CAT_ID&pSCM_SUPPLIER_ID&pMC_FAB_PROD_ORD_H_ID&pMC_BYR_ACC_ID&pFIRSTDATE&pLASTDATE
        public IHttpActionResult getKntStatementData(
             Int32 pageNumber,
             Int32 pageSize,
             Int64? pRF_FAB_PROD_CAT_ID=null,
             Int64? pSCM_SUPPLIER_ID=null,
             Int64? pMC_FAB_PROD_ORD_H_ID=null,
             Int64? pMC_BYR_ACC_ID=null,
             DateTime? pFIRSTDATE=null,
             DateTime? pLASTDATE =null,
             string pPCT_DONE_CODE = null,
             Int64? pKNT_YD_PRG_H_ID = null
        )
        {
            try
            {
                var obList = new KNT_YD_STATEMENTModel().Query(pageNumber, pageSize, pRF_FAB_PROD_CAT_ID, pSCM_SUPPLIER_ID, pMC_FAB_PROD_ORD_H_ID, pMC_BYR_ACC_ID, pFIRSTDATE, pLASTDATE, pPCT_DONE_CODE, pKNT_YD_PRG_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitYdProgram/GetPlAdjHdr")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitYdProgram/GetPlAdjHdr
        public IHttpActionResult GetPlAdjHdr(Int64 pKNT_YD_PL_ADJ_H_ID)
        {
            try
            {
                var list = new KNT_YD_PL_ADJ_HModel().GetPlAdjHdr(pKNT_YD_PL_ADJ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/GetPlAdjDtl")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/GetPlAdjDtl?pKNT_YD_PL_ADJ_H_ID
        public IHttpActionResult GetPlAdjDtl(Int64? pKNT_YD_PL_ADJ_H_ID)
        {
            try
            {
                var obList = new KNT_YD_PL_ADJ_DModel().GetPlAdjDtl(pKNT_YD_PL_ADJ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitYdProgram/PlAdjBatchSave")]
        [HttpPost]
        [NoCache]
        // GET :  api/knit/KnitYdProgram/PlAdjBatchSave
        public IHttpActionResult PlAdjBatchSave([FromBody] KNT_YD_PL_ADJ_HModel ob)
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

        [Route("KnitYdProgram/GetPlAdjUserLavel")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitYdProgram/GetPlAdjUserLavel
        public IHttpActionResult GetPlAdjUserLavel()
        {
            try
            {
                var list = new KNT_YDPL_ADJ_USR_LAVELModel().GetPlAdjUserLavel();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitYdProgram/GetPlAdjList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Knit/KnitYdProgram/GetPlAdjList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetPlAdjList(int pageNumber, int pageSize, Int64? pSCM_SUPPLIER_ID, string pPL_ADJ_MEMO_NO = null, DateTime? pADJ_DT = null)
        {
            try
            {
                var obList = new KNT_YD_PL_ADJ_HModel().GetPlAdjList(pageNumber, pageSize, pSCM_SUPPLIER_ID, pPL_ADJ_MEMO_NO, pADJ_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


    }
}
