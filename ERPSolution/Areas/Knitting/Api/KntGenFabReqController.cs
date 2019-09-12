using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class KntGenFabReqController : ApiController
    {        
        [Route("GenFabReq/GetFabReqHdr")]
        [HttpGet]
        // GET :  /api/knit/GenFabReq/GetFabReqHdr
        public IHttpActionResult GetFabReqHdr(Int64 pKNT_GEN_FAB_REQ_H_ID)
        {
            try
            {
                var list = new KNT_GEN_FAB_REQ_HModel().GetFabReqHdr(pKNT_GEN_FAB_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GenFabReq/GetFabReqDtl")]
        [HttpGet]
        // GET :  /api/knit/GenFabReq/GetFabReqDtl
        public IHttpActionResult GetFabReqDtl(Int64 pKNT_GEN_FAB_REQ_H_ID)
        {
            try
            {
                var list = new KNT_GEN_FAB_REQ_DModel().GetFabReqDtl(pKNT_GEN_FAB_REQ_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GenFabReq/GetFabReqList")]
        [HttpGet]
        // GET :  /api/knit/GenFabReq/GetFabReqList
        public IHttpActionResult GetFabReqList(int pageNumber, int pageSize, string pGFAB_REQ_NO = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            try
            {
                var obList = new KNT_GEN_FAB_REQ_HModel().GetFabReqList(pageNumber, pageSize, pGFAB_REQ_NO, pFROM_DT, pTO_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GenFabReq/BatchSave")]
        [HttpPost]
        // GET :  api/knit/GenFabReq/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_GEN_FAB_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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
