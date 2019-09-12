using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Garments.Api
{
    [RoutePrefix("api/gmt")]
    [Authorize]
    public class GmtMcnSpecController : ApiController
    {
        [Route("GmtMcn/GetSwMcnGuideType")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnGuideType
        public IHttpActionResult GetSwMcnGuideType()
        {
            try
            {
                var obList = new GMT_SM_GUIDE_TYPModel().GetSwMcnGuideType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetSwAppType")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwAppType
        public IHttpActionResult GetSwAppType()
        {
            try
            {
                var obList = new GMT_SW_APP_TYPModel().GetSwAppType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetGmtStitchType")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetGmtStitchType
        public IHttpActionResult GetGmtStitchType()
        {
            try
            {
                var obList = new GMT_STCH_TYPModel().GetGmtStitchType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetSwMcnBedType")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnBedType
        public IHttpActionResult GetSwMcnBedType()
        {
            try
            {
                var obList = new GMT_SM_BED_TYPModel().GetSwMcnBedType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetSwMcnType")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnType
        public IHttpActionResult GetSwMcnType()
        {
            try
            {
                var obList = new GMT_SW_MCN_TYPModel().GetSwMcnType();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetSwMcnSpecList")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnSpecList
        public IHttpActionResult GetSwMcnSpecList(Int64 pageNumber, Int64 pageSize, string pSW_MCN_SPEC = null)
        {
            try
            {
                var obList = new GMT_SW_MCN_SPECModel().GetSwMcnSpecList(pageNumber, pageSize, pSW_MCN_SPEC);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetSwMcnSpecById")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnSpecById
        public IHttpActionResult GetSwMcnSpecById(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            try
            {
                var obList = new GMT_SW_MCN_SPECModel().GetSwMcnSpecById(pGMT_SW_MCN_SPEC_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtMcn/GetThrdConsList")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetThrdConsList
        public IHttpActionResult GetThrdConsList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            try
            {
                var obList = new GMT_SM_THRD_CONSModel().GetThrdConsList(pGMT_SW_MCN_SPEC_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetMcnGuideList")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetMcnGuideList
        public IHttpActionResult GetMcnGuideList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            try
            {
                var obList = new GMT_SW_MCN_GUIDEModel().GetMcnGuideList(pGMT_SW_MCN_SPEC_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtMcn/GetMcnProfileList")]
        [HttpGet]
        // GET :  /api/gmt/GmtMcn/GetSwMcnProfileList
        public IHttpActionResult GetSwMcnProfileList(Int64 pGMT_SW_MCN_SPEC_ID)
        {
            try
            {
                var obList = new GMT_SW_MACHINEModel().GetSwMcnProfileList(pGMT_SW_MCN_SPEC_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtMcn/SaveMcnSpec")]
        [HttpPost]
        // GET :  /api/gmt/GmtMcn/SaveMcnSpec
        public IHttpActionResult SaveMcnSpec([FromBody] GMT_SW_MCN_SPECModel ob)
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
