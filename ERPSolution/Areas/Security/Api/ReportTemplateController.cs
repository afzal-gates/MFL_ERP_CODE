using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Security.Api
{
    [RoutePrefix("api/security")]
    public class ReportTemplateController : ApiController
    {
        [Route("RptTmp/SelectAll")]
        [HttpGet]
        // GET :  /api/security/RptTmp/SelectAll
        public IHttpActionResult SelectAll(Int64? pSC_USER_ID = null)
        {
            try
            {
                var list = new RF_RPT_TMPLTModel().SelectAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("RptTmp/GetTemplateBuyerListByID")]
        [HttpGet]
        // GET :  /api/security/RptTmp/GetTemplateBuyerListByID?pRF_RPT_TMPLT_ID=
        public IHttpActionResult GetTemplateBuyerListByID(Int64? pRF_RPT_TMPLT_ID = null)
        {
            try
            {
                var list = new RF_RPT_TMPLT_BYRModel().SelectByID(pRF_RPT_TMPLT_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("RptTmp/Save")]
        [HttpPost]
        // GET :  /api/security/RptTmp/Save
        public IHttpActionResult Save([FromBody] RF_RPT_TMPLTModel ob)
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
