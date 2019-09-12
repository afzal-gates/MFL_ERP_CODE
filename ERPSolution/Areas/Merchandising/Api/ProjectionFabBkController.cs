using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    public class ProjectionFabBkController : ApiController
    {


        [Route("ProjectionFabBk/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/ProjectionFabBk/SelectAll
        public IHttpActionResult SelectAll(int? pageNo = null, int? pageSize = null, string pORDER_NO = null, string pSTYLE_NO = null, string pPROV_FAB_BK_DT = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null)
        {
            var data = new MC_PROV_FAB_BK_HModel().SelectAll(pageNo, pageSize, pORDER_NO, pSTYLE_NO, pPROV_FAB_BK_DT, pMC_BYR_ACC_ID, pMC_STYLE_H_ID);
            int total = 0;

            if (data.Count > 0)
                total = Convert.ToInt32(data.FirstOrDefault().TOTAL_REC.ToString());
            return Ok(new { total, data });
        }



        [Route("ProjectionFabBk/SelectByID")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/ProjectionFabBk/SelectByID?pMC_PROV_FAB_BK_H_ID=
        public IHttpActionResult SelectByID(Int64? pMC_PROV_FAB_BK_H_ID = null)
        {
            var ob = new MC_PROV_FAB_BK_HModel().Select(pMC_PROV_FAB_BK_H_ID);
            return Ok(ob);
        }

        [Route("ProjectionFabBk/Save")]
        [HttpPost]
        //[System.Web.Http.Authorize]
        public IHttpActionResult Save(MC_PROV_FAB_BK_HModel ob)
        {
            string jsonStr = "";

            //var errors = new Hashtable();
            //return Ok(new { success = false, errors });

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


        [Route("ProjectionFabBk/Revise")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult Revise(MC_PROV_FAB_BK_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Revise();
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



        [Route("ProjectionFabBk/Cancel")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult Cancel(MC_PROV_FAB_BK_HModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Cancel();
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


        [Route("ProjectionFabBk/getProjectionFabBookingByOrderID")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/ProjectionFabBk/getProjectionFabBookingByOrderID?pMC_ORDER_H_ID=&pMC_PROV_FAB_BK_H_ID=
        public IHttpActionResult getProjectionFabBookingByOrderID(Int64? pMC_ORDER_H_ID = null, Int64? pMC_PROV_FAB_BK_H_ID=null)
        {
            var ob = new MC_PROV_ORD_DETModel().SelectByID(pMC_ORDER_H_ID, pMC_PROV_FAB_BK_H_ID);
            return Ok(ob);
        }


        [Route("ProjectionFabBk/getCallOffOrderByOrderID")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  /api/mrc/ProjectionFabBk/getCallOffOrderByOrderID?pMC_ORDER_H_ID=
        public IHttpActionResult getCallOffOrderByOrderID(Int64? pMC_ORDER_H_ID = null)
        {
            var ob = new MC_PROV_ORD_DETModel().SelectAll(pMC_ORDER_H_ID);
            return Ok(ob);
        }

    }
}
