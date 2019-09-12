using ERP.Model;
using ERP.Model.Purchase;
using ERPSolution.Hubs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/dye")]
    [Authorize]
    [NoCache]
    public class DyeBatchPlanController : ApiController
    {
        [Route("DyeBatchPlan/getDyeBatchPlanResourceData")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getDyeBatchPlanResourceData?pDYE_MC_TYPE_ID&pIS_SMP_BLK&pDYE_BATCH_SCDL_ID
        public IHttpActionResult getDyeBatchPlanResourceData(Int64? pDYE_MC_TYPE_ID, String pIS_SMP_BLK, Int64 pDYE_BATCH_SCDL_ID, Int64? pDYE_MACHINE_ID=null, Int64? pHR_COMPANY_ID=null)
        {
            try
            {
                var obList = new BATCH_PLAN_RESOURCEModel().QueryData(pDYE_MC_TYPE_ID, pIS_SMP_BLK, pDYE_BATCH_SCDL_ID, pDYE_MACHINE_ID, pHR_COMPANY_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeBatchPlan/getSchedulePlanDatas")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getSchedulePlanDatas?pSCDL_REF_NO
        public IHttpActionResult getSchedulePlanDatas(string pSCDL_REF_NO = null, Int64? pDYE_BATCH_SCDL_ID = null, string pIS_SMP_BLK = "B")
        {
            try
            {
                var obList = new DYE_BATCH_SCDLModel().SelectAll(pSCDL_REF_NO, pDYE_BATCH_SCDL_ID, pIS_SMP_BLK);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatchPlan/getSchedulePlanData")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getSchedulePlanData?pDYE_BATCH_SCDL_ID
        public IHttpActionResult getSchedulePlanDatas(Int64? pDYE_BATCH_SCDL_ID, string pIS_SMP_BLK="B")
        {
            try
            {
                var obList = new DYE_BATCH_SCDLModel().Select(pDYE_BATCH_SCDL_ID, pIS_SMP_BLK);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeBatchPlan/getProgAvailabilityWovenFab")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getProgAvailabilityWovenFab?pMC_FAB_PROD_ORD_H_ID&pMC_COLOR_ID&pINV_ITEM_CAT_ID
        public IHttpActionResult getProgAvailabilityWovenFab(string pMC_FAB_PROD_ORD_H_LST, Int64 pMC_COLOR_ID, Int64 pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new MC_TRMS_DY_PRODModel().QueryStockBalance(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatchPlan/getProgAvailabilityWovenFabScP")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getProgAvailabilityWovenFabScP?pMC_FAB_PROD_ORD_H_ID&pMC_COLOR_ID&pINV_ITEM_CAT_ID
        public IHttpActionResult getProgAvailabilityWovenFabScP(string pMC_FAB_PROD_ORD_H_LST, Int64 pMC_COLOR_ID, Int64 pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new MC_TRMS_DY_PRODModel().QueryStockBalanceScP(pMC_FAB_PROD_ORD_H_LST, pMC_COLOR_ID, pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatchPlan/getColorList")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getColorList?pMC_FAB_PROD_ORD_H_ID&pMC_COLOR_ID&pINV_ITEM_CAT_ID
        public IHttpActionResult getColorList(string pMC_FAB_PROD_ORD_H_LST, Int64 pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new MC_COLORModel().getColorListByDyeBatch(pMC_FAB_PROD_ORD_H_LST, pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DyeBatchPlan/loadEventData")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/loadEventData?pSTART_DT&pEND_DT
        public IHttpActionResult loadEventData(DateTime pSTART_DT, DateTime pEND_DT)
        {
            try
            {
                var obList = new BATCH_PLAN_EVENTModel().QueryData(pSTART_DT, pEND_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatchPlan/loadEventDataRpt")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/loadEventDataRpt?pDYE_BATCH_SCDL_ID
        public IHttpActionResult loadEventData(Int64? pDYE_BATCH_SCDL_ID)
        {
            try
            {
                var obList = new BATCH_PLAN_EVENTModel().loadEventDataRpt(pDYE_BATCH_SCDL_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("DyeBatchPlan/getDataForDropDown")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getDataForDropDown?pOption
        public IHttpActionResult getDataForDropDown(Int32 pOption)
        {
            try
            {
                var obList = new BATCH_PLAN_EVENTModel().getDataForDropDown(pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeBatchPlan/getLastScPlanDt")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getLastScPlanDt?pIS_SMP_BLK
        public IHttpActionResult getLastScPlanDt(string pIS_SMP_BLK)
        {
            try
            {
                var obList = new DYE_BATCH_SCDLModel().getLastScPlanDt(pIS_SMP_BLK);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatchPlan/SaveSchedulePlanData")]
        [HttpPost]
        // GET :  api/dye/DyeBatchPlan/SaveSchedulePlanData
        public IHttpActionResult Save([FromBody] DYE_BATCH_SCDLModel ob)
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



        [Route("DyeBatchPlan/SaveProgamData")]
        [HttpPost]
        // GET :  api/dye/DyeBatchPlan/SaveProgamData
        public IHttpActionResult Save([FromBody] BATCH_PLAN_EVENTModel ob)
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

        [Route("DyeBatchPlan/XMLDataSaving")]
        [HttpPost]
        // POST :  api/knit/DyeBatchPlan/XMLDataSaving?pXML&pOption
        public IHttpActionResult XMLDataSaving(String pXML, Int64? pOption = 1000)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new BATCH_PLAN_EVENTModel().XMLDataSaving(pXML, pOption);
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


        [Route("DyeBatchPlan/getDFProcessData")]
        [HttpGet]
        // GET :  /api/dye/DyeBatchPlan/getDFProcessData?pDYE_BATCH_PLAN_ID=1
        public IHttpActionResult getDFProcessData(Int64? pDYE_BATCH_PLAN_ID)
        {
            try
            {
                var obList = new DF_PROC_TYPEModel().getGroupData(pDYE_BATCH_PLAN_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
