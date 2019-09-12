﻿using ERP.Model;
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
    [Authorize]
    public class KntGreyFabRcvController : ApiController
    {


        [Route("KntGreyFabRcv/GetFabRcvDataByDate")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabRcv/GetFabRcvDataByDate
        public IHttpActionResult GetFabRcvDataByDate(int pageNumber, int pageSize, Int32? pSCM_STORE_ID=null, DateTime? pRCV_DT = null)
        {
            try
            {
                var obList = new KNT_FAB_STR_RCVModel().GetFabRcvDataByDate(pageNumber, pageSize, pSCM_STORE_ID, pRCV_DT);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntGreyFabRcv/GetRollData")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabRcv/GetRollData
        public IHttpActionResult GetRollData(string pFAB_ROLL_NO, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var obList = new KNT_FAB_STR_RCV_VM().GetRollData(pFAB_ROLL_NO, pSCM_SUPPLIER_ID, pSCM_STORE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntGreyFabRcv/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntGreyFabRcv/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_FAB_STR_RCVModel ob)
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
