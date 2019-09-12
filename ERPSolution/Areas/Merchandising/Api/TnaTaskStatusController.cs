using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class TnaTaskStatusController : ApiController
    {

        [Route("TnaTaskStatus/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskStatus/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_TNA_TASK_STATUSModel().SelectAll();
            return Ok(obList);
        }


        [Route("TnaTaskStatus/SelectApprovRejectStatus")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskStatus/SelectApprovRejectStatus
        public IHttpActionResult SelectApprovRejectStatus(int? pMC_TNA_TASK_ID = null, int? pPARENT_ID = null, string pIS_FB_FRM_BUYER = null, int? pHR_DEPARTMENT_ID = null)
        {
            var obList = new MC_TNA_TASK_STATUSModel().SelectApprovRejectStatus(pMC_TNA_TASK_ID, pPARENT_ID, pIS_FB_FRM_BUYER, pHR_DEPARTMENT_ID);
            return Ok(obList);
        }


        [Route("TnaTaskStatus/SelectByTnaID")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskStatus/SelectByTnaID
        public IHttpActionResult SelectByTnaID(Int64? pMC_TNA_TASK_ID = null)
        {
            var ob = new MC_TNA_TASK_STATUSModel().SelectByTnaID(pMC_TNA_TASK_ID);
            return Ok(ob);
        }

        [Route("TnaTaskStatus/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskStatus/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_TNA_TASK_STATUSModel().Select(ID);
            return Ok(ob);
        }

        [Route("TnaTaskStatus/GetDepartmentList")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskStatus/GetDepartmentList
        public IHttpActionResult GetDepartmentList()
        {
            var ob = new HrDepartmentModel().SelectAll();
            return Ok(ob);
        }

        [Route("TnaTaskStatus/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/TnaTaskStatus/Save
        public IHttpActionResult Save([FromBody] MC_TNA_TASK_STATUSModel ob)
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


        [Route("TnaTaskStatus/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/TnaTaskStatus/Update
        public IHttpActionResult Update([FromBody] MC_TNA_TASK_STATUSModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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