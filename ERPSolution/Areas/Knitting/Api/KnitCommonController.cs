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
    [Authorize]
    public class KnitCommonController : ApiController
    {
        [Route("KnitCommon/getMachineDiaList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/getMachineDiaList
        public IHttpActionResult getMachineDiaList()
        {
            try
            {
                var obList = new KNT_MC_DIAModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitCommon/KntMcTypeTreeList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/KntMcTypeTreeList
        public IHttpActionResult KntMcTypeTreeList()
        {
            try
            {
                var obList = new KNT_MC_TYPEModel().KntMcTypeTreeList();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitCommon/KntMcTypeList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/KntMcTypeList
        public IHttpActionResult KntMcTypeList()
        {
            try
            {
                var obList = new KNT_MC_TYPEModel().KntMcTypeList();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("KnitCommon/KntMcTypeListByUser")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/KntMcTypeListByUser
        public IHttpActionResult KntMcTypeListByUser()
        {
            try
            {
                var obList = new KNT_MC_TYPEModel().KntMcTypeListByUser();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("KnitCommon/GetMachineList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/GetMachineList
        public IHttpActionResult GetMachineList(Int32? pKNT_MC_TYPE_ID = null)
        {
            try
            {
                var obList = new KNT_MACHINEModel().GetMachineList(pKNT_MC_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("KnitCommon/Save")]
        [HttpPost]
        // GET :  api/knit/KnitCommon/Save
        public IHttpActionResult Save([FromBody] KNT_MACHINEModel ob)
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


        [Route("KnitCommon/GetDailyProdList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/GetDailyProdList
        public IHttpActionResult GetDailyProdList(int pageNumber, int pageSize, Int32? pRF_FAB_PROD_CAT_ID = null, Int32? pMC_BYR_ACC_ID = null,
            Int32? pMC_COLOR_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, string pORDER_NO_LST = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            try
            {
                var obList = new KNT_STYL_FAB_ITEMModel().GetDailyProdList(pageNumber, pageSize, pRF_FAB_PROD_CAT_ID, pMC_BYR_ACC_ID,
                    pMC_COLOR_ID, pMC_FAB_PROD_ORD_H_ID, pORDER_NO_LST, pFROM_DT, pTO_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitCommon/GetFabStrStkList")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/GetFabStrStkList
        public IHttpActionResult GetFabStrStkList(int pageNumber, int pageSize, Int32? pRF_FAB_PROD_CAT_ID = null, Int32? pMC_BYR_ACC_ID = null,
            Int32? pMC_COLOR_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, string pORDER_NO_LST = null, string pIS_G_F = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            try
            {
                var obList = new KNT_FAB_STR_STK_VM().GetFabStrStkList(pageNumber, pageSize, pRF_FAB_PROD_CAT_ID, pMC_BYR_ACC_ID,
                    pMC_COLOR_ID, pMC_FAB_PROD_ORD_H_ID, pORDER_NO_LST, pIS_G_F, pFROM_DT, pTO_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        
        [Route("KnitCommon/GetProcRateListByParty")]
        [HttpGet]
        // GET :  /api/Knit/KnitCommon/GetProcRateListByParty
        public IHttpActionResult GetProcRateListByParty(Int32? pSCM_SUPPLIER_ID = null, Int64? pRF_FAB_TYPE_ID = null, Int64? pLK_FK_DGN_TYP_ID = null)
        {
            try
            {
                var obList = new MC_FAB_PROC_RATEModel().GetProcRateListByParty(pSCM_SUPPLIER_ID, pRF_FAB_TYPE_ID, pLK_FK_DGN_TYP_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }
        

    }
}
