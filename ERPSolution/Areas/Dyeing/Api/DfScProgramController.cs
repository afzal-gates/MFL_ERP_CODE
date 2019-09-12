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
    public class DfScProgramController : ApiController
    {
        [Route("DfScProgram/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pPRG_ISS_NO = null, string pSC_PRG_DT = null, string pEXP_DELV_DT = null, Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, string pDYE_BATCH_NO = null, Int64? pUSER_ID = null)
        {
            try
            {
                if (pSC_PRG_DT != null)
                    pSC_PRG_DT = Convert.ToDateTime(pSC_PRG_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                if (pEXP_DELV_DT != null)
                    pEXP_DELV_DT = Convert.ToDateTime(pEXP_DELV_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DF_SC_PRG_ISS_HModel().SelectAll(pageNo, pageSize, pPRG_ISS_NO, pSC_PRG_DT, pEXP_DELV_DT, pHR_COMPANY_ID, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pDYE_BATCH_NO, pUSER_ID);
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

        [Route("DfScProgram/SelectForScProgram")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/SelectForScProgram?pDYE_BATCH_NO
        public IHttpActionResult SelectForScProgram(string pDYE_BATCH_NO = null,string pIS_AOP = null)
        {
            try
            {
                var list = new DF_BT_PRODModel().SelectForScProgram(pDYE_BATCH_NO, pIS_AOP);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfScProgram/GetScProgramInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/GetScProgramInfo?pDF_SC_PRG_ISS_H_ID
        public IHttpActionResult GetScProgramInfo(Int64? pDF_SC_PRG_ISS_H_ID = null)
        {
            try
            {
                var list = new DF_SC_PRG_ISS_HModel().Select(pDF_SC_PRG_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfScProgram/GetScProgramDetailInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/GetScProgramDetailInfo?pDF_SC_PRG_ISS_H_ID
        public IHttpActionResult GetScProgramDetailInfo(Int64? pDF_SC_PRG_ISS_H_ID = null, string pCHALAN_NO = null)
        {
            try
            {
                var list = new DF_SC_PRG_ISS_BT_D1Model().SelectByID(pDF_SC_PRG_ISS_H_ID, pCHALAN_NO);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("DfScProgram/GetScProgramProcessDetail")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/GetScProgramProcessDetail?pDF_SC_PRG_ISS_H_ID
        public IHttpActionResult GetScProgramProcessDetail(Int64? pDF_SC_PRG_ISS_H_ID = null)
        {
            try
            {
                var list = new DF_SC_PRG_ISS_PROC_D2Model().SelectByID(pDF_SC_PRG_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfScProgram/Save")]
        [HttpPost]
        // GET :  api/Dye/DfScProgram/Save
        public IHttpActionResult Save([FromBody] DF_SC_PRG_ISS_HModel ob)
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

        [Route("DfScProgram/SelectByChallanNo")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/SelectByChallanNo?pCHALAN_NO=
        public IHttpActionResult SelectByChallanNo(string pCHALAN_NO = null)
        {
            try
            {
                var list = new DF_SC_PRG_ISS_HModel().SelectByChallanNo(pCHALAN_NO);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfScProgram/SelectByPartyID")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/SelectByPartyID?pSCM_SUPPLIER_ID=
        public IHttpActionResult SelectByPartyID(Int64? pSCM_SUPPLIER_ID = null, string pIS_AOP = null)
        {
            try
            {
                var list = new DF_SC_BT_CHL_ISS_HModel().SelectByPartyID(pSCM_SUPPLIER_ID, pIS_AOP);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfScProgram/ScChallanGetAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/dye/DfScProgram/ScChallanGetAll
        public IHttpActionResult ScChallanGetAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pCHALAN_NO = null, string pCHALAN_DT = null, Int64? pUSER_ID = null)
        {
            if (pCHALAN_DT != null)
                pCHALAN_DT = Convert.ToDateTime(pCHALAN_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

            var data = new DF_SC_BT_CHL_ISS_HModel().SelectAll(pageNo, pageSize, pSCM_SUPPLIER_ID, pCHALAN_NO, pCHALAN_DT);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }


        [Route("DfScProgram/DfScProgramChallanGetByID")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/DfScProgramChallanGetByID?pDF_SC_BT_CHL_ISS_H_ID=
        public IHttpActionResult DfScProgramChallanGetByID(Int64? pDF_SC_BT_CHL_ISS_H_ID = null)
        {
            try
            {
                var list = new DF_SC_BT_CHL_ISS_HModel().SelectByID(pDF_SC_BT_CHL_ISS_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfScProgram/DfScProgramChallanDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/DfScProgramChallanDtlByID?pDF_SC_BT_CHL_ISS_H_ID=
        public IHttpActionResult DfScProgramChallanDtlByID(Int64? pDF_SC_BT_CHL_ISS_H_ID = null, string pCHALAN_NO = null)
        {
            try
            {
                var list = new DF_SC_BT_CHL_ISS_DModel().SelectByID(pDF_SC_BT_CHL_ISS_H_ID, pCHALAN_NO);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfScProgram/DfScProgramBySupplierID")]
        [HttpGet]
        // GET :  /api/dye/ScPreTreatment/DfScProgramBySupplierID
        public IHttpActionResult DfScProgramBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            var obList = new DF_SC_PRG_ISS_HModel().ScProgramBySupplierID(pSCM_SUPPLIER_ID);
            return Ok(obList);
        }



        [Route("DfScProgram/SaveChallan")]
        [HttpPost]
        // GET :  api/Dye/DfScProgram/SaveChallan
        public IHttpActionResult SaveChallan([FromBody] DF_SC_BT_CHL_ISS_HModel ob)
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


        [Route("DfScProgram/SelectRcvAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/SelectRcvAll
        public IHttpActionResult SelectRcvAll(int pageNo, int pageSize, string pRCV_REF_NO = null, string pRCV_DT = null, string pCHALAN_NO = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, string pDYE_BATCH_NO = null, Int64? pUSER_ID = null)
        {
            try
            {
                if (pRCV_DT != null)
                    pRCV_DT = Convert.ToDateTime(pRCV_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DF_SCO_CHL_RCV_HModel().SelectAll(pageNo, pageSize, pRCV_REF_NO, pRCV_DT, pCHALAN_NO, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pDYE_BATCH_NO, pUSER_ID);
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

        [Route("DfScProgram/GetScProgramReceiveInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/GetScProgramReceiveInfo?pDF_SCO_CHL_RCV_H_ID
        public IHttpActionResult GetScProgramReceiveInfo(Int64? pDF_SCO_CHL_RCV_H_ID = null)
        {
            try
            {
                var list = new DF_SCO_CHL_RCV_HModel().Select(pDF_SCO_CHL_RCV_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DfScProgram/GetScProgramReceiveDtlInfo")]
        [HttpGet]
        // GET :  /api/Dye/DfScProgram/GetScProgramReceiveDtlInfo?pDF_SCO_CHL_RCV_H_ID
        public IHttpActionResult GetScProgramReceiveDtlInfo(Int64? pDF_SCO_CHL_RCV_H_ID = null)
        {
            try
            {
                var list = new DF_SCO_CHL_RCV_DModel().SelectByID(pDF_SCO_CHL_RCV_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("DfScProgram/Receive")]
        [HttpPost]
        // GET :  api/Dye/DfScProgram/Receive
        public IHttpActionResult Receive([FromBody] DF_SCO_CHL_RCV_HModel ob)
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
