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
    public class KnitPlanController : ApiController
    {

        [Route("KnitPlan/getYarnItemByFabId")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getYarnItemByFabId?pMC_STYLE_D_FAB_ID
        public IHttpActionResult GetYarnReceiveInfo(Int64? pMC_STYLE_D_FAB_ID, Int64? pRF_FAB_PROD_CAT_ID, Int64? pKNT_SC_PRG_RCV_ID)
        {
            try
            {
                var obList = new MC_STYLE_D_FAB_YRNModel().queryData(null, pMC_STYLE_D_FAB_ID, pRF_FAB_PROD_CAT_ID, pKNT_SC_PRG_RCV_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,e.Message);
            }
        }

        [Route("KnitPlan/getDyedYarnListForKp")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getDyedYarnListForKp?pMC_FAB_PROD_ORD_H_ID&pSCM_SUPPLIER_ID
        public IHttpActionResult GetYarnReceiveInfo(Int64? pMC_FAB_PROD_ORD_H_ID, Int64? pSCM_SUPPLIER_ID =null)
        {
            try
            {
                var obList = new MC_STYLE_D_FAB_YRNModel().getDyedYarnListForKp(pMC_FAB_PROD_ORD_H_ID, pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitPlan/getYarnLot")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getYarnLot?pKNT_YRN_LOT_ID_LST&pRF_BRAND_ID
        public IHttpActionResult getYarnLot(String pKNT_YRN_LOT_ID_LST = null, Int64? pRF_BRAND_ID = null, Int64? pYARN_ITEM_ID = null, string pIS_SOLID = "S", Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pSCM_SUPPLIER_ID=null)
        {
            try
            {
                var obList = new KNT_YRN_LOTModel().SelectAll(pKNT_YRN_LOT_ID_LST, pRF_BRAND_ID, pYARN_ITEM_ID, pIS_SOLID, pMC_FAB_PROD_ORD_H_ID, pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,e.Message);
            }
        }

        [Route("KnitPlan/getYarnLotForJobCard")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getYarnLotForJobCard?pKNT_PLAN_H_ID=1
        public IHttpActionResult getYarnLotForJobCard(Int64 pKNT_PLAN_H_ID, string pIS_YD = "N")
        {
            try
            {
                var obList = new KNT_YRN_LOTModel().YarnLotForJobCard(pKNT_PLAN_H_ID, pIS_YD);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getCompanyList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getCompanyList
        public IHttpActionResult getCompanyList()
        {
            try
            {
                var obList = new HrCompanyModel().CompanyListDataForKnit();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getKnitPlanHeaderData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitPlanHeaderData?pMC_FAB_PROD_ORD_D_ID
        public IHttpActionResult getYarnLot(Int64 pMC_FAB_PROD_ORD_D_ID,int? pRF_FAB_PROD_CAT_ID=null)
        {
            try
            {
                var ob = new KNT_PLAN_HModel().QueryData(pMC_FAB_PROD_ORD_D_ID, pRF_FAB_PROD_CAT_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getKnitPlanHeaderDataCollarCuff")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitPlanHeaderDataCollarCuff?pMC_FAB_PROD_ORD_D_ID&pRF_FAB_PROD_CAT_ID
        public IHttpActionResult getKnitPlanHeaderDataCollarCuff(Int64 pMC_FAB_PROD_ORD_D_ID, Int32 pRF_FAB_PROD_CAT_ID)
        {
            try
            {
                var ob = new KNT_PLAN_HModel().QueryDataCollarCuff(pMC_FAB_PROD_ORD_D_ID, pRF_FAB_PROD_CAT_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getKnitPlanCollarCuff")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitPlanCollarCuff?pMC_FAB_PROD_ORD_H_ID&pFAB_COLOR_ID
        public IHttpActionResult getKnitPlanCollarCuff(Int64 pMC_FAB_PROD_ORD_H_ID, Int32 pFAB_COLOR_ID)
        {
            try
            {
                var ob = new KNT_PLAN_HModel().CollarCuffQueryData(pMC_FAB_PROD_ORD_H_ID, pFAB_COLOR_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitPlan/getFleeceStitchLenRatio")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getFleeceStitchLenRatio?pRF_FAB_TYPE_ID&pLK_YFAB_PART_ID&pRF_YRN_CNT_ID&pSTITCH_LEN
        public IHttpActionResult getFleeceStitchLenRatio(Int64 pRF_FAB_TYPE_ID, Int64 pLK_YFAB_PART_ID, Int64 pRF_YRN_CNT_ID, Decimal? pSTITCH_LEN)
        {
            try
            {
                var ob = new KNT_PLAN_HModel().fleece_stitch_len_ratio_find(pRF_FAB_TYPE_ID, pLK_YFAB_PART_ID, pRF_YRN_CNT_ID, pSTITCH_LEN);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitPlan/getYarnReqInHouseProd")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getYarnReqInHouseProd?pKNT_JOB_CRD_H_LST&pKNT_PLAN_H_LST
        public IHttpActionResult getYarnReqInHouseProd(String pKNT_JOB_CRD_H_LST, String pKNT_PLAN_H_LST)
        {
            try
            {
                var ob = new KNT_JOB_CRD_DModel().GetYarnRequisitionData(pKNT_JOB_CRD_H_LST, pKNT_PLAN_H_LST);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/saveYarnReqInHouseProd")]
        [HttpPost]
        [NoCache]
        // GET :  /api/knit/KnitPlan/saveYarnReqInHouseProd?pXML
        public IHttpActionResult saveYarnReqInHouseProd(String pXML)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_JOB_CRD_HModel().saveYarnReqInHouseProd(pXML);
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


        [Route("KnitPlan/getJobCardHeaderData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getJobCardHeaderData?pKNT_PLAN_H_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult getJobCardHeaderData(Int64? pKNT_PLAN_H_ID=null, Int64? pKNT_JOB_CRD_H_ID = null, Int64? pJOB_CRD_NO = null)
        {
            try
            {
                var ob = new KNT_JOB_CRD_HModel().Select(pKNT_PLAN_H_ID, pKNT_JOB_CRD_H_ID, pJOB_CRD_NO);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("KnitPlan/getYarnDetailsDataByKp")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getYarnDetailsDataByKp?pKNT_PLAN_H_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult getYarnDetailsDataByKp(Int64? pKNT_PLAN_H_ID = null, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            try
            {
                var ob = new KNT_JOB_CRD_DModel().getYarnDetailsData(pKNT_PLAN_H_ID, pKNT_JOB_CRD_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitPlan/getJobCardHeaderDataRollProd")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getJobCardHeaderDataRollProd?pKNT_JOB_CRD_H_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult getJobCardHeaderData(Int64? pKNT_JOB_CRD_H_ID = null, Int64? pJOB_CRD_NO = null)
        {
            try
            {
                var ob = new KNT_JOB_CRD_HModel().SelectRollProd(pKNT_JOB_CRD_H_ID, pJOB_CRD_NO);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getScKnitProgramYd")]
        [HttpGet]
        [NoCache]

        // GET :  /api/knit/KnitPlan/getScKnitProgramYd?pKNT_SC_PRG_ISS_ID
        public IHttpActionResult getScKnitProgramYd(Int64 pKNT_SC_PRG_ISS_ID)
        {
            try
            {
                var ob = new KNT_SC_RPTModel().Query(pKNT_SC_PRG_ISS_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("KnitPlan/getScheduleByMachine")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getScheduleByMachine?pKNT_MACHINE_ID&pDURATION
        public IHttpActionResult getScheduleByMachine(Int64 pKNT_MACHINE_ID, long pDURATION, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            try
            {
                var ob = new KNT_JOB_CRD_HModel().getScheduleByMachine(pKNT_MACHINE_ID, pDURATION, pKNT_JOB_CRD_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getJobCardDataByMcId")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getJobCardDataByMcId?pKNT_MACHINE_ID
        public IHttpActionResult getJobCardDataByMcId(Int64 pKNT_MACHINE_ID)
        {
            try
            {
                var ob = new KNT_JOB_CRD_HModel().getJobCardDataByMcId(pKNT_MACHINE_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitPlan/getShiftDataByPlanId")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getShiftDataByPlanId?pHR_SHIFT_PLAN_ID
        public IHttpActionResult getShiftDataByPlanId(Int64 pHR_SHIFT_PLAN_ID)
        {
            try
            {
                var obList = new HR_SCHEDULE_HModel().ScheduleListDataWithActiveSche(pHR_SHIFT_PLAN_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getOperatorByMCnSchedule")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getOperatorByMCnSchedule?pKNT_MACHINE_ID&pHR_SCHEDULE_H_ID
        public IHttpActionResult getOperatorByMCnSchedule(Int64 pKNT_MACHINE_ID,int pHR_SCHEDULE_H_ID)
        {
            try
            {
                var obList = new KNT_MACHN_OPRModel().GetAssiPersonListByMachinId(pKNT_MACHINE_ID, pHR_SCHEDULE_H_ID,3003);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getOperatorByCurrentSchedule")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getOperatorByCurrentSchedule?pKNT_MACHINE_ID_LST
        public IHttpActionResult getOperatorByMCnSchedule(String pKNT_MACHINE_ID_LST)
        {
            try
            {
                var obList = new KNT_MACHN_OPRModel().GetAssiPersonListByMachinId(null, null, 3004, pKNT_MACHINE_ID_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        


        [Route("KnitPlan/getKnitMachine")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitMachine?pACT_MC_DIA_ID&pHR_PROD_FLR_ID
        public IHttpActionResult getKnitMachine(int? pACT_MC_DIA_ID = null, int? pHR_PROD_FLR_ID = null,int? pHR_COMPANY_ID = null, int pKNT_MC_TYPE_ID = 1)
        {
            try
            {
                var obList = new KNT_MACHINEModel().QueryData(pACT_MC_DIA_ID, pHR_PROD_FLR_ID, pHR_COMPANY_ID, pKNT_MC_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KnitPlan/getKnitCardList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitCardList
        public IHttpActionResult getKnitCardList(Int64 pageNumber, Int64 pageSize, Int32? pRF_FAB_PROD_CAT_ID = null, string pJOB_CRD_NO = null)
        {
            try
            {
                var obList = new KNT_JOB_CRD_HModel().getKnitCardList(pageNumber, pageSize, pRF_FAB_PROD_CAT_ID, pJOB_CRD_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("KnitPlan/getJobCardList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getJobCardList?pKNT_PLAN_H_ID&pKNT_SC_PRG_ISS_ID
        public IHttpActionResult getJobCardList(Int64? pKNT_PLAN_H_ID = null, Int64? pKNT_SC_PRG_ISS_ID = null, Int32? pOption=3003)
        {
            try
            {
                var obList = new KNT_JOB_CRD_HModel().SelectAll(pKNT_PLAN_H_ID, pKNT_SC_PRG_ISS_ID, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getKnitCardPendingList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getKnitCardPendingList?pOption&pHR_PROD_FLR_ID&pACT_MC_DIA_ID&pWORK_STYLE_NO
        public IHttpActionResult getKnitCardPendingList(Int64 pOption, string pWORK_STYLE_NO, Int64? pHR_PROD_FLR_ID = null, Int64? pACT_MC_DIA_ID = null, Int64? pLK_COL_TYPE_ID=null, string pIS_RIB=null)
        {
            try
            {
                var obList = new KNT_PENDING_KCModel().QueryData(pOption, pHR_PROD_FLR_ID, pACT_MC_DIA_ID, pWORK_STYLE_NO, pLK_COL_TYPE_ID, pIS_RIB);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/getLoadPlanEditKc")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getLoadPlanEditKc?pKNT_JOB_CRD_H_ID
        public IHttpActionResult getLoadPlanEditKc(Int64 pKNT_JOB_CRD_H_ID)
        {
            try
            {
                var ob = new KNT_PENDING_KCModel().selectData(pKNT_JOB_CRD_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("KnitPlan/getJobCardListCollarCuff")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getJobCardListCollarCuff?pKNT_PLAN_H_ID&pKNT_SC_PRG_ISS_ID&pOption
        public IHttpActionResult getJobCardListCollarCuff(Int64? pKNT_SC_PRG_ISS_ID = null, Int32? pOption = 3008)
        {
            try
            {
                var obList = new KNT_JOB_CRD_HModel().getKPList(pKNT_SC_PRG_ISS_ID, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/GetJobCardListCollarCuffSco")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/GetJobCardListCollarCuffSco?pKNT_YRN_STR_REQ_H_ID
        public IHttpActionResult GetJobCardListCollarCuffSco(Int64 pKNT_YRN_STR_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_YRN_STR_REQ_DModel().GetJobCardListCollarCuffSco(pKNT_YRN_STR_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("KnitPlan/getSubContractProgramData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/getSubContractProgramData?pKNT_SC_PRG_ISS_ID
        public IHttpActionResult getSubContractProgramData(Int64? pKNT_SC_PRG_ISS_ID)
        {
            try
            {
                var obList = new KNT_SC_PRG_ISSModel().Select(pKNT_SC_PRG_ISS_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/RollProductionList")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/RollProductionList?pKNT_JOB_CRD_H_ID&pDateFilter
        public IHttpActionResult RollProductionList(Int64? pKNT_JOB_CRD_H_ID = null, DateTime? pDateFilter = null)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().QueryData(pKNT_JOB_CRD_H_ID, pDateFilter);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("KnitPlan/ScProgramPagingData")]
        [HttpGet]
        [NoCache]
        // GET :  /api/knit/KnitPlan/ScProgramPagingData?pKNT_SC_PRG_ISS_ID
        public IHttpActionResult ScProgramPagingData(Int64 pageNumber, Int64 pageSize, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null,
            Int64? pMC_STYLE_H_EXT_ID = null, string pPRG_ISS_NO = null, string pSC_PRG_DT = null, string pSUP_TRD_NAME_EN = null, string pSC_PRG_STATUS_NAME = null,
            Int64? pKNT_SC_PRG_ISS_ID = null, string pIS_TI_TE = null,
            string pIS_YD = null, string pRF_FAB_PROD_CAT_ID_LST = null
            )
        {
            try
            {
                var obList = new KNT_SC_PRG_ISSModel().SelectAll(pageNumber, pageSize, pRF_FAB_PROD_CAT_ID, pSCM_SUPPLIER_ID, pMC_BYR_ACC_ID, pMC_BUYER_ID, pMC_STYLE_H_EXT_ID, pPRG_ISS_NO, pSC_PRG_DT, pSUP_TRD_NAME_EN,
                    pSC_PRG_STATUS_NAME, pKNT_SC_PRG_ISS_ID, pIS_TI_TE, pIS_YD, pRF_FAB_PROD_CAT_ID_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KnitPlan/Save")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/Save
        public IHttpActionResult Save([FromBody] KNT_PLAN_HModel ob)
        {
            string jsonStr = "";

            try
            {
                jsonStr = ob.Save();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        jsonStr = ob.Save();
            //    }
            //    catch (Exception e)
            //    {
            //        ModelState.AddModelError("", e.Message);
            //    }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Ok(new { success = false, errors });
            //}
            return Ok(new { success = true, jsonStr });
        }

        [Route("KnitPlan/CreateStoreRequisition")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisition?pKNT_SC_PRG_ISS_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult CreateStoreRequisition(Int64? pKNT_SC_PRG_ISS_ID = null, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_PLAN_HModel().CreateStoreRequisition(pKNT_SC_PRG_ISS_ID, pKNT_JOB_CRD_H_ID);
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
        
        [Route("KnitPlan/CreateStoreRequisitionYdSc")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionYdSc?pKNT_SC_PRG_ISS_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult CreateStoreRequisitionYdSc(Int64? pKNT_SC_PRG_ISS_ID = null, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionYdSc(pKNT_SC_PRG_ISS_ID, pKNT_JOB_CRD_H_ID);
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

        [Route("KnitPlan/CreateStoreRequisitionYdScTrns")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionYdScTrns?pKNT_SC_PRG_ISS_ID&pKNT_JOB_CRD_H_ID
        public IHttpActionResult CreateStoreRequisitionYdScTrns(Int64? pKNT_SC_PRG_ISS_ID = null, Int64? pKNT_JOB_CRD_H_ID = null)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionYdScTrns(pKNT_SC_PRG_ISS_ID, pKNT_JOB_CRD_H_ID);
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

        [Route("KnitPlan/CreateStoreRequisitionSR")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionSR
        public IHttpActionResult CreateStoreRequisitionSR([FromBody] SMPL_YARN_REQ_MODEL o)
        {
            string jsonStr = "";
            jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionSR(o.XML, o.KNT_YRN_STR_REQ_H_ID, o.IS_EDIT_MODE, o.IS_SUBMIT);
            return Ok(new { success = true, jsonStr });
        }

        [Route("KnitPlan/CreateStoreRequisitionFBR")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionFBR
        public IHttpActionResult CreateStoreRequisitionFBR([FromBody] SMPL_YARN_REQ_MODEL o)
        {
            string jsonStr = "";
            jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionFBR(o.XML, o.KNT_YRN_STR_REQ_H_ID, o.IS_EDIT_MODE, o.IS_SUBMIT);
            return Ok(new { success = true, jsonStr });
        }


        [Route("KnitPlan/CreateStoreRequisitionCollarCuff")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionCollarCuff
        public IHttpActionResult CreateStoreRequisitionCollarCuff([FromBody] SMPL_YARN_REQ_MODEL o)
        {
            string jsonStr = "";
            jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionCollarCuff(o.XML, o.KNT_YRN_STR_REQ_H_ID, o.KNT_SCO_CLCF_PRG_H_ID, o.IS_EDIT_MODE,o.IS_SUBMIT);
            return Ok(new { success = true, jsonStr });
        }



        
        [Route("KnitPlan/SaveJobCardData")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/SaveJobCardData
        public IHttpActionResult Save([FromBody] KNT_JOB_CRD_HModel ob)
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

        [Route("KnitPlan/UpdateJobCardData")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/UpdateJobCardData
        public IHttpActionResult UpdateJobCardData([FromBody] KNT_JOB_CRD_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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

        [Route("KnitPlan/SaveUnassignJobCardData")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/SaveUnassignJobCardData
        public IHttpActionResult SaveUnassignJobCardData([FromBody] KNT_JOB_CRD_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveUnassignJobCardData();
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

        [Route("KnitPlan/SaveScProgram")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/SaveScProgram
        public IHttpActionResult Save([FromBody] KNT_SC_PRG_ISSModel ob)
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

        [Route("KnitPlan/DeleteScProgram")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/DeleteScProgram
        public IHttpActionResult DeleteScProgram([FromBody] KNT_SC_PRG_ISSModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Delete();
            }
            catch (Exception e)
            {
                return Ok(new { success = false, jsonStr = e.Message.ToString() });
            }
            return Ok(new { success = true, jsonStr });
        }


        [Route("KnitPlan/SaveRollProductionData")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/SaveRollProductionData
        public IHttpActionResult SaveRollProductionData([FromBody] KNT_FAB_ROLLModel ob)
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



        [Route("KnitPlan/SplitRoll")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/SplitRoll
        public IHttpActionResult SplitRoll([FromBody] KNT_FAB_ROLLModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SplitRoll();
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

        [Route("KnitPlan/LabelPrint")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/LabelPrint?pKNT_FAB_ROLL_ID&pRLBL_PRNTR_NAME
        public IHttpActionResult LabelPrint(Int64 pKNT_FAB_ROLL_ID, String pPRNTR_ADDRESS)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = new KNT_FAB_ROLLModel().LabelPrint(pKNT_FAB_ROLL_ID, pPRNTR_ADDRESS);
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



        [Route("KnitPlan/RemoveRollProductionData")]
        [HttpPost]
        // GET :  api/knit/KnitPlan/RemoveRollProductionData
        public IHttpActionResult RemoveRollProductionData([FromBody] KNT_FAB_ROLLModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Remove();
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

        [Route("KnitPlan/deleteStoreReq")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/deleteStoreReq
        public IHttpActionResult deleteStoreReq([FromBody] KNT_YRN_STR_REQ_HModel o)
        {
            try
            {
                string jsonStr = "";
                jsonStr = o.deleteStoreReq();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }


        }

        [Route("KnitPlan/CreateStoreRequisitionYdKp")]
        [HttpPost]
        // POST :  api/knit/KnitPlan/CreateStoreRequisitionYdKp?pKNT_PLAN_H_ID
        public IHttpActionResult CreateStoreRequisitionYdKp(Int64 pKNT_PLAN_H_ID)
        {
            string jsonStr = "";
            try
            {
                jsonStr = new KNT_PLAN_HModel().CreateStoreRequisitionYdKp(pKNT_PLAN_H_ID);
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }

    }

    public class SMPL_YARN_REQ_MODEL
    {
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }
        public Int64? KNT_SCO_CLCF_PRG_H_ID { get; set; }
        public string XML { get; set; }
        public string IS_EDIT_MODE { get; set; }
        public string IS_SUBMIT { get; set; }
    }
}
