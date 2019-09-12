
using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class YarnSpecController : ApiController
    {


        [Route("YarnSpec/CountConfigData")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/CountConfigData
        public IHttpActionResult CountConfigData(Int64? pRF_FAB_TYPE_ID, Int64? pMC_FIB_COMB_TMPLT_ID, Int64? pLK_MAC_GG_ID)
        {
            var ob = new MC_YRN_CNT_CFGModel().CountConfigData(pRF_FAB_TYPE_ID, pMC_FIB_COMB_TMPLT_ID, pLK_MAC_GG_ID);
            return Ok(ob);
        }


        [Route("YarnSpec/SuggestedCount/Composition/{RF_FIB_COMP_ID}/Construction/{RF_FAB_TYPE_ID}/GsmFrom/{FB_WT_MIN}/GsmTo/{FB_WT_MAX}")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/SuggestedCount/Composition/1/Construction/2/GsmFrom/220/GsmTo/3323
        public IHttpActionResult SuggestedCount(Int64? RF_FIB_COMP_ID, Int64 RF_FAB_TYPE_ID, Int64 FB_WT_MIN, Int64 FB_WT_MAX)
        {
            var ob = new MC_YRN_CNT_CFGModel().SuggestedCount(RF_FIB_COMP_ID, RF_FAB_TYPE_ID, FB_WT_MIN, FB_WT_MAX);
            return Ok(ob);
        }


        [Route("YarnSpec/FiberCompGroup")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/FiberCompGroup
        public IHttpActionResult FiberCompGroup(String Short = "Y")
        {
            var obList = new RF_FIB_COMP_GRPModel().SelectAll(Short);
            return Ok(obList);
        }


        [Route("YarnSpec/YarnItemData")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/YarnItemData
        public IHttpActionResult YarnItemData()
        {
            var obList = new INV_ITEM_YRN_SPECModel().SelectAll();
            return Ok(obList);
        }

        [Route("YarnSpec/FibCombTemplateData")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/FibCombTemplateData
        public IHttpActionResult FibCombTemplateData(Int64? pMC_FIB_COMB_TMPLT_ID)
        {
            var obList = new MC_FIB_COMB_TMPLTModel().SelectAll(pMC_FIB_COMB_TMPLT_ID);
            return Ok(obList);
        }

        [Route("YarnSpec/FiberCombConfigData")]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/FiberCombConfigData
        public IHttpActionResult FiberCombConfigData(Int64? pOption, Int64? pMC_FIB_COMB_TMPLT_ID)
        {
            var obList = new MC_FIB_COMB_CFGModel().SelectAll(pOption, pMC_FIB_COMB_TMPLT_ID);
            return Ok(obList);
        }


        [Route("YarnSpec/YrnParamForFabri")]
        [Authorize]
        [HttpGet]
        // GET :  api/mrc/YarnSpec/YrnParamForFabri
        public IHttpActionResult YrnParamForFabrication(Int64 RF_FIB_COMP_ID)
        {
            try
            {
                var obList = new INV_ITEM_YRN_SPECModel().YrnParamForFabrication(RF_FIB_COMP_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }







        [Route("YarnSpec/BatchSave")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/YarnSpec/BatchSave
        public IHttpActionResult BatchSave([FromBody] MC_YRN_CNT_CFGModel ob)
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

        [Route("YarnSpec/FiberConfigData")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/YarnSpec/FiberConfigData
        public IHttpActionResult BatchSave([FromBody] MC_FIB_COMB_CNT_CFGModel ob)
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


        [Route("YarnSpec/YarnItemSave")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/YarnSpec/BatchSave
        public IHttpActionResult BatchSave([FromBody] INV_ITEM_YRN_SPECModel ob)
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