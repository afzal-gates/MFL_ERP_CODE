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
    public class GmtSewProdScanController : ApiController
    {
        [Route("GmtSewProdScan/GetSewProdScanSummery")]
        [HttpGet]
        // GET :  /api/gmt/GmtSewProdScan/GetSewProdScanSummery?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult GetSewProdScanSummery(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_SEW_PROD_SCANModel().GetSewProdScanSummery(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtSewProdScan/GetSewProdDefectData")]
        [HttpGet]
        // GET :  /api/gmt/GmtSewProdScan/GetSewProdDefectData?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult GetSewProdDefectData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_SEW_PROD_SCANModel().GetSewProdDefectData(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Route("GmtSewProdScan/FinSewProdScanHrlyData")]
        [HttpGet]
        // GET :  /api/gmt/GmtSewProdScan/FinSewProdScanHrlyData?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult FinSewProdScanHrlyData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_SEW_PROD_SCANModel().FinSewProdScanHrlyData(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        


        [Route("GmtSewProdScan/SaveGmtSewProdScan")]
        [HttpPost]
        // GET :  /api/gmt/GmtSewProdScan/SaveGmtSewProdScan
        public IHttpActionResult SaveGmtSewProdScan([FromBody] GMT_SEW_PROD_SCANModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }
        }
    }
}
