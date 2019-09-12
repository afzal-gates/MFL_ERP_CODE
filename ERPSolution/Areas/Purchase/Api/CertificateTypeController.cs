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
    public class CertificateTypeController : ApiController
    {
        [Route("CertificateType/SelectAll")]
        [HttpGet]
        // GET :  /api/Purchase/CertificateType/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var data = new RF_AUD_CERT_TYPEModel().SelectAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CertificateType/GetOrganization")]
        [HttpGet]
        // GET :  /api/Purchase/CertificateType/GetOrganization
        public IHttpActionResult GetOrganization()
        {
            try
            {
                var data = new RF_DOC_ISS_ORGModel().SelectAll();
                int total = 0;

                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CertificateType/Save")]
        [HttpPost]
        // GET :  api/Purchase/CertificateType/Save
        public IHttpActionResult Save([FromBody] RF_AUD_CERT_TYPEModel ob)
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
