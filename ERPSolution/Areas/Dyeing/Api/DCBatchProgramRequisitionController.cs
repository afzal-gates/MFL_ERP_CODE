using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
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

    public class DCBatchProgramRequisitionController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("DCBatchProgramRequisition/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Dye/DCBatchProgramRequisition/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");
                if (pOption == 3004)
                    pCOLOR_NAME_EN = pCOLOR_NAME_EN == null ? null : Convert.ToDateTime(pCOLOR_NAME_EN.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DYE_STR_REQ_HModel().SelectAllBatch(pageNo, pageSize, pSTR_REQ_NO, pCOLOR_NAME_EN, pSTR_REQ_DT, pREQ_TYPE_NAME, pDYE_BATCH_NO, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pDYE_STR_REQ_H_ID, pOption);
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



        [Route("DCBatchProgramRequisition/SelectAllQCBatch/{pageNo}/{pageSize}")]
        [HttpGet]
        [NoCache]
        // GET :  /api/Dye/DCBatchProgramRequisition/SelectAllQCBatch
        public IHttpActionResult SelectAllQCBatch(int pageNo, int pageSize, string pFROM_DATE = null, string pTO_DATE = null, Int64? pMC_BYR_ACC_GRP_ID = null, string pSTYLE_ORDER_NO = null, Int64? pMC_COLOR_ID = null, string pDYE_BATCH_NO = null, string pDT_TYPE_ID = null, Int64? pLK_CHQ_RL_STS_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pFROM_DATE != null)
                    pFROM_DATE = Convert.ToDateTime(pFROM_DATE).ToString("yyyy-MMM-dd");
                if (pTO_DATE != null)
                    pTO_DATE = Convert.ToDateTime(pTO_DATE).ToString("yyyy-MMM-dd");

                var data = new DYE_STR_REQ_HModel().SelectAllQCBatch(pageNo, pageSize, pFROM_DATE, pTO_DATE, pMC_BYR_ACC_GRP_ID, pSTYLE_ORDER_NO, pMC_COLOR_ID, pDYE_BATCH_NO, pDT_TYPE_ID, pLK_CHQ_RL_STS_ID, pUSER_ID, pOption);
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


        [Route("DCBatchProgramRequisition/SelectAllBatchDtl/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/SelectAll
        public IHttpActionResult SelectAllBatchDtl(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");
                var data = new DYE_STR_REQ_HModel().SelectAllBatch(pageNo, pageSize, pSTR_REQ_NO, pCOLOR_NAME_EN, pSTR_REQ_DT, pREQ_TYPE_NAME, pUSER_NAME_EN, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pDYE_STR_REQ_H_ID, pOption);
                var xlist = data.OrderBy(x => x.PRE_DYE_STR_REQ_H_ID).ToList();
                return Ok(xlist);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfo
        public IHttpActionResult GetDCBatchProgramRequisitionInfo(Int64? pDYE_STR_REQ_H_ID = null, string pLOT_ID = null, string pON_LINE_QC_ID = null, string pRIB_ID=null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D1Model().Select(pDYE_STR_REQ_H_ID, pLOT_ID, pON_LINE_QC_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetDCBatchProgramAdditionRequisitionInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramAdditionRequisitionInfo
        public IHttpActionResult GetDCBatchProgramAdditionRequisitionInfo(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D1Model().GetDCBatchProgramAdditionRequisitionInfo(pDYE_STR_REQ_H_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetBatchWaitingList")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchWaitingList
        public IHttpActionResult GetBatchWaitingList()
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D1Model().SelectBatchWaitingList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCBatchProgramRequisitionInfoDtlByID(Int64? pDYE_STR_REQ_H_ID = null, Int64? pQUERY_TYPE_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D2Model().Select(pDYE_STR_REQ_H_ID, pQUERY_TYPE_ID);
                var obList = new INV_ITEMModel().ItemDataListByStoreNCatList("3,4", pSCM_STORE_ID);
                foreach (var x in list)
                {
                    var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.DC_AGENT_ID) select new { itm }).ToList();

                    var item = itemLst.ToList().Select(
                     md => new INV_ITEMModel
                     {
                         ITEM_NAME_EN = md.itm.ITEM_NAME_EN,
                         INV_ITEM_ID = md.itm.INV_ITEM_ID,
                         ITEM_CAT_NAME_EN = md.itm.ITEM_CAT_NAME_EN,
                         MOU_CODE = md.itm.MOU_CODE,
                     });

                    x.ItemList = item.ToList();
                }
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlForARByID")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramRequisitionInfoDtlForARByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCBatchProgramRequisitionInfoDtlForARByID(Int64? pDYE_STR_REQ_H_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D2Model().SelectForAR(pDYE_STR_REQ_H_ID);
                var obList = new INV_ITEMModel().ItemDataListByStoreNCatList("3,4", pSCM_STORE_ID);
                foreach (var x in list)
                {
                    var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.DC_AGENT_ID) select new { itm }).ToList();

                    var item = itemLst.ToList().Select(
                     md => new INV_ITEMModel
                     {
                         ITEM_NAME_EN = md.itm.ITEM_NAME_EN,
                         INV_ITEM_ID = md.itm.INV_ITEM_ID,
                         ITEM_CAT_NAME_EN = md.itm.ITEM_CAT_NAME_EN,
                         MOU_CODE = md.itm.MOU_CODE,
                     });

                    x.ItemList = item.ToList();
                }
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetDCBatchProgramReqInfoDtlForAddition")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramReqInfoDtlForAddition?pDYE_STR_REQ_H_ID
        public IHttpActionResult GetDCBatchProgramReqInfoDtlForAddition(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D2Model().SelectForAddition(pDYE_STR_REQ_H_ID);
                var addition = (from a in list
                                select new { a.DYE_STR_REQ_H_ID }
                              ).ToList().Distinct().OrderBy(a => a.DYE_STR_REQ_H_ID).ToList();

                var obList = new INV_ITEMModel().ItemDataListByCatList("3,4");
                foreach (var x in list)
                {
                    var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.DC_AGENT_ID) select new { itm }).ToList();

                    var item = itemLst.ToList().Select(
                     md => new INV_ITEMModel
                     {
                         ITEM_NAME_EN = md.itm.ITEM_NAME_EN,
                         INV_ITEM_ID = md.itm.INV_ITEM_ID,
                         ITEM_CAT_NAME_EN = md.itm.ITEM_CAT_NAME_EN,
                         MOU_CODE = md.itm.MOU_CODE,
                     });

                    x.ItemList = item.ToList();
                }
                return Ok(new { list, addition });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetDCBatchProgramReqInfoDtlAllAddition")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetDCBatchProgramReqInfoDtlAllAddition?pDYE_STR_REQ_H_ID
        public IHttpActionResult GetDCBatchProgramReqInfoDtlAllAddition(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D2Model().SelectAllAddition(pDYE_STR_REQ_H_ID);
                var addition = (from a in list
                                select new { a.DYE_STR_REQ_H_ID }
                              ).ToList().Distinct().OrderBy(a => a.DYE_STR_REQ_H_ID).ToList();

                var obList = new INV_ITEMModel().ItemDataListByCatList("3,4");
                foreach (var x in list)
                {
                    var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.DC_AGENT_ID) select new { itm }).ToList();

                    var item = itemLst.ToList().Select(
                     md => new INV_ITEMModel
                     {
                         ITEM_NAME_EN = md.itm.ITEM_NAME_EN,
                         INV_ITEM_ID = md.itm.INV_ITEM_ID,
                         ITEM_CAT_NAME_EN = md.itm.ITEM_CAT_NAME_EN,
                         MOU_CODE = md.itm.MOU_CODE,
                     });

                    x.ItemList = item.ToList();
                }
                return Ok(new { list, addition });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetPendingBatchProgram")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetPendingBatchProgram
        public IHttpActionResult GetPendingBatchProgram(Int64? pDYE_BT_CARD_H_ID = null)
        {
            try
            {
                var list = new DYE_BT_CARD_HModel().SelectForDCProgram(pDYE_BT_CARD_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetStockInfoForBP/{pDC_ITEM_ID}/{pFRM_STORE_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetStockInfoForBP
        public IHttpActionResult GetStockInfoForBP(Int64 pDC_ITEM_ID, Int64 pFRM_STORE_ID)
        {
            try
            {
                var list = new DYE_STR_TR_REQ_DModel().GetStockInfoForBP(pDC_ITEM_ID, pFRM_STORE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetBatchExtraTreatmentItemList/{pMC_COLOR_GRP_ID}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetPendingBatchProgram
        public IHttpActionResult GetBatchExtraTreatmentItemList(Int64 pMC_COLOR_GRP_ID)
        {
            try
            {
                var list = new DYE_DOSE_TMPLTModel().SelectByCG(pMC_COLOR_GRP_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/Save")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/Save
        public IHttpActionResult Save([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveBP();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }



        [Route("DCBatchProgramRequisition/SaveAddition")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/SaveAddition
        public IHttpActionResult SaveAddition([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveAddition();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCBatchProgramRequisition/SaveABP")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/SaveABP
        public IHttpActionResult SaveABP([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveABP();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCBatchProgramRequisition/Update")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/Update
        public IHttpActionResult Update([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.UpdateBP();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/UpdateRunningBatch")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateRunningBatch
        public IHttpActionResult UpdateRunningBatch([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.UpdateRunningBatch();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/GetBatchReprocessTypeList")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchReprocessTypeList
        public IHttpActionResult GetBatchReprocessTypeList()
        {
            try
            {
                var list = new DYE_RE_PROC_TYPEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/UpdateBatchProd")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBatchProd
        public IHttpActionResult UpdateBatchProd([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Update();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }




        [Route("DCBatchProgramRequisition/UpdateBatchCheck")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBatchCheck
        public IHttpActionResult UpdateBatchCheck([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Update_Batch();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }




        [Route("DCBatchProgramRequisition/GetBatchStatusType")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchStatusType
        public IHttpActionResult GetBatchStatusType()
        {
            try
            {
                var list = new DYE_BT_STS_TYPEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetBatchForStatusUpdate")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchForStatusUpdate
        public IHttpActionResult GetBatchForStatusUpdate(Int64? pPageNo = null, Int64? pMC_BYR_ACC_ID = null, string pSTYLE_ORDER_NO = null, Int64? pMC_COLOR_ID = null, string pDYE_MACHINE_NO = null, string pDYE_BATCH_NO = null, Int64? pDYE_BT_STS_TYPE_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, Int64? pLK_CHQ_RL_STS_ID = null)
        {
            try
            {
                var list = new DYE_BT_PRODModel().SelectBatch(pPageNo, pMC_BYR_ACC_ID, pSTYLE_ORDER_NO, pMC_COLOR_ID, pDYE_MACHINE_NO, pDYE_BATCH_NO, pDYE_BT_STS_TYPE_ID, pRF_FAB_PROD_CAT_ID, pLK_CHQ_RL_STS_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/CompleteBatchProgram/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/CompleteBatchProgram
        public IHttpActionResult CompleteBatchProgram(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");
                var data = new DYE_STR_REQ_HModel().SelectAllBatch(pageNo, pageSize, pSTR_REQ_NO, pCOLOR_NAME_EN, pSTR_REQ_DT, pREQ_TYPE_NAME, pDYE_BATCH_NO, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pDYE_STR_REQ_H_ID, pOption);
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

        [Route("DCBatchProgramRequisition/SearchBatch")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/SearchBatch
        public IHttpActionResult SearchBatch(string pDYE_BATCH_NO = null, Int64? pMC_STYLE_H_ID = null)
        {
            try
            {
                var data = new DYE_BT_CARD_HModel().SearchBatch(pDYE_BATCH_NO, pMC_STYLE_H_ID);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/SelectPendingBatch/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/SelectPendingBatch
        public IHttpActionResult SelectPendingBatch(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DYE_STR_REQ_HModel().SelectAllBatch(pageNo, pageSize, pSTR_REQ_NO, pCOLOR_NAME_EN, pSTR_REQ_DT, pREQ_TYPE_NAME, pDYE_BATCH_NO, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pDYE_STR_REQ_H_ID, pOption);

                return Ok(data);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/UpdateBTREQD2")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBTREQD2
        public IHttpActionResult UpdateBTREQD2([FromBody] DYE_BT_DC_REQ_D2Model ob)
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

            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/UpdateBatchCardLabRecipeNo")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/UpdateBatchCardLabRecipeNo
        public IHttpActionResult UpdateBatchCardLabRecipeNo(Int64? pDYE_BT_CARD_H_ID = null, Int64? pMC_COLOR_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            try
            {
                var list = new DYE_BT_CARD_HModel().UpdateBatchCardLabRecipeNo(pDYE_BT_CARD_H_ID, pMC_COLOR_ID, pMC_STYLE_H_EXT_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/Delete")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/Delete
        public IHttpActionResult Delete([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteBP();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCBatchProgramRequisition/IssueDelete")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/IssueDelete
        public IHttpActionResult IssueDelete([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteIssue();
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
            Hub.Clients.All.batchRequisitionList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/GetBatchForProduction")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchForProduction
        public IHttpActionResult GetBatchForProduction(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var batch = new DYE_BT_PRODModel().GetBatchForProductionBI(pDYE_BATCH_NO);
                var fab = new DYE_BT_PRODModel().GetBatchForProduction(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, pOption == null ? 3001 : pOption);
                var prd = new DYE_BT_PRODModel().GetBatchForProduction(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, pOption == null ? 3002 : pOption);

                return Ok(new { batch, fab, prd });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Route("DCBatchProgramRequisition/GetBatchFabricForProduction")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchFabricForProduction
        public IHttpActionResult GetBatchFabricForProduction(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var fab = new DYE_BT_PRODModel().GetBatchForProduction(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, pOption == null ? 3001 : pOption);
                return Ok(fab);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DCBatchProgramRequisition/GetBatchFabrication")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchFabrication
        public IHttpActionResult GetBatchFabrication(string pDYE_BATCH_NO = null, Int64? pOption = null)
        {
            try
            {
                if (pOption == null)
                    pOption = 3005;
                Int64? pDYE_STR_REQ_H_ID = 0;

                var fab = new DYE_BT_PRODModel().GetBatchForProduction(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, pOption);
                return Ok(new { fab });
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/UpdateBarcodeBatchProd")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchProd
        public IHttpActionResult UpdateBarcodeBatchProd([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.UpdateBarcode(1000);
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
            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            Hub.Clients.All.dyeMcMaintenanceList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/UpdateBarcodeBatchQC")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBarcodeBatchQC
        public IHttpActionResult UpdateBarcodeBatchQC([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.UpdateBarcode(2000);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/UpdateBarcodeRibBatchQC")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBarcodeRibBatchQC
        public IHttpActionResult UpdateBarcodeRibBatchQC([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.UpdateRibBarcode(3000);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/BatchQCStatusUpdate")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/BatchQCStatusUpdate
        public IHttpActionResult BatchQCStatusUpdate([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.BatchQCStatusUpdate();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCBatchProgramRequisition/GetHoldQCList")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetHoldQCList
        public IHttpActionResult GetHoldQCList(Int64? pMC_BYR_ACC_GRP_ID = null, string pDYE_BATCH_NO = null, string pORDER_NO = null, string pCHECK_ROLL_DT = null)
        {
            try
            {
                var List = new DYE_BT_PRODModel().GetHoldQCList(pMC_BYR_ACC_GRP_ID, pDYE_BATCH_NO, pORDER_NO, pCHECK_ROLL_DT);
                return Ok(List);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetBatchForDF")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchForDF
        public IHttpActionResult GetBatchForDF(string pDYE_BATCH_NO = null, Int64? pLK_DIA_TYPE_ID = null, string pLK_FBR_GRP_LST = null)
        {
            try
            {
                var batch = new DYE_BT_PRODModel().GetBatchForDF(pDYE_BATCH_NO, pLK_DIA_TYPE_ID, pLK_FBR_GRP_LST);
                return Ok(batch);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/RunningBatchList/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/RunningBatchList
        public IHttpActionResult RunningBatchList(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pCOLOR_NAME_EN = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pDYE_BATCH_NO = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");
                var data = new DYE_STR_REQ_HModel().SelectRunningBatch(pageNo, pageSize, pSTR_REQ_NO, pCOLOR_NAME_EN, pSTR_REQ_DT, pREQ_TYPE_NAME, pDYE_BATCH_NO, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pDYE_STR_REQ_H_ID, pOption);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });
                //return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetBatchFabricByBatchNo")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchFabricByBatchNo
        public IHttpActionResult GetBatchFabricByBatchNo(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var data = new DYE_BT_CARD_HModel().GetBatchFabricByBatchNo(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, pOption);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchIssueFabricByBatchNo
        public IHttpActionResult GetBatchIssueFabricByBatchNo(string pDYE_BATCH_NO = null, Int64? pDYE_STR_REQ_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var data = new DYE_BT_CARD_HModel().GetBatchIssueFabricByBatchNo(pDYE_BATCH_NO, pDYE_STR_REQ_H_ID, 3009);
                return Ok(data);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCBatchProgramRequisition/PreventionalMaintenance")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/PreventionalMaintenance
        public IHttpActionResult PreventionalMaintenance([FromBody] DYE_BT_PRODModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.PreventionalMaintenance(1000);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            Hub.Clients.All.dyeMcMaintenanceList();
            return Ok(new { success = true, jsonStr });
        }



        [Route("DCBatchProgramRequisition/UpdateBack2Dyeing")]
        [HttpPost]
        // GET :  api/Dye/DCBatchProgramRequisition/UpdateBack2Dyeing
        public IHttpActionResult UpdateBack2Dyeing([FromBody] DF_BT_SUB_LOTModel ob)
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

            Hub.Clients.All.batchRequisitionList();
            Hub.Clients.All.CompleteBatchList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCBatchProgramRequisition/GetRespDeptListByID")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetRespDeptListByID?pMULTI_DEPT_LST=
        public IHttpActionResult GetRespDeptListByID(string pMULTI_DEPT_LST = null)
        {
            try
            {
                var batch = new RF_RESP_DEPTModel().getRespDeptListByID(pMULTI_DEPT_LST);

                return Ok(batch);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCBatchProgramRequisition/GetBatchSubLotByID")]
        [HttpGet]
        // GET :  /api/Dye/DCBatchProgramRequisition/GetBatchSubLotByID
        public IHttpActionResult GetBatchSubLotByID(string pDYE_BATCH_NO = null, Int64? pLK_DIA_TYPE_ID = null)
        {
            try
            {
                var list = new DYE_BT_DC_REQ_D1Model().GetBatchSubLotByID(pDYE_BATCH_NO, pLK_DIA_TYPE_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
