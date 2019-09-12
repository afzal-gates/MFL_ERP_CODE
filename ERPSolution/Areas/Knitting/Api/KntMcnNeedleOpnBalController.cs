using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using ERPSolution.Hubs;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    [System.Web.Http.Authorize]
    public class KntMcnNeedleOpnBalController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("McNeedleOpnBal/GetNeedleOpnBalList")]
        [HttpGet]
        // GET :  /api/knit/McNeedleOpnBal/GetNeedleOpnBalList
        public IHttpActionResult GetNeedleOpnBalList(Int64 pageNumber, Int64 pageSize, Int64? pHR_COMPANY_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var obList = new KNT_MCN_NDL_OBModel().GetNeedleOpnBalList(pageNumber, pageSize, pHR_COMPANY_ID, pSCM_STORE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("McNeedleOpnBal/GetNeedleBrokenByHdrID")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetNeedleBrokenByHdrID
        public IHttpActionResult GetNeedleBrokenByHdrID(Int64 pKNT_MCN_NDL_BRK_H_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetNeedleBrokenByHdrID(pKNT_MCN_NDL_BRK_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("McNeedleOpnBal/GetNeedleBrokenDtlByID")]
        [HttpGet]
        // GET :  /api/knit/McNeedleBroken/GetNeedleBrokenDtlByID
        public IHttpActionResult GetNeedleBrokenDtlByID(long pKNT_MCN_NDL_BRK_H_ID)
        {
            try
            {
                var obList = new KNT_MCN_NDL_BRK_HModel().GetNeedleBrokenDtlByID(pKNT_MCN_NDL_BRK_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("McNeedleOpnBal/Save")]
        [HttpPost]
        // GET :  api/knit/McNeedleOpnBal/Save
        public IHttpActionResult Save([FromBody] KNT_MCN_NDL_OBModel ob)
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

        [Route("McNeedleOpnBal/Delete")]
        [HttpPost]
        // GET :  api/knit/McNeedleOpnBal/Delete
        public IHttpActionResult Delete([FromBody] KNT_MCN_NDL_OBModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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

        [Route("McNeedleOpnBal/FinalizeAll")]
        [HttpPost]
        // GET :  api/knit/McNeedleOpnBal/FinalizeAll
        public IHttpActionResult FinalizeAll([FromBody] KNT_MCN_NDL_OBModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.FinalizeAll();
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
