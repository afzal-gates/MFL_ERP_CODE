using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Garments.Api
{
    [RoutePrefix("api/gmt")]
    [Authorize]
    public class GmtFinController : ApiController
    {
        [Route("GmtFin/GetGmtFinIronData")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinIronData
        public IHttpActionResult GetGmtFinIronData(DateTime pCALENDAR_DT, string pHR_PROD_FLR_LST)
        {
            try
            {
                var obList = new GMT_FIN_IRON_HModel().GetGmtFinIronData(pCALENDAR_DT, pHR_PROD_FLR_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtFin/GetGmtFinOrderList")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinOrderList
        public IHttpActionResult GetGmtFinOrderList(Int64? pGMT_FIN_IRON_H_ID = null, string pMC_ORDER_H_LST = null)
        {
            try
            {
                var obList = new GMT_FIN_IRON_HModel().GetGmtFinOrderList(pGMT_FIN_IRON_H_ID, pMC_ORDER_H_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [Route("GmtFin/BatchSaveGmtFinIron")]
        [HttpPost]
        // GET :  /api/gmt/GmtFin/BatchSaveGmtFinIron
        public IHttpActionResult BatchSaveGmtFinIron([FromBody] GMT_FIN_IRON_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveGmtFinIron();
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



        [Route("GmtFin/GetGmtFinPolyData")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinPolyData
        public IHttpActionResult GetGmtFinPolyData(DateTime pCALENDAR_DT, string pHR_PROD_FLR_LST)
        {
            try
            {
                var obList = new GMT_FIN_POLY_HModel().GetGmtFinPolyData(pCALENDAR_DT, pHR_PROD_FLR_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtFin/GetGmtFinPolyOrderList")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinPolyOrderList
        public IHttpActionResult GetGmtFinPolyOrderList(Int64? pGMT_FIN_POLY_H_ID = null, string pMC_ORDER_H_LST = null)
        {
            try
            {
                var obList = new GMT_FIN_POLY_HModel().GetGmtFinPolyOrderList(pGMT_FIN_POLY_H_ID, pMC_ORDER_H_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtFin/BatchSaveGmtFinPoly")]
        [HttpPost]
        // GET :  /api/gmt/GmtFin/BatchSaveGmtFinPoly
        public IHttpActionResult BatchSaveGmtFinPoly([FromBody] GMT_FIN_POLY_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveGmtFinPoly();
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



        [Route("GmtFin/GetGmtFinFoldData")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinFoldData
        public IHttpActionResult GetGmtFinFoldData(DateTime pCALENDAR_DT, string pHR_PROD_FLR_LST)
        {
            try
            {
                var obList = new GMT_FIN_FOLD_HModel().GetGmtFinFoldData(pCALENDAR_DT, pHR_PROD_FLR_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtFin/GetGmtFinFoldOrderList")]
        [HttpGet]
        // GET :  /api/gmt/GmtFin/GetGmtFinFoldOrderList
        public IHttpActionResult GetGmtFinFoldOrderList(Int64? pGMT_FIN_FOLD_H_ID = null, string pMC_ORDER_H_LST = null)
        {
            try
            {
                var obList = new GMT_FIN_FOLD_HModel().GetGmtFinFoldOrderList(pGMT_FIN_FOLD_H_ID, pMC_ORDER_H_LST);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GmtFin/BatchSaveGmtFinFold")]
        [HttpPost]
        // GET :  /api/gmt/GmtFin/BatchSaveGmtFinFold
        public IHttpActionResult BatchSaveGmtFinFold([FromBody] GMT_FIN_FOLD_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSaveGmtFinFold();
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
