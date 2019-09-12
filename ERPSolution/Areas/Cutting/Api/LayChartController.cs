using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Cutting
{
    [RoutePrefix("api/Cutting")]
    public class LayChartController : ApiController
    {
        [Route("LayChart/GetCuttingList")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetCuttingList
        public IHttpActionResult GetCuttingList(Int64 pMC_ORDER_H_ID, Int64? pMC_ORDER_SHIP_ID = null, Int64? pGMT_COLOR_ID = null)
        {
            try
            {
                var obList = new GMT_CUT_INFOModel().GetCuttingList(pMC_ORDER_H_ID, pMC_ORDER_SHIP_ID, pGMT_COLOR_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("LayChart/GetBundleCardList")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetBundleCardList
        public IHttpActionResult GetBundleCardList(Int64 pGMT_CUT_INFO_ID)
        {
            try
            {
                var obList = new GMT_BNDL_CRD_PDATAModel().GetBundleCardList(pGMT_CUT_INFO_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetCutInfoUserLavel")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetCutInfoUserLavel
        public IHttpActionResult GetCutInfoUserLavel()
        {
            try
            {
                var list = new GMT_CUT_INFOModel().GetCutInfoUserLavel();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetCutInfoHdr")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetCutInfoHdr
        public IHttpActionResult GetCutInfoHdr(Int64 pGMT_CUT_INFO_ID)
        {
            try
            {
                var list = new GMT_CUT_INFOModel().GetCutInfoHdr(pGMT_CUT_INFO_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetCutInfoSzRto")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetCutInfoSzRto
        public IHttpActionResult GetCutInfoSzRto(Int64 pGMT_MRKR_ID, Int64? pGMT_CUT_INFO_ID = null)
        {
            try
            {
                var obList = new GMT_CUT_INFO_SZ_RTO_ITEMModel().GetCutInfoSzRto(pGMT_MRKR_ID, pGMT_CUT_INFO_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/LayChartBatchSave")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/LayChartBatchSave
        public IHttpActionResult LayChartBatchSave([FromBody] GMT_CUT_INFOModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.LayChartBatchSave();
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

        [Route("LayChart/ProcBndlCard")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/ProcBndlCard
        public IHttpActionResult ProcBndlCard([FromBody] GMT_CUT_INFOModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.ProcBndlCard();
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

        [Route("LayChart/SaveMarkAsCuttingProd")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/SaveMarkAsCuttingProd
        public IHttpActionResult SaveMarkAsCuttingProd([FromBody] GMT_CUT_INFOModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveMarkAsCuttingProd();
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

        [Route("LayChart/GetBndlCardInfo")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetBndlCardInfo
        public IHttpActionResult GetBndlCardInfo(string pBUNDLE_BARCODE)
        {
            try
            {
                var list = new GMT_BNDL_CRD_PDATAModel().GetBndlCardInfo(pBUNDLE_BARCODE);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetLastBndlAmndInfo")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetLastBndlAmndInfo
        public IHttpActionResult GetLastBndlAmndInfo(Int64 pGMT_CUT_BNDL_AMND_ID)
        {
            try
            {
                var list = new GMT_BNDL_CARD_AMND_VIEWModel().GetLastBndlAmndInfo(pGMT_CUT_BNDL_AMND_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetBndlAmndList")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetBndlAmndList
        public IHttpActionResult GetBndlAmndList(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var list = new GMT_BNDL_CARD_AMND_VIEWModel().GetBndlAmndList(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/GetBndlAmndList4Finalize")]
        [HttpGet]
        // GET :  /api/Cutting/LayChart/GetBndlAmndList4Finalize
        public IHttpActionResult GetBndlAmndList4Finalize(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var list = new GMT_BNDL_CARD_AMND_VIEWModel().GetBndlAmndList4Finalize(pGMT_PROD_PLN_CLNDR_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("LayChart/SaveBundleCardAmnd")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/SaveBundleCardAmnd
        public IHttpActionResult SaveBundleCardAmnd([FromBody] GMT_CUT_BNDL_AMNDModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveBundleCardAmnd();
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

        [Route("LayChart/RemoveBundleCardAmnd")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/RemoveBundleCardAmnd
        public IHttpActionResult RemoveBundleCardAmnd([FromBody] GMT_CUT_BNDL_AMNDModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RemoveBundleCardAmnd();
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

        [Route("LayChart/FinalizeBundleCardAmnd")]
        [HttpPost]
        // POST :   /api/Cutting/LayChart/FinalizeBundleCardAmnd
        public IHttpActionResult FinalizeBundleCardAmnd([FromBody] GMT_CUT_BNDL_AMNDModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.FinalizeBundleCardAmnd();
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
