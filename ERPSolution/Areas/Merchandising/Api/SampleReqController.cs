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
using System.Web.Http;
using System.Web.Http.Description;



namespace ERPSolution.Areas.Merchandising.Api
{   
    [RoutePrefix("api/mrc")]
    public class SampleReqController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("SampleReq/SaveDevSmpAutoCreate")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult SaveDevSmpAutoCreate(MC_SMP_REQ_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveDevSmpAutoCreate();

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



        [Route("SampleReq/getSmpTypList4Prod")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getSmpTypList4Prod
        public IHttpActionResult getSmpTypList4Prod()
        {
            var ob = new MC_SMP_PRODModel().getSmpTypList4Prod();
            return Ok(ob);
        }

        [Route("SampleReq/getSmplProdQty")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getSmplProdQty?
        public IHttpActionResult getSmplProdQty(DateTime? pPROD_DT = null, int? pPROD_BATCH_NO = null)
        {
            var ob = new MC_SMP_PRODModel().getSmplProdQty(pPROD_DT, pPROD_BATCH_NO);
            return Ok(ob);
        }

        [Route("SampleReq/getSmplProdBatchListByDate")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getSmplProdBatchListByDate?
        public IHttpActionResult getSmplProdBatchListByDate(DateTime? pPROD_DT = null)
        {
            var ob = new MC_SMP_PRODModel().getSmplProdBatchListByDate(pPROD_DT);
            return Ok(ob);
        }

        [Route("SampleReq/getNewProdBatch")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getNewProdBatch?
        public IHttpActionResult getStyleSmpTypWiseProdQty(DateTime pPROD_DT, Int64? pPROD_BATCH_NO = null)
        {
            var ob = new MC_SMP_PRODModel().getNewProdBatch(pPROD_DT, pPROD_BATCH_NO);
            return Ok(ob);
        }

        [Route("SampleReq/getStyleSmpTypWiseProdQty")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getStyleSmpTypWiseProdQty?
        public IHttpActionResult getStyleSmpTypWiseProdQty(Int64 pMC_SMP_REQ_H_ID, Int64 pMC_STYLE_H_EXT_ID, Int64 pRF_SMPL_TYPE_ID, 
            DateTime pPROD_DT, Int64 pPROD_BATCH_NO)
        {
            var ob = new MC_SMP_PRODModel().getStyleSmpTypWiseProdQty(pMC_SMP_REQ_H_ID, pMC_STYLE_H_EXT_ID, pRF_SMPL_TYPE_ID, pPROD_DT, pPROD_BATCH_NO);
            return Ok(ob);
        }

        [Route("SampleReq/getOrdListByReqId/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getOrdListByReqId/1
        public IHttpActionResult getOrdListByReqId(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_HModel().getOrdListByReqId(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }


        [Route("SampleReq/getByrAcListByReqId/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/getByrAcListByReqId/1
        public IHttpActionResult getByrAcListByReqId(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_HModel().getByrAcListByReqId(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }


        [Route("SampleReq/GetSmplStyleCheck")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  mrc/api/SampleReq/GetSmplStyleCheck
        public IHttpActionResult GetSmplStyleCheck(MC_SMP_REQ_HModel ob)
        {
            string jsonStr = "";

            jsonStr = ob.GetSmplStyleCheck();

            return Ok(new { success = true, jsonStr });
        }

        [Route("SampleReq/BatchSaveSmplProd")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult BatchSaveSmplProd(MC_SMP_PRODVM_Model ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveSmplProd();
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
        

        [Route("SampleReq/GenTestInsList/{pMC_BUYER_ID}/{pMC_STYLE_H_ID}/{pIS_NEED_TEST}/{pIS_ASSORT}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/GenTestInsList/1/Y/1
        public IHttpActionResult GenTestInsList(Int64 pMC_BUYER_ID, Int64 pMC_STYLE_H_ID, string pIS_NEED_TEST, string pIS_ASSORT, Int64? pMC_ORDER_H_ID = null)
        {
            var ob = new MC_SMP_REQ_DModel().GenTestInsList(pMC_BUYER_ID, pMC_STYLE_H_ID, pIS_NEED_TEST, pIS_ASSORT, pMC_ORDER_H_ID);
            return Ok(ob);
        }
    

        [Route("SampleReq/SampWiseItmCfgList/{pMC_SMP_REQ_D_ID}/{pIS_NEED_TEST}/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/SampWiseItmCfgList/1/Y/1
        public IHttpActionResult SampWiseItmCfgList(Int64 pMC_SMP_REQ_D_ID, string pIS_NEED_TEST, Int64 pMC_STYLE_H_ID, Int64 pMC_ORDER_H_ID)
        {
            var ob = new MC_SMP_REQ_DModel().SampWiseItmCfgList(pMC_SMP_REQ_D_ID, pIS_NEED_TEST, pMC_STYLE_H_ID, pMC_ORDER_H_ID);
            return Ok(ob);
        }

        [Route("SampleReq/GetAssortList/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/GetAssortList/1
        public IHttpActionResult GetAssortList(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_D_ASORTModel().GetAssortList(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }

        [Route("SampleReq/StyleListWiseSampList/{pMC_STYLE_H_EXT_IDS}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/StyleListWiseSampList/1,2,5
        public IHttpActionResult StyleListWiseSampList(string pMC_STYLE_H_EXT_IDS)
        {
            var ob = new MC_SMP_REQ_DModel().StyleListWiseSampList(pMC_STYLE_H_EXT_IDS);
            return Ok(ob);
        }

        [Route("SampleReq/BuyerAccWiseBookingList/{pMC_BYR_ACC_ID}/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/SampleReq/BuyerAccWiseBookingList/1
        public IHttpActionResult BuyerAccWiseBookingList(int pageNumber, int pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_SMP_REQ_H_ID, string pLK_ORDER_TYPE_LST = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            var obList = new MC_SMP_REQ_HModel().BuyerAccWiseBookingList(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_SMP_REQ_H_ID, pLK_ORDER_TYPE_LST, pMC_STYLE_H_EXT_ID);
            return Ok(obList);
        }

        [Route("SampleReq/Select/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/SampleReq/Select/1
        public IHttpActionResult Select(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_HModel().Select(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }

        [Route("SampleReq/getSmpHdr/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/SampleReq/Select/1
        public IHttpActionResult getSmpHdr(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_HModel().getSmpHdr(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }
        

        [Route("SampleReq/SampReqDtlListByHID/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/SampReqDtlListByHID/1
        public IHttpActionResult SampReqDtlListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_DModel().SampReqDtlListByHID(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }

        [Route("SampleReq/SampReqDtl1ListByHID/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/SampReqDtl1ListByHID/1
        public IHttpActionResult SampReqDtl1ListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_D1Model().SampReqDtl1ListByHID(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }


        [Route("SampleReq/SamFabQtyStyleListByHID/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/SamFabQtyStyleListByHID/1
        public IHttpActionResult SamFabQtyStyleListByHID(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_D2Model().SamFabQtyStyleListByHID(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }


        [Route("SampleReq/SamFabBookingQtyList/{pMC_SMP_REQ_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/SampleReq/SamFabBookingList/1
        public IHttpActionResult SamFabBookingQtyList(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_SMP_REQ_D2Model().SamFabBookingQtyList(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }


        [Route("SampleReq/BroadcastSmpReqNotif")]
        [HttpGet]
        //[Authorize]
        // GET :  /api/mrc/SampleReq/BroadcastSmpReqNotif
        public IHttpActionResult BroadcastSmpReqNotif(int pageNumber, int pageSize, string pSTYLE_NO_LST = null, string pSMP_REQ_TYPE = null,
            string pBYR_ACC_NAME_EN_LST = null, DateTime? pSMP_REQ_DT = null)
        {
            var obList = new MC_SMP_REQ_HModel().getSmpReqNotif(pageNumber, pageSize, pSTYLE_NO_LST, pSMP_REQ_TYPE, pBYR_ACC_NAME_EN_LST, pSMP_REQ_DT);
            return Ok(obList);
        }


        [Route("SampleReq/getSmpReqNotifDtl")]
        [HttpGet]
        //[System.Web.Http.Authorize]
        public IHttpActionResult getSmpReqNotifDtl(Int64 pMC_SMP_REQ_H_ID)
        {
            var obList = new MC_SMP_REQ_NotifDtlModel().getSmpReqNotifDtl(pMC_SMP_REQ_H_ID);
            return Ok(obList);
        }
        

        [Route("SampleReq/DeleteSampType")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult DeleteSampType(MC_SMP_REQ_DModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteSampType();
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

        [Route("SampleReq/DeleteSampCfg")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult DeleteSampCfg(MC_SMP_REQ_D_CFGModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteSampCfg();
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


        [Route("SampleReq/DeleteSampFabColor")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult DeleteSampFabColor(MC_SMP_REQ_D2Model ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.DeleteSampFabColor();
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



        [Route("SampleReq/BatchSaveRevise")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult BatchSaveRevise(MC_SMP_REQ_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveRevise();
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

            //Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });

        }

        [Route("SampleReq/BatchSave")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult BatchSave(MC_SMP_REQ_HModel ob)
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
            //var x = new { jsonStr. };

            //Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });

        }

        [Route("SampleReq/SendToSample")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult SendToSample(MC_SMP_REQ_HModel ob)
        {
            string jsonStr = "";
            jsonStr = ob.SendToSample();

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        jsonStr = ob.SendToSample();
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
            

            Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("SampleReq/AcceptSample")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult AcceptSample(MC_SMP_REQ_HModel ob)
        {
            string jsonStr = "";
            jsonStr = ob.AcceptSample();
            
            Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("SampleReq/BatchSaveSendToInhouse")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult BatchSaveSendToInhouse(MC_SMP_PRODVM_Model ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveSendToInhouse();
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

            Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });

        }

        [Route("SampleReq/SampleSendToBuyer")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult SampleSendToBuyer(MC_SMP_REQ_DModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    MC_SMP_PRODVM_Model obj = new MC_SMP_PRODVM_Model();
                    jsonStr = obj.SampleSendToBuyer(ob);
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

            Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });

        }

        [Route("SampleReq/BatchSaveByrFeedback")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult BatchSaveByrFeedback(MC_SMP_FEEDBKModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveByrFeedback();
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

            Hub.Clients.All.broadcastSmpReqNotif();
            return Ok(new { success = true, jsonStr });

        }
        

        [Route("SampleReq/GetSmpNotifDtl4ByrFeedback/{pMC_SMP_REQ_D_ID}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetSmpNotifDtl4ByrFeedback(Int64 pMC_SMP_REQ_D_ID)
        {
            var obList = new MC_SMP_REQ_NotifDtlModel().GetSmpNotifDtl4ByrFeedback(pMC_SMP_REQ_D_ID);
            return Ok(obList);
        }

        [Route("SampleReq/SampReqDtl1ListByDID")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/SampleReq/SampReqDtl1ListByDID
        public IHttpActionResult SampReqDtl1ListByDID(Int64 pMC_SMP_REQ_D_ID)
        {
            var ob = new MC_SMP_REQ_D1Model().SampReqDtl1ListByDID(pMC_SMP_REQ_D_ID);
            return Ok(ob);
        }

        [Route("SampleReq/SmplByrFeedbackStatus")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public IHttpActionResult SmplByrFeedbackStatus(Int64? pMC_SMP_REQ_D_ID = null)
        {
            var obList = new MC_SMP_FEEDBKModel().SmplByrFeedbackStatus(pMC_SMP_REQ_D_ID);
            return Ok(obList);
        }

        [Route("SampleReq/GetMailSendPendingListSmplProg")]
        [HttpGet]
        [System.Web.Http.Authorize]
        public IHttpActionResult GetMailSendPendingListSmplProg()
        {
            var obList = new MC_SMP_REQ_HModel().GetMailSendPendingListSmplProg();

            return Ok(obList);
        }


    }
}
