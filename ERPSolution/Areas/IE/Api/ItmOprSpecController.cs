using ERP.Model;
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
    [Authorize]
    public class ItmOprSpecController : ApiController
    {
        [Route("ItmOprSpec/BuyerStyleOrderList")]
        [HttpGet]
        // GET :  /api/ie/ItmOprSpec/BuyerStyleOrderList
        public IHttpActionResult BuyerStyleOrderList(int pageNumber, int pageSize, Int64? pMC_BYR_ACC_ID = null, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, string pMC_ORDER_H_ID_LST = null, int? pRF_FAB_PROD_CAT_ID = null)
        {
            var data = new MC_ORDER_HModel().BuyerStyleOrderList(pageNumber, pageSize, pMC_BYR_ACC_ID, pFIRSTDATE, pLASTDATE, pMC_ORDER_H_ID_LST, pRF_FAB_PROD_CAT_ID);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }

        [Route("ItmOprSpec/GetCatItmWiseItmOprMapList")]
        [HttpGet]
        // GET :  /api/ie/ItmOprSpec/GetCatItmWiseItmOprMapList
        public IHttpActionResult GetCatItmWiseItmOprMapList(int pINV_ITEM_CAT_ID, Int64 pMC_STYLE_D_ITEM_ID)
        {
            try
            {
                var obList = new GMT_MAP_GARM_PARTModel().GetCatItmWiseItmOprMapList(pINV_ITEM_CAT_ID, pMC_STYLE_D_ITEM_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("ItmOprSpec/BatchSave")]
        [HttpPost]
        // GET :  /api/ie/ItmOprSpec/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_MAP_ITM_OPR_SPECModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSave();
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
        
        
        //[Route("ItmOprSpec/GetSwMcnGuideType")]
        //[HttpGet]
        //// GET :  /api/gmt/GmtMcn/GetSwMcnGuideType
        //public IHttpActionResult GetSwMcnGuideType()
        //{
        //    try
        //    {
        //        var obList = new GMT_SM_GUIDE_TYPModel().GetSwMcnGuideType();
        //        return Ok(obList);

        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}
       
        //[Route("GmtMcn/GetSwMcnSpecList")]
        //[HttpGet]
        //// GET :  /api/gmt/GmtMcn/GetSwMcnSpecList
        //public IHttpActionResult GetSwMcnSpecList(Int64 pageNumber, Int64 pageSize, string pSW_MCN_SPEC = null)
        //{
        //    try
        //    {
        //        var obList = new GMT_SW_MCN_SPECModel().GetSwMcnSpecList(pageNumber, pageSize, pSW_MCN_SPEC);
        //        return Ok(obList);

        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}
        
        //[Route("GmtMcn/SaveMcnSpec")]
        //[HttpPost]
        //// GET :  /api/gmt/GmtMcn/SaveMcnSpec
        //public IHttpActionResult SaveMcnSpec([FromBody] GMT_SW_MCN_SPECModel ob)
        //{
        //    string jsonStr = "";
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            jsonStr = ob.Save();
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

        
       
        
    }
}
