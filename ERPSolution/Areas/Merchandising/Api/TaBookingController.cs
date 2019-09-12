//using Codaxy.WkHtmlToPdf;
using ERP.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI;
using WkHtmlToPdf;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    //[Authorize]
    public class TaBookingController : ApiController
    {
        [Route("TaBooking/getItemTabList")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getItemTabList
        public IHttpActionResult getItemTabList(string pBLK_BOM_LIST = null, Int64? pBLK_BOM_ACT = null, Int64? pMC_ACCS_PO_H_ID = null, Int64? pSCM_PURC_REQ_H_ID = null, Int64? pMC_STYL_BGT_H_ID = null, Int64? pOption = null)
        {
            try
            {
                var obList = new MC_STYLE_BLK_BOMModel().Query(pBLK_BOM_LIST, pBLK_BOM_ACT, pMC_ACCS_PO_H_ID, pSCM_PURC_REQ_H_ID, pMC_STYL_BGT_H_ID, pOption);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TaBooking/getPdf")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getPdf
        public HttpResponseMessage getPdf()
        {

            string customPath = ConfigurationManager.AppSettings["ACC_BOOKING:path"];
            customPath = HttpContext.Current.Server.MapPath(customPath).ToString() + "/aminul.pdf";
            string Url = "http://" + HttpContext.Current.Request.Url.Authority + "/Merchandising/Mrc/TaBookingRpt/80/#/RptView";

            try
            {

                PdfConvert.ConvertHtmlToPdf(new PdfDocument
                {
                    Url = Url,
                    //HeaderLeft = "[title]",
                    //HeaderRight = "[date] [time]",
                    FooterCenter = "Page [page] of [topage]"

                }, new PdfOutput
                {
                    OutputFilePath = customPath
                });
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }




        [Route("TaBooking/getContrlsByItemNBuyer")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getContrlsByItemNBuyer
        public IHttpActionResult getContrlsByItemNBuyer(Int64 pINV_ITEM_ID, Int64 pMC_STYLE_H_ID, Int64? pMC_BUYER_ID, Int64? pMC_STYL_BGT_H_ID, Int64? pMC_ACCS_PO_H_ID, Int64? pINV_ITEM_CAT_ID)
        {
            try
            {
                var obList = new MC_ACCS_PO_TMPLT_CFGModel().getContrlsByItemNBuyer(pINV_ITEM_ID, pMC_STYLE_H_ID, pMC_BUYER_ID, pMC_STYL_BGT_H_ID, pMC_ACCS_PO_H_ID, pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TaBooking/AccPoHeaderList")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getContrlsByItemNBuyer
        public IHttpActionResult AccPoHeaderList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_BUYER_ID = null, Int64? pMC_STYLE_H_EXT_ID = null,

             string pACCS_PO_SUB = null, string pACCS_PO_DT = null, string pACCS_DELV_DT = null, string pLK_PURCH_TYPE_NAME = null, string pIS_SUPP_ONLINE = null, string pDELV_WH_NAME = null,
             string pSUP_TRD_NAME_EN = null, string pMC_TNA_TASK_STATUS_ID = null, int? pSC_USER_ID = null,
             Int64? pMC_ORDER_H_ID = null, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null

            )
        {
            try
            {
                var obList = new MC_ACCS_PO_HModel().AccPoHeaderList(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_BUYER_ID, pMC_STYLE_H_EXT_ID,
                    pACCS_PO_SUB, pACCS_PO_DT, pACCS_DELV_DT, pLK_PURCH_TYPE_NAME,
                    pIS_SUPP_ONLINE, pDELV_WH_NAME, pSUP_TRD_NAME_EN, pMC_TNA_TASK_STATUS_ID,
                    pSC_USER_ID,
                    pMC_ORDER_H_ID, pFIRSTDATE, pLASTDATE);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TaBooking/setTnAStatus")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/setTnAStatus?pMC_ACCS_PO_H_ID&pMC_TNA_TASK_STATUS_ID
        public IHttpActionResult setTnAStatus(Int64 pMC_ACCS_PO_H_ID, Int64 pMC_TNA_TASK_STATUS_ID)
        {
            try
            {
                var obList = new MC_ACCS_PO_HModel().setTnAStatus(pMC_ACCS_PO_H_ID, pMC_TNA_TASK_STATUS_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TaBooking/getItemForList")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getItemForList
        public IHttpActionResult AccPoHeaderList(Int64 pMC_ACCS_PO_H_ID)
        {
            try
            {
                var obList = new MC_ACCS_PO_D_ITEMModel().getItemForList(pMC_ACCS_PO_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TaBooking/getItemForPurchaseList")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getItemForPurchaseList
        public IHttpActionResult getItemForPurchaseList(Int64? pSCM_PURC_REQ_H_ID = null)
        {
            try
            {
                var obList = new SCM_PURC_REQ_DModel().getItemForList(pSCM_PURC_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }





        [Route("TaBooking/getPoDetail")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getPoDetail?pMC_ORD_TRMS_ITEM_ID=1
        public IHttpActionResult getPoDetail(Int64? pSCM_PURC_REQ_D_ID)
        {
            try
            {
                var obList = new MC_ORD_TRMS_ITEMModel().Query(pSCM_PURC_REQ_D_ID);
                //var obList = new MC_ACCS_PO_D_SPECModel().Query(pMC_ACCS_PO_D_ITEM_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TaBooking/DashboardApprovalData")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/DashboardApprovalData?XML
        public IHttpActionResult saveDashboardApprovalData(string XML)
        {
            try
            {
                var resultStr = new MC_ACCS_PO_HModel().saveDashboardApprovalData(XML);
                return Ok(resultStr);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("TaBooking/getPoHeaderData/{pMC_ACCS_PO_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getPoHeaderData/0?pHR_DEPARTMENT_ID
        public IHttpActionResult getPoHeaderData(Int64 pMC_ACCS_PO_H_ID, int? pHR_DEPARTMENT_ID = null)
        {
            try
            {
                var obList = new MC_ACCS_PO_HModel().Select(pMC_ACCS_PO_H_ID, pHR_DEPARTMENT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TaBooking/getSoreData")]
        [HttpGet]
        // GET :  api/mrc/TaBooking/getSoreData
        public IHttpActionResult getSoreData()
        {
            try
            {
                var obList = new SCM_STOREModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("TaBooking/TotalSave")]
        [HttpPost]
        // GET :  api/mrc/TaBooking/TotalSave
        public IHttpActionResult TotalSave([FromBody] MC_ACCS_PO_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Submit();
                    //JObject jObject = JObject.Parse(jsonStr);
                    //String OP_ACCS_PO_NO = (String) jObject["OP_ACCS_PO_NO"];
                    //Int64 OP_MC_STYL_BGT_H_ID = (Int64)jObject["OP_MC_STYL_BGT_H_ID"];

                    //if (!string.IsNullOrEmpty(OP_ACCS_PO_NO) && ob.IS_CHANGED)
                    //{


                    //    string customPath = ConfigurationManager.AppSettings["ACC_BOOKING:path"];
                    //    customPath = HttpContext.Current.Server.MapPath(customPath).ToString() +"/"+OP_ACCS_PO_NO+".pdf";
                    //    string Url = "http://" + HttpContext.Current.Request.Url.Authority + "/Merchandising/Mrc/TaBookingRpt/" + OP_MC_STYL_BGT_H_ID + "/#/RptView";


                    //    PdfConvert.ConvertHtmlToPdf(new PdfDocument
                    //    {
                    //        Url = Url,
                    //        FooterCenter = "Page [page] of [topage]"

                    //    }, new PdfOutput
                    //    {
                    //        OutputFilePath = customPath
                    //    });
                    //}
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
                        errors[pair.Key.Replace("ob.", "")] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Ok(new { success = false, errors });
            }
            return Ok(new { success = true, jsonStr });
        }



        [Route("TaBooking/Save")]
        [HttpPost]
        // GET :  mrc/api/TaBooking/Save
        public IHttpActionResult BatchSave([FromBody] MC_ACCS_PO_TMPLT_HModel ob)
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