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
    public class DCIssueRequisitionController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("DCIssueRequisition/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueRequisition/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null, string pDYE_MACHINE_NO = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            try
            {
                if (pSTR_REQ_DT != null)
                    pSTR_REQ_DT = Convert.ToDateTime(pSTR_REQ_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                var data = new DYE_STR_REQ_HModel().SelectAll(pageNo, pageSize, pSTR_REQ_NO, pSTR_REQ_DT, pREQ_TYPE_NAME, pUSER_NAME_EN, pEVENT_NAME, pDYE_MACHINE_NO, pRF_REQ_TYPE_ID, pUSER_ID, pOption);
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

        [Route("DCIssueRequisition/GetDCOtherTemplate")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueRequisition/GetDCOtherTemplate
        public IHttpActionResult GetDCOtherTemplate(Int64? pLK_DC_CONS_TYPE_ID = null)
        {
            try
            {
                var list = new DYE_OTH_CONS_TMPLTModel().SelectAll(pLK_DC_CONS_TYPE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("DCIssueRequisition/GetDCIssueRequisitionInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfo
        public IHttpActionResult GetDCIssueRequisitionInfo(Int64? pDYE_STR_REQ_H_ID = null, string pSTR_REQ_NO = null)
        {
            try
            {
                var list = new DYE_STR_REQ_HModel().Select(pDYE_STR_REQ_H_ID, pSTR_REQ_NO);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssueRequisition/GetMachineInfoByTypeID")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueRequisition/GetMachineInfoByTypeID
        public IHttpActionResult GetMachineInfoByTypeID(Int64? pDYE_MC_TYPE_ID = null, Int64? pHR_COMPANY_ID=null)
        {
            try
            {
                var list = new DYE_MACHINEModel().GetMachineInfoByTypeID(pDYE_MC_TYPE_ID, pHR_COMPANY_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCIssueRequisition/GetDCIssueRequisitionInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCIssueRequisitionInfoDtlByID(Int64? pDYE_STR_REQ_H_ID = null)
        {
            try
            {
                var list = new DYE_STR_REQ_DModel().Select(pDYE_STR_REQ_H_ID);
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
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssueRequisition/Save")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/Save
        public IHttpActionResult Save([FromBody] DYE_STR_REQ_HModel ob)
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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCIssueRequisition/Update")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/Update
        public IHttpActionResult Update([FromBody] DYE_STR_REQ_HModel ob)
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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }
        [Route("DCIssueRequisition/DeleteGenReqIssue")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/DeleteGenReqIssue
        public IHttpActionResult DeleteGenReqIssue([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteGenReqIssue();

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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }


        [Route("DCIssueRequisition/DeleteGenRequisition")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/DeleteGenRequisition
        public IHttpActionResult DeleteGenRequisition([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteGenRequisition();

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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCIssueRequisition/DeleteIndividualReqItem")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/DeleteIndividualReqItem
        public IHttpActionResult DeleteIndividualReqItem([FromBody] DYE_STR_REQ_DModel ob)
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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }

        [Route("DCIssueRequisition/SaveMW")]
        [HttpPost]
        // GET :  api/Dye/DCIssueRequisition/SaveMW
        public IHttpActionResult SaveMW([FromBody] DYE_STR_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveMW();

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

            Hub.Clients.All.requisitionList();
            return Ok(new { success = true, jsonStr });
        }

        //[Route("DCIssueRequisition/requisitionList/{pRF_REQ_TYPE_ID}/{pUSER_ID}")]
        //[HttpGet]
        //public IHttpActionResult requisitionList(Int64 pRF_REQ_TYPE_ID, Int64 pUSER_ID)
        //{
        //    try
        //    {
        //        var data = new DYE_STR_REQ_HModel().SelectAll(1, 10, null, null, null, null, null, pRF_REQ_TYPE_ID, pUSER_ID);
        //        int total = 0;
        //        if (data.Count > 0)
        //            total = data.FirstOrDefault().TOTAL_REC;
        //        return Ok(new { total, data });

        //    }
        //    catch (Exception e)
        //    {
        //        return Content(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}

    }
}
