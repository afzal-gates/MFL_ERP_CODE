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
    public class DFFabricInspectionController : ApiController
    {


        [Route("DfFabInsp/GetDfQCRpt")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRpt
        public IHttpActionResult GetDfQCRpt(string pDYE_BATCH_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var data = new DF_FAB_QC_RPTModel().SelectAll(pDYE_BATCH_NO, pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pMC_COLOR_ID);
                return Ok(data);
                //int total = 0;
                //if (data.Count > 0)
                //    total = data.FirstOrDefault().TOTAL_REC;
                //return Ok(new { total, data });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFabInsp/GetDfQCRptByID")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRptByID
        public IHttpActionResult GetDfQCRptByID(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            try
            {
                var data = new DF_FAB_QC_RPTModel().Select(pDF_FAB_QC_RPT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFabInsp/GetDfQCRptDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRptDtlByID
        public IHttpActionResult GetDfQCRptDtlByID(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            try
            {
                var data = new DF_BT_SUB_LOTModel().SelectByID(pDF_FAB_QC_RPT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfFabInsp/GetDfQCRptByBatchNo")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRptByBatchNo?pDYE_BATCH_NO=
        public IHttpActionResult GetDfQCRptByBatchNo(string pDYE_BATCH_NO = null)
        {
            try
            {
                var data = new DF_BT_SUB_LOTModel().SelectByBatchNo(pDYE_BATCH_NO);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFabInsp/GetSubLotDefectListByLotID")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetSubLotDefectListByLotID?pDF_BT_SUB_LOT_ID=
        public IHttpActionResult GetSubLotDefectListByLotID(Int64? pDF_BT_SUB_LOT_ID = null)
        {
            try
            {
                var data = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(pDF_BT_SUB_LOT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("DfFabInsp/GetDfQCRollByID")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRollByID
        public IHttpActionResult GetDfQCRollByID(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            try
            {
                var data = new DF_FAB_QC_ROLLModel().SelectAll(pDF_FAB_QC_RPT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFabInsp/GetDfQCRollLotByID")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQCRollLotByID
        public IHttpActionResult GetDfQCRollLotByID(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            try
            {
                var data = new DF_FAB_QC_ROLLModel().SelectAllLot(pDF_FAB_QC_RPT_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfFabInsp/GetFabricFaultyList")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetFabricFaultyList
        public IHttpActionResult GetFabricFaultyList()
        {
            try
            {
                var data = new RF_FB_FLT_APRN_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFabInsp/GetDfQcParamTypeList")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfQcParamTypeList
        public IHttpActionResult GetDfQcParamTypeList()
        {
            try
            {
                var data = new RF_DF_QC_PARAM_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        
        [Route("DfFabInsp/Save")]
        [HttpPost]
        // GET :  api/Dye/DfFabInsp/Save
        public IHttpActionResult Save([FromBody] DF_FAB_QC_RPTModel ob)
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


        [Route("DfFabInsp/SaveReport")]
        [HttpPost]
        // GET :  api/Dye/DfFabInsp/SaveReport
        public IHttpActionResult SaveReport([FromBody] DF_FAB_QC_RPTModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveReport();
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


        [Route("DfFabInsp/GetDfBTQcStatusInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfBTQcStatusInfo?pDYE_BT_CARD_H_ID
        public IHttpActionResult GetDfBTQcStatusInfo(Int64? pDYE_BT_CARD_H_ID=null)
        {
            try
            {
                var data = new DF_BT_QC_STATUSModel().SelectByID(pDYE_BT_CARD_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("DfFabInsp/GetDfBTQcParamList")]
        [HttpGet]
        // GET :  /api/Dye/DfFabInsp/GetDfBTQcParamList
        public IHttpActionResult GetDfBTQcParamList(Int64? pDYE_BT_CARD_H_ID = null, Int64? pDF_RIB_SHADE_RPT_H_ID = null)
        {
            try
            {
                var data = new DF_BT_QC_CHQ_LSTModel().SelectByID(pDYE_BT_CARD_H_ID, pDF_RIB_SHADE_RPT_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

                
        
        [Route("DfFabInsp/SaveQC")]
        [HttpPost]
        // GET :  api/Dye/DfFabInsp/SaveQC
        public IHttpActionResult SaveQC([FromBody] DF_BT_QC_STATUSModel ob)
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
