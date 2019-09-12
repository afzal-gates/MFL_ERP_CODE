using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Planning.Api
{
    [RoutePrefix("api/pln")]
    [Authorize]
    public class PlnActvtyPermissionController : ApiController
    {
        [Route("PlnActvtyPermission/GetEventAccess")]
        [HttpGet]
        // GET :  /api/pln/PlnActvtyPermission/GetEventAccess
        public IHttpActionResult GetEventAccess(long pSC_USER_ID)
        {
            try
            {
                var obList = new GMT_PLN_EVNT_ACCESSModel().GetEventAccess(pSC_USER_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("PlnActvtyPermission/EventSave")]
        [HttpPost]
        // GET :  /api/pln/PlnActvtyPermission/EventSave
        public IHttpActionResult EventSave([FromBody] GMT_PLN_EVNT_ACCESSModel ob)
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


        [Route("PlnActvtyPermission/GetPlnRsurcAccessFlrLnByUser")]
        [HttpGet]
        // GET :  /api/pln/PlnActvtyPermission/GetPlnRsurcAccessFlrLnByUser
        public IHttpActionResult GetPlnRsurcAccessFlrLnByUser(Int64 pLK_PFLR_TYP_ID, Int64 pSC_USER_ID)
        {
            try
            {
                var obList = new HR_PROD_FLRModel().GetPlnRsurcAccessFlrLnByUser(pLK_PFLR_TYP_ID, pSC_USER_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PlnActvtyPermission/ResourceSave")]
        [HttpPost]
        // GET :  /api/pln/PlnActvtyPermission/ResourceSave
        public IHttpActionResult ResourceSave([FromBody] GMT_PLN_RSRC_ACCESSModel ob)
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


        [Route("PlnActvtyPermission/GetPlnOrdAccess")]
        [HttpGet]
        // GET :  /api/pln/PlnActvtyPermission/GetPlnOrdAccess
        public IHttpActionResult GetPlnOrdAccess(Int64 pSC_USER_ID)
        {
            try
            {
                var obList = new GMT_PLN_ORD_ACCESSModel().GetPlnOrdAccess(pSC_USER_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PlnActvtyPermission/OrdAccessSave")]
        [HttpPost]
        // GET :  /api/pln/PlnActvtyPermission/OrdAccessSave
        public IHttpActionResult OrdAccessSave([FromBody] GMT_PLN_ORD_ACCESSModel ob)
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
