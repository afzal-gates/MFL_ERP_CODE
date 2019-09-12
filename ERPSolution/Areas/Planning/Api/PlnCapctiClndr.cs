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
    public class CapctiClndrController : ApiController
    {        
        [Route("CapctiClndr/GetClndrYearList")]
        [HttpGet]
        // GET :  /api/pln/CapctiClndr/GetClndrYearList
        public IHttpActionResult GetClndrYearList(string pFY_NAME_EN = null)
        {
            try
            {
                var obList = new GMT_PROD_PLN_CLNDRModel().GetClndrYearList(pFY_NAME_EN);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CapctiClndr/GetClndrMonthList")]
        [HttpGet]
        // GET :  /api/pln/CapctiClndr/GetClndrMonthList
        public IHttpActionResult GetClndrMonthList(Int64 pRF_FISCAL_YEAR_ID, string pMONTH_NAME_EN = null)
        {
            try
            {
                var obList = new GMT_PROD_PLN_CLNDRModel().GetClndrMonthList(pRF_FISCAL_YEAR_ID, pMONTH_NAME_EN);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("CapctiClndr/GetClndrWkList")]
        [HttpGet]
        // GET :  /api/pln/CapctiClndr/GetClndrWkList
        public IHttpActionResult GetClndrWkList(Int64? pRF_FISCAL_YEAR_ID = null, Int64? pRF_CAL_MONTH_ID = null, Int64? pPARENT_ID = null)
        {
            try
            {
                var obList = new GMT_PROD_PLN_CLNDRModel().GetClndrWkList(pRF_FISCAL_YEAR_ID, pRF_CAL_MONTH_ID, pPARENT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CapctiClndr/getCurrentProdCalendarList")]
        [HttpGet]
        // GET :  /api/pln/CapctiClndr/getCurrentProdCalendarList
        public IHttpActionResult getCurrentProdCalendarList()
        {
            try
            {
                var obList = new GMT_PROD_PLN_CLNDRModel().getCurrentProdCalendarList();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        

        [Route("CapctiClndr/ProdClndrProcess")]
        [HttpPost]
        // GET :  /api/pln/CapctiClndr/ProdClndrProcess
        public IHttpActionResult ProdClndrProcess([FromBody] GMT_PROD_PLN_CLNDRModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ProdClndrProcess();
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
