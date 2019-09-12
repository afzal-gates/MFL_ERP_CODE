using ERP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;



namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class OrderController : ApiController
    {

        [Route("Order/GetOrderList")]
        [HttpGet]
        // GET :  /api/knit/Order/GetOrderList
        public IHttpActionResult GetOrderList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null,
            Int64? pMC_STYLE_H_EXT_ID = null, string pLK_ORDER_TYPE_LST = null, Int64? pRF_FAB_PROD_CAT_ID = null)
        {
            try
            {
                var obList = new MC_STYLE_H_EXTModel().GetOrderList(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_BUYER_ID, pSTYLE_NO,
                    pMC_STYLE_H_EXT_ID, pLK_ORDER_TYPE_LST, pRF_FAB_PROD_CAT_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("Order/GetStyleExOrderList")]
        [HttpGet]
        // GET :  /api/knit/Order/GetStyleExOrderList
        public IHttpActionResult GetStyleExOrderList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pLK_ORDER_TYPE_LST = null, Int64? pRF_FAB_PROD_CAT_ID = null)
        {
            try
            {
                var obList = new MC_STYLE_H_EXTModel().GetStyleExOrderList(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_BUYER_ID, pSTYLE_NO, pMC_STYLE_H_EXT_ID, pLK_ORDER_TYPE_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("Order/DevStyleWiseOrdSizeList")]
        [HttpGet]
        // GET :  /api/mrc/Order/DevStyleWiseOrdSizeList?pMC_STYLE_H_ID=1&pMC_STYLE_H_EXT_ID=2
        public IHttpActionResult DevStyleWiseOrdSizeList(Int64 pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            var obList = new MC_DEV_ORDER_SIZEModel().DevStyleWiseOrdSizeList(pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID);
            return Ok(obList);
        }

        [Route("Order/GetOrderShipmentList")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetOrderShipmentList?pMC_STYLE_H_ID=1
        public IHttpActionResult GetOrderShipmentList(Int64 pMC_STYLE_H_ID, Int32 pNUMBER_OF_REC, Int64? pMC_ORDER_SHIP_ID = null, string pORDER_NO = null)
        {
            var obList = new MC_ORDER_SHIPModel().GetOrderShipmentList(pMC_STYLE_H_ID, pNUMBER_OF_REC, pMC_ORDER_SHIP_ID, pORDER_NO);
            return Ok(obList);
        }

        [Route("Order/GetGmtCapacityBookedData")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetGmtCapacityBookedData?pSHIP_DT=1
        public IHttpActionResult GetGmtCapacityBookedData(Int64 pMC_BYR_ACC_ID, DateTime pSHIP_DT, Int64? pMC_ORDER_H_ID = null)
        {
            var obList = new MC_ORDER_HModel().GetGmtCapacityBookedData(pMC_BYR_ACC_ID, pSHIP_DT, pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/GetGmtCapBkData4OTPModal")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetGmtCapBkData4OTPModal?pSHIP_DT=1
        public IHttpActionResult GetGmtCapBkData4OTPModal(Int64 pMC_BYR_ACC_ID, DateTime pSHIP_DT, Int64? pMC_ORDER_H_ID = null)
        {
            var obList = new MC_ORDER_HModel().GetGmtCapBkData4OTPModal(pMC_BYR_ACC_ID, pSHIP_DT, pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/SaveDevOrder")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveDevOrder(MC_ORDER_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveDevOrder();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("Order/WorkStyleListByUserBuyerAcc/{pMC_STYLE_H_EXT_ID}/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/WorkStyleListByUserBuyerAcc/1
        public IHttpActionResult WorkStyleListByUserBuyerAcc(Int64? pMC_STYLE_H_EXT_ID, Int64? pMC_STYLE_H_ID, string pIS_TNA_FINALIZED = null)
        {
            var obList = new MC_WORK_STYLEModel().WorkStyleListByUserBuyerAcc(pMC_STYLE_H_EXT_ID, pMC_STYLE_H_ID, pIS_TNA_FINALIZED);
            return Ok(obList);
        }

        [Route("Order/OrderWiseItemList/{pMC_ORDER_H_ID}/{pMC_ORDER_H_IDS}")]
        [HttpGet]
        // GET :  mrc/api/Order/OrderDataList
        public IHttpActionResult OrderWiseItemList(Int64? pMC_ORDER_H_ID, string pMC_ORDER_H_IDS)
        {
            var obList = new MC_STYLE_D_ITEMModel().OrderWiseItemList(pMC_ORDER_H_ID, pMC_ORDER_H_IDS);
            return Ok(obList);
        }

        [Route("Order/OrderDataList/{pMC_BUYER_ID}/{pMC_ORDER_H_ID}/{pMC_STYLE_H_ID}/{pMC_BUYER_OFF_ID}/{pMC_BYR_ACC_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderDataList
        public IHttpActionResult OrderDataList(Int64? pMC_BUYER_ID, Int64? pMC_ORDER_H_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_BUYER_OFF_ID, Int64? pMC_BYR_ACC_ID, String pIS_PROV = null)
        {
            var obList = new MC_ORDER_HModel().OrderDataList(pMC_BUYER_ID, pMC_ORDER_H_ID, pMC_STYLE_H_ID, pMC_BUYER_OFF_ID, pMC_BYR_ACC_ID, pIS_PROV);
            return Ok(obList);
        }

        [Route("Order/OrderHdrDataList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/mrc/Order/OrderHdrDataList
        public IHttpActionResult OrderHdrDataList(
            Int64? pMC_BUYER_ID = null,
            Int64? pMC_ORDER_H_ID = null,
            Int64? pMC_STYLE_H_ID = null,
            Int64? pMC_BUYER_OFF_ID = null,
            Int64? pMC_BYR_ACC_ID = null,
            Int64? pLK_ORD_STATUS_ID = null,
            Int64? pMC_STYLE_H_EXT_ID = null,
            Int64? pEX_ORD_STATUS_ID = null)
        {
            var obList = new MC_ORDER_HModel().OrderHdrDataList(pMC_BUYER_ID, pMC_ORDER_H_ID, pMC_STYLE_H_ID, pMC_BUYER_OFF_ID, pMC_BYR_ACC_ID, pLK_ORD_STATUS_ID, pMC_STYLE_H_EXT_ID, pEX_ORD_STATUS_ID);
            return Ok(obList);
        }

        [Route("Order/OrderListForSCM/{pageNo}/{pageSize}/{MC_BYR_ACC_ID}/{STYLE_NO}/{ORDER_NO}/{ORDER_TYPE}/{NATURE_OF_ORDER}/{pFIRST_DATE}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderListForSCM
        //public IHttpActionResult OrderListForSCM(int pageNo, int pageSize, string pBYR_ACC_NAME_EN = null, string pSTYLE_DESC = null, string pSTYLE_NO = null,
        //    string pORDER_NO = null, string pORDER_TYPE_NAME_EN = null, string pNATURE_OF_ORDER = null)
        public IHttpActionResult OrderListForSCM(int pageNo, int pageSize, Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null,
            string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, string pFIRST_DATE = null)
        {           

            if (STYLE_NO == "null")
                STYLE_NO = null;

            if (ORDER_NO == "null")
                ORDER_NO = null;

            if (ORDER_TYPE == "null")
                ORDER_TYPE = null;

            if (NATURE_OF_ORDER == "null")
                NATURE_OF_ORDER = null;

            if (pFIRST_DATE == "null")
                pFIRST_DATE = null;
            else
            {
                var lst=pFIRST_DATE.Split('-');
                pFIRST_DATE = lst[1]+"/"+lst[2]+"/"+lst[0];
            }

            var data = new MC_ORDER_HModel().OrderListForSCM(pageNo, pageSize, MC_BYR_ACC_ID, STYLE_NO, ORDER_NO, ORDER_TYPE, NATURE_OF_ORDER, pFIRST_DATE);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }

        [Route("Order/SelectShipmentMonth/{MC_BYR_ACC_ID}/{STYLE_NO}/{ORDER_NO}/{ORDER_TYPE}/{NATURE_OF_ORDER}")]
        [HttpGet]
        // GET :  /api/mrc/Order/SelectShipmentMonth
        public IHttpActionResult SelectShipmentMonth(Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null, string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            if (STYLE_NO == "null")
                STYLE_NO = null;

            if (ORDER_NO == "null")
                ORDER_NO = null;

            if (ORDER_TYPE == "null")
                ORDER_TYPE = null;

            if (NATURE_OF_ORDER == "null")
                NATURE_OF_ORDER = null;

            var data = new MC_ORDER_HModel().SelectShipmentMonth(MC_BYR_ACC_ID, STYLE_NO, ORDER_NO, ORDER_TYPE, NATURE_OF_ORDER, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID);
            return Ok(data);
        }

        [Route("Order/OrderListForCollarCuff/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderListForSCM
        public IHttpActionResult OrderListForCollarCuff(int pageNo, int pageSize, string pBYR_ACC_NAME_EN = null, string pSTYLE_DESC = null, string pSTYLE_NO = null,
            string pORDER_NO = null, string pORDER_TYPE_NAME_EN = null, string pNATURE_OF_ORDER = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_BYR_ACC_ID = null)
        {
            if (pBYR_ACC_NAME_EN == "null")
                pBYR_ACC_NAME_EN = null;
            if (pSTYLE_NO == "null")
                pSTYLE_NO = null;

            var data = new MC_ORDER_HModel().OrderListForCollarCuff(pageNo, pageSize, pBYR_ACC_NAME_EN, pSTYLE_DESC, pSTYLE_NO, pORDER_NO, pORDER_TYPE_NAME_EN, pNATURE_OF_ORDER, pMC_STYLE_H_ID, pMC_BYR_ACC_ID);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }


        [Route("Order/OrderHdrData/{pMC_ORDER_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderHdrDataList/1
        public IHttpActionResult OrderHdrData(Int64? pMC_ORDER_H_ID, Int64? pLK_ORD_STATUS_ID = null)
        {
            var ob = new MC_ORDER_HModel().OrderHdrData(pMC_ORDER_H_ID, pLK_ORD_STATUS_ID);
            return Ok(ob);
        }

        //[Route("Order/OrderItemsData/{pMC_ORDER_H_ID}")]
        //[HttpGet]
        //// GET :  /api/mrc/Order/OrderItemsData/1
        //public IHttpActionResult OrderItemsData(Int64? pMC_ORDER_H_ID)
        //{
        //    var ob = new MC_ORDER_HModel().OrderItemsData(pMC_ORDER_H_ID);
        //    return Ok(ob);
        //}

        

        [Route("Order/OrderHdrDataListWithTnaData")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderHdrDataListWithTnaData?pMC_STYLE_H_EXT_LST&pHAS_EXT&pMC_TNA_TASK_ID=7&pMC_STYLE_H_ID
        public IHttpActionResult OrderHdrDataList(String pMC_STYLE_H_EXT_LST, string pHAS_EXT, Int64 pMC_TNA_TASK_ID, Int64 pMC_STYLE_H_ID)
        {
            var obList = new MC_ORDER_HModel().OrderHdrDataListWithTnaData(pMC_STYLE_H_EXT_LST, pHAS_EXT, pMC_TNA_TASK_ID, pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("Order/OrderOrByerAccWiseColorList/{pMC_ORDER_H_ID}/{pMC_BYR_ACC_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderOrByerAccWiseColorList/1/2
        public IHttpActionResult OrderOrByerAccWiseColorList(Int64? pMC_ORDER_H_ID, Int64? pMC_BYR_ACC_ID, Int64? pGMT_MRKR_ID = null)
        {
            var obList = new MC_COLORModel().OrderOrByerAccWiseColorList(pMC_ORDER_H_ID, pMC_BYR_ACC_ID, pGMT_MRKR_ID);
            return Ok(obList);
        }

        [Route("Order/OrderWiseSizeList/{pMC_ORDER_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderWiseSizeList/1
        public IHttpActionResult OrderWiseSizeList(Int64? pMC_ORDER_H_ID)
        {
            var obList = new MC_SIZEModel().OrderWiseSizeList(pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/GetOrdShipList")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetOrdShipList?pMC_ORDER_H_ID=1
        public IHttpActionResult GetOrdShipList(Int64? pMC_ORDER_H_ID)
        {
            var obList = new MC_ORDER_HModel().GetOrdShipList(pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/GetOrdCountryList")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetOrdCountryList?pMC_ORDER_H_ID=1
        public IHttpActionResult GetOrdCountryList(Int64 pMC_ORDER_H_ID)
        {
            var obList = new MC_ORDER_HModel().GetOrdCountryList(pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/GetOrdColList")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetOrdColList?pMC_ORDER_H_ID=1
        public IHttpActionResult GetOrdColList(Int64 pMC_ORDER_H_ID)
        {
            var obList = new MC_ORDER_HModel().GetOrdColList(pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("Order/GetOrdItmList")]
        [HttpGet]
        // GET :  /api/mrc/Order/GetOrdItmList
        public IHttpActionResult GetOrdItmList(Int64? pMC_ORDER_H_ID, Int64? pMC_COLOR_ID=null)
        {
            var obList = new MC_ORDER_HModel().GetOrdItmList(pMC_ORDER_H_ID, pMC_COLOR_ID);
            return Ok(obList);
        }

        [Route("Order/OrderItemWiseColorList")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderItemWiseColorList?pMC_ORDER_H_ID=1&pMC_STYLE_D_ITEM_ID=12
        public IHttpActionResult OrderItemWiseColorList(Int64? pMC_ORDER_H_ID = null, Int64? pMC_STYLE_D_ITEM_ID = null, string pIS_DUMMY_COLOR = null, Int64? pMC_STYLE_H_ID = null)
        {
            var obList = new MC_COLORModel().OrderItemWiseColorList(pMC_ORDER_H_ID, pMC_STYLE_D_ITEM_ID, pIS_DUMMY_COLOR, pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("Order/MultiOrderWiseColorList/{pMC_ORDER_H_IDS}")]
        [HttpGet]
        // GET :  /api/mrc/Order/MultiOrderWiseColorList/1,2,3
        public IHttpActionResult MultiOrderWiseColorList(string pMC_ORDER_H_IDS)
        {
            var obList = new MC_COLORModel().MultiOrderWiseColorList(pMC_ORDER_H_IDS);
            return Ok(obList);
        }

        [Route("Order/MultiOrderWiseSizeList/{pMC_ORDER_H_IDS}")]
        [HttpGet]
        // GET :  /api/mrc/Order/MultiOrderWiseSizeList/1,2,4
        public IHttpActionResult MultiOrderWiseSizeList(string pMC_ORDER_H_IDS)
        {
            var obList = new MC_SIZEModel().MultiOrderWiseSizeList(pMC_ORDER_H_IDS);
            return Ok(obList);
        }

        [Route("Order/StyleWiseSizeList/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/Order/StyleWiseSizeList/1
        public IHttpActionResult StyleWiseSizeList(Int64 pMC_STYLE_H_ID)
        {
            var obList = new MC_SIZEModel().StyleWiseSizeList(pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("Order/ExportOrderDataToExcel/{pMC_ORDER_H_ID}/{pSTYLE_NO}")]
        [HttpGet]
        // GET :  /api/mrc/Order/ExportOrderDataToExcel/1/STYLE
        public IHttpActionResult ExportOrderDataToExcel(Int64 pMC_ORDER_H_ID, string pSTYLE_NO)
        {
            string jsonStr = "";
            try
            {
                MC_ORDER_HModel ob = new MC_ORDER_HModel();
                jsonStr = ob.ExportOrderDataToExcel(pMC_ORDER_H_ID, pSTYLE_NO);
            }
            catch (Exception e)
            {
                //ModelState.AddModelError("", e.Message);                
                jsonStr = e.Message;
            }

            return Ok(new { success = true, jsonStr });
        }

        [Route("Order/BatchSaveOrder")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult BatchSaveOrder(MC_ORDER_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveOrder();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }


        [Route("Order/SaveOrderRevision")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveOrderRevision(MC_ORDER_REVISIONModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveOrderRevision();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        //[Route("Order/FinalizeOrder")]
        //[HttpPost]
        //[Authorize]
        //public IHttpActionResult FinalizeOrder(string MC_ORDER_HIDS)
        //{
        //    string jsonStr = "";
        //    MC_ORDER_HModel ob = new MC_ORDER_HModel();

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.FinalizeOrder();
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }
        //    else
        //    {
        //        var errors = new Hashtable();
        //        foreach (var pair in ModelState)
        //        {
        //            if (pair.Value.Errors.Count > 0)
        //            {
        //                errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
        //            }
        //        }
        //        return Ok(new { success = false, errors });
        //    }

        //    return Ok(new { success = true, jsonStr });
        //}

        [Route("Order/OrderListByOrderNo/{pORDER_NO}")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderListByOrderNo/1?pLK_ORD_STATUS_ID=363&pIS_PROV=N
        public IHttpActionResult OrderListByOrderNo(string pORDER_NO, Int32? pLK_ORD_STATUS_ID = null, string pIS_PROV = null)
        {
            var obList = new MC_ORDER_HModel().OrderListByOrderNo(pORDER_NO, pLK_ORD_STATUS_ID, pIS_PROV);
            return Ok(obList);
        }

        [Route("Order/OrderShipmentMonth")]
        [HttpGet]
        // GET :  /api/mrc/Order/OrderShipmentMonth?pMC_BYR_ACC_ID=1
        public IHttpActionResult OrderShipmentMonth(Int64? pMC_BYR_ACC_ID=null)
        {
            try
            {
                var obList = new MC_ORDER_HModel().OrderShipmentMonth(pMC_BYR_ACC_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("Order/StyleExtOrByerAccWiseColorList")]
        [HttpGet]
        // GET :  /api/mrc/Order/StyleExtOrByerAccWiseColorList
        public IHttpActionResult StyleExtOrByerAccWiseColorList(Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            var obList = new MC_COLORModel().StyleExtOrByerAccWiseColorList(pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID);
            return Ok(obList);
        }

    }
}
