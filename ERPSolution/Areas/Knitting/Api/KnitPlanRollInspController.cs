using ERP.Model;
using ERPSolution.Hubs;
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
    public class KnitPlanRollInspController : ApiController
    {
        [Route("KnitPlanRollInsp/RollDelails")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/RollDelails?pFAB_ROLL_NO
        public IHttpActionResult RollDelails(string pFAB_ROLL_NO, Int64? pOption = null)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().QueryRollData(pFAB_ROLL_NO, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlanRollInsp/RollQCDelails")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/RollDelails?pFAB_ROLL_NO
        public IHttpActionResult RollQCDelails(string pFAB_ROLL_NO)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().QueryRollQCData(pFAB_ROLL_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlanRollInsp/DefectTypeList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/DefectTypeList
        public IHttpActionResult DefectTypeList()
        {
            try
            {
                var obList = new RF_FB_DFCT_TYPEModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlanRollInsp/GetRollQCGRD")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/GetRollQCGRD
        public IHttpActionResult GetRollQCGRD(Decimal? pGRADE_PT=null)
        {
            try
            {
                var obList = new KNT_ROLL_QC_GRDModel().SelectByValue(pGRADE_PT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlanRollInsp/getRollDataList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/getRollDataList?pKNT_QC_STS_TYPE_ID
        public IHttpActionResult getRollDataList(Int64? pKNT_QC_STS_TYPE_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_COLOR_ID = null, string pSTYLE_ORDER_NO = null, string pCOLOR_NAME_EN = null, string pQC_DT = null)
        {
            try
            {
                if (pQC_DT != null)
                    pQC_DT = Convert.ToDateTime(pQC_DT.Split('T')[0].ToString()).ToString("dd-MMM-yyyy");
                var obList = new KNT_FAB_ROLLModel().getRollDataList(pKNT_QC_STS_TYPE_ID, pMC_BYR_ACC_GRP_ID, pMC_COLOR_ID, pSTYLE_ORDER_NO, pCOLOR_NAME_EN, pQC_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlanRollInsp/getQCUserList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlanRollInsp/getQCUserList
        public IHttpActionResult getQCUserList()
        {
            try
            {
                var obList = new SCM_MAP_OPRModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("KnitPlanRollInsp/Save")]
        [HttpPost]
        // GET :  api/knit/KnitPlanRollInsp/Save
        public IHttpActionResult Save([FromBody] KNT_FAB_ROLLModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveQC();
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
