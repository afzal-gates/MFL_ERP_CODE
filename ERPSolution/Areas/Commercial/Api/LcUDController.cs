using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/cmr")]
    public class LcUDController : ApiController
    {
        [Route("LcUD/GetAllUDInfo/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/cmr/LcUD/GetAllUDInfo/{pageNo}/{pageSize}?pMC_BUYER_ID=
        public IHttpActionResult GetAllUDInfo(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, Int64? pMC_BUYER_ID = null, string pLC_NO = null)
        {
            try
            {
                var data = new CM_UD_HModel().SelectAll(pSCM_SUPPLIER_ID, pMC_BUYER_ID, pLC_NO);
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

        [Route("LcUD/GetUDInfo")]
        [HttpGet]
        // GET :  /api/cmr/LcUD/GetUDInfo?pCM_UD_H_ID=
        public IHttpActionResult GetUDInfo(Int64? pCM_UD_H_ID = null)
        {
            try
            {
                var list = new CM_UD_HModel().Select(pCM_UD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("LcUD/GetExpUDInfo")]
        [HttpGet]
        // GET :  /api/cmr/LcUD/GetExpUDInfo?pCM_UD_H_ID=
        public IHttpActionResult GetExpUDInfo(Int64? pCM_UD_H_ID = null)
        {
            try
            {
                var list = new CM_UD_DModel().SelectExpUD(pCM_UD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("LcUD/GetImpUDInfo")]
        [HttpGet]
        // GET :  /api/cmr/LcUD/GetImpUDInfo?pCM_UD_H_ID=
        public IHttpActionResult GetImpUDInfo(Int64? pCM_UD_H_ID = null)
        {
            try
            {
                var list = new CM_UD_DModel().SelectImpUD(pCM_UD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LcUD/Save")]
        [HttpPost]
        // GET :  api/Cmr/LcUD/Save
        public IHttpActionResult Save([FromBody] CM_UD_HModel ob)
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


        [Route("LcUD/Update")]
        [HttpPost]
        // GET :  api/Cmr/LcUD/Update
        public IHttpActionResult Update([FromBody] CM_UD_HModel ob)
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
    }
}
