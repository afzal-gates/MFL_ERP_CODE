using ERP.DAL;
using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Controllers
{

    [RoutePrefix("api/inquiry")]
    //[System.Web.Http.Authorize]
    public class InquiryApiController : ApiController
    {
        [Route("SaveMktInqrQuot")]
        [HttpPost]
        // GET :  /api/inquiry/SaveMktInqrQuot
        public IHttpActionResult SaveMktInqrQuot([FromBody]MKT_INQR_QUOTModel ob)
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = ob.Save()
            });
        }

        [Route("SaveMktInqrQuotItm")]
        [HttpPost]
        // GET :  /api/inquiry/SaveMktInqrQuotItm
        public IHttpActionResult SaveMktInqrQuotItm([FromBody]MKT_INQR_QUOT_ITMModel ob)
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = ob.Save()
            });
        }


        [Route("SelectMktInqrQuot")]
        [HttpGet]
        // GET :  /api/inquiry/SelectMktInqrQuot?pMKT_INQR_QUOT_ID=1&pOption=3001
        public IHttpActionResult SelectMktInqrQuot(Int64 pMKT_INQR_QUOT_ID, int pOption)
        {
            return Ok(new ResponseMessage<MKT_INQR_QUOTModel>()
            {
                Result = new MKT_INQR_QUOTModel().Select(pMKT_INQR_QUOT_ID, pOption)
            });
        }

        [Route("FibreCompList")]
        [HttpGet]
        // GET :  /api/inquiry/FibreCompList
        public IHttpActionResult FibreCompList()
        {
            return Ok(new ResponseMessage<List<RF_FIB_COMPModel>>()
            {
                Result = new RF_FIB_COMPModel().getFibComposition4DD()
            });
        }


        [Route("YarnCountList")]
        [HttpGet]
        // GET :  /api/inquiry/YarnCountList
        public IHttpActionResult YarnCountList()
        {
            return Ok(new ResponseMessage<List<RF_YRN_CNTModel>>()
            {
                Result =  new RF_YRN_CNTModel().SelectAll()
            });
        }

        [Route("LookupListData")]
        [HttpGet]
        // GET :  api/inquiry/LookupListData?ID=1
        public IHttpActionResult LookupListData(Int64 ID)
        {
            return Ok(new ResponseMessage<List<LookupDataModel>>()
            {
                Result = new MKT_INQR_QUOT_ITMModel().LookupListData(ID)
            });
        }


        [Route("getBuyerDropdownList")]
        [HttpGet]
        // GET :  api/inquiry/getBuyerDropdownList
        public IHttpActionResult getBuyerDropdownList()
        {
            return Ok(new ResponseMessage<List<MC_BUYERModel>>()
            {
                Result = new MC_BUYERModel().getBuyerDdForCostApp()
            });
        }

        [Route("getBuyerAccListByUserId")]
        [HttpGet]
        // GET :  api/inquiry/getBuyerAccListByUserId?pSC_USER_ID
        public IHttpActionResult getBuyerAccListByUserId(Int64 pSC_USER_ID)
        {
            return Ok(new ResponseMessage<List<MC_BYR_ACCModel>>()
            {
                Result = new MC_BYR_ACCModel().getByrAccsByUserId4Costing(pSC_USER_ID)
            });
        }


        [Route("GetInqListData")]
        [HttpGet]
        // GET :  /api/inquiry/GetInqListData?pINQR_DT_FROM&pINQR_DT_TO&pINQR_NO&pLK_INQ_STATUS_ID&pLK_ITEM_GRP_ID&pSC_USER_ID
        public IHttpActionResult GetInqListData(DateTime? pINQR_DT_FROM = null, DateTime? pINQR_DT_TO = null, string pINQR_NO = null,
             Int32? pLK_INQ_STATUS_ID = null, Int32? pLK_ITEM_GRP_ID = null, Int32? pSC_USER_ID = null, Int32? pLK_FAB_TYPE_ID = null, Int32? pRF_FIB_COMP_ID = null)
        {
            return Ok(new ResponseMessage<List<MKT_INQR_QUOTDVModel>>()
            {
                Result = new MKT_INQR_QUOTModel().GetInqListData(pINQR_DT_FROM, pINQR_DT_TO, pINQR_NO, pLK_INQ_STATUS_ID, pLK_ITEM_GRP_ID, pSC_USER_ID, pLK_FAB_TYPE_ID, pRF_FIB_COMP_ID)
            }); 
        }

        [Route("FindSuggestedYarnCount")]
        [HttpGet]
        // GET :  /api/inquiry/FindSuggestedYarnCount?pGSM&pLK_FAB_TYPE_ID
        public IHttpActionResult FindSuggestedYarnCount(int pGSM, Int32 pLK_FAB_TYPE_ID)
        {
            return Ok(new ResponseMessage<string>()
            {
                Result = new MKT_YRN_CNT_CFGModel().FindSuggestedYarnCount(pGSM, pLK_FAB_TYPE_ID)
            }); 
        }

        [Route("GetItmVariationData4Costing")]
        [HttpGet]
        // GET :  /api/inquiry/GetItmVariationData4Costing?pLK_ITEM_GRP_ID&pORD_VOL
        public IHttpActionResult GetItmVariationData4Costing(int pLK_ITEM_GRP_ID, Int32 pORD_VOL = 0, string pSEARCH_TXT = null)
        {
            return Ok(new ResponseMessage<List<GMT_IE_ITM_VR_HEADModel>>()
            {
                Result = new GMT_IE_ITM_VARIATIONModel().getItmVariationData4Costing(pLK_ITEM_GRP_ID, pORD_VOL, pSEARCH_TXT)
            }); 
        }



        [Route("GetMktRateChartData")]
        [HttpGet]
        // GET :  /api/inquiry/GetMktRateChartData
        public IHttpActionResult GetMktRateChartData()
        {
            try
            {
                var obList = new MKT_FAB_RATE_TMPLTModel().getMktRateChartData();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UpdateRateForInq")]
        [HttpPost]
        // GET :  /api/inquiry/UpdateRateForInq
        public IHttpActionResult UpdateRateForInq([FromBody] MKT_FAB_RATE_TMPLTModel ob)
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
