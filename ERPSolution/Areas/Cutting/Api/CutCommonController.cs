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
    public class CutCommonController : ApiController
    {
        [Route("CutTblTgt/GetProdPlnClndrId")]
        [HttpGet]
        // GET :  /api/Cutting/CutTblTgt/GetProdPlnClndrId
        public IHttpActionResult GetProdPlnClndrId(DateTime? pCALENDAR_DT = null)
        {
            try
            {
                var obList = new GMT_CUT_TBL_TGTModel().GetProdPlnClndrId(pCALENDAR_DT);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("CutTblTgt/GetCuttingTableTergetList")]
        [HttpGet]
        // GET :  /api/Cutting/CutTblTgt/GetCuttingTableTergetList
        public IHttpActionResult GetCuttingTableTergetList(Int64 pGMT_PROD_PLN_CLNDR_ID)
        {
            try
            {
                var obList = new GMT_CUT_TBL_TGTModel().GetCuttingTableTergetList(pGMT_PROD_PLN_CLNDR_ID);

                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("CutTblTgt/BatchSave")]
        [HttpPost]
        // POST :   /api/Cutting/CutTblTgt/BatchSave
        public IHttpActionResult BatchSave([FromBody] GMT_CUT_TBL_TGTModel ob)
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


        
    }
}
