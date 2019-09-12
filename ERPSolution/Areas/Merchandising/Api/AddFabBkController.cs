using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using WkHtmlToPdf;


namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class AddFabBkController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();




        [Route("AddFabBk/GetAddFabBkingHdr")]
        [HttpGet]
        // GET :  api/mrc/AddFabBk/GetAddFabBkingHdr
        public IHttpActionResult GetAddFabBkingHdr(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            var ob = new MC_BLK_ADFB_REQ_HModel().GetAddFabBkingHdr(pMC_BLK_ADFB_REQ_H_ID);
            return Ok(ob);
        }


        [Route("AddFabBk/GetAddFabBkingList")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkingList
        // For Grid into Booking List Page
        public IHttpActionResult GetAddFabBkingList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pSTYLE_ORDER_NO = null)
        {
            var obList = new MC_BLK_ADFB_REQ_HModel().GetAddFabBkingList(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID, pSTYLE_ORDER_NO);
            return Ok(obList);
        }


        [Route("AddFabBk/GetAddFabBkingDtl")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkingDtl        
        public IHttpActionResult GetAddFabBkingDtl(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            var obList = new MC_BLK_ADFB_REQ_DModel().GetAddFabBkingDtl(pMC_BLK_ADFB_REQ_H_ID);
            return Ok(obList);
        }

        [Route("AddFabBk/GetAddFabBkingCollarCuffReq")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkingCollarCuffReq
        public IHttpActionResult GetAddFabBkingCollarCuffReq(Int64 pMC_STYLE_H_EXT_ID, Int64 pFAB_COLOR_ID, Int64 pMC_BLK_ADFB_REQ_D_ID)
        {
            try
            {
                var obList = new MC_BLK_ADFB_REQ_D1Model().GetAddFabBkingCollarCuffReq(pMC_STYLE_H_EXT_ID, pFAB_COLOR_ID, pMC_BLK_ADFB_REQ_D_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AddFabBk/GetAddFabBkUserLavel")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkUserLavel
        public IHttpActionResult GetAddFabBkUserLavel()
        {
            try
            {
                var list = new MC_BLK_ADFB_REQ_USR_LAVELModel().GetAddFabBkUserLavel();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AddFabBk/GetAddFabBkRpt")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkRpt
        public IHttpActionResult GetAddFabBkRpt(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            try
            {
                var obList = new MC_BLK_ADFB_REQ_RPTModel().GetAddFabBkRpt(pMC_BLK_ADFB_REQ_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("AddFabBk/GetAddFabBkRptPdf")]
        [HttpGet]
        // GET :  /api/mrc/AddFabBk/GetAddFabBkRptPdf
        public HttpResponseMessage GetAddFabBkRptPdf()
        {

            string customPath = ConfigurationManager.AppSettings["ACC_BOOKING:path"];
            customPath = HttpContext.Current.Server.MapPath(customPath).ToString() + "\\aminul.pdf";
            string Url = "http://" + HttpContext.Current.Request.Url.Authority + "/Merchandising/MrcPdfRpt/AddFabBkingRpt?a=142/#/AddFabBkingRpt?pMC_BLK_ADFB_REQ_H_ID=5";

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

        
        [Route("AddFabBk/BatchSave")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/AddFabBk/BatchSave
        public IHttpActionResult BatchSave(MC_BLK_ADFB_REQ_HModel ob)
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

        [Route("AddFabBk/SubmitAddFabBk")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/AddFabBk/SubmitAddFabBk
        public IHttpActionResult SubmitAddFabBk(MC_BLK_ADFB_REQ_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SubmitAddFabBk();
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

        [Route("AddFabBk/ApproveAddFabBk")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/AddFabBk/ApproveAddFabBk
        public IHttpActionResult ApproveAddFabBk(MC_BLK_ADFB_REQ_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ApproveAddFabBk();
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

        [Route("AddFabBk/ReviseAddFabBk")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/AddFabBk/ReviseAddFabBk
        public IHttpActionResult ReviseAddFabBk(MC_BLK_ADFB_REQ_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ReviseAddFabBk();
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
