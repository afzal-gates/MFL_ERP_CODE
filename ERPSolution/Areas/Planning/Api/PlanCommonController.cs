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
    public class PlanCommonController : ApiController
    {


        [Route("PlanCommon/GmtPlnOrderShipDtChange")]
        [HttpPost]
        // GET :  /api/pln/PlanCommon/GmtPlnOrderShipDtChange
        public IHttpActionResult GmtPlnOrderShipDtChange(string pXML)
        {
            try
            {
                var jsonStr = new GMT_PLN_ODR_LIST_CAP_WKModel().Save(pXML);
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, jsonStr = e.Message });
            }
        }


        [Route("PlanCommon/getGmtPlanCapcityDshBrdData")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getGmtPlanCapcityDshBrdData?pGMT_PROD_PLN_CLNDR_ID_MN&pGMT_PROD_PLN_CLNDR_ID_WK
        public IHttpActionResult getGmtPlanCapcityDshBrdData(Int64 pGMT_PROD_PLN_CLNDR_ID_MN, Int64? pGMT_PROD_PLN_CLNDR_ID_WK)
        {
            try
            {
                var ob = new GMT_PLN_CAPACITY_DSHBRD_H().getGmtPlanDashboardData(pGMT_PROD_PLN_CLNDR_ID_MN, pGMT_PROD_PLN_CLNDR_ID_WK);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PlanCommon/getGmtPlanCapcityDshBrdDataWkShip")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getGmtPlanCapcityDshBrdDataWkShip?pSHIP_DT
        public IHttpActionResult getGmtPlanCapcityDshBrdDataWkShip(DateTime pSHIP_DT)
        {
            try
            {
                var ob = new GMT_PLN_CAPACITY_DSHBRD_H().getGmtPlanCapcityDshBrdDataWkShip(pSHIP_DT);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("PlanCommon/getGmtPlanCapcityDshBrdDataLineChrt")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getGmtPlanCapcityDshBrdDataLineChrt?pGMT_PROD_PLN_CLNDR_ID_MN
        public IHttpActionResult getGmtPlanCapcityDshBrdDataLineChrt(Int64 pGMT_PROD_PLN_CLNDR_ID_MN)
        {
            try
            {
                var ob = new GMT_PLN_CAPACITY_DSHBRD_H().getGmtPlanDashboardData_line_chrt (pGMT_PROD_PLN_CLNDR_ID_MN);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("PlanCommon/getHolydayData")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getHolydayData?pHR_COMPANY_ID&pHR_OFFICE_ID&pCORE_DEPT_ID&pSTART_DT&pEND_DT
        public IHttpActionResult getHolydayData(Int32 pHR_COMPANY_ID, Int32 pHR_OFFICE_ID, Int32? pCORE_DEPT_ID, DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            try
            {
                var ob = new GMT_PLN_HOLYDAYModel().Query(pHR_COMPANY_ID, pHR_OFFICE_ID, pCORE_DEPT_ID, pSTART_DT, pEND_DT);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("PlanCommon/getHolydayDataMD")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getHolydayDataMD?pHR_COMPANY_ID&pHR_OFFICE_ID&pSTART_DT&pEND_DT
        public IHttpActionResult getHolydayDataMD(Int32 pHR_COMPANY_ID, Int32 pHR_OFFICE_ID, DateTime? pSTART_DT, DateTime? pEND_DT)
        {
            try
            {
                var ob = new CORE_DEPT_MODEL().query(pHR_COMPANY_ID, pHR_OFFICE_ID, pSTART_DT, pEND_DT);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("PlanCommon/SaveHolyDayModelData")]
        [HttpPost]
        // GET :  /api/pln/PlanCommon/SaveHolyDayModelData
        public IHttpActionResult CritProcessSave([FromBody] GMT_PLN_HOLYDAYModel ob)
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

        [Route("PlanCommon/getBuyerAccListByUser")]
        [HttpGet]
        // GET :  /api/pln/PlanCommon/getBuyerAccListByUser
        public IHttpActionResult getBuyerAccListByUser()
        {
            try
            {
                var ob = new MC_BYR_ACCModel().getBuyerAccListForLineLoadPlan();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
    }
}
