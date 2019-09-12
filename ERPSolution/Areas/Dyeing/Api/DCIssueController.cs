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
    public class DCIssueController : ApiController
    {
        [Route("DCIssue/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/Dye/DCIssue/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pISS_REF_NO = null, string pISS_REF_DT = null, string pREQ_TYPE_NAME = null, string pUSER_NAME_EN = null, string pEVENT_NAME = null)
        {
            try
            {
                var data = new DYE_DC_ISS_HModel().SelectAll(pageNo, pageSize, pISS_REF_NO, pISS_REF_DT, pREQ_TYPE_NAME, pUSER_NAME_EN, pEVENT_NAME);
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

        [Route("DCIssue/GetDCIssueInfo")]
        [HttpGet]
        // GET :  /api/Dye/DCIssue/GetDCIssueInfo
        public IHttpActionResult GetDCIssueInfo(Int64? pDYE_DC_ISS_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_ISS_HModel().Select(pDYE_DC_ISS_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssue/GetDCIssueInfoDtlByID")]
        [HttpGet]
        // GET :  /api/Dye/DCIssue/GetDCIssueInfoDtlByID?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetDCIssueInfoDtlByID(Int64? pDYE_DC_ISS_H_ID = null)
        {
            try
            {
                var list = new DYE_DC_ISS_DModel().Select(pDYE_DC_ISS_H_ID);
                var obList = new INV_ITEMModel().ItemDataListByCatList("3,4");
                foreach (var x in list)
                {
                    var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.INV_ITEM_CAT_ID) select new { itm }).ToList();

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

        [Route("DCIssue/GetDCIssueInfoByReqID")]
        [HttpGet]
        // GET :  /api/Dye/DCIssue/GetDCIssueInfoByReqID?pDYE_STR_REQ_H_ID
        public IHttpActionResult GetDCIssueInfoByReqID(Int64? pDYE_STR_REQ_H_ID = null, Int64? pUSER_ID = null)
        {
            try
            {
                var list = new DYE_DC_ISS_HModel().GetDCIssueInfoByReqID(pDYE_STR_REQ_H_ID, pUSER_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssue/GetIssueByRefNo/{pISS_REF_NO}")]
        [HttpGet]
        // GET :  /api/Dye/DCIssue/GetIssueByRefNo
        public IHttpActionResult GetIssueByRefNo(string pISS_REF_NO)
        {
            try
            {
                var list = new DYE_DC_ISS_HModel().GetIssueByRefNo(pISS_REF_NO);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DCIssue/Save")]
        [HttpPost]
        // GET :  api/Dye/DCIssue/Save
        public IHttpActionResult Save([FromBody] DYE_DC_ISS_HModel ob)
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

        [Route("DCIssue/DeleteIndividualItem")]
        [HttpPost]
        // GET :  api/Dye/DCIssue/DeleteIndividualItem
        public IHttpActionResult DeleteIndividualItem([FromBody] DYE_DC_ISS_DModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Delete();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Ok(new { success = true, jsonStr });
        }
    }
}
