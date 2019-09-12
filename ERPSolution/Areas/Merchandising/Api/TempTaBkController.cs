using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    [Authorize]
    public class TempTaBkController : ApiController
    {
        [Route("TempTaBk/getTempHeaderList")]
        [HttpGet]
        // GET :  api/mrc/TempTaBk/getTempHeaderList
        public IHttpActionResult getTempHeaderList()
        {
            try
            {
                var obList = new MC_ACCS_PO_TMPLT_HModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TempTaBk/TrimAccItemList")]
        [HttpGet]
        // GET :  api/mrc/TempTaBk/TrimAccItemList
        public IHttpActionResult TrimAccItemList()
        {
            try
            {
                var obList = new MC_ACCS_PO_TMPLT_DModel().getTrim_n_AccItemList();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TempTaBk/getTempHeader")]
        [HttpGet]
        // GET :  api/mrc/TempTaBk/getTempHeader?pMC_ACCS_PO_TMPLT_H=0
        public IHttpActionResult getTempHeader(Int64 pMC_ACCS_PO_TMPLT_H)
        {
            try
            {
                var ob = new MC_ACCS_PO_TMPLT_HModel().Select(pMC_ACCS_PO_TMPLT_H);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TempTaBk/getTemplateControl")]
        [HttpGet]
        // GET :  api/mrc/TempTaBk/getTemplateControl
        public IHttpActionResult getTemplateControl(Int64? pMC_ACCS_PO_TMPLT_H_ID=null)
        {
            try
            {
                var ob = new MC_ACCS_PO_TMPLT_CFGModel().getTempControllsData(pMC_ACCS_PO_TMPLT_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("TempTaBk/Save")]
        [HttpPost]
        // GET :  mrc/api/TempTaBk/Save
        public IHttpActionResult Save([FromBody] MC_ACCS_PO_TMPLT_HModel ob)
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


        [Route("TempTaBk/SaveHrdData")]
        [HttpPost]
        // GET :  mrc/api/TempTaBk/SaveHrdData
        public IHttpActionResult SaveHrdData([FromBody] MC_ACCS_PO_TMPLT_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveHrdData();
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