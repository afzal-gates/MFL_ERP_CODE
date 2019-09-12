using ERP.Model;
using ERP.Model.Purchase;
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
    [Authorize]
    [NoCache]
    public class GreyFabReqController : ApiController
    {
        [Route("GreyFabReq/List")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/list
        public IHttpActionResult getGreyFabReqList(Int64 pageNumber, Int64 pageSize, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pRF_ACTN_STATUS_ID = null, 
            Int64? pRF_ACTN_TYPE_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, string pDYE_BATCH_NO = null, Int64? pMC_COLOR_ID=null)
        {
            try
            {
                var obList = new DYE_GSTR_REQ_HModel().getGreyFabReqList(pageNumber, pageSize, pMC_FAB_PROD_ORD_H_ID, pMC_BYR_ACC_ID, pRF_ACTN_STATUS_ID, pRF_ACTN_TYPE_ID, pSCM_SUPPLIER_ID, pSCM_STORE_ID, pDYE_BATCH_NO, pMC_COLOR_ID);

                return Ok(new { obList.total, obList.data });

                //return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("GreyFabReq/GetBatchCardInfo")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/GetBatchCardInfo?pDYE_BT_CARD_H_ID=1&pDYE_GSTR_ISS_H_ID
        public IHttpActionResult getGreyFabReqList(
            Int64 pDYE_BT_CARD_H_ID, Int64 pDYE_GSTR_ISS_H_ID
         )
        {
            try
            {
                var obList = new DYE_BT_CARD_HModel().getBatchDataWithGroup(pDYE_BT_CARD_H_ID, pDYE_GSTR_ISS_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("GreyFabReq/GetPreTreatmentInfo")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/GetPreTreatmentInfo?pDYE_GSTR_REQ_H_ID=
        public IHttpActionResult GetPreTreatmentInfo(Int64 pDYE_GSTR_REQ_H_ID)
        {
            try
            {
                var obList = new DYE_BT_CARD_HModel().GetPreTreatmentInfo(pDYE_GSTR_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("GreyFabReq/GetNonColCuffItemData")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/GetNonColCuffItemData?pDYE_BT_CARD_GRP_ID=1&pDYE_GSTR_ISS_H_ID
        public IHttpActionResult GetNonColCuffItemData(
            Int64 pDYE_BT_CARD_GRP_ID,
            Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            try
            {
                var obList = new NON_COL_CUF_A_VIEWMODEL().getItemDataByBtGrp(pDYE_BT_CARD_GRP_ID, pDYE_GSTR_ISS_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/GetNonPreTreatmentItemData")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/GetNonPreTreatmentItemData?pKNT_STYL_FAB_ITEM_ID=
        public IHttpActionResult GetNonPreTreatmentItemData(Int64 pKNT_STYL_FAB_ITEM_ID, Int64 pDYE_GSTR_REQ_H_ID)
        {
            try
            {
                var obList = new NON_COL_CUF_A_VIEWMODEL().getItemDataByPreTreatment(pKNT_STYL_FAB_ITEM_ID, pDYE_GSTR_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/GetColCuffItemData")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/GetColCuffItemData?pDYE_BT_CARD_GRP_ID=1&pDYE_GSTR_ISS_H_ID
        public IHttpActionResult GetColCuffItemData(
            Int64 pDYE_BT_CARD_GRP_ID,
            Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            try
            {
                var obList = new COL_CUF_A_VIEWMODEL().getColCufAvailabilityDataByGrop(pDYE_BT_CARD_GRP_ID, pDYE_GSTR_ISS_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/CheckIsRollIssuable")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/CheckIsRollIssuable?pDYE_BT_CARD_GRP_ID=1&pFAB_ROLL_NO
        public IHttpActionResult CheckIsRollIssuable(Int64? pDYE_BT_CARD_GRP_ID = null, string pFAB_ROLL_NO = null, Int64? pKNT_STYL_FAB_ITEM_ID = null, Int64? pDYE_GSTR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var obList = new NON_COL_CUF_A_VIEWMODEL().CheckIsRollIssuable(pDYE_BT_CARD_GRP_ID, pFAB_ROLL_NO, pKNT_STYL_FAB_ITEM_ID, pDYE_GSTR_REQ_H_ID, pOption);

                //if (obList.Count == 0){
                //    Console.Beep();
                //    Console.Beep();
                //    Console.Beep();
                //}
                //else
                //    System.Media.SystemSounds.Asterisk.Play();

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GreyFabReq/SaveRollForIssue")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/SaveRollForIssue
        public IHttpActionResult Save([FromBody] DYE_BT_CARD_ROLL ob)
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


        [Route("GreyFabReq/UpdateIssueRollHole")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/UpdateIssueRollHole
        public IHttpActionResult UpdateIssueRollHole([FromBody] DYE_GSTR_BT_ISS_D1Model ob)
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

        [Route("GreyFabReq/SaveRollForPreTreatmentIssue")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/SaveRollForPreTreatmentIssue
        public IHttpActionResult SavePT([FromBody] DF_SC_PT_ISS_ROLLModel ob)
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



        [Route("GreyFabReq/DeleteReq")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/DeleteReq
        public IHttpActionResult DeleteReq([FromBody] DYE_GSTR_REQ_HModel ob)
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


        [Route("GreyFabReq/getIssuableRollList")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getIssuableRollList?pDYE_BT_CARD_H_ID=1&pDYE_GSTR_ISS_H_ID
        public IHttpActionResult getIssuableRollList(
            Int64 pDYE_BT_CARD_H_ID,
            Int64 pDYE_BT_CARD_GRP_ID,
            Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().getRollListByBatch(pDYE_BT_CARD_H_ID, pDYE_BT_CARD_GRP_ID, pDYE_GSTR_ISS_H_ID);
                int i = 1; //66
                var objFab = obList.GroupBy(x => new { x.KNT_YRN_LOT_LST, x.KNT_STYL_FAB_ITEM_ID }).Select(g => new
                                {
                                    KNT_YRN_LOT_LST = g.Key.KNT_YRN_LOT_LST,
                                    KNT_STYL_FAB_ITEM_ID = g.Key.KNT_STYL_FAB_ITEM_ID,
                                    FAB_ROLL_WT = g.Sum(x => x.FAB_ROLL_WT),
                                    HOLE_NO = i++
                                }).ToList();

                return Ok(new { obList, objFab });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/getFinishFabricInsRollByBatchID")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getFinishFabricInsRollByBatchID?pDYE_BT_CARD_H_ID=0
        public IHttpActionResult getFinishFabricInsRollByBatchID(Int64 pDYE_BT_CARD_H_ID)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().getFinishFabricInsRollByBatchID(pDYE_BT_CARD_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/getIssuableRollListForPT")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getIssuableRollListForPT?pKNT_STYL_FAB_ITEM_ID
        public IHttpActionResult getIssuableRollListForPT(Int64 pKNT_STYL_FAB_ITEM_ID, Int64 pDYE_GSTR_REQ_H_ID)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().getRollListByPreTreatment(pKNT_STYL_FAB_ITEM_ID, pDYE_GSTR_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GreyFabReq/getAllRollListByBatchInfo")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getAllRollListByBatchInfo?pDYE_BT_CARD_H_ID=1&pLK_FBR_GRP_ID
        public IHttpActionResult getAllRollListByBatchInfo(Int64 pDYE_BT_CARD_H_ID, Int64 pLK_FBR_GRP_ID, Int64? pDYE_BT_CARD_GRP_ID)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().getAllRollListByBatchInfo(pDYE_BT_CARD_H_ID, pLK_FBR_GRP_ID, pDYE_BT_CARD_GRP_ID); //66
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/getAllRollListByKntFab")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getAllRollListByKntFab?pKNT_STYL_FAB_ITEM_ID=
        public IHttpActionResult getAllRollListByKntFab(Int64 pKNT_STYL_FAB_ITEM_ID)
        {
            try
            {
                var obList = new KNT_FAB_ROLLModel().getAllRollListByKntFab(pKNT_STYL_FAB_ITEM_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GreyFabReq/SaveColCuffIssueData")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/SaveColCuffIssueData
        public IHttpActionResult Save([FromBody] COL_CUF_A_VIEWMODEL ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveColCuffIssueData();
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

        [Route("GreyFabReq/getWovenTrimData")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/getWovenTrimData?pDYE_BT_CARD_H_ID=1&pDYE_GSTR_ISS_H_ID
        public IHttpActionResult getWovenTrimData(
            Int64 pDYE_BT_CARD_H_ID,
            Int64 pDYE_GSTR_ISS_H_ID
        )
        {
            try
            {
                var obList = new TRIM_ISSUE_VMModel().Query(pDYE_BT_CARD_H_ID, pDYE_GSTR_ISS_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GreyFabReq/SaveTrimWovenFab")]
        [HttpPost]
        // GET :  api/dye/GreyFabReq/SaveTrimWovenFab
        public IHttpActionResult Save([FromBody] TRIM_ISSUE_VMModel ob)
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



        [Route("GreyFabReq/SaveDyeGstrIssH")]
        [HttpPost]
        // GET :  /api/dye/GreyFabReq/SaveDyeGstrIssH
        public IHttpActionResult SaveDyeGstrIssH([FromBody]DYE_GSTR_ISS_HModel ob)
        {
            string json = "";
            if (ModelState.IsValid)
            {
                try
                {
                    json = ob.Save();
                    //return Ok(new { success = true, json });
                }
                catch (Exception e)
                {
                    return Content(HttpStatusCode.InternalServerError, e.Message);
                }
            }
            else
            {
                var errors = new List<string>();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors.Add(pair.Value.Errors.Select(error => error.Exception.Message).ToList()[0]);
                    }
                }
                return Ok(new { success = false, errors });
            }
            return Ok(new { success = true, json });
        }

        [Route("GreyFabReq/SelectDyeGstrIssH")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/SelectDyeGstrIssH?pDYE_GSTR_ISS_H_ID=1&pOption=3001
        public IHttpActionResult SelectDyeGstrIssH(Int64 pDYE_GSTR_ISS_H_ID, int pOption)
        {
            try
            {
                var ob = new DYE_GSTR_ISS_HModel().Select(pDYE_GSTR_ISS_H_ID, pOption);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GreyFabReq/QueryDyeGstrIssH")]
        [HttpGet]
        // GET :  /api/dye/GreyFabReq/QueryDyeGstrIssH?pOption=3000&pDYE_GSTR_REQ_H_ID
        public IHttpActionResult QueryDyeGstrIssH(int pOption, Int64 pDYE_GSTR_REQ_H_ID)
        {
            try
            {
                var ob = new DYE_GSTR_ISS_HModel().Query(pOption, pDYE_GSTR_REQ_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



    }
}
