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
    public class DFProductionController : ApiController
    {
        [Route("DFProduction/GetDFProductionByBatchNo")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetDFProductionByBatchNo
        public IHttpActionResult GetDFProductionByBatchNo(string pDYE_BATCH_NO = null, string pSC_PRG_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST = null)
        {
            try
            {
                var list = new DF_BT_PRODModel().GetDFProductionByBatchNo(pDYE_BATCH_NO, pSC_PRG_NO, pLK_DIA_TYPE_ID, pLK_FBR_GRP_LST);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DFProduction/GetBatchForDFQC")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetBatchForDFQC
        public IHttpActionResult GetBatchForDFQC(string pDYE_BATCH_NO = null)
        {
            try
            {
                var batch = new DYE_BT_PRODModel().GetBatchForDfQc(pDYE_BATCH_NO);
                return Ok(batch);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/GetDFProcessType")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetDFProcessType
        public IHttpActionResult GetDFProcessType()
        {
            try
            {
                var list = new DF_PROC_TYPEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/GetDFProcessTypeByID")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetDFProcessTypeByID?pLK_DIA_TYPE_ID
        public IHttpActionResult GetDFProcessTypeByID(Int64? pLK_DIA_TYPE_ID = null)
        {
            try
            {
                var list = new DF_PROC_TYPEModel().SelectByID(pLK_DIA_TYPE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/GetMachineByProcessType")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetMachineByProcessType?pDYE_BT_CARD_H_ID
        public IHttpActionResult GetMachineByProcessType(Int64? pDYE_BT_CARD_H_ID = null, Int64? pDF_SC_PT_ISS_H_ID = null, Int64? pDF_BT_SUB_LOT_ID = null, Int64? pLK_DIA_TYPE_ID = null)
        {
            try
            {
                var list = new DF_MACHINEModel().SelectByProcType(pDYE_BT_CARD_H_ID, pDF_SC_PT_ISS_H_ID, pDF_BT_SUB_LOT_ID, pLK_DIA_TYPE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DFProduction/Save")]
        [HttpPost]
        // GET :  api/Dye/DFProduction/Save
        public IHttpActionResult Save([FromBody] DF_BT_PRODModel ob)
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

        [Route("DFProduction/GetDFMachineProcessMappingList")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/GetDFMachineProcessMappingList
        public IHttpActionResult GetDFMachineProcessMappingList(Int64? pDF_MACHINE_ID = null, Int64? pDF_PROC_TYPE_ID = null, Int64? pLK_DIA_TYPE_ID = null)
        {
            try
            {
                var list = new DF_MAP_MCN_PROCModel().SelectByID(pDF_MACHINE_ID, pDF_PROC_TYPE_ID, pLK_DIA_TYPE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/SaveMapping")]
        [HttpPost]
        // GET :  api/Dye/DFProduction/SaveMapping
        public IHttpActionResult SaveMapping([FromBody] DF_MAP_MCN_PROCModel ob)
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

        [Route("DFProduction/DeleteMapping")]
        [HttpPost]
        // GET :  api/Dye/DFProduction/DeleteMapping
        public IHttpActionResult DeleteMapping([FromBody] DF_MAP_MCN_PROCModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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

        [Route("DFProduction/SelectRunningBatch")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectRunningBatch
        public IHttpActionResult SelectRunningBatch(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pLK_DIA_TYPE_ID = null, string pMONTHOF = null,
            Int64? pMC_STYLE_H_ID = null, Int64? pMC_COLOR_ID = null, string pFROM_DATE = null, Int64? pDF_PROC_TYPE_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pFROM_DATE != null)
                    pFROM_DATE = Convert.ToDateTime(pFROM_DATE.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DYE_STR_REQ_HModel().SelectRunningBatch(pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pLK_DIA_TYPE_ID, pMONTHOF, pMC_STYLE_H_ID, pMC_COLOR_ID, pFROM_DATE, pDF_PROC_TYPE_ID, pOption);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/SelectDfQcUser")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectDfQcUser
        public IHttpActionResult SelectDfQcUser(Int64? pLK_DF_EMP_TYPE_ID = null)
        {
            try
            {
                var data = new DF_OPR_RESP_EMPModel().SelectAll(pLK_DF_EMP_TYPE_ID);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DFProduction/SelectOnlineQCList")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectOnlineQCList
        public IHttpActionResult SelectOnlineQCList()
        {
            try
            {
                var data = new DF_PROC_QC_RPT_HModel().SelectAll();

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/SelectOnlineQCByID")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectOnlineQCByID
        public IHttpActionResult SelectOnlineQCByID(Int64? pDF_PROC_QC_RPT_H_ID = null)
        {
            try
            {
                var data = new DF_PROC_QC_RPT_HModel().Select(pDF_PROC_QC_RPT_H_ID);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DFProduction/SelectForBatchReProcessByID")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectForBatchReProcessByID?pDYE_BATCH_NO=
        public IHttpActionResult SelectForBatchReProcessByID(string pDYE_BATCH_NO = null)
        {
            try
            {
                var data = new DF_PROC_QC_RPT_HModel().SelectForBatchReProcessByID(pDYE_BATCH_NO);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DFProduction/SelectOnlineQcDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectOnlineQcDtlByID?pDF_PROC_QC_RPT_H_ID=
        public IHttpActionResult SelectOnlineQcDtlByID(Int64? pDF_PROC_QC_RPT_H_ID = null)
        {
            try
            {
                var data = new DF_PROC_QC_RPT_DModel().SelectByID(pDF_PROC_QC_RPT_H_ID);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DFProduction/SelectOnlineQcDectListByID")]
        [HttpGet]
        // GET :  /api/Dye/DFProduction/SelectOnlineQcDectListByID?pDF_PROC_QC_RPT_H_ID=
        public IHttpActionResult SelectOnlineQcDectListByID(Int64? pDF_PROC_QC_RPT_H_ID = null, Int64? pLK_FAB_INSP_TYPE_ID = null)
        {
            try
            {
                var data = new DF_PROC_QC_RPT_DFCTModel().SelectByID(pDF_PROC_QC_RPT_H_ID, pLK_FAB_INSP_TYPE_ID);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("DFProduction/OnlineQc")]
        [HttpPost]
        // GET :  api/Dye/DFProduction/OnlineQc
        public IHttpActionResult OnlineQc([FromBody] DF_PROC_QC_RPT_HModel ob)
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
