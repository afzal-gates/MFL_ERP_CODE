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
    [Authorize]
    public class McLoadPlanController : ApiController
    {

        [Route("McLoadPlan/loadEventData")]
        [HttpGet]
        // GET :  /api/knit/McLoadPlan/loadEventData
        public IHttpActionResult loadEventData(DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            try
            {
                var obList = new MC_PLAN_EVENTModel().QueryData(pSTART_DT, pEND_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,e.Message);
            }
        }

        [Route("McLoadPlan/loadResourceData")]
        [HttpGet]
        // GET :  /api/knit/McLoadPlan/loadResourceData?pHR_PROD_FLR_ID&pKNT_MACHINE_ID&pHR_PROD_BLDNG_ID&pHR_COMPANY_ID
        public IHttpActionResult loadResourceData(int? pHR_PROD_FLR_ID = null, int? pKNT_MACHINE_ID = null, int? pHR_PROD_BLDNG_ID = null, int? pHR_COMPANY_ID=null,int?pKNT_MC_DIA_ID=null)
        {
            try
            {
                var obList = new MC_PLAN_RESOURCEModel().QueryData(pHR_PROD_FLR_ID, pKNT_MACHINE_ID, pHR_PROD_BLDNG_ID, pHR_COMPANY_ID, pKNT_MC_DIA_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,e.Message);
            }
        }


        [Route("McLoadPlan/loadCompanyBuildingData")]
        [HttpGet]
        // GET :  /api/knit/McLoadPlan/loadCompanyBuildingData?pHR_COMPANY_ID&psLK_PFLR_TYP_ID
        public IHttpActionResult loadCompanyBuildingData(Int64 pHR_COMPANY_ID, Int16 pLK_PFLR_TYP_ID)
        {
            try
            {
                var obList = new HR_PROD_FLRModel().getBuildingData(pHR_COMPANY_ID, pLK_PFLR_TYP_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        



        [Route("McLoadPlan/CreateStoreRequisition")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisition?pKNT_SC_PRG_ISS_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult CreateStoreRequisition(Int64? pKNT_SC_PRG_ISS_ID = null, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_PLAN_HModel().CreateStoreRequisition(pKNT_SC_PRG_ISS_ID, pKNT_JOB_CRD_H_ID);
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


        [Route("McLoadPlan/saveEventMovedData")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/saveEventMovedData?pXML&pOption
        public IHttpActionResult saveEventMovedData(String pXML, Int64? pOption = 1000)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new MC_PLAN_EVENTModel().saveEventMovedData(pXML, pOption);
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


        [Route("McLoadPlan/removeEmptyCell")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/removeEmptyCell
        public IHttpActionResult removeEmptyCell([FromBody] MC_PLAN_RESOURCEModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.removeEmptyCell();
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
