using ERP.Model;
//using ERPSolution.Hubs;
//using Microsoft.AspNet.SignalR;
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
    public class PrintStrikeOffController : ApiController
    {
        //private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        //[Route("PrintStrikeOff/PrintStrikeOffDataList/{pMC_BUYER_ID:int}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffDataList
        //public IHttpActionResult PrintStrikeOffDataList(Int64 pMC_BUYER_ID)
        //{
        //    var obList = new MC_PRNSO_REQ_HModel().PrintStrikeOffDataList(pMC_BUYER_ID);
        //    return Ok(obList);
        //}

        [Route("PrintStrikeOff/PrintStrikeOffListByBuyerAcc/{pMC_BYR_ACC_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/PrintStrikeOff/PrintStrikeOffListByBuyerAcc
        public IHttpActionResult PrintStrikeOffListByBuyerAcc(Int64 pMC_BYR_ACC_ID)
        {
            var obList = new MC_PRNSO_REQ_HModel().PrintStrikeOffListByBuyerAcc(pMC_BYR_ACC_ID);
            return Ok(obList);
        }

        //[Route("PrintStrikeOff/PrintStrikeOffSubListByBuyerAcc/{pMC_BYR_ACC_ID:int}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffSubListByBuyerAcc
        //public IHttpActionResult PrintStrikeOffSubListByBuyerAcc(Int64 pMC_BYR_ACC_ID)
        //{
        //    var obList = new MC_PRNSO_REQ_HModel().PrintStrikeOffSubListByBuyerAcc(pMC_BYR_ACC_ID);
        //    return Ok(obList);
        //}

        //[Route("PrintStrikeOff/PrintStrikeOffColorStyleWise/{pSTYLE_NO}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffColorStyleWise
        //public IHttpActionResult PrintStrikeOffColorStyleWise(string pSTYLE_NO)
        //{
        //    var obList = new MC_LD_SUBMITModel().PrintStrikeOffColorStyleWise(pSTYLE_NO);
        //    return Ok(obList);
        //}

        //[Route("PrintStrikeOff/PrintStrikeOffColorStyleWiseHistory/{pSTYLE_NO}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffColorStyleWiseHistory
        //public IHttpActionResult PrintStrikeOffColorStyleWiseHistory(string pSTYLE_NO)
        //{
        //    var obList = new MC_LD_SUBMITModel().PrintStrikeOffColorStyleWiseHistory(pSTYLE_NO);
        //    return Ok(obList);
        //}

        //[Route("PrintStrikeOff/PrintStrikeOffDataListByHID/{pMC_LD_REQ_H_ID}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffDataListByHID
        //public IHttpActionResult PrintStrikeOffDataListByHID(Int64 pMC_LD_REQ_H_ID)
        //{
        //    var obList = new MC_PRNSO_REQ_HModel().PrintStrikeOffDataListByHID(pMC_LD_REQ_H_ID);
        //    return Ok(obList);
        //}

        //[Route("PrintStrikeOff/PrintStrikeOffProgramForDB")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/PrintStrikeOffProgramForDB
        //public IHttpActionResult PrintStrikeOffProgramForDB()
        //{
        //    var obList = new MC_PRNSO_REQ_HModel().PrintStrikeOffProgramForDB(Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]));
        //    return Ok(obList);
        //}

        [Route("PrintStrikeOff/SaveBatchData")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveBatchData(MC_PRNSO_REQ_HModel ob)
        {
            if (ob.MC_PRNSO_REQ_H_ID > 0)
            {
                ob = new MC_PRNSO_REQ_HModel().PrintStrikeOffListByHID(ob.MC_PRNSO_REQ_H_ID);
                //if (ob.items.Count() > 0)
                //{
                //    for (int i = 0; i < ob.items.Count(); i++)
                //    {
                //        ob.items[i].RF_ACTN_STATUS_ID = 2;
                //    }
                //}
            }
            string jsonStr = "";
            jsonStr = ob.SaveBatchData();
            //Hub.Clients.All.PrintStrikeOffRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("PrintStrikeOff/UpdateBatchData")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult UpdateBatchData(MC_PRNSO_REQ_HModel ob)
        {            
            string jsonStr = "";
            jsonStr = ob.SaveBatchData();
            //Hub.Clients.All.PrintStrikeOffRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        //[Route("PrintStrikeOff/BatchUpdatePrintStrikeOff/{pMC_LD_REQ_H_ID}/{pRF_ACTN_STATUS_ID}")]
        //[HttpGet]
        //// GET :  mrc/api/PrintStrikeOff/BatchUpdatePrintStrikeOff
        //public IHttpActionResult BatchUpdatePrintStrikeOff(Int64 pMC_LD_REQ_H_ID, Int64 pRF_ACTN_STATUS_ID)
        //{
        //    string jsonStr = "";
        //    if (pMC_LD_REQ_H_ID > 0)
        //    {
        //        var ob = new MC_PRNSO_REQ_HModel().PrintStrikeOffDataWOApprNo(pMC_LD_REQ_H_ID);                
        //        if (ob.items.Count() > 0)
        //        {
        //            for (int i = 0; i < ob.items.Count(); i++)
        //            {
        //                if (ob.items[i].APRV_LD_REF == "")
        //                {
        //                    ob.items[i].RF_ACTN_STATUS_ID = pRF_ACTN_STATUS_ID;
        //                }
        //            }
        //        }
        //        jsonStr = ob.SaveBatchData();
        //        Hub.Clients.All.PrintStrikeOffRequestProgramNotif();
        //    }
        //    return Ok(new { success = true, jsonStr });
        //}

    }
}
