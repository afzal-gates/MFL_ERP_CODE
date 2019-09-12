using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Planning.Api
{
    [RoutePrefix("api/pln")]
    [Authorize]
    public class ProdCapctiController : ApiController
    {

        [Route("ProdCapcti/GetCapctyProdMonthList")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetCapctyProdMonthList
        public IHttpActionResult GetCapctyProdMonthList(Int64 pPROD_UNIT_ID, Int64 pRF_FISCAL_YEAR_ID, Int64? pGMT_PRODUCT_TYP_ID = null)
        {
            try
            {
                var obList = new GMT_CAPACITY_MN_ListModel().GetCapctyProdMonthList(pPROD_UNIT_ID, pRF_FISCAL_YEAR_ID, pGMT_PRODUCT_TYP_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/GetProdTypeList")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetProdTypeList
        public IHttpActionResult GetProdTypeList()
        {
            try
            {
                var obList = new GMT_PRODUCT_TYPModel().GetProdTypeList();
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/GetCapctiMonById")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetCapctiMonById
        public IHttpActionResult GetCapctiMonById(long pGMT_CAPACITY_MN_ID)
        {
            try
            {
                var obList = new GMT_CAPACITY_MNModel().GetCapctiMonById(pGMT_CAPACITY_MN_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/getCapTrnsferData")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/getCapTrnsferData?pGMT_CAPACITY_MN_ID&pGMT_PRODUCT_TYP_ID
        public IHttpActionResult getCapTrnsferData(Int64 pGMT_CAPACITY_MN_ID, Int64? pGMT_PRODUCT_TYP_ID)
        {
            try
            {
                var obList = new GMT_CAP_TRNFERModel().Query(pGMT_CAPACITY_MN_ID, pGMT_PRODUCT_TYP_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("ProdCapcti/GetCapctiMonList4Copy")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetCapctiMonList4Copy
        public IHttpActionResult GetCapctiMonList4Copy(long pRF_FISCAL_YEAR_ID, Int64 pHR_COMPANY_ID, 
            Int64 pPROD_UNIT_ID, Int64? pGMT_PRODUCT_TYP_ID = null, Int32? pGMT_PROD_PLN_CLNDR_ID = null,
            Int64? pCORE_DEPT_ID = null,
            Int64? pLK_CALC_MTHD_ID = null)
        {
            try
            {
                var obList = new GMT_PROD_PLN_CLNDRModel().GetCapctiMonList4Copy(pRF_FISCAL_YEAR_ID, pHR_COMPANY_ID, pPROD_UNIT_ID,
                    pGMT_PRODUCT_TYP_ID, pGMT_PROD_PLN_CLNDR_ID, pCORE_DEPT_ID, pLK_CALC_MTHD_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("ProdCapcti/Save")]
        [HttpPost]
        // GET :  /api/pln/ProdCapcti/Save
        public IHttpActionResult Save([FromBody] GMT_CAPACITY_MNModel ob)
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

        [Route("ProdCapcti/gmtProdTypeLineMapSave")]
        [HttpPost]
        // GET :  /api/pln/ProdCapcti/gmtProdTypeLineMapSave
        public IHttpActionResult Save([FromBody] GMT_PRODUCT_TYPModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }
            
        }


        [Route("ProdCapcti/GetByrWiseMnCapcty")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetByrWiseMnCapcty
        public IHttpActionResult GetByrWiseMnCapcty(long pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_CAPACITY_BYR_ALLOCModel().GetByrWiseMnCapcty(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/GetMnCapctyFree4ByrAlloc")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetMnCapctyFree4ByrAlloc
        public IHttpActionResult GetMnCapctyFree4ByrAlloc(long pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_CAPACITY_BYR_ALLOCModel().GetMnCapctyFree4ByrAlloc(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/GetByrWiseWkCapcty")]
        [HttpGet]
        // GET :  /api/pln/ProdCapcti/GetByrWiseWkCapcty
        public IHttpActionResult GetByrWiseWkCapcty(long pGMT_PROD_PLN_CLNDR_ID, Int64 pMC_BYR_ACC_GRP_ID)
        {
            try
            {
                var obList = new GMT_CAPACITY_BYR_ALLOCModel().GetByrWiseWkCapcty(pGMT_PROD_PLN_CLNDR_ID, pMC_BYR_ACC_GRP_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ProdCapcti/ByrWiseWkCapctySave")]
        [HttpPost]
        // GET :  /api/pln/ProdCapcti/ByrWiseWkCapctySave
        public IHttpActionResult ByrWiseWkCapctySave([FromBody] GMT_CAPACITY_BYR_ALLOCModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.BatchSave();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, errors = e.Message });
            }

        }
       
        
    }
}
