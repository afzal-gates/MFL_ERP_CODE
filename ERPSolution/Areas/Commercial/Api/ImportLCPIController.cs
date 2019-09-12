using ERP.Model;
//using ERPSolution.Common;
using System;
using System.Collections;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/cmr")]
    public class ImportLCPIController : ApiController
    {
        [Route("ImportLCPI/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/SelectAll/{pageNo}/{pageSize}
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pPI_NO_IMP = null, string pPO_NO_IMP = null, string pPI_DT_IMP = null)
        {
            try
            {
                if (pPI_DT_IMP != null)
                    pPI_DT_IMP = Convert.ToDateTime(pPI_DT_IMP.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new CM_IMP_PI_HModel().SelectAll(pageNo, pageSize, pSCM_SUPPLIER_ID, pPI_NO_IMP, pPO_NO_IMP, pPI_DT_IMP);
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



        [Route("ImportLCPI/GetPOBySupplier")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetPOBySupplier?pSCM_SUPPLIER_ID=
        public IHttpActionResult GetPOBySupplier(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new CM_IMP_PO_HModel().SelectByID(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ImportLCPI/GetImportLCPIInfo")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetImportLCPIInfo?pCM_IMP_PI_H_ID=
        public IHttpActionResult GetImportLCPIInfo(Int64? pCM_IMP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PI_HModel().Select(pCM_IMP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ImportLCPI/GetImportLCPIInfoBySupplierID")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetImportLCPIInfoBySupplierID?pSCM_SUPPLIER_ID=
        public IHttpActionResult GetImportLCPIInfoBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var list = new CM_IMP_PI_HModel().SelectByID(pSCM_SUPPLIER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ImportLCPI/GetPIDtlInfo")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetPIDtlInfo?pCM_IMP_PI_H_ID=
        public IHttpActionResult GetPIDtlInfo(Int64? pCM_IMP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PI_D_YRNModel().SelectByID(pCM_IMP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ImportLCPI/GetPIDocDtlInfo")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetPIDocDtlInfo?pCM_IMP_PI_H_ID=
        public IHttpActionResult GetPIDocDtlInfo(Int64? pCM_IMP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_IMP_PI_DOCModel().SelectByID(pCM_IMP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ImportLCPI/GetPOListByID")]
        [HttpGet]
        // GET :  /api/cmr/ImportLCPI/GetPOListByID?pCM_IMP_PI_H_ID=
        public IHttpActionResult GetPOListByID(Int64? pCM_IMP_PI_H_ID = null)
        {
            try
            {
                var list = new CM_EXP_PI_D_POModel().GetPOListByID(pCM_IMP_PI_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ImportLCPI/Save")]
        [HttpPost]
        // GET :  api/Cmr/ImportLCPI/Save
        public IHttpActionResult Save([FromBody] CM_IMP_PI_HModel ob)
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


        [Route("ImportLCPI/Update")]
        [HttpPost]
        // GET :  api/Cmr/ImportLCPI/Update
        public IHttpActionResult Update([FromBody] CM_IMP_PI_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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


        [Route("ImportLCPI/RevisePO")]
        [HttpPost]
        // GET :  api/Cmr/ImportLCPI/RevisePO
        public IHttpActionResult RevisePO([FromBody] CM_IMP_PI_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RevisePO();
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
