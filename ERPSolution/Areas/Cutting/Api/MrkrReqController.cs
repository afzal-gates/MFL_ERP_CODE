using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Cutting
{
    [RoutePrefix("api/Cutting")]
    public class MrkrReqController : ApiController
    {
        [Route("MrkrReq/GetCuttingTableList")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetCuttingTableList
        public IHttpActionResult GetCuttingTableList()
        {
            try
            {
                var obList = new GMT_CUT_TABLEModel().GetCuttingTableList();

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MrkrReq/GetMrkrHdr")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetMrkrHdr
        public IHttpActionResult GetMrkrHdr(Int64 pGMT_MRKR_ID)
        {
            try
            {
                var list = new GMT_MRKRModel().GetMrkrHdr(pGMT_MRKR_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MrkrReq/GetMarkerList")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetMarkerList
        public IHttpActionResult GetMarkerList(Int64 pMC_STYLE_H_ID, Int64? pMC_ORDER_H_ID = null, Int64? pGMT_COLOR_ID = null)
        {
            try
            {
                var obList = new GMT_MRKRModel().GetMarkerList(pMC_STYLE_H_ID, pMC_ORDER_H_ID, pGMT_COLOR_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MrkrReq/GetOrdSizeCutRatio")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetOrdSizeCutRatio
        public IHttpActionResult GetOrdSizeCutRatio(Int64 pMC_ORDER_H_ID, Int64 pGMT_COLOR_ID, Int64? pGMT_MRKR_ID = null)
        {
            try
            {
                var obList = new GMT_MRKR_D_ITEMModel().GetOrdSizeCutRatio(pMC_ORDER_H_ID, pGMT_COLOR_ID, pGMT_MRKR_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("MrkrReq/GetMrkrReqList")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetMrkrReqList
        public IHttpActionResult GetMrkrReqList(int pageNumber, int pageSize, Int64 pMC_BYR_ACC_GRP_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            try
            {
                var obList = new GMT_MRKR_REQModel().GetMrkrReqList(pageNumber, pageSize, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MrkrReq/GetMrkrReqHdr")]
        [HttpGet]
        // GET :  /api/Cutting/MrkrReq/GetMrkrReqHdr
        public IHttpActionResult GetMrkrReqHdr(Int64 pGMT_MRKR_REQ_ID)
        {
            try
            {
                var list = new GMT_MRKR_REQModel().GetMrkrReqHdr(pGMT_MRKR_REQ_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("MrkrReq/MrkrBatchSave")]
        [HttpPost]
        // POST :   /api/Cutting/MrkrReq/MrkrBatchSave
        public IHttpActionResult MrkrBatchSave([FromBody] GMT_MRKRModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MrkrBatchSave();
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


        [Route("MrkrReq/MrkrReqSave")]
        [HttpPost]
        // POST :   /api/Cutting/MrkrReq/MrkrReqSave
        public IHttpActionResult MrkrReqSave([FromBody] GMT_MRKR_REQModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MrkrReqSave();
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


        [Route("MrkrReq/MrkrReqFinalize")]
        [HttpPost]
        // POST :   /api/Cutting/MrkrReq/MrkrReqFinalize
        public IHttpActionResult MrkrReqFinalize([FromBody] GMT_MRKR_REQModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MrkrReqFinalize();
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
