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
    public class CutCutPnlInspectController : ApiController
    {
        [Route("CutCutPnlInspect/CheckScannedBarcode")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/CheckScannedBarcode
        public IHttpActionResult CheckScannedBarcode(string pBARCODE, Int64 pRF_GARM_PART_ID, Int32? pOption=3000)
        {
            try
            {
                var obList = new GMT_CUT_PNL_INSPTN_HModel().CheckScannedBarcode(pBARCODE, pRF_GARM_PART_ID, pOption);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutCutPnlInspect/SaveBundleCard")]
        [HttpPost]
        // GET :  /api/Cutting/CutCutPnlInspect/SaveBundleCard
        public IHttpActionResult SaveBundleCard([FromBody] GMT_CUT_PNL_INSPTN_HModel ob)
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

        [Route("CutCutPnlInspect/getLastBundleCardScannedInfo")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/getLastBundleCardScannedInfo?pGMT_CUT_PNL_INSPTN_H_ID&pOption
        public IHttpActionResult getLastBundleCardScannedInfo(Int64 pGMT_CUT_PNL_INSPTN_H_ID, int? pOption = null)
        {
            try
            {
                var obList = new GMT_BNDL_CARD_VIEWMODELModel().Select(pGMT_CUT_PNL_INSPTN_H_ID, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutCutPnlInspect/GetCalendarId")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/GetCalendarId
        public IHttpActionResult GetCalendarId(DateTime? pCALENDAR_DT = null)
        {
            try
            {
                var Obj = new GMT_CUT_PNL_INSPTN_HModel().GetCalendarId(pCALENDAR_DT);

                return Ok(Obj);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutCutPnlInspect/getCutPanelInsSummeryData")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/getCutPanelInsSummeryData?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult getCutPanelInsSummeryData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_BNDL_CARD_VIEWMODELModel().Query(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutCutPnlInspect/getRecutSummeryData")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/getRecutSummeryData?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult getRecutSummeryData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_BNDL_CARD_VIEWMODELModel().SummaryDataForRecut(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("CutCutPnlInspect/MarkAsQcPass")]
        [HttpPost]
        // GET :  /api/Cutting/CutCutPnlInspect/MarkAsQcPass
        public IHttpActionResult MarkAsQcPass([FromBody] GMT_CUT_PNL_INSPTN_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.MarkAsQcPass();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

        [Route("CutCutPnlInspect/CutPanelInsActions")]
        [HttpPost]
        // GET :  /api/Cutting/CutCutPnlInspect/CutPanelInsActions
        public IHttpActionResult CutPanelInsActions([FromBody] GMT_CUT_PNL_INSPTN_HModel ob)
        {
            //pOption=1002 => Mark As OK,
            //pOption= 1003 => Remove
            string jsonStr = "";
            try
            {
                jsonStr = ob.CutPanelInsActions();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

        [Route("CutCutPnlInspect/GetBundleListOfRejectedPanel")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/GetBundleListOfRejectedPanel?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult GetBundleListOfRejectedPanel(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_BNDL_CARD_VIEWMODELModel().GetBundleListOfRejectedPanel(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutCutPnlInspect/SaveDefectData")]
        [HttpPost]
        // GET :  /api/Cutting/CutCutPnlInspect/SaveDefectData
        public IHttpActionResult SaveDefectData([FromBody] GMT_CUT_PNL_INSPTN_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr =ob.SaveDefectData();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

        [Route("CutCutPnlInspect/ExecuteRecutAction")]
        [HttpPost]
        // GET :  /api/Cutting/CutCutPnlInspect/ExecuteRecutAction
        public IHttpActionResult ExecuteRecutAction([FromBody] GMT_CUT_PNL_RECUTModel ob)
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

        [Route("CutCutPnlInspect/getRecutSummeryDataOfRecut")]
        [HttpGet]
        // GET :  /api/Cutting/CutCutPnlInspect/getRecutSummeryDataOfRecut?pGMT_PROD_PLN_CLNDR_ID
        public IHttpActionResult getRecutSummeryDataOfRecut(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_BNDL_CARD_VIEWMODELModel().getBundlesOfRecutModal(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}
