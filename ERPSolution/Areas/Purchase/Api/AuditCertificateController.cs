using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/purchase")]
    public class AuditCertificateController : ApiController
    {
        [Route("AuditCertificate/SelectAll")]
        [HttpGet]
        // GET :  /api/Purchase/AuditCertificate/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var data = new HR_AUD_CERT_REGModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AuditCertificate/GetCertificateByID")]
        [HttpGet]
        // GET :  /api/Purchase/AuditCertificate/GetCertificateByID
        public IHttpActionResult GetCertificateByID(Int32? pHR_AUD_CERT_REG_ID = null)
        {
            try
            {
                var data = new HR_AUD_CERT_REGModel().Select(pHR_AUD_CERT_REG_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AuditCertificate/GetOldCertificateByID")]
        [HttpGet]
        // GET :  /api/Purchase/AuditCertificate/GetOldCertificateByID?pHR_COMPANY_ID=&pRF_AUD_CERT_TYPE_ID=
        public IHttpActionResult GetOldCertificateByID(Int32? pHR_COMPANY_ID = null, Int32? pRF_AUD_CERT_TYPE_ID = null)
        {
            try
            {
                var data = new HR_AUD_CERT_REGModel().SelectByID(pHR_COMPANY_ID, pRF_AUD_CERT_TYPE_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AuditCertificate/GetCapByID")]
        [HttpGet]
        // GET :  /api/Purchase/AuditCertificate/GetCapByID
        public IHttpActionResult GetCapByID(Int32? pHR_AUD_CERT_REG_ID = null)
        {
            try
            {
                var data = new HR_AUD_CERT_CAPModel().SelectAll(pHR_AUD_CERT_REG_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AuditCertificate/Save")]
        [HttpPost]
        // GET :  api/Purchase/AuditCertificate/Save
        public IHttpActionResult Save([FromBody] HR_AUD_CERT_REGModel ob)
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
            //Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("AuditCertificate/SaveCap")]
        [HttpPost]
        // GET :  api/Purchase/AuditCertificate/SaveCap
        public IHttpActionResult SaveCap([FromBody] HR_AUD_CERT_CAPModel ob)
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
            //Hub.Clients.All.RefreshPurchasePlanList();
            return Ok(new { success = true, jsonStr });
        }
    }
}
