using ERP.Model;
using ERPSolution.Hubs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/dye")]
    [NoCache]
    public class DfFinishFabricController : ApiController
    {

        [Route("DfFinishFabric/SelectPendingLotRcv2DfStore")]
        [HttpGet]
        // GET :  /api/dye/DfFinishFabric/SelectPendingLotRcv2DfStore
        public IHttpActionResult SelectPendingLotRcv2DfStore(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, string pMONTHOF = null)
        {
            try
            {
                var data = new DF_BT_SUB_LOTModel().SelectPendingLotRcv2DfStore(pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pRF_FAB_PROD_CAT_ID, pMONTHOF);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DfFinishFabric/SelectPendingLotRcv2CutStore")]
        [HttpGet]
        // GET :  /api/dye/DfFinishFabric/SelectPendingLotRcv2CutStore
        public IHttpActionResult SelectPendingLotRcv2CutStore(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, string pMONTHOF = null)
        {
            try
            {
                var data = new DF_BT_SUB_LOTModel().SelectPendingLotRcv2CutStore(pMC_BYR_ACC_GRP_ID, pMC_FAB_PROD_ORD_H_ID, pRF_FAB_PROD_CAT_ID, pMONTHOF);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DfFinishFabric/SaveDfStore")]
        [HttpPost]
        // GET :  api/Dye/DfFinishFabric/SaveDfStore
        public IHttpActionResult SaveDfStore([FromBody] DF_BT_MVM_DFIN_STRModel ob)
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

        [Route("DfFinishFabric/SaveCutStore")]
        [HttpPost]
        // GET :  api/Dye/DfFinishFabric/SaveCutStore
        public IHttpActionResult SaveCutStore([FromBody] DF_BT_MVM_CUT_STR_HModel ob)
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

        //[Route("DfFinishFabric/GetExportLcContactInfo")]
        //[HttpGet]
        //// GET :  /api/dye/DfFinishFabric/GetExportLcContactInfo?pCM_EXP_LC_H_ID=
        //public IHttpActionResult GetExportLcContactInfo(Int64? pCM_EXP_LC_H_ID = null)
        //{
        //    try
        //    {
        //        var list = new DF_BT_MVM_FAB_STR_HModel().Select(pCM_EXP_LC_H_ID);
        //        return Ok(list);
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

    }
}
