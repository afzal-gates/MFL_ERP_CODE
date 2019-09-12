using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class BudgetSheetController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        //[Route("BudgetSheet/getRate/ProcessGrp/{MC_FAB_PROC_GRP_ID}/FibCompGrp/{RF_FIB_COMP_GRP_ID}/FabType/{RF_FAB_TYPE_ID}/ColType/{LK_COL_TYPE_ID}/YdType/{LK_YD_TYPE_ID}/Feeder/{LK_FEDER_TYPE_ID}/ColGrp/{MC_COLOR_GRP_ID}/AopType/{LK_AOP_TYPE_ID}")]
        //[HttpGet]
        //// GET :  api/mrc/BudgetSheet/getRate/ProcessGrp/{MC_FAB_PROC_GRP_ID}/FibCompGrp/{RF_FIB_COMP_GRP_ID}/FabType/{RF_FAB_TYPE_ID}/ColType/{LK_COL_TYPE_ID}/YdType/{LK_YD_TYPE_ID}/Feeder/{LK_FEDER_TYPE_ID}/ColGrp/{MC_COLOR_GRP_ID}/AopType/{LK_AOP_TYPE_ID}
        //public IHttpActionResult getRateSuggestion(Int64? MC_FAB_PROC_GRP_ID, Int64? RF_FIB_COMP_GRP_ID, Int64? RF_FAB_TYPE_ID, Int64? LK_COL_TYPE_ID, Int64? LK_YD_TYPE_ID, Int64? LK_FEDER_TYPE_ID, Int64? MC_COLOR_GRP_ID, Int64? LK_AOP_TYPE_ID)
        //{
        //    var obList = new MC_FAB_PROC_RATEModel().getRateSuggestion(MC_FAB_PROC_GRP_ID, RF_FIB_COMP_GRP_ID, RF_FAB_TYPE_ID, LK_COL_TYPE_ID, LK_YD_TYPE_ID, LK_FEDER_TYPE_ID, MC_COLOR_GRP_ID, LK_AOP_TYPE_ID);
        //    return Ok(obList);
        //}

        [Route("BudgetSheet/getRate")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/getRate?pMC_FAB_PROC_GRP_ID&pRF_FIB_COMP_GRP_ID&pRF_FAB_TYPE_ID&pLK_COL_TYPE_ID&pLK_YD_TYPE_ID&pLK_FEDER_TYPE_ID&pMC_COLOR_GRP_ID&pLK_AOP_TYPE_ID
        public IHttpActionResult getRateSuggestion(Int64? pMC_FAB_PROC_GRP_ID = null, Int64? pRF_FIB_COMP_GRP_ID = null, Int64? pRF_FAB_TYPE_ID = null, Int64? pLK_COL_TYPE_ID = null, Int64? pLK_YD_TYPE_ID = null,
            Int64? pLK_FEDER_TYPE_ID = null, Int64? pMC_COLOR_GRP_ID = null, Int64? pLK_AOP_TYPE_ID = null, Int64? pLK_FK_DGN_TYP_ID = null)
        {
            var obList = new MC_FAB_PROC_RATEModel().getRateSuggestion(pMC_FAB_PROC_GRP_ID, pRF_FIB_COMP_GRP_ID, pRF_FAB_TYPE_ID, pLK_COL_TYPE_ID, pLK_YD_TYPE_ID, pLK_FEDER_TYPE_ID, pMC_COLOR_GRP_ID, pLK_AOP_TYPE_ID, pLK_FK_DGN_TYP_ID);
            return Ok(obList);
        }


        [Route("BudgetSheet/FindCostHeadForBlkFab/{MC_BLK_FAB_REQ_H_ID:int}/{MC_STYL_BGT_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/FindCostHeadForBlkFab/{MC_BLK_FAB_REQ_H_ID}/{MC_STYL_BGT_H_ID}
        public IHttpActionResult getRateSuggestion(Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            var obList = new MC_COST_HEADModel().FindCostHeadForBlkFab(MC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID);
            return Ok(obList);
        }

        [Route("BudgetSheet/getFabProcessCost/{MC_BLK_FAB_REQ_H_ID:int}/{MC_STYL_BGT_H_ID}")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/getFabProcessCost/{MC_BLK_FAB_REQ_H_ID}/{MC_STYL_BGT_H_ID}
        public IHttpActionResult getFabProcessCost(Int64 MC_BLK_FAB_REQ_H_ID, Int64 MC_STYL_BGT_H_ID)
        {
            var obList = new MC_FAB_PROC_GRPModel().getFabProcessCost(MC_BLK_FAB_REQ_H_ID, MC_STYL_BGT_H_ID);
            return Ok(obList);
        }

        [Route("BudgetSheet/getBudgetHeaderData/{MC_BLK_FAB_REQ_H_ID:int}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/mrc/BudgetSheet/getBudgetHeaderData/{MC_BLK_FAB_REQ_H_ID}
        public IHttpActionResult getBudgetHeaderData(Int64 MC_BLK_FAB_REQ_H_ID)
        {

            try
            {
                var obList = new MC_STYL_BGT_HModel().Select(MC_BLK_FAB_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("BudgetSheet/get_refreshed_oq_bom")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/get_refreshed_oq_bom
        public IHttpActionResult get_refreshed_oq_bom(Int64? pMC_BLK_FAB_REQ_H_ID, string pITEM_CAT_CODE, Int64? pINV_ITEM_ID, Int64 pLK_FAB_QTY_SRC)
        {
            string xmlString = new MC_STYLE_D_BOMModel().get_refreshed_oq_bom(pMC_BLK_FAB_REQ_H_ID, pITEM_CAT_CODE, pINV_ITEM_ID, pLK_FAB_QTY_SRC);
            return Ok(new {
                xmlString = xmlString
            });
        }

        [Route("BudgetSheet/get_oq_refresh_fab")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/get_oq_refresh_fab
        public IHttpActionResult get_oq_refresh_fab(Int64? pMC_BLK_FAB_REQ_H_ID, Int64? pMC_FAB_PROC_RATE_ID, Int64? pMC_FAB_PROC_GRP_ID,string pLK_WASH_TYPE_ID, string pLK_FINIS_TYPE_ID)
        {
            string xmlString = new MC_STYLE_D_BOMModel().get_oq_refresh_fab(pMC_BLK_FAB_REQ_H_ID, pMC_FAB_PROC_RATE_ID, pMC_FAB_PROC_GRP_ID, pLK_WASH_TYPE_ID, pLK_FINIS_TYPE_ID);
            return Ok(new
            {
                xmlString = xmlString
            });
        }


        [Route("BudgetSheet/KnittingRateData")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/KnittingRateData
        public IHttpActionResult KnittingRateData()
        {
            var obList = new MC_RATE_SPEC_KNITModel().SelectAll();
            return Ok(obList);
        }


        [Route("BudgetSheet/DyeingRateData")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/DyeingRateData
        public IHttpActionResult DyeingRateData()
        {
            var obList = new MC_RATE_SPEC_DYEModel().SelectAll();
            return Ok(obList);
        }


        [Route("BudgetSheet/YarnDyeingRateData")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/YarnDyeingRateData
        public IHttpActionResult YarnDyeingRateData()
        {
            var obList = new MC_RATE_SPEC_YDModel().SelectAll();
            return Ok(obList);
        }

        [Route("BudgetSheet/AOPRateData")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/AOPRateData
        public IHttpActionResult AOPRateData()
        {
            var obList = new MC_RATE_SPEC_AOPModel().SelectAll();
            return Ok(obList);
        }

        [Route("BudgetSheet/DFINRateData")]
        [HttpGet]
        // GET :  api/mrc/BudgetSheet/DFINRateData
        public IHttpActionResult DFINRateData()
        {
            var obList = new MC_RATE_SPEC_DFINModel().SelectAll();
            return Ok(obList);
        }
        
        [Route("BudgetSheet/SaveOverAllCostData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveOverAllCostData
        public IHttpActionResult Save(MC_STYL_BGT_COSTModel ob)
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

            Hub.Clients.All.broadcastBfbkBudgetNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("BudgetSheet/reviseBudgetSheet")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/reviseBudgetSheet
        public IHttpActionResult reviseBudgetSheet(MC_STYL_BGT_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                     jsonStr = ob.Save();
                     return Ok(new { success = true, jsonStr });
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.InternalServerError, e.Message);
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
        }


        [Route("BudgetSheet/SaveFabricProcessData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveFabricProcessData
        public IHttpActionResult Save(MC_BLK_FAB_COSTModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveFabricProcessData();
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

        [Route("BudgetSheet/SaveKnittingRateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveKnittingRateData
        public IHttpActionResult SaveKnittingRateData(MC_RATE_SPEC_KNITModel ob)
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

        [Route("BudgetSheet/SaveDyeingRateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveDyeingRateData
        public IHttpActionResult SaveDyeingRateData(MC_RATE_SPEC_DYEModel ob)
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



        [Route("BudgetSheet/SaveYDRateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveYDRateData
        public IHttpActionResult SaveYDRateData(MC_RATE_SPEC_YDModel ob)
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


        [Route("BudgetSheet/SaveAOPRateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveAOPRateData
        public IHttpActionResult SaveAOPRateData(MC_RATE_SPEC_AOPModel ob)
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

        [Route("BudgetSheet/SaveDFINRateData")]
        [HttpPost]
        [System.Web.Http.Authorize]

        // GET :  api/mrc/BudgetSheet/SaveDFINRateData
        public IHttpActionResult SaveDFINRateData(MC_RATE_SPEC_DFINModel ob)
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