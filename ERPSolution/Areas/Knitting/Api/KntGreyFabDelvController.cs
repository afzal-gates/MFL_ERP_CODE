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
    public class KntGreyFabDelvController : ApiController
    {

        [Route("KntGreyFabDelv/GetScFabDelvChlnListByPartyID")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/GetScFabDelvChlnListByPartyID
        public IHttpActionResult GetScFabDelvChlnListByPartyID(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, Int64? pKNT_SC_PRG_RCV_ID=null)
        {
            try
            {
                var obList = new KNT_SC_GFAB_DLV_HModel().GetScFabDelvChlnListByPartyID(pageNumber, pageSize, pSCM_SUPPLIER_ID, pKNT_SC_PRG_RCV_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntGreyFabDelv/ScGreyFabDelvChallanAuto")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/ScGreyFabDelvChallanAuto
        public IHttpActionResult ScGreyFabDelvChallanAuto(string pCHALAN_NO)
        {
            try
            {
                var obList = new KNT_SC_GFAB_DLV_HModel().ScGreyFabDelvChallanAuto(pCHALAN_NO);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntGreyFabDelv/GetRollData4Delv")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/GetRollData4Delv
        public IHttpActionResult GetRollData4Delv(string pFAB_ROLL_NO, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var obList = new KNT_FAB_STR_RCV_VM().GetRollData4Delv(pFAB_ROLL_NO, pSCM_SUPPLIER_ID, pSCM_STORE_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntGreyFabDelv/GetScGreyFabDelvHdr")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/GetScGreyFabDelvHdr
        public IHttpActionResult GetScGreyFabDelvHdr(Int64 pKNT_SC_GFAB_DLV_H_ID)
        {
            try
            {
                var obList = new KNT_SC_GFAB_DLV_HModel().GetScGreyFabDelvHdr(pKNT_SC_GFAB_DLV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntGreyFabDelv/GetScFabDelvDtlByDelvHid")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/GetScFabDelvDtlByDelvHid
        public IHttpActionResult GetScFabDelvDtlByDelvHid(int pageNumber, int pageSize, Int64 pKNT_SC_GFAB_DLV_H_ID)
        {
            try
            {
                var obList = new KNT_SC_GFAB_DLV_DModel().GetScFabDelvDtlByDelvHid(pageNumber, pageSize, pKNT_SC_GFAB_DLV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntGreyFabDelv/GetSciYarnRtnList")]
        [HttpGet]
        // GET :  /api/knit/KntGreyFabDelv/GetSciYarnRtnList
        public IHttpActionResult GetSciYarnRtnList(Int64 pKNT_SC_GFAB_DLV_H_ID)
        {
            try
            {
                var obList = new KNT_SCI_YRN_RET_DModel().GetSciYarnRtnList(pKNT_SC_GFAB_DLV_H_ID);
                return Ok(obList);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntGreyFabDelv/BatchSave")]
        [HttpPost]
        // GET :  api/knit/KntGreyFabDelv/BatchSave
        public IHttpActionResult BatchSave([FromBody] KNT_SC_GFAB_DLV_HModel ob)
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
            return Ok(new { success = true, jsonStr });
        }


        [Route("KntGreyFabDelv/RemoveFabDelv")]
        [HttpPost]
        // GET :  api/knit/KntGreyFabDelv/RemoveFabDelv
        public IHttpActionResult RemoveFabDelv([FromBody] KNT_SC_GFAB_DLV_DModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RemoveFabDelv();
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

        [Route("KntGreyFabDelv/RemoveYarnReturn")]
        [HttpPost]
        // GET :  api/knit/KntGreyFabDelv/RemoveYarnReturn
        public IHttpActionResult RemoveYarnReturn([FromBody] KNT_SCI_YRN_RET_DModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.RemoveYarnReturn();
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
