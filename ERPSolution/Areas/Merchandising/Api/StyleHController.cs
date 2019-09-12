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
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class StyleHController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("StyleH/GetStyleList")]
        [HttpGet]
        // GET :  /api/mrc/StyleH/GetStyleList
        public IHttpActionResult GetStyleList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, string pSTYLE_NO = null)
        {
            try
            {
                var obList = new MC_STYLE_HModel().GetStyleList(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pSTYLE_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StyleH/BuyerAccWiseStyleListData/{pMC_BYR_ACC_ID}")]
        [HttpGet]
        // GET :  /api/mrc/StyleH/BuyerAccWiseStyleListData/70
        public IHttpActionResult BuyerAccWiseStyleListData(Int64? pMC_BYR_ACC_ID)
        {
            try
            {
                var obList = new MC_STYLE_HModel().BuyerAccWiseStyleListData(pMC_BYR_ACC_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("StyleH/BuyerWiseStyleListData/{pMC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/BuyerWiseStyleListData/1
        public IHttpActionResult BuyerWiseStyleListData(Int64 pMC_BUYER_ID)
        {

            try
            {
                var obList = new MC_STYLE_HModel().BuyerWiseStyleListData(pMC_BUYER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/BuyerWiseStyleListDataByID/{pMC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/BuyerWiseStyleListData/1
        public IHttpActionResult BuyerWiseStyleListDataByID(Int64 pMC_BUYER_ID)
        {

            try
            {
                var obList = new MC_STYLE_HModel().BuyerWiseStyleListDataByID(pMC_BUYER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        

        [Route("StyleH/ItemDataForStyleBOM/{MC_STYLE_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/ItemDataForStyleBOM/1
        public IHttpActionResult ItemDataForStyleBOM(Int64 MC_STYLE_H_ID, Int64? MC_BUYER_ID, Int64? pMC_BYR_ACC_ID)
        {

            try
            {
                var obList = new INV_ITEM_CATModel().ItemDataForStyleBOM(MC_STYLE_H_ID, MC_BUYER_ID, pMC_BYR_ACC_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("StyleH/ItemDataForBuyerBOM/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/ItemDataForBuyerBOM/1
        public IHttpActionResult ItemDataForBuyerBOM(Int64 MC_BUYER_ID, Int64? pMC_BYR_ACC_ID)
        {

            try
            {
                var obList = new INV_ITEM_CATModel().ItemDataForBuyerBOM(MC_BUYER_ID, pMC_BYR_ACC_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("StyleH/FindMktCostHead/{MC_STYLE_H_EXT_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/FindMktCostHead/1
        public IHttpActionResult FindMktCostHead(Int64 MC_STYLE_H_EXT_ID)
        {
            try
            {
                var obList = new MC_STYL_MKT_COSTModel().FindMktCostHead(MC_STYLE_H_EXT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("StyleH/ItemDataForStyleBlkBOM/{MC_STYLE_H_ID:int}/{MC_BLK_FAB_REQ_H_ID:int}/{MC_STYL_BGT_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/ItemDataForStyleBlkBOM/1/2
        public IHttpActionResult ItemDataForStyleBlkBOM(Int64 MC_STYLE_H_ID, Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            try
            {
                var obList = new INV_ITEM_CATModel().ItemDataForStyleBlkBOM(MC_STYLE_H_ID, MC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("StyleH/BuyerWiseStyleList")]
        [HttpGet]
        // GET :  api/mrc/StyleH/BuyerWiseStyleList
        public IHttpActionResult BuyerWiseStyleList(
                Int64 pMC_BYR_ACC_ID, 
                Int64 pageNumber, 
                Int64 pageSize, 
                String pSTYLE_NO = null, 
                String pITEM_CAT_NAME_EN = null, 
                String pGARM_TYPE_NAME = null, 
                String pGARM_DEPT_NAME = null, 
                String pSLV_TYPE_NAME = null, 
                String pNECK_TYPE_NAME = null,
                Int64? pLK_STYL_DEV_ID = null
        )
        {

            try
            {
                var obList = new MC_STYLE_ITEM_ViewModel().BuyerWiseStyleList(pMC_BYR_ACC_ID, pageNumber, pageSize, pSTYLE_NO, pITEM_CAT_NAME_EN, pGARM_TYPE_NAME, pGARM_DEPT_NAME, pSLV_TYPE_NAME, pNECK_TYPE_NAME, pLK_STYL_DEV_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/BuyerAcWiseStyleList")]
        [HttpGet]
        // GET :  api/mrc/StyleH/BuyerAcWiseStyleList
        public IHttpActionResult BuyerAcWiseStyleList(Int64 pageNumber, Int64 pageSize, Int64 pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID = null, String pSTYLE_NO = null)
        {

            try
            {
                var obList = new MC_STYLE_HModel().BuyerAcWiseStyleList(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pSTYLE_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("StyleH/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/StyleH/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var obList = new MC_STYLE_HModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [System.Web.Http.Authorize]
        [Route("StyleH/getStyleDropDown")]
        [HttpGet]
        // GET :  api/mrc/StyleH/getStyleDropDown
        public IHttpActionResult getStyleDropDown(string pSTYLE_NO, Int64? pMC_BUYER_ID, Int64? pMC_STYLE_H_ID)
        {
            try
            {
                var obList = new MC_STYLE_HModel().getStyleDropDown(pSTYLE_NO, pMC_BUYER_ID, pMC_STYLE_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/Select
        public IHttpActionResult Select(Int64 ID, Int64? pLK_STYL_DEV_ID = null)
        {

            try
            {
                var ob = new MC_STYLE_HModel().Select(ID, pLK_STYL_DEV_ID);
                return Ok(ob);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StyleH/StyleNoLabdipRefAuto/{STYLE_NO}/{MC_COLOR_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/StyleNoLabdipRefAuto/1/1
        public IHttpActionResult Select(string STYLE_NO, Int64 MC_COLOR_ID)
        {
            try
            {
                var ob = new MC_STYLE_HModel().StyleNoLabdipRefAuto(STYLE_NO, MC_COLOR_ID);
                return Ok(ob);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        


        [Route("StyleH/getStyleExtAuto/{MC_STYLE_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/getStyleExtAuto/1
        public IHttpActionResult getStyleExtAuto(Int64 MC_STYLE_H_ID)
        {
            try
            {
                String ext = new MC_STYLE_HModel().getStyleExtAuto(MC_STYLE_H_ID);
                return Ok(ext);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        

        [Route("StyleH/TnAList/OrderType/{LK_ORDER_TYPE_ID?}/Buyer/{MC_BUYER_ID?}/LeadTime/{LEAD_TIME?}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/TnAList/OrderType/1/Buyer/2/LeadTime/3
        public IHttpActionResult TnAList(Int64? LK_ORDER_TYPE_ID, Int64? MC_BUYER_ID, Int64? LEAD_TIME)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_HModel().GetTnAList(LK_ORDER_TYPE_ID, MC_BUYER_ID, LEAD_TIME);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StyleH/TnAList/TnATemp/{MC_TNA_TMPLT_H_ID:int}/Order/{MC_ORDER_H_ID:int}/Buyer/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/TnAList/TnATemp/1/Order/2/Buyer/1
        public IHttpActionResult TnAListByTemplate(Int64 MC_TNA_TMPLT_H_ID, Int64 MC_ORDER_H_ID, Int64 MC_BUYER_ID)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_DModel().TnAListByTemplate(MC_TNA_TMPLT_H_ID, MC_ORDER_H_ID, MC_BUYER_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/TnAList/TnATempDev/{MC_TNA_TMPLT_H_ID:int}/Order/{MC_ORDER_H_ID:int}/Buyer/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/TnAList/TnATempDev/1/Order/2/Buyer/1
        public IHttpActionResult TnAListByTemplateDev(Int64 MC_TNA_TMPLT_H_ID, Int64 MC_ORDER_H_ID, Int64 MC_BUYER_ID)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_DModel().TnAListByTemplateDev(MC_TNA_TMPLT_H_ID, MC_ORDER_H_ID, MC_BUYER_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/getSampleTasksByOrder")]
        [HttpGet]
        // GET :  api/mrc/StyleH/getSampleTasksByOrder?pMC_ORDER_H_ID;
        public IHttpActionResult getSampleTasksByOrder(Int64 pMC_ORDER_H_ID)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_DModel().getSampleTasksByOrder(pMC_ORDER_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StyleH/getStrikeOffTasksByOrder")]
        [HttpGet]
        // GET :  api/mrc/StyleH/getStrikeOffTasksByOrder?pMC_ORDER_H_ID;
        public IHttpActionResult getStrikeOffTasksByOrder(Int64 pMC_ORDER_H_ID)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_DModel().getStrikeOffTasksByOrder(pMC_ORDER_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }





        [Route("StyleH/TnAModal/TnaRevise")]
        [HttpGet]
        // GET :  api/mrc/StyleH/TnAModal/TnaRevise?MC_ORDER_H_ID=1&REVISION_NO=1&REVISION_DT&REV_REASON
        public IHttpActionResult TnaRevise(Int64 MC_ORDER_H_ID, Int32 REVISION_NO,  DateTime REVISION_DT, String REV_REASON)
        {
            try
            {
                var ob = new MC_TNA_TMPLT_HModel().TnaRevise(MC_ORDER_H_ID,REVISION_NO,REVISION_DT,REV_REASON);

                Hub.Clients.All.updateNotifCount();
                Hub.Clients.All.getMessageData();

                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("StyleH/SaveTnATemplateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc//StyleH/SaveTnATemplateData
        public IHttpActionResult Save([FromBody] MC_TNA_TMPLT_DModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveTnATemplateData();

                    if (ob.IS_TNA_FINALIZED == "Y")
                    {
                        Hub.Clients.All.updateNotifCount();
                        Hub.Clients.All.getMessageData();
                    }
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


        [Route("StyleH/SameOrderTnASave")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/SameOrderTnASave
        public IHttpActionResult SameOrderTnASave([FromBody] StyleOrderViewModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SameOrderTnASave();
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


        [Route("StyleH/SaveOrder")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/SaveOrder
        public IHttpActionResult SaveOrder([FromBody] StyleOrderViewModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveOrder();
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


        [Route("StyleH/SaveStyleBomData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/SaveStyleBomData
        public IHttpActionResult Save([FromBody] MC_STYLE_D_BOMModel ob)
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

        [Route("StyleH/SaveBuyerBomData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/SaveBuyerBomData
        public IHttpActionResult Save([FromBody] MC_BUYER_BOMModel ob)
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

        [Route("StyleH/SaveStyleBomBlkData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/SaveStyleBomBlkData
        public IHttpActionResult Save([FromBody] MC_STYLE_BLK_BOMModel ob)
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









        [Route("StyleH/SelectStyle/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/SelectStyle
        public IHttpActionResult SelectStyle(Int64 ID)
        {
            try
            {
                var ob = new StyleOrderViewModel().SelectStyle(ID);
                return Ok(ob);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StyleH/SelectOrder/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/SelectOrder
        public IHttpActionResult SelectOrder(Int64 ID, Int64? pMC_STYLE_H_ID=null)
        {
            try
            {
                if (ID < 1)
                {
                    var ob = new StyleOrderViewModel() {
                       LK_ORD_STATUS_ID = 363,
                       QTY_MOU_ID =1,
                       RF_CURRENCY_ID = 2,
                       IS_MULTI_SHIP_DT= "N",
                       ORD_CONF_DT = DateTime.Now.Date,
                       HR_COMPANY_ID =1,
                       PROD_UNIT_ID =2,
                       IS_PROV = "N",
                       MC_ORDER_H_ID = -1,
                       IS_ORD_FINALIZED = "N",
                       NO_GMT_ITEM_ENTRY = new StyleOrderViewModel().FindNoOfGmtItemsByStyle(pMC_STYLE_H_ID),
                       cap_itms = new MC_GMT_CAP_ITEMModel().Query(-1),
                       itmsOrdShipDT = new List<MC_ORDER_SHIPModel>()
                       {
                           new MC_ORDER_SHIPModel() { 
                             MC_ORDER_SHIP_ID = 0,
                             MC_ORDER_H_ID =0,
                             SHIP_DT= DateTime.Now.Date,
                             SHIP_DESC = "Shipment-1",
                             cap_itms = new MC_GMT_CAP_ITEMModel().Query(-1),
                           }
                       }
                    };
                   return Ok(ob);    
                }
                else
                {
                    var ob = new StyleOrderViewModel().SelectOrder(ID);
                    return Ok(ob);
                }
                

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("StyleH/StyleDataByRefID/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/StyleDataByRefID/1
        public IHttpActionResult StyleDataByRefID(Int64 ID, Int64? pLK_STYL_DEV_ID = null)
        {
            try
            {
                var ob = new MC_STYLE_HModel().Select(ID, pLK_STYL_DEV_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("StyleH/OrdersByStyleID/{MC_STYLE_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleH/OrdersByStyleID/1
        public IHttpActionResult OrdersByStyleID(Int64 MC_STYLE_H_ID)
        {
            try
            {
                var obList = new StyleOrderViewModel().SelectOrdersByStyleId(MC_STYLE_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        
        
        [Route("StyleH/Save")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc//StyleH/Save
        public IHttpActionResult Save([FromBody] MC_STYLE_HModel ob)
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


        [Route("StyleH/Update")]
        [HttpPut]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/Update
        public IHttpActionResult Update([FromBody] MC_STYLE_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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
      
        
        [Route("StyleH/UpdateOrderStyle")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/StyleH/UpdateOrderStyle
        public IHttpActionResult UpdateOrderStyle([FromBody] StyleOrderViewModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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

        [Route("Style/getTargetOrdDocRecv")]
        [HttpGet]
        // GET :  api/mrc/Style/getTargetOrdDocRecv
        public IHttpActionResult getTargetOrdDocRecv(
            Int64 pageNumber,
            Int64 pageSize,
            string pTGT_ORD_DOC_RCV_DT = null,
            string pSTYLE_NO = null,
            Int32? pFILTER_TYPE = null
            )
        {
            var obList = new StyleOrderViewModel().getTargetOrdDocRecv(pageNumber, pageSize, pTGT_ORD_DOC_RCV_DT, pSTYLE_NO, pFILTER_TYPE);
            return Ok(obList);
        }



    }
}