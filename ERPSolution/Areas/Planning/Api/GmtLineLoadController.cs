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
    //[Authorize]
    public class GmtLineLoadController : ApiController
    {
        [Route("GmtLineLoad/getEventData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getEventData?pSTART_DT&pEND_DT
        public IHttpActionResult getEventData(
            DateTime pSTART_DT, DateTime pEND_DT, String pRESOURCES, Int64? pMC_ORDER_SHIP_ID = null, string pIS_REGULAR_VIEW = "Y", string pIS_GMT_ITEM_VIEW = "N"
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getEventData(pSTART_DT, pEND_DT, pRESOURCES, pMC_ORDER_SHIP_ID, pIS_REGULAR_VIEW, pIS_GMT_ITEM_VIEW);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getCompanyData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getCompanyData
        public IHttpActionResult getCompanyData()
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getCompanyData();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getOfficeList")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getOfficeList&pHR_COMPANY_ID
        public IHttpActionResult getOfficeList(Int32? pHR_COMPANY_ID=null)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getOfficeList(pHR_COMPANY_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getFloorData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getFloorData?pHR_OFFICE_ID
        public IHttpActionResult getFloorData(Int32? pHR_OFFICE_ID = null)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getFloorData(pHR_OFFICE_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getLineData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getLineData?pHR_PROD_FLR_ID
        public IHttpActionResult getLineData(Int32? pHR_PROD_FLR_ID = null)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getLineData(pHR_PROD_FLR_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GmtLineLoad/getResourceData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getResourceData
        public IHttpActionResult getResourceData(Int64? pHR_COMPANY_ID, Int64? pHR_OFFICE_ID, Int64? pHR_PROD_FLR_ID, Int64? pHR_PROD_LINE_ID, DateTime pSTART_DT, DateTime pEND_DT, String pRESOURCES, Int64? pMC_ORDER_SHIP_ID = null)
        {
            try
            {
                var ob = new LINE_LOAD_RESOURCEModel().getResourceData(pHR_COMPANY_ID, pHR_OFFICE_ID, pHR_PROD_FLR_ID, pHR_PROD_LINE_ID, pSTART_DT, pEND_DT, pRESOURCES, pMC_ORDER_SHIP_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getResourceDataDD")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getResourceDataDD
        public IHttpActionResult getResourceDataDD()
        {
            try
            {
                var ob = new LINE_LOAD_RESOURCEModel().getResourceDataDD();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getGmtLnLoadPhaseData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getGmtLnLoadPhaseData
        public IHttpActionResult getGmtLnLoadPhaseData()
        {
            try
            {
                var ob = new LINE_LOAD_RESOURCEModel().getGmtLnLoadPhaseData();
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }





        [Route("GmtLineLoad/updateEvent")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/updateEvent
        public IHttpActionResult updateEvent([FromBody] GMT_PLN_LINE_LOADModel ob)
        {
              string jsonStr = "";
                try
                {
                    jsonStr = ob.EventUpdate();
                    return Ok(new { success = true, jsonStr });
                }
                catch (Exception e)
                {                 
                    return Ok(new { success = false, errors = e.Message });
                }

        }

        [Route("GmtLineLoad/SaveManualLoading")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/SaveManualLoading
        public IHttpActionResult SaveManualLoading([FromBody] GMT_PLN_LINE_LOADModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.SaveManualLoading();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }

        [Route("GmtLineLoad/findPlnStatusRulesWithLine")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/findPlnStatusRulesWithLine?pRF_FAB_CLASS_ID&pMC_STYLE_D_ITEM_ID&pGMT_PRODUCT_TYP_ID&pINV_ITEM_CAT_ID
        public IHttpActionResult findPlnStatusRulesWithLine(Int64 pRF_FAB_CLASS_ID,Int64 pMC_STYLE_D_ITEM_ID,Int64 pGMT_PRODUCT_TYP_ID,Int64 pINV_ITEM_CAT_ID,
                  DateTime? pPLAN_START_DT,
                  DateTime? pPLAN_END_DT,
                  DateTime? pSHIP_DT,
                  Int64 pALLOCATED_QTY,
                  Int64 pPLAN_MP,
                  Int64 pPLAN_WH,
                  String pIS_LRN_CRV_APP,
                  Decimal? pPLAN_EFFICIENCY,
                  Int64 pMC_ORDER_ITEM_PLN_ID,
                  Decimal pSMV,
                  int pPLAN_OP
            
        )
        {
            try
            {
                var ob = new GMT_PLN_STS_RULEModel().findPlnStatusRulesWithLine(
                    pRF_FAB_CLASS_ID, 
                    pMC_STYLE_D_ITEM_ID, 
                    pGMT_PRODUCT_TYP_ID, 
                    pINV_ITEM_CAT_ID,
                    pPLAN_START_DT,
                    pPLAN_END_DT,
                    pSHIP_DT,
                    pALLOCATED_QTY,
                    pPLAN_MP,
                    pPLAN_WH,
                    pIS_LRN_CRV_APP,
                    pPLAN_EFFICIENCY,
                    pMC_ORDER_ITEM_PLN_ID,
                    pSMV,
                    pPLAN_OP
            );
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getForcastLineLoadData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getForcastLineLoadData?pHR_PROD_LINE_ID&pALLOCATED_QTY&pSEW_START_DT&pPLAN_MP&pPLAN_WH&pIS_LRN_CRV_APP
        //&pPLAN_EFFICIENCY&pMC_ORDER_ITEM_PLN_ID&pGMT_PRODUCT_TYP_ID&pRF_FAB_CLASS_ID&pSMV

        public IHttpActionResult getForcastLineLoadData(
            Int64 pHR_PROD_LINE_ID,
            Int64 pALLOCATED_QTY,
            Int64 pPLAN_MP,
            Int64 pPLAN_WH,
            string pIS_LRN_CRV_APP,
            decimal? pPLAN_EFFICIENCY,
            Int64 pMC_ORDER_ITEM_PLN_ID,
            Int64 pGMT_PRODUCT_TYP_ID,
            Int64 pRF_FAB_CLASS_ID,
            decimal pSMV,
            Int64? pGMT_PLN_LINE_LOAD_ID = null,
            DateTime? pSEW_START_DT = null,
            DateTime? pSEW_END_DT = null
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getForcastLineLoadData(
                         pHR_PROD_LINE_ID,pALLOCATED_QTY,pSEW_START_DT, pSEW_END_DT, pPLAN_MP,pPLAN_WH, pIS_LRN_CRV_APP,
                         pPLAN_EFFICIENCY, pMC_ORDER_ITEM_PLN_ID, pGMT_PRODUCT_TYP_ID, pRF_FAB_CLASS_ID,
                         pSMV,pGMT_PLN_LINE_LOAD_ID
                 );
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getEstimatedAllocatedQty")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getEstimatedAllocatedQty?pHR_PROD_LINE_ID&pSEW_START_DT&pSEW_END_DT&pPLAN_MP&pPLAN_WH&pIS_LRN_CRV_APP
        //&pPLAN_EFFICIENCY&pMC_ORDER_ITEM_PLN_ID&pGMT_PRODUCT_TYP_ID&pRF_FAB_CLASS_ID&pSMV

        public IHttpActionResult getEstimatedAllocatedQty(
            Int64 pHR_PROD_LINE_ID,
            DateTime pSEW_START_DT,
            DateTime pSEW_END_DT,
            Int64 pPLAN_MP,
            Int64 pPLAN_WH,
            string pIS_LRN_CRV_APP,
            decimal? pPLAN_EFFICIENCY,
            Int64 pMC_ORDER_ITEM_PLN_ID,
            Int64 pGMT_PRODUCT_TYP_ID,
            Int64 pRF_FAB_CLASS_ID,
            decimal pSMV,
            Int64? pGMT_PLN_LINE_LOAD_ID
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getEstimatedAllocatedQty(pHR_PROD_LINE_ID, pSEW_START_DT, pSEW_END_DT, pPLAN_MP, pPLAN_WH, pIS_LRN_CRV_APP, pPLAN_EFFICIENCY, pMC_ORDER_ITEM_PLN_ID, pGMT_PRODUCT_TYP_ID, pRF_FAB_CLASS_ID, pSMV, pGMT_PLN_LINE_LOAD_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtLineLoad/FindSewingStartDateByLine")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/FindSewingStartDateByLine?pHR_PROD_LINE_ID

        public IHttpActionResult FindSewingStartDateByLine(
            Int64 pHR_PROD_LINE_ID
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().FindSewingStartDateByLine(pHR_PROD_LINE_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getEventDataForTuning")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getEventDataForTuning?pGMT_PLN_LINE_LOAD_ID
        public IHttpActionResult getEventDataForTuning(
            Int64 pGMT_PLN_LINE_LOAD_ID
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getEventDataForTuning(pGMT_PLN_LINE_LOAD_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getOrderItmPlanList")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getOrderItmPlanList?pMC_STYLE_H_ID&pMC_STYLE_D_ITEM_ID
        public IHttpActionResult getOrderItmPlanList(Int64 pMC_STYLE_H_ID, Int64 pMC_STYLE_D_ITEM_ID, Int64 pMC_ORDER_ITEM_PLN_ID )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getOrderItmPlanList(pMC_STYLE_H_ID, pMC_STYLE_D_ITEM_ID,pMC_ORDER_ITEM_PLN_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        

        [Route("GmtLineLoad/getEventDataById")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getEventDataById?pGMT_PLN_LINE_LOAD_ID
        public IHttpActionResult getEventDataById( Int64 pGMT_PLN_LINE_LOAD_ID )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getEventDataById(pGMT_PLN_LINE_LOAD_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtLineLoad/updateEventForTuning")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/updateEventForTuning
        public IHttpActionResult updateEventForTuning([FromBody] GMT_PLN_LINE_LOADModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.updateEventForTuning();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }

        [Route("GmtLineLoad/getTnaDatas")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getTnaDatas?pGMT_PLN_LINE_LOAD_ID
        public IHttpActionResult getTnaDatas(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            try
            {
                var obList = new GMT_PLN_LINE_LD_CPModel().Query(pGMT_PLN_LINE_LOAD_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/getForcastLineLoadDataById")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getForcastLineLoadDataById?pGMT_PLN_LINE_LOAD_ID&pIS_DYNAMIC_START

        public IHttpActionResult getForcastLineLoadData(
            Int64 pGMT_PLN_LINE_LOAD_ID,
            string pIS_DYNAMIC_START = "N"
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getForcastLineLoadDataById(pGMT_PLN_LINE_LOAD_ID, pIS_DYNAMIC_START);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtLineLoad/getForcastLineLoadDataById2")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getForcastLineLoadDataById2?pGMT_PLN_LINE_LOAD_ID&pIS_DYNAMIC_END
        public IHttpActionResult getForcastLineLoadDataById2(
            Int64 pGMT_PLN_LINE_LOAD_ID,
            string pIS_DYNAMIC_END = "N"
        )
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getForcastLineLoadDataById2(pGMT_PLN_LINE_LOAD_ID, pIS_DYNAMIC_END);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtLineLoad/UpdateLineLoadCpData")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/UpdateLineLoadCpData
        public IHttpActionResult UpdateLineLoadCpData([FromBody] GMT_PLN_LINE_LD_CPModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.UpdateLineLoadCpData();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }

        [Route("GmtLineLoad/DeletePlanData")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/DeletePlanData
        public IHttpActionResult DeletePlanData([FromBody] GMT_PLN_LINE_LOADModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.DeletePlanData();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }



        [Route("GmtLineLoad/GetCpSummeryData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/GetCpSummeryData?pGMT_PLN_LINE_LOAD_ID
        public IHttpActionResult GetCpSummeryData(Int64 pGMT_PLN_LINE_LOAD_ID)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LD_CPModel().GetCpSummeryData(pGMT_PLN_LINE_LOAD_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtLineLoad/getProdPlanDataByLine")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/getProdPlanDataByLine?pHR_PROD_LINE_ID&pSEW_START_DT&pEND_DT
        public IHttpActionResult getProdPlanDataByLine(Int32 pHR_PROD_LINE_ID, DateTime? pSTART_DT = null, DateTime? pEND_DT = null, Int32? pOption = null)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LOADModel().getProdPlanDataByLine(pHR_PROD_LINE_ID, pSTART_DT, pEND_DT, pOption);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/FindProdMonitoringData")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/FindProdMonitoringData?pMC_ORDER_ITEM_PLN_ID
        public IHttpActionResult FindProdMonitoringData(Int64 pMC_ORDER_ITEM_PLN_ID)
        {
            try
            {
                var ob = new GMT_PLN_LINE_LD_CPModel().FindProdMonitoringData(pMC_ORDER_ITEM_PLN_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GmtLineLoad/GetStyleChangeLogDataById")]
        [HttpGet]
        // GET :  /api/pln/GmtLineLoad/GetStyleChangeLogDataById?pGMT_PLN_LINE_LOAD_ID&pGMT_PLN_STYL_CNGE_LOG_ID
        public IHttpActionResult GetStyleChangeLogDataById(Int64 pGMT_PLN_LINE_LOAD_ID, Int64 pGMT_PLN_STYL_CNGE_LOG_ID)
        {
            try
            {
                var ob = new GMT_PLN_STYL_CNGE_LOGModel().Select(pGMT_PLN_LINE_LOAD_ID, pGMT_PLN_STYL_CNGE_LOG_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtLineLoad/SavePlanChangeData")]
        [HttpPost]
        // GET :  /api/pln/GmtLineLoad/SavePlanChangeData
        public IHttpActionResult SavePlanChangeData([FromBody] GMT_PLN_STYL_CNGE_LOGModel ob)
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
