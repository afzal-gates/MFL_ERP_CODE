using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/knit")]
    public class DailyMachineOilConsumptionController : ApiController
    {
        [Route("MacOilCons/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, Int64? SCM_STORE_ID = null, Int64? HR_SCHEDULE_H_ID = null, string pISS_REF_NO = null, string pISS_REF_DT = null)
        {
            try
            {
                var data = new KNT_MCN_OIL_ISS_HModel().SelectAll(pageNo, pageSize,SCM_STORE_ID,HR_SCHEDULE_H_ID, pISS_REF_NO, pISS_REF_DT);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("MacOilCons/GetOilStockByStore")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/GetOilStockByStore
        public IHttpActionResult GetOilStockByStore(Int64? pSCM_STORE_ID = null, Int64? pINV_ITEM_ID = null)
        {
            try
            {
                var list = new INV_MCN_OIL_STKModel().SelectByItem(pSCM_STORE_ID, pINV_ITEM_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MacOilCons/GetMacOilConsInfo")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/GetMacOilConsInfo
        public IHttpActionResult GetMacOilConsInfo(Int64 pKNT_MCN_OIL_ISS_H_ID)
        {
            try
            {
                var list = new KNT_MCN_OIL_ISS_HModel().Select(pKNT_MCN_OIL_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MacOilCons/GetMacOilConsInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/GetMacOilConsInfoDtlByID?pKNT_MCN_OIL_ISS_H_ID
        public IHttpActionResult GetMacOilConsInfoDtlByID(Int64 pKNT_MCN_OIL_ISS_H_ID)
        {
            try
            {
                var list = new KNT_MCN_OIL_ISS_DModel().SelectByID(pKNT_MCN_OIL_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("MacOilCons/GetTrimsItemListByID")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/GetTrimsItemListByID?pINV_ITEM_CAT_ID&pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult GetTrimsItemListByID(Int64? pINV_ITEM_CAT_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            try
            {
                var list = new MC_ORD_TRMS_ITEMModel().SelectByID(pINV_ITEM_CAT_ID, pMC_STYLE_H_EXT_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("MacOilCons/GetTrimsItemListByPordOrdID")]
        [HttpGet]
        // GET :  /api/knit/MacOilCons/GetTrimsItemListByPordOrdID?pINV_ITEM_CAT_ID&pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult GetTrimsItemListByPordOrdID(Int64? pINV_ITEM_CAT_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null)
        {
            try
            {
                var list = new MC_ORD_TRMS_ITEMModel().GetTrimsItemListByPordOrdID(pINV_ITEM_CAT_ID, pMC_FAB_PROD_ORD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("MacOilCons/Save")]
        [HttpPost]
        // GET :  api/knit/MacOilCons/Save
        public IHttpActionResult Save([FromBody] KNT_MCN_OIL_ISS_HModel ob)
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
