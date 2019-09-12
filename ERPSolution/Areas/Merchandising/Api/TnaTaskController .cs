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
    public class TnaTaskController : ApiController
    {

        [Route("TnaTask/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/TnaTask/SelectAll
        public IHttpActionResult SelectAll(string IS_ORDER_EXEC = null)
        {
            var obList = new MC_TNA_TASKModel().SelectAll(IS_ORDER_EXEC);
            return Ok(obList);
        }

        [Route("TnaTask/List/{MC_TNA_TASK_GRP_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TnaTask/List/1
        public IHttpActionResult List(Int64 MC_TNA_TASK_GRP_ID)
        {
            var obList = new MC_TNA_TASKModel().ListData(MC_TNA_TASK_GRP_ID);
            return Ok(obList);
        }


        [Route("TnaTask/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TnaTask/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_TNA_TASKModel().Select(ID);
            return Ok(ob);
        }

        [Route("TnaTask/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/TnaTask/Save
        public IHttpActionResult Save([FromBody] MC_TNA_TASKModel ob)
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


        [Route("TnaTask/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/TnaTask/Update
        public IHttpActionResult Update([FromBody] MC_TNA_TASKModel ob)
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

        [Route("TnaTask/getHrDeptsForTna")]
        [HttpGet]
        // GET :  api/mrc/TnaTask/getHrDeptsForTna
        public IHttpActionResult getHrDeptsForTna()
        {
            var ob = new HrDepartmentModel().getHrDeptsForTna();
            return Ok(ob);
        }

    }
}