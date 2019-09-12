using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    public class MrcPmsController : ApiController
    {
        [Route("Pms/BuyerStyleOrderFollowupList")]
        [HttpGet]
        // GET :  /api/mrc/Pms/BuyerStyleOrderFollowupList
        public IHttpActionResult BuyerStyleOrderFollowupList(int pageNumber, int pageSize, Int64? pMC_BYR_ACC_ID = null, DateTime? pFIRSTDATE = null, DateTime? pLASTDATE = null, string pMC_ORDER_H_ID_LST = null, int? pRF_FAB_PROD_CAT_ID = null, string pSTYLE_ORDER_NO = null, string WITH_SUM = "N")
        {
            var data = new MC_ORDER_HModel().BuyerStyleOrderFollowupList(pageNumber, pageSize, pMC_BYR_ACC_ID, pFIRSTDATE, pLASTDATE, pMC_ORDER_H_ID_LST, pRF_FAB_PROD_CAT_ID, pSTYLE_ORDER_NO);
            int total = 0;
            Int64 TOT_ORD_QTY_COL_SUM = 0;
            decimal NET_GFAB_QTY_SUM = 0;
            decimal KNIT_QC_PASS_QTY_SUM = 0;
            decimal DYE_QC_PASS_QTY_SUM = 0;

            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            if (WITH_SUM=="Y")
            {
                TOT_ORD_QTY_COL_SUM = data.Sum(x => x.TOT_ORD_QTY_COL);
                NET_GFAB_QTY_SUM = data.Sum(x => x.NET_GFAB_QTY);
                KNIT_QC_PASS_QTY_SUM = data.Sum(x => x.KNIT_QC_PASS_QTY);
                DYE_QC_PASS_QTY_SUM = data.Sum(x => x.DYE_QC_PASS_QTY);
             
            }

            return Ok(new { total, data, TOT_ORD_QTY_COL_SUM, NET_GFAB_QTY_SUM, KNIT_QC_PASS_QTY_SUM, DYE_QC_PASS_QTY_SUM });
        }

        [Route("Pms/BuyerStyleOrderFollowupListForPln")]
        [HttpGet]
        // GET :  /api/mrc/Pms/BuyerStyleOrderFollowupListForPln
        public IHttpActionResult BuyerStyleOrderFollowupList(
                 int pageNumber, int pageSize,
                 long? pMC_BYR_ACC_ID=null,
                 DateTime? pFIRSTDATE =null,
                 DateTime? pLASTDATE =null,
                 string pMC_ORDER_H_ID_LST = null,
                 Int64? pINV_ITEM_CAT_ID_P = null,
                 Int64? pINV_ITEM_CAT_ID = null,
                 Int64? pLK_ORD_TYPE_ID  = null,
                 int? pOption =null,
                 int? pLK_GARM_TYPE_ID = null
            
         )
        {
            var data = new MC_ORDER_HModel().BuyerStyleOrderFollowupListForPln(pageNumber, pageSize, pMC_BYR_ACC_ID, pFIRSTDATE, pLASTDATE, pMC_ORDER_H_ID_LST, pINV_ITEM_CAT_ID_P, pINV_ITEM_CAT_ID, pLK_ORD_TYPE_ID, pOption, pLK_GARM_TYPE_ID);
            int total = 0;
            if (data.Count > 0)
                total = data.FirstOrDefault().TOTAL_REC;
            return Ok(new { total, data });
        }
    }
}
