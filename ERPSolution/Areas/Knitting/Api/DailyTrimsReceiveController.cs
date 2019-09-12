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
    [RoutePrefix("api/Knit")]
    public class DailyTrimsReceiveController : ApiController
    {
        [Route("DailyTrimsReceive/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pMRR_NO = null, string pMRR_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            try
            {
                var data = new INV_TRMS_RCV_HModel().SelectAll(pageNo, pageSize, pMRR_NO, pMRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pREMARKS);
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


        [Route("DailyTrimsReceive/GetTrimsType")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/GetTrimsType
        public IHttpActionResult GetTrimsType()
        {
            try
            {
                var list = new RF_TRM_CATModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DailyTrimsReceive/GetDailyTrimsReceiveInfo")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/GetDailyTrimsReceiveInfo
        public IHttpActionResult GetDailyTrimsReceiveInfo(Int64 pINV_TRMS_RCV_H_ID)
        {
            try
            {
                var list = new INV_TRMS_RCV_HModel().Select(pINV_TRMS_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DailyTrimsReceive/GetDailyTrimsReceiveInfoDtlByID")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/GetDailyTrimsReceiveInfoDtlByID?pINV_TRMS_RCV_H_ID
        public IHttpActionResult GetDailyTrimsReceiveInfoDtlByID(Int64? pINV_TRMS_RCV_H_ID = null)
        {
            try
            {
                var list = new INV_TRMS_RCV_DModel().SelectByID(pINV_TRMS_RCV_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DailyTrimsReceive/GetTrimsItemListByID")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/GetTrimsItemListByID?pINV_ITEM_CAT_ID&pMC_FAB_PROD_ORD_H_ID
        public IHttpActionResult GetTrimsItemListByID(Int64? pINV_ITEM_CAT_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_BYR_ACC_ID = null)
        {
            try
            {
                var list = new MC_ORD_TRMS_ITEMModel().SelectByID(pINV_ITEM_CAT_ID, pMC_STYLE_H_EXT_ID, pMC_BYR_ACC_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        
        [Route("DailyTrimsReceive/GetTrimsItemListByPordOrdID")]
        [HttpGet]
        // GET :  /api/knit/DailyTrimsReceive/GetTrimsItemListByPordOrdID?pINV_ITEM_CAT_ID&pMC_FAB_PROD_ORD_H_ID
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

        [Route("DailyTrimsReceive/SaveTrimsItem")]
        [HttpPost]
        // GET :  api/knit/DailyTrimsReceive/SaveTrimsItem
        public IHttpActionResult SaveTrimsItem([FromBody] MC_ORD_TRMS_ITEMModel ob)
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

        [Route("DailyTrimsReceive/Save")]
        [HttpPost]
        // GET :  api/knit/DailyTrimsReceive/Save
        public IHttpActionResult Save([FromBody] INV_TRMS_RCV_HModel ob)
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


        [Route("DailyTrimsReceive/DeleteTrimsItem")]
        [HttpPost]
        // GET :  api/knit/DailyTrimsReceive/DeleteTrimsItem
        public IHttpActionResult DeleteTrimsItem([FromBody] INV_TRMS_RCV_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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
