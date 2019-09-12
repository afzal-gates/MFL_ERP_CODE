using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    public class AccessoriesBookingController : ApiController
    {

        [Route("AccBk/OrderListForAccBk/{pageNo}/{pageSize}/{MC_BYR_ACC_ID}/{STYLE_NO}/{ORDER_NO}/{ORDER_TYPE}/{NATURE_OF_ORDER}/{pFIRST_DATE}/{pMC_STYLE_H_EXT_ID}")]
        [HttpGet]
        // GET :  /api/mrc/AccBk/OrderListForAccBk
        public IHttpActionResult OrderListForAccBk(int pageNo, int pageSize, Int64? MC_BYR_ACC_ID = null, string STYLE_NO = null,
            string ORDER_NO = null, string ORDER_TYPE = null, string NATURE_OF_ORDER = null, string pFIRST_DATE = null, Int64? pMC_STYLE_H_EXT_ID = null)
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
                var lst = pFIRST_DATE.Split('-');
                pFIRST_DATE = lst[1] + "/" + lst[2] + "/" + lst[0];
            }

            var data = new MC_ORDER_HModel().OrderListForAccBk(pageNo, pageSize, MC_BYR_ACC_ID, STYLE_NO, ORDER_NO, ORDER_TYPE, NATURE_OF_ORDER, pFIRST_DATE, pMC_STYLE_H_EXT_ID);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }

        [Route("AccBk/PurchaseReqList")]
        [HttpGet]
        // GET :  /api/mrc/AccBk/PurchaseReqList
        public IHttpActionResult PurchaseReqList(Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            try
            {
                var data = new SCM_PURC_REQ_HModel().SelectForAccBooking(pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID);
                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AccBk/BOMListForAccBkByID")]
        [HttpGet]
        // GET :  /api/mrc/AccBk/BOMListForAccBkByID?pMC_STYLE_H_ID=&pMC_STYLE_H_EXT_ID=
        public IHttpActionResult BOMListForAccBkByID(Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            var data = new MC_FAB_PROD_H_BOMModel().BOMListForAccBkByID(pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID);

            return Ok(data);
        }


        [Route("AccBk/getItemTabList")]
        [HttpGet]
        // GET :  api/mrc/AccBk/getItemTabList?pBLK_BOM_LIST=&pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult getItemTabList(string pBLK_BOM_LIST = null, Int64? pBLK_BOM_ACT = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var obList = new MC_FAB_PROD_H_BOMModel().QueryAccessoriesBooking(pBLK_BOM_LIST, pBLK_BOM_ACT, pMC_FAB_PROD_ORD_H_ID, pSCM_PURC_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("AccBk/getItemTemplet")]
        [HttpGet]
        // GET :  api/mrc/AccBk/getItemTemplet?pINV_ITEM_ID=&pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult getItemTemplet(Int64? pINV_ITEM_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var obList = new MC_FAB_PROD_D_BOMModel().SelectByID(pINV_ITEM_ID, pMC_FAB_PROD_ORD_H_ID, pSCM_PURC_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("AccBk/AccSave")]
        [HttpPost]
        // GET :  api/mrc/AccBk/AccSave
        public IHttpActionResult AccSave([FromBody] SCM_PURC_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Submit();
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
                        errors[pair.Key.Replace("ob.", "")] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Ok(new { success = false, errors });
            }
            return Ok(new { success = true, jsonStr });
        }

    }
}
