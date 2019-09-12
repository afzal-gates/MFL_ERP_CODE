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
    public class StyleHExtController : ApiController
    {

        [Route("StyleHExt/MultiByrAccWiseStyleListData/{pMC_BYR_ACC_IDS}")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/MultiByrAccWiseStyleListData/70,71,43
        public IHttpActionResult MultiByrAccWiseStyleListData(string pMC_BYR_ACC_IDS, string pIS_TNA_FINALIZED = null, string pHAS_SMP_YRN_INHOUSE_TNA = null)
        {
            var obList = new MC_STYLE_H_EXTModel().MultiByrAccWiseStyleListData(pMC_BYR_ACC_IDS, pIS_TNA_FINALIZED, pHAS_SMP_YRN_INHOUSE_TNA);
            return Ok(obList);
        }

        [Route("StyleHExt/BuyerWiseStyleHExtList/{pMC_BYR_ACC_ID}/{pMC_BUYER_ID}")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/BuyerWiseStyleHExtList/1/2?pMC_STYLE_H_EXT_LIST
        public IHttpActionResult BuyerWiseStyleHExtList(Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null,
            string pMC_STYLE_H_EXT_LIST = null, Int64? pMC_BYR_ACC_GRP_ID = null)
        {
            var obList = new MC_STYLE_H_EXTModel().BuyerWiseStyleHExtList(pMC_BYR_ACC_ID, pMC_BUYER_ID, pSTYLE_NO, pMC_STYLE_H_EXT_ID, pMC_STYLE_H_EXT_LIST, pMC_BYR_ACC_GRP_ID);
            return Ok(obList);
        }

        [Route("StyleHExt/getFabProdOrderData")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/getFabProdOrderData?pMC_BYR_ACC_ID&pSTYLE_NO&pMC_FAB_PROD_ORD_H_ID&pFIRSTDATE&pLASTDATE&pRF_FAB_PROD_CAT_ID
        public IHttpActionResult getFabProdOrderData(Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, string pSTYLE_NO = null, Int64? pMC_FAB_PROD_ORD_H_ID = null,
            DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, string pRF_FAB_PROD_CAT_LST = null, string pNATURE_OF_ORDER = null, string pHAS_FK_CLRCUF = null)
        {
            var obList = new MC_STYLE_H_EXTModel().getFabProdOrderData(pMC_BYR_ACC_ID, pMC_BYR_ACC_GRP_ID, pSTYLE_NO, pMC_FAB_PROD_ORD_H_ID, pFIRSTDATE, pLASTDATE, pRF_FAB_PROD_CAT_LST, pNATURE_OF_ORDER, pHAS_FK_CLRCUF);
            return Ok(obList);
        }

        [Route("StyleHExt/getFabProdOrderDataOh")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/getFabProdOrderDataOh?pMC_BYR_ACC_ID&pORDER_NO_LST&pMC_FAB_PROD_ORD_H_ID&pFIRSTDATE&pLASTDATE&pRF_FAB_PROD_CAT_ID
        public IHttpActionResult getFabProdOrderDataOh(
              long? pMC_BYR_ACC_ID = null,
            Int64? pMC_BYR_ACC_GRP_ID = null,
              string pORDER_NO_LST = null,
             long? pMC_FAB_PROD_ORD_H_ID = null,
             DateTime? pFIRSTDATE = null,
             DateTime? pLASTDATE = null,
             string pRF_FAB_PROD_CAT_LST = null,
             string pNATURE_OF_ORDER = null,
             string pIS_YD_ONLY = null,
             string pMC_FAB_PROD_ORD_H_LST = null,
             long? pMC_STYLE_H_ID = null
        )
        {
            var obList = new MC_STYLE_H_EXTModel().getFabProdOrderDataOh(pMC_BYR_ACC_ID, pMC_BYR_ACC_GRP_ID, pORDER_NO_LST, pMC_FAB_PROD_ORD_H_ID, pFIRSTDATE, pLASTDATE, pRF_FAB_PROD_CAT_LST, pNATURE_OF_ORDER, pIS_YD_ONLY, pMC_FAB_PROD_ORD_H_LST, pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("StyleHExt/getFabProdOrderDataOhMerge")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/getFabProdOrderDataOhMerge?pMC_FAB_PROD_ORD_H_ID&pFAB_COLOR_ID&pRF_FAB_TYPE_ID&pRF_FAB_PROD_CAT_ID&pINV_ITEM_CAT_ID
        public IHttpActionResult getFabProdOrderDataOhMerge(long? pMC_FAB_PROD_ORD_H_ID = null, long? pFAB_COLOR_ID = null, Int64? pRF_FAB_TYPE_ID = null, string pORDER_NO_LST = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pINV_ITEM_CAT_ID = null)
        {
            var obList = new MC_STYLE_H_EXTModel().getFabProdOrderDataOhMerge(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pORDER_NO_LST, pRF_FAB_PROD_CAT_ID, pINV_ITEM_CAT_ID);
            return Ok(obList);
        }


        [Route("StyleHExt/ByrWiseBookingStyleHExtList/{pMC_BYR_ACC_ID}/{pMC_BUYER_ID}")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/ByrWiseBookingStyleHExtList/1/2?pMC_STYLE_H_EXT_LIST
        public IHttpActionResult ByrWiseBookingStyleHExtList(Int64? pMC_BYR_ACC_ID, Int64? pMC_BUYER_ID, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pMC_STYLE_H_EXT_LIST = null)
        {
            var obList = new MC_STYLE_H_EXTModel().ByrWiseBookingStyleHExtList(pMC_BYR_ACC_ID, pMC_BUYER_ID, pSTYLE_NO, pMC_STYLE_H_EXT_ID, pMC_STYLE_H_EXT_LIST);
            return Ok(obList);
        }


        [Route("StyleHExt/BookingStyleHExtList")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/BookingStyleHExtList
        public IHttpActionResult BookingStyleHExtList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, string pSTYLE_NO = null, Int64? pMC_STYLE_H_EXT_ID = null, string pMC_STYLE_H_EXT_LIST = null)
        {
            var obList = new MC_STYLE_H_EXTModel().BookingStyleHExtList(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_BUYER_ID, pSTYLE_NO, pMC_STYLE_H_EXT_ID, pMC_STYLE_H_EXT_LIST);
            return Ok(obList);
        }

        [Route("StyleHExt/MainStyleWrtStyleHExtList")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/MainStyleWrtStyleHExtList?pMC_STYLE_H_ID
        public IHttpActionResult MainStyleWrtStyleHExtList(Int64? pMC_STYLE_H_ID = null)
        {
            try
            {
                var obList = new MC_STYLE_H_EXTModel().MainStyleWrtStyleHExtList(pMC_STYLE_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("StyleHExt/ByrWiseStyleHExtWithoutBookingList")]
        [HttpGet]
        // GET :  /api/mrc/StyleHExt/ByrWiseStyleHExtWithoutBookingList?pMC_BUYER_ID=&pSTYLE_NO=
        public IHttpActionResult ByrWiseStyleHExtWithoutBookingList(Int64? pMC_BUYER_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, string pSTYLE_NO = null)
        {
            var obList = new MC_STYLE_H_EXTModel().ByrWiseStyleHExtWithoutBookingList(pMC_BUYER_ID, pMC_BYR_ACC_ID, pMC_BYR_ACC_GRP_ID, pSTYLE_NO);
            return Ok(obList);
        }

        //[Route("StyleHExt/StyleListForSCM")]
        //[HttpGet]
        //// GET :  /api/mrc/StyleHExt/StyleListForSCM?pMC_BUYER_ID=
        //public IHttpActionResult StyleListForSCM(Int64? pMC_BUYER_ID)
        //{
        //    var obList = new MC_STYLE_H_EXTModel().StyleListForSCM(pMC_BUYER_ID);
        //    return Ok(obList);
        //}

        [Route("StyleHExt/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleHExt/Save
        public IHttpActionResult Save([FromBody] MC_STYLE_H_EXTModel ob)
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







    }
}
