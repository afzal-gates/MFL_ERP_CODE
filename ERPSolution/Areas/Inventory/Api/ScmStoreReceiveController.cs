using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.invting.Api
{
    [RoutePrefix("api/inv")]
    public class ScmStoreReceiveController : ApiController
    {

        [Route("StoreReceive/GetOpnBalList")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/GetOpnBalList
        public IHttpActionResult GetOpnBalList(Int64 pageNumber, Int64 pageSize, Int64? pHR_COMPANY_ID = null, Int64? pSCM_STORE_ID = null, Int64? pINV_ITEM_CAT_ID = null)
        {
            try
            {
                var obList = new SCM_GEN_ITEM_OBModel().SelectAll(pageNumber, pageSize, pHR_COMPANY_ID, pSCM_STORE_ID, pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreReceive/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pMRR_DT = null,
            Int64? pHR_COMPANY_ID = null, Int64? pSCM_STORE_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pRF_REQ_TYPE_ID = null, string pIMP_LC_NO = null)
        {
            try
            {
                var data = new SCM_STR_RCV_HModel().SelectAll(pageNo, pageSize, pMRR_NO, pMRR_DT, pHR_COMPANY_ID, pSCM_STORE_ID, pSCM_SUPPLIER_ID, pRF_REQ_TYPE_ID, pIMP_LC_NO);
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


        [Route("StoreReceive/GetStoreReceiveInfoByID")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/GetStoreReceiveInfoByID?pSCM_STR_RCV_H_ID
        public IHttpActionResult GetStoreReceiveInfoByID(Int64? pSCM_STR_RCV_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_RCV_HModel().Select(pSCM_STR_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreReceive/GetStoreReceiveDtlInfoByID")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/GetStoreReceiveDtlInfoByID?pSCM_STR_RCV_H_ID=
        public IHttpActionResult GetStoreReceiveDtlInfoByID(Int64? pSCM_STR_RCV_H_ID = null)
        {
            try
            {
                var list = new SCM_STR_RCV_DModel().SelectAll(pSCM_STR_RCV_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreReceive/PendingPOListForReceive")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/PendingPOListForReceive?pSCM_SUPPLIER_ID=
        public IHttpActionResult PendingPOListForReceive(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_HModel().PendingPOListForReceive(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StoreReceive/GetPoDetlByID")]
        [HttpGet]
        // GET :  /api/inv/StoreReceive/GetPoDetlByID?pCM_IMP_PO_H_ID=
        public IHttpActionResult GetPoDetlByID(Int64? pCM_IMP_PO_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_D_YRNModel().SelectForReceive(pCM_IMP_PO_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreReceive/SaveOB")]
        [HttpPost]
        // GET :  api/inv/StoreReceive/SaveOB
        public IHttpActionResult SaveOB([FromBody] SCM_GEN_ITEM_OBModel ob)
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


        [Route("StoreReceive/Delete")]
        [HttpPost]
        // GET :  api/inv/StoreReceive/Delete
        public IHttpActionResult Delete([FromBody] SCM_GEN_ITEM_OBModel ob)
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

        [Route("StoreReceive/FinalizeAll")]
        [HttpPost]
        // GET :  api/inv/StoreReceive/FinalizeAll
        public IHttpActionResult FinalizeAll([FromBody] SCM_GEN_ITEM_OBModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.FinalizeAll();
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

        [Route("StoreReceive/Save")]
        [HttpPost]
        // GET :  api/inv/StoreReceive/Save
        public IHttpActionResult Save([FromBody] SCM_STR_RCV_HModel ob)
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
