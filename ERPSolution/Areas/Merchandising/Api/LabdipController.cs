using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
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
    public class LabdipController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("Labdip/LabdipDataList/{pMC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipDataList
        public IHttpActionResult LabdipDataList(Int64 pMC_BUYER_ID)
        {
            var obList = new MC_LD_REQ_HModel().LabdipDataList(pMC_BUYER_ID);
            return Ok(obList);
        }

        [Route("Labdip/LabdipListByBuyerAcc/{pMC_BYR_ACC_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipListByBuyerAcc
        public IHttpActionResult LabdipListByBuyerAcc(Int64 pMC_BYR_ACC_ID)
        {
            var obList = new MC_LD_REQ_HModel().LabdipListByBuyerAcc(pMC_BYR_ACC_ID);
            return Ok(obList);
        }

        //[Route("Labdip/LabdipBuyerWiseList/{pMC_BUYER_ID:int}")]
        //[HttpGet]
        //// GET :  mrc/api/Labdip/LabdipBuyerWiseList
        //public IHttpActionResult LabdipBuyerWiseList(Int64 pMC_BUYER_ID)
        //{
        //    var obList = new MC_LD_REQ_HModel().LabdipBuyerWiseList(pMC_BUYER_ID);
        //    return Ok(obList);
        //}

        [Route("Labdip/LabdipSubListByBuyerAcc/{pMC_BYR_ACC_ID}/{pMC_LD_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipSubListByBuyerAcc
        public IHttpActionResult LabdipSubListByBuyerAcc(Int64? pMC_BYR_ACC_ID, Int64? pMC_LD_REQ_H_ID)
        {
            var obList = new MC_LD_REQ_HModel().LabdipSubListByBuyerAcc(pMC_BYR_ACC_ID, pMC_LD_REQ_H_ID);
            return Ok(obList);
        }

        [Route("Labdip/LabdipColorStyleWise/{pMC_STYLE_H_ID}/{pMC_LD_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipColorStyleWise
        public IHttpActionResult LabdipColorStyleWise(Int64 pMC_STYLE_H_ID, Int64 pMC_LD_REQ_H_ID)
        {
            int count = 0;
            var obList = new MC_LD_SUBMITModel().LabdipColorStyleWise(pMC_STYLE_H_ID, pMC_LD_REQ_H_ID);
            for (int i = 0; i < obList.Count; i++)
            {
                if (obList[i].REVISION_NO > 0 && (obList[i].MC_TNA_TASK_STATUS_ID==11 || obList[i].MC_TNA_TASK_STATUS_ID==7))
                {
                    count = 1;
                }
            }
            if (count > 0)
            {
                obList = obList.Where(r => r.REVISION_NO > 0).ToList();
            }
            return Ok(obList);
        }

        [Route("Labdip/LabdipColorStyleWiseHistory/{pSTYLE_NO}/{pMC_LD_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipColorStyleWiseHistory
        public IHttpActionResult LabdipColorStyleWiseHistory(string pSTYLE_NO, Int64 pMC_LD_REQ_H_ID)
        {
            int count = 0;
            var obList = new MC_LD_SUBMITModel().LabdipColorStyleWiseHistory(pSTYLE_NO, pMC_LD_REQ_H_ID);
            for (int i = 0; i < obList.Count; i++)
            {
                if (obList[i].REVISION_NO > 0)
                {
                    count = 1;
                }
            }
            if (count > 0)
            {
                obList = obList.Where(r => r.REVISION_NO > 0).ToList();
            }
            return Ok(obList);
        }

        [Route("Labdip/LabdipDataListByHID/{pMC_LD_REQ_H_ID}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipDataListByHID
        public IHttpActionResult LabdipDataListByHID(Int64 pMC_LD_REQ_H_ID)
        {
            var obList = new MC_LD_REQ_HModel().LabdipDataListByHID(pMC_LD_REQ_H_ID);
            return Ok(obList);
        }

        [Route("Labdip/LabdipProgramForDB")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipProgramForDB
        public IHttpActionResult LabdipProgramForDB()
        {
            var obList = new MC_LD_REQ_HModel().LabdipProgramForDB(Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]));
            return Ok(obList);
        }


        [Route("Labdip/LabdipProgramForDBStyleWise")]
        [HttpGet]
        // GET :  mrc/api/Labdip/LabdipProgramForDBStyleWise
        public IHttpActionResult LabdipProgramForDBStyleWise(String pSearchParams=null)
        {
            var obList = new MC_LD_REQ_HModel().LabdipProgramForDBStyleWise(Convert.ToInt64(HttpContext.Current.Session["multiScUserId"]), pSearchParams);
            return Ok(obList);
        }


        [Route("Labdip/BatchSaveLabdip")]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult BatchSaveLabdip(MC_LD_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ob.items.Count() > 0)
            {
                for (int i = 0; i < ob.items.Count(); i++)
                {
                    ob.items[i].MC_TNA_TASK_STATUS_ID = 1;
                }
            }

            jsonStr = ob.BatchSaveLabdip();
            //Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("Labdip/SendToLab")]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult SendToLab(MC_LD_REQ_HModel ob)
        {
            if (ob.items.Count() > 0)
            {
                for (int i = 0; i < ob.items.Count(); i++)
                {
                    ob.items[i].MC_TNA_TASK_STATUS_ID = 2;
                }
            }
            string jsonStr = "";
            jsonStr = ob.BatchSaveLabdip();
            Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("Labdip/BatchUpdateLabdip")]
        [HttpPost]
        //[Authorize]
        public IHttpActionResult BatchUpdateLabdip(MC_LD_REQ_HModel ob)
        {
            string jsonStr = "";
            if (ob.items.Count() > 0)
            {
                for (int i = 0; i < ob.items.Count(); i++)
                {
                    ob.items[i].MC_TNA_TASK_STATUS_ID = 1;
                }
            }

            jsonStr = ob.BatchSaveLabdip();
            Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("Labdip/BatchUpdateLabdip/{pMC_LD_REQ_H_ID}/{pMC_TNA_TASK_STATUS_ID}/{MC_LD_REQ_D_ID}")]
        [HttpGet]
        // GET :  mrc/api/Labdip/BatchUpdateLabdip
        public IHttpActionResult BatchUpdateLabdip(Int64 pMC_LD_REQ_H_ID, Int64 pMC_TNA_TASK_STATUS_ID, Int64? MC_LD_REQ_D_ID)
        {
            string jsonStr = "";
            if (pMC_LD_REQ_H_ID > 0)
            {
                var ob = new MC_LD_REQ_HModel().LabdipDataWOApprNo(pMC_LD_REQ_H_ID);
                if (ob.items.Count() > 0)
                {
                    for (int i = 0; i < ob.items.Count(); i++)
                    {
                        if (MC_LD_REQ_D_ID!=null)
                        {
                            ob.items[i].MC_TNA_TASK_STATUS_ID = (ob.items[i].MC_STYLE_H_ID == MC_LD_REQ_D_ID) ? pMC_TNA_TASK_STATUS_ID : ob.items[i].MC_TNA_TASK_STATUS_ID;
                        }
                            
                    }
                }
                jsonStr = ob.BatchSaveLabdip();
                Hub.Clients.All.LabdipRequestProgramNotif();
            }
            return Ok(new { success = true, jsonStr });
        }

    }
}
