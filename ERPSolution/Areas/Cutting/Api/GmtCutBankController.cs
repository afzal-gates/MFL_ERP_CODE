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
    public class GmtCutBankController : ApiController
    {
        [Route("GmtCutBank/GetStoreRecvSummaryData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetStoreRecvSummaryData?pGMT_PROD_PLN_CLNDR_ID=1
        public IHttpActionResult GetStoreRecvSummaryData(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().Query(pGMT_PROD_PLN_CLNDR_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/getLastScannedBundle")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getLastScannedBundle?pGMT_CUT_PNL_BNK_ID=1
        public IHttpActionResult getLastScannedBundle(Int64 pGMT_CUT_PNL_BNK_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().Select(pGMT_CUT_PNL_BNK_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/GetData4BndlStatus")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetData4BndlStatus
        public IHttpActionResult GetData4BndlStatus(Int64 pGMT_CUT_INFO_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().GetData4BndlStatus(pGMT_CUT_INFO_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/GetRescanData4SewInput")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetRescanData4SewInput
        public IHttpActionResult GetRescanData4SewInput()
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().GetRescanData4SewInput();

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getPrintDeliveryChallan")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getPrintDeliveryChallan?pOption=3000&pGMT_CUT_PRN_DELV_CHL_H_LST
        public IHttpActionResult getPrintDeliveryChallan(int pOption, string pGMT_CUT_PRN_DELV_CHL_H_LST)
        {
            try
            {
                var obList = new GMT_CUT_PRN_DELV_CHL_HModel().Query(pOption, pGMT_CUT_PRN_DELV_CHL_H_LST);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        



        [Route("GmtCutBank/SaveAndOrDelete")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/SaveAndOrDelete
        public IHttpActionResult SaveAndOrDelete([FromBody] GMT_CUT_PNL_BNKModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.SaveAndOrDelete();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

        [Route("GmtCutBank/SavePrintEmbrDelChallan")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/SavePrintEmbrDelChallan
        public IHttpActionResult SavePrintEmbrDelChallan([FromBody] GMT_CUT_PRN_DELV_CHL_HModel ob)
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

        [Route("GmtCutBank/SaveSewingDelChallan")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/SaveSewingDelChallan
        public IHttpActionResult SaveSewingDelChallan([FromBody] GMT_CUT_SEW_DLV_CHLModel ob)
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

        [Route("GmtCutBank/GetSewingDelChallanData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetSewingDelChallanData?pOption=3000&pGMT_CUT_SEW_DLV_CHL_ID
        public IHttpActionResult GetSewingDelChallanData(Int64 pGMT_CUT_SEW_DLV_CHL_ID, int pOption)
        {
            try
            {
                var obList = new GMT_CUT_SEW_DLV_CHLModel().Select(pGMT_CUT_SEW_DLV_CHL_ID, pOption);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/GetScSewingDelChallanData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetScSewingDelChallanData?pOption=3000&pGMT_CUT_SEW_DLV_CHL_ID
        public IHttpActionResult GetScSewingDelChallanData(Int64 pGMT_CUT_SEW_DLV_CHL_ID, int pOption)
        {
            try
            {
                var obList = new GMT_CUT_SC_DLV_CHLModel().Select(pGMT_CUT_SEW_DLV_CHL_ID, pOption);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/SaveScSewingDelChallan")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/SaveScSewingDelChallan
        public IHttpActionResult SaveScSewingDelChallan([FromBody] GMT_CUT_SC_DLV_CHLModel ob)
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

        [Route("GmtCutBank/GetPrintEmbrRecvData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetPrintEmbrRecvData?pOption=3006
        public IHttpActionResult GetPrintEmbrRecvData(Int32? pOption=null)
        {
            try
            {
                var obList = new GMT_EBR_SENT_VMODELModel().Query(pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/CutPrnRcvChlDsave")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/CutPrnRcvChlDsave
        public IHttpActionResult SaveScSewingDelChallan([FromBody] GMT_CUT_PRN_RCV_CHL_DModel ob)
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


        [Route("GmtCutBank/GetCountDataAtStoreRecvFromPrint")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetCountDataAtStoreRecvFromPrint
        public IHttpActionResult GetCountDataAtStoreRecvFromPrint()
        {
            try
            {
                var obList = new GMT_EBR_SENT_VMODELModel().GetCountDataAtStoreRecvFromPrint();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/getRejectedPrintEmbrData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getRejectedPrintEmbrData
        public IHttpActionResult getRejectedPrintEmbrData()
        {
            try
            {
                var obList = new GMT_EBR_SENT_VMODELModel().getRejectedPrintEmbrData();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/getPrintDeliveryRecvChallan")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getPrintDeliveryRecvChallan
        public IHttpActionResult getPrintDeliveryRecvChallan()
        {
            try
            {
                var obList = new GMT_CUT_PRN_RCV_CHL_HModel().Query();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/SavePrintEmbrRecvChallan")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/SavePrintEmbrRecvChallan
        public IHttpActionResult SavePrintEmbrRecvChallan([FromBody] GMT_CUT_PRN_RCV_CHL_HModel ob)
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

        [Route("GmtCutBank/getSewingDeliveryChallan")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getSewingDeliveryChallan?pGMT_CUT_SEW_DLV_CHL_ID=1
        public IHttpActionResult getSewingDeliveryChallan(Int64 pGMT_CUT_SEW_DLV_CHL_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().getSewingDeliveryChallan(pGMT_CUT_SEW_DLV_CHL_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getSewingScDeliveryChallan")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getSewingScDeliveryChallan?pGMT_CUT_SC_DLV_CHL_ID=1
        public IHttpActionResult getSewingScDeliveryChallan(Int64 pGMT_CUT_SC_DLV_CHL_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().getSewingScDeliveryChallan(pGMT_CUT_SC_DLV_CHL_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getPrintEmbrDelChallanSumData")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getPrintEmbrDelChallanSumData?pGMT_CUT_PRN_DELV_CHL_H_ID=1
        public IHttpActionResult getPrintEmbrDelChallanSumData(Int64 pGMT_CUT_PRN_DELV_CHL_H_ID)
        {
            try
            {
                var obList = new GMT_CUT_PNL_BNK_VModel().getPrintEmbrDelChallanSumData(pGMT_CUT_PRN_DELV_CHL_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getSewingDeliveryAutoCompl")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getSewingDeliveryAutoCompl?pCHALAN_NO=111
        public IHttpActionResult getSewingDeliveryAutoCompl(string pCHALAN_NO, Int64? pGMT_CUT_SEW_DLV_CHL_ID=null)
        {
            try
            {
                var obList = new GMT_CUT_SEW_DLV_CHLModel().getSewingDeliveryAutoCompl(pCHALAN_NO, pGMT_CUT_SEW_DLV_CHL_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getSewingScDeliveryAutoCompl")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getSewingScDeliveryAutoCompl?pCHALAN_NO=111
        public IHttpActionResult getSewingScDeliveryAutoCompl(string pCHALAN_NO = null, Int64? pGMT_CUT_SC_DLV_CHL_ID = null)
        {
            try
            {
                var obList = new GMT_CUT_SC_DLV_CHLModel().getSewingScDeliveryAutoCompl(pCHALAN_NO, pGMT_CUT_SC_DLV_CHL_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GmtCutBank/getPrintEmbrDeliveryChallanAutoCompl")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/getPrintEmbrDeliveryChallanAutoCompl?pCHALAN_NO=111
        public IHttpActionResult getPrintEmbrDeliveryChallanAutoCompl(string pCHALAN_NO = null, Int64? pGMT_CUT_PRN_DELV_CHL_H_ID = null)
        {
            try
            {
                var obList = new GMT_CUT_PRN_DELV_CHL_HModel().getPrintEmbrDeliveryChallanAutoCompl(pCHALAN_NO, pGMT_CUT_PRN_DELV_CHL_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/GetSewDelvChallanList")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetSewDelvChallanList
        public IHttpActionResult GetSewDelvChallanList(Int64? pGMT_CUT_SEW_DLV_CHL_ID = null, DateTime? pCHALAN_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_ORDER_H_ID = null,
            Int64? pMC_COLOR_ID = null, Int64? pHR_PROD_LINE_ID = null, string pIS_TAG = null, string pIS_FINALIZED = null)
        {
            try
            {
                var obList = new GMT_CUT_SEW_DLV_CHLModel().GetSewDelvChallanList(pGMT_CUT_SEW_DLV_CHL_ID, pCHALAN_DT, pMC_BYR_ACC_GRP_ID, pMC_ORDER_H_ID, pMC_COLOR_ID, pHR_PROD_LINE_ID, pIS_TAG, pIS_FINALIZED);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/GetPrintEmbrDelvChalnList")]
        [HttpGet]
        // GET :  /api/Cutting/GmtCutBank/GetPrintEmbrDelvChalnList
        public IHttpActionResult GetPrintEmbrDelvChalnList(Int64? pGMT_CUT_PRN_DELV_CHL_H_ID = null, DateTime? pCHALAN_DT = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_ID = null,
            Int64? pMC_ORDER_H_ID = null, Int64? pMC_COLOR_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pLK_BNDL_MVM_RSN_ID = null, string pIS_TAG = null, string pIS_FINALIZED = null)
        {
            try
            {
                var obList = new GMT_CUT_PRN_DELV_CHL_HModel().GetPrintEmbrDelvChalnList(pGMT_CUT_PRN_DELV_CHL_H_ID, pCHALAN_DT, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_ID, pMC_ORDER_H_ID, pMC_COLOR_ID, pSCM_SUPPLIER_ID, pLK_BNDL_MVM_RSN_ID, pIS_TAG, pIS_FINALIZED);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtCutBank/MergeChallan")]
        [HttpPost]
        // GET :  /api/Cutting/GmtCutBank/MergeChallan
        public IHttpActionResult MergeChallan([FromBody] GMT_CUT_PRN_DELV_CHL_HModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.MergeChallan();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }




    }
}
