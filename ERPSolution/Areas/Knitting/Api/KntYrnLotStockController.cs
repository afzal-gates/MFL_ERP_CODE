using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    [Authorize]
    public class KntYrnLotStockController : ApiController
    {
        [Route("KntYrnLotStock/GetYarnLotStockList")]
        [HttpGet]
        // GET :  /api/knit/KntYrnLotStock/GetYarnLotStockList
        public IHttpActionResult GetYarnLotStockList(int pageNumber, int pageSize, Int32? pRF_YRN_CNT_ID = null, Int32? pRF_FIB_COMP_ID = null, Int32? pLK_SPN_PRCS_ID = null,
            Int32? pLK_COTN_TYPE_ID = null, string pIS_SLUB = null, string pIS_MELLANGE = null, Int64? pKNT_YRN_LOT_ID = null, Int32? pRF_BRAND_ID = null,
            Int32? pSCM_STORE_ID = null)
        {
            try
            {
                var obList = new KNT_YRN_LOT_STKModel().GetYarnLotStockList(pageNumber, pageSize, pRF_YRN_CNT_ID, pRF_FIB_COMP_ID, pLK_SPN_PRCS_ID,
                    pLK_COTN_TYPE_ID, pIS_SLUB, pIS_MELLANGE, pKNT_YRN_LOT_ID, pRF_BRAND_ID, pSCM_STORE_ID);                
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntYrnLotStock/GetBrandWiseYarnLotList")]
        [HttpGet]
        // GET :  /api/knit/KntYrnLotStock/GetBrandWiseYarnLotList
        public IHttpActionResult GetBrandWiseYarnLotList(Int64 pRF_BRAND_ID, string pYRN_LOT_NO = null)
        {
            try
            {
                var obList = new KNT_YRN_LOTModel().GetBrandWiseYarnLotList(pRF_BRAND_ID, pYRN_LOT_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }  
        
        
        
    }
}
