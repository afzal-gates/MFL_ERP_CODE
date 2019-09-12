using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Hr.Api
{
    [RoutePrefix("api/hr")]
    [System.Web.Http.Authorize]
    public class HrIncrimentApiController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("HrIncriment/GetIncrMemoList")]
        [HttpGet]      
        // GET :  /api/hr/HrIncriment/GetIncrMemoList
        public IHttpActionResult GetIncrMemoList(Int64 pageNumber, Int64 pageSize, string pAUTH_NO = null, string pRF_INCR_TYPE_ID_LST = null, string pINCR_TYPE_NAME_EN = null, string pCOMP_NAME_EN = null, 
            string pMEMO_NO = null, string pIS_FINALIZED = null, string pIS_CLOSED = null)
        {
            try
            {
                var obList = new HR_INCR_MEMOModel().GetIncrMemoList(pageNumber, pageSize, pAUTH_NO, pRF_INCR_TYPE_ID_LST, pINCR_TYPE_NAME_EN, pCOMP_NAME_EN, pMEMO_NO, pIS_FINALIZED, pIS_CLOSED);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/Save")]
        [HttpPost]        
        public IHttpActionResult Save(HR_INCR_MEMOModel ob)
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

        [Route("HrIncriment/MemoFinalize")]
        [HttpPost]        
        public IHttpActionResult MemoFinalize(HR_INCR_MEMOModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MemoFinalize();
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


        //=======================
        [Route("HrIncriment/GetIncrBatchList")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetIncrBatchList?pHR_INCR_MEMO_ID=1
        public IHttpActionResult GetIncrBatchList(Int64? pHR_INCR_MEMO_ID = null)
        {
            try
            {
                var obList = new HR_YR_INCR_HModel().GetIncrBatchList(pHR_INCR_MEMO_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetIncrGradeList")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetIncrGradeList
        public IHttpActionResult GetIncrGradeList(Int64? pHR_EMPLOYEE_TYPE_ID = null)
        {
            try
            {
                var obList = new HR_INCR_GRADEModel().GetIncrGradeList(pHR_EMPLOYEE_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetSection4IncrProp")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetSection4IncrProp
        public IHttpActionResult GetSection4IncrProp()
        {
            try
            {
                var obList = new HrDepartmentModel().GetSection4IncrProp();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetFloor4IncrProp")]
        [HttpGet]       
        // GET :  /api/hr/HrIncriment/GetFloor4IncrProp
        public IHttpActionResult GetFloor4IncrProp()
        {
            try
            {
                var obList = new HR_PROD_FLRModel().GetFloor4IncrProp();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetIncrHdr")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetIncrHdr
        public IHttpActionResult GetIncrHdr(Int64? pHR_YR_INCR_H_ID = null, Int64? pHR_INCR_MEMO_ID = null, Int64? pHR_DEPARTMENT_ID = null, Int64? pLK_FLOOR_ID = null)
        {
            try
            {
                var obList = new HR_YR_INCR_HModel().GetIncrHdr(pHR_YR_INCR_H_ID, pHR_INCR_MEMO_ID, pHR_DEPARTMENT_ID, pLK_FLOOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetEmp4IncrProposal")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetEmp4IncrProposal
        public IHttpActionResult GetEmp4IncrProposal(Int64 pageNumber, Int64 pageSize, Int64? pHR_INCR_MEMO_ID = null, Int32? pEMPLOYEE_TYPE_ID = null, Int64? pHR_YR_INCR_H_ID = null, Int64? pHR_DEPARTMENT_ID = null, Int32? pLK_FLOOR_ID = null)
        {
            try
            {
                var obList = new HR_YR_INCR_DModel().GetEmp4IncrProposal(pageNumber, pageSize, pHR_INCR_MEMO_ID, pEMPLOYEE_TYPE_ID, pHR_YR_INCR_H_ID, pHR_DEPARTMENT_ID, pLK_FLOOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrIncriment/GetEmpIncrHistory")]
        [HttpGet]        
        // GET :  /api/hr/HrIncriment/GetEmpIncrHistory
        public IHttpActionResult GetEmpIncrHistory(Int64? pHR_EMPLOYEE_ID = null)
        {
            try
            {
                var obList = new HR_YR_INCR_DModel().GetEmpIncrHistory(pHR_EMPLOYEE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("HrIncriment/IncrBatchSave")]
        [HttpPost]        
        public IHttpActionResult IncrBatchSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IncrBatchSave();
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

        [Route("HrIncriment/IncrProposeSave")]
        [HttpPost]        
        public IHttpActionResult IncrProposeSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IncrProposeSave();
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

            //Hub.Clients.All.getAlertData();
            Hub.Clients.All.updateNotifCount();
            Hub.Clients.All.getMessageData();
            return Ok(new { success = true, jsonStr });
        }

        [Route("HrIncriment/IncrVeryficationSave")]
        [HttpPost]        
        public IHttpActionResult IncrVeryficationSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IncrVeryficationSave();
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

            //Hub.Clients.All.getAlertData();
            Hub.Clients.All.getMessageData();
            Hub.Clients.All.updateNotifCount();
            return Ok(new { success = true, jsonStr });
        }

        [Route("HrIncriment/IncrFinalizeSave")]
        [HttpPost]        
        public IHttpActionResult IncrFinalizeSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IncrFinalizeSave();
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

            //Hub.Clients.All.BroadcastIncrProposalNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("HrIncriment/IncrEffectProcess")]
        [HttpPost]        
        public IHttpActionResult IncrEffectProcess(HR_INCR_MEMOModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.IncrEffectProcess();
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

        
        //==================== Special Incriment ========================
        [Route("HrSpecialIncr/GetEmpSearch4SpecialIncr")]
        [HttpGet]
        // GET :  /api/hr/HrSpecialIncr/GetEmpSearch4SpecialIncr
        public IHttpActionResult GetEmpSearch4SpecialIncr(string pEMPLOYEE_CODE, Int64 pHR_INCR_MEMO_ID, Int64 pHR_YR_INCR_H_ID)
        {
            try
            {
                var obList = new HR_YR_INCR_DModel().GetEmpSearch4SpecialIncr(pEMPLOYEE_CODE, pHR_INCR_MEMO_ID, pHR_YR_INCR_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrSpecialIncr/SpecialIncrBatchSave")]
        [HttpPost]
        public IHttpActionResult SpecialIncrBatchSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SpecialIncrBatchSave();
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

        [Route("HrSpecialIncr/GetSpecialIncrHdr")]
        [HttpGet]
        // GET :  /api/hr/HrSpecialIncr/GetSpecialIncrHdr
        public IHttpActionResult GetSpecialIncrHdr(Int64? pHR_YR_INCR_H_ID = null, Int64? pHR_INCR_MEMO_ID = null)
        {
            try
            {
                var obList = new HR_YR_INCR_HModel().GetSpecialIncrHdr(pHR_YR_INCR_H_ID, pHR_INCR_MEMO_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrSpecialIncr/GetSpecialIncrDtl")]
        [HttpGet]
        // GET :  /api/hr/HrSpecialIncr/GetSpecialIncrDtl
        public IHttpActionResult GetSpecialIncrDtl(Int64 pHR_YR_INCR_H_ID)
        {
            try
            {
                var obList = new HR_YR_INCR_DModel().GetSpecialIncrDtl(pHR_YR_INCR_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("HrSpecialIncr/SpecialIncrProposeSave")]
        [HttpPost]
        public IHttpActionResult SpecialIncrProposeSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SpecialIncrProposeSave();
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

            //Hub.Clients.All.getAlertData();
            Hub.Clients.All.updateNotifCount();
            Hub.Clients.All.getMessageData();
            return Ok(new { success = true, jsonStr });
        }

        [Route("HrSpecialIncr/SpecialIncrFinalizeSave")]
        [HttpPost]
        public IHttpActionResult SpecialIncrFinalizeSave(HR_YR_INCR_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SpecialIncrFinalizeSave();
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

            //Hub.Clients.All.BroadcastIncrProposalNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("HrSpecialIncr/GetSpecialIncrMemoList")]
        [HttpGet]
        // GET :  /api/hr/HrSpecialIncr/GetSpecialIncrMemoList
        public IHttpActionResult GetSpecialIncrMemoList(string pAUTH_NO = null, string pRF_INCR_TYPE_ID_LST = null, string pINCR_TYPE_NAME_EN = null, string pCOMP_NAME_EN = null,
            string pMEMO_NO = null, string pIS_FINALIZED = null, string pIS_CLOSED = null)
        {
            try
            {
                var obList = new HR_INCR_MEMOModel().GetSpecialIncrMemoList(pAUTH_NO, pRF_INCR_TYPE_ID_LST, pINCR_TYPE_NAME_EN, pCOMP_NAME_EN, pMEMO_NO, pIS_FINALIZED, pIS_CLOSED);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
