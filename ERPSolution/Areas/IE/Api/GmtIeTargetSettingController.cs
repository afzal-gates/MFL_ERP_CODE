using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.IE.Api
{
    [RoutePrefix("api/ie")]
   
    public class GmtIeTargetSettingController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();
        [Route("GmtIeTarget/GetDataForTargetSetting")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/GetDataForTargetSetting?pRF_CALENDAR_DATE&pHR_PROD_FLR_ID
        public IHttpActionResult GetDataForTargetSetting(DateTime pRF_CALENDAR_DATE, Int32 pHR_PROD_FLR_ID )
        {
            try
            {
                var obList = new GMT_IE_TARGETModel().GetDataForTargetSetting(pRF_CALENDAR_DATE, pHR_PROD_FLR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/GetDataForTargetSettingBoard")]
        [HttpGet]
        // GET :  /api/ie/GmtIeTarget/GetDataForTargetSettingBoard?pRF_CALENDAR_DATE&pHR_PROD_FLR_LST
        public IHttpActionResult GetDataForTargetSettingBoard(DateTime pRF_CALENDAR_DATE, string pHR_PROD_FLR_LST)
        {
            try
            {
                var obList = new GMT_IE_TARGETModel().GetDataForTargetSettingBoard(pRF_CALENDAR_DATE, pHR_PROD_FLR_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/SewPlanProd4OutOfPlan")]
        [HttpGet]
        // GET :  /api/ie/GmtIeTarget/SewPlanProd4OutOfPlan?pGMT_PLN_LINE_LOAD_ID&pGMT_PROD_PLN_CLNDR_ID&pHR_PROD_LINE_ID
        public IHttpActionResult SewPlanProd4OutOfPlan(Int32 pGMT_PLN_LINE_LOAD_ID, Int32 pGMT_PROD_PLN_CLNDR_ID, Int32 pHR_PROD_LINE_ID)
        {
            try
            {
                var obList = new GMT_IE_TARGETModel().SewPlanProd4OutOfPlan(pGMT_PLN_LINE_LOAD_ID, pGMT_PROD_PLN_CLNDR_ID, pHR_PROD_LINE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        


        [Route("GmtIeTarget/Save")]
        [HttpPost]
        // GET :  /api/ie/GmtIeTarget/Save
        public IHttpActionResult Save([FromBody] GMT_IE_TARGET_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                Hub.Clients.All.executedFromServer();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {

                return Ok(new { success = false, e.Message });
            }
        }

        [Route("GmtIeTarget/SaveOutOfPlanProd")]
        [HttpPost]
        // GET :  /api/ie/GmtIeTarget/SaveOutOfPlanProd
        public IHttpActionResult SaveOutOfPlanProd([FromBody] GMT_IE_TARGET_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.SaveOutOfPlanProd();
                Hub.Clients.All.executedFromServer();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

        [Route("GmtIeTarget/GetDataIeManMinAdjustment")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/getDataIeManMinAdjustment?pOption&pHR_PROD_FLR_LST&pHR_PROD_LINE_LST&pRF_CALENDAR_DATE
        public IHttpActionResult getDataIeManMinAdjustment(int pOption, DateTime pRF_CALENDAR_DATE, string pHR_PROD_LINE_LST = null, string pHR_PROD_FLR_LST = null)
        {
            try
            {
                var obList = new GMT_IE_MIN_ADJModel().Query(pOption, pRF_CALENDAR_DATE, pHR_PROD_LINE_LST, pHR_PROD_FLR_LST);

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/SaveManMinAdjustmentData")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/SaveManMinAdjustmentData
        public IHttpActionResult SaveManMinAdjustmentData([FromBody] GMT_IE_MIN_ADJModel ob)
        {
            try
            {
                var obList = ob.Save();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/NptDataSave")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/NptDataSave
        public IHttpActionResult NptDataSave([FromBody] GMT_IE_NPT_DATA_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {

                return Ok(new { success = false, e.Message });
            }
        }

        [Route("GmtIeTarget/GetNptData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/GetNptData?pGMT_PROD_PLN_CLNDR_ID&pHR_PROD_LINE_ID
        public IHttpActionResult getDataIeManMinAdjustment(Int64 pGMT_PROD_PLN_CLNDR_ID, Int64 pHR_PROD_LINE_ID )
        {
            try
            {
                var obList = new GMT_IE_NPT_DATAModel().getNptHeaderData(pGMT_PROD_PLN_CLNDR_ID, pHR_PROD_LINE_ID);

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/GetItNptReasonData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/GetItNptReasonData?pOption
        public IHttpActionResult GetItNptReasonData(int pOption)
        {
            try
            {
                var obList = new GMT_IE_NPT_REASONModel().Query(pOption);

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtIeTarget/SaveItNptReasonData")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  /api/ie/GmtIeTarget/SaveItNptReasonData
        public IHttpActionResult SaveManMinAdjustmentData([FromBody] GMT_IE_NPT_REASONModel ob)
        {
            try
            {
                var obList = ob.Save();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
