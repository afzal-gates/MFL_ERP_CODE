using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;


namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class BulkFabBkController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();


        [Route("BulkFabBk/RevisionList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/mrc/BulkFabBk/RevisionList
        public IHttpActionResult RevisionList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {           
            try
            {
                var obList = new MC_BLK_FAB_REVISIONModel().RevisionList(pMC_BLK_FAB_REQ_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("BulkFabBk/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/BulkFabBk/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BLK_FAB_REQ_HModel().Select(ID);
            return Ok(ob);
        }

        [Route("BulkFabBk/SelectForBudgetSheet/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/BulkFabBk/SelectForBudgetSheet
        public IHttpActionResult SelectForBudgetSheet(Int64 ID)
        {
            var ob = new MC_BLK_FAB_REQ_HModel().SelectForBudgetSheet(ID);
            return Ok(ob);
        }

        //[Route("BulkFabBk/SelectDtl/{ID:int}")]
        //[HttpGet]
        //// GET :  api/mrc/BulkFabBk/Select ///////////
        //public IHttpActionResult SelectDtl(Int64 ID)
        //{
        //    var ob = new MC_BLK_FAB_REQ_DModel().SelectDtl(ID);
        //    return Ok(ob);
        //}




        [Route("BulkFabBk/BulkFabBookingList/{pMC_BYR_ACC_ID}/{pMC_STYLE_H_ID}/{pMC_STYLE_H_EXT_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/BulkFabBookingList
        // For Grid into Booking List Page
        public IHttpActionResult BulkFabBookingList(Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID, Int64 pageNumber, Int64 pageSize,
                                    string pWORK_STYLE_NO_LST = null, string pORDER_NO_LST = null, string pBLK_FAB_REQ_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null)
        {
            var obList = new MC_BLK_FAB_REQ_HModel().BulkFabBookingList(pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID, pageNumber, pageSize, pWORK_STYLE_NO_LST, pORDER_NO_LST, pBLK_FAB_REQ_NO, pMC_BYR_ACC_GRP_ID);
            return Ok(obList);
        }

        [Route("BulkFabBk/ApprovedBlkFabBkList")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/ApprovedBlkFabBkList
        // For Grid into Booking List Page
        public IHttpActionResult ApprovedBlkFabBkList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null,
                                    string pWORK_STYLE_NO_LST = null, string pORDER_NO_LST = null, string pBLK_FAB_REQ_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null,
            DateTime? pSHIP_FROM_DATE = null, DateTime? pSHIP_TO_DATE = null, DateTime? pFROM_DATE = null, DateTime? pTO_DATE = null)
        {
            var obList = new MC_BLK_FAB_REQ_HModel().ApprovedBlkFabBkList(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pMC_STYLE_H_EXT_ID, pWORK_STYLE_NO_LST, pORDER_NO_LST, pBLK_FAB_REQ_NO, pMC_BYR_ACC_GRP_ID, pSHIP_FROM_DATE, pSHIP_TO_DATE, pFROM_DATE, pTO_DATE);
            return Ok(obList);
        }

        [Route("BulkFabBk/GetBulkFabAprvlPendingList")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/GetBulkFabAprvlPendingList
        // For Grid into Booking Approval Page
        public IHttpActionResult GetBulkFabAprvlPendingList(Int64? pMC_BYR_ACC_ID = null)
        {
            var ob = new MC_BLK_FAB_REQ_HModel().GetBulkFabAprvlPendingList(pMC_BYR_ACC_ID);
            //return Ok(obList);
            Int64 vEmpId = Convert.ToInt64(HttpContext.Current.Session["multiLoginEmpId"]);

            return Ok(ob);
        }


        [Route("BulkFabBk/GetMailSendPendingListBulk")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/GetMailSendPendingListBulk
        // For Grid into Booking Approval Page
        public IHttpActionResult GetMailSendPendingListBulk()
        {
            var obList = new MC_BLK_FAB_REQ_HModel().GetMailSendPendingListBulk();

            return Ok(obList);
        }

        
        [Route("BulkFabBk/BulkFabProcessConsList/{pMC_BLK_FAB_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/BulkFabProcessConsList/1
        public IHttpActionResult BulkFabProcessConsList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            var obList = new MC_BLK_FAB_REQ_CALModel().BulkFabProcessConsList(pMC_BLK_FAB_REQ_H_ID);
            return Ok(obList);
        }

        [Route("BulkFabBk/BulkFabBookingDtlList/{pMC_BLK_FAB_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/BulkFabBookingDtlList/1
        public IHttpActionResult BulkFabBookingDtlList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            var obList = new MC_BLK_FAB_REQ_DModel().BulkFabBookingDtlList(pMC_BLK_FAB_REQ_H_ID);
            return Ok(obList);
        }


        [Route("BulkFabBk/ProcessLossColorList/{pMC_BLK_FAB_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/ProcessLossColorList/1
        public IHttpActionResult ProcessLossColorList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            var obList = new MC_BLK_FAB_PLOSSModel().ProcessLossColorList(pMC_BLK_FAB_REQ_H_ID);
            return Ok(obList);
        }

        [Route("BulkFabBk/ReportRowCategoryList/{pMC_BLK_FAB_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/ReportRowCategoryList/1
        public IHttpActionResult ReportRowCategoryList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            var obList = new MC_BLK_FAB_DATAModel().ReportRowCategoryList(pMC_BLK_FAB_REQ_H_ID);
            return Ok(obList);
        }

        [Route("BulkFabBk/CollarCuffMeasList/{pMC_BLK_FAB_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/CollarCuffMeasList
        public IHttpActionResult CollarCuffMeasList(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            var obList = new MC_BLK_COL_REQModel().CollarCuffMeasList(pMC_BLK_FAB_REQ_H_ID);
            return Ok(obList);
        }


        [Route("BulkFabBk/ProcessBulkFabBooking")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/ProcessBulkFabBooking
        public IHttpActionResult ProcessBulkFabBooking(MC_BLK_FAB_REQ_HModel ob)
        {
            string jsonStr = "";

            jsonStr = ob.ProcessBulkFabBooking();
            
            return Ok(new { success = true, jsonStr });
        }


        [Route("BulkFabBk/BatchSaveBulkFabPL")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/BatchSaveBulkFabPL
        public IHttpActionResult BatchSaveBulkFabPL(MC_BLK_FAB_PLOSSModel ob)
        {
            string jsonStr = "";
            //MC_BLK_FAB_PLOSSModel ob = new MC_BLK_FAB_PLOSSModel();

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveBulkFabPL();

                    
                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/BatchSaveBulkFabCollarCuff")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/BatchSaveBulkFabCollarCuff
        public IHttpActionResult BatchSaveBulkFabCollarCuff(MC_BLK_COL_REQModel ob)
        {
            string jsonStr = "";
            //MC_BLK_COL_REQModel ob = new MC_BLK_COL_REQModel();

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveBulkFabCollarCuff();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/BatchSaveBulkFabRptDisplayOrder")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/BatchSaveBulkFabRptDisplayOrder
        public IHttpActionResult BatchSaveBulkFabRptDisplayOrder(MC_BLK_FAB_DATAModel ob)
        {
            string jsonStr = "";
            //MC_BLK_COL_REQModel ob = new MC_BLK_COL_REQModel();

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveBulkFabRptDisplayOrder();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/BatchSaveBulkFabBooking")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/BatchSaveBulkFabBooking
        public IHttpActionResult BatchSaveBulkFabBooking(MC_BLK_FAB_REQ_HModel ob)
        {
            string jsonStr = "";            

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveBulkFabBooking();

                    
                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/SaveBulkFabFinalize")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/SaveBulkFabFinalize
        public IHttpActionResult SaveBulkFabFinalize(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            string jsonStr = "";
            MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();

            jsonStr = ob.SaveBulkFabFinalize(pMC_BLK_FAB_REQ_H_ID);

            return Ok(new { success = true, jsonStr });
        }

        [Route("BulkFabBk/SaveBlkBookingRevision")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult SaveBlkBookingRevision(MC_BLK_FAB_REVISIONModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveBlkBookingRevision();


                    //var objects = JArray.Parse(jsonStr);
                    //Jsono json = JObject.Parse(jsonStr);
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/Send4BulkBudgetAprvl")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/Send4BulkBudgetAprvl
        public IHttpActionResult Send4BulkBudgetAprvl(Int64 pMC_BLK_FAB_REQ_H_ID)
        {
            string jsonStr = "";
            MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();

            jsonStr = ob.Send4BulkBudgetAprvl(pMC_BLK_FAB_REQ_H_ID);

            Hub.Clients.All.broadcastBfbkBudgetNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("BulkFabBk/Send4BulkBudgetAprvl")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/Send4BulkBudgetAprvl
        public IHttpActionResult Send4BulkBudgetAprvl(string pXML, int pLK_BFBK_STATUS_ID)
        {
            string jsonStr = "";
            MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();

            jsonStr = ob.Send4BulkBudgetAprvl(pXML, pLK_BFBK_STATUS_ID);

            Hub.Clients.All.broadcastBfbkBudgetNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("BulkFabBk/BroadcastBfbkBudgetNotif")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/BulkFabBk/BroadcastBfbkBudgetNotif
        public IHttpActionResult BroadcastBfbkBudgetNotif(int pageNumber, int pageSize, string pSTYLE_NO = null, string pBYR_ACC_NAME_EN = null, DateTime? pBLK_FAB_REQ_DT = null)
        {
            var obList = new MC_BLK_FAB_REQ_HModel().getBfbkBudgetNotif(pageNumber, pageSize, pSTYLE_NO, pBYR_ACC_NAME_EN, pBLK_FAB_REQ_DT);
            return Ok(obList);
        }

        [Route("BulkFabBk/BatchSaveTrmsDyBooking")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // POST: /api/mrc/BulkFabBk/BatchSaveTrmsDyBooking
        public IHttpActionResult BatchSaveTrmsDyBooking(MC_TRMS_DY_PRODModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveTrmsDyBooking();
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
            //var x = new { jsonStr. };
            return Ok(new { success = true, jsonStr });

        }

        [Route("BulkFabBk/GetTrmsDyProdList")]
        [HttpGet]
        // GET :  /api/mrc/BulkFabBk/GetTrmsDyProdList
        public IHttpActionResult GetTrmsDyProdList(Int64 pMC_FAB_PROD_ORD_H_ID)
        {
            var obList = new MC_TRMS_DY_PRODModel().GetTrmsDyProdList(pMC_FAB_PROD_ORD_H_ID);
            return Ok(obList);
        }

    }
}
