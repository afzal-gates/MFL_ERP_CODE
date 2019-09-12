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
    public class TnaTaskGroupController : ApiController
    {




        [Route("TnaTaskGroup/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskGroup/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_TNA_TASK_GRPModel().SelectAll();
            return Ok(obList);
        }



        [Route("TnaTaskGroup/getTaskList/{MC_TNA_TMPLT_H_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskGroup/getTaskList
        public IHttpActionResult SelectAll(Int64 MC_TNA_TMPLT_H_ID)
        {
            var obList = new MC_TNA_TASK_GRPModel().SelectAll(MC_TNA_TMPLT_H_ID);
            return Ok(obList);
        }

        [Route("TnaTaskGroup/getDeSelectTnaTaskList")]
        [HttpGet]
        // GET :  mrc/api/TnaTaskGroup/getDeSelectTnaTaskList?pMC_STYLE_H_ID
        public IHttpActionResult getDeSelectTnaTaskList(Int64 pMC_STYLE_H_ID)
        {
            string DeSelectTnaTaskList = new MC_TNA_TASK_GRPModel().getDeSelectTnaTaskList(pMC_STYLE_H_ID);


            return Ok(DeSelectTnaTaskList);
        }

        [Route("TnaTaskGroup/parentTasksList/{MC_TNA_TMPLT_H_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/TaskListByGroup/1
        public IHttpActionResult parentTasksList(Int64 MC_TNA_TMPLT_H_ID)
        {
            var ob = new MC_TNA_TASK_GRPModel().parentTasksList(MC_TNA_TMPLT_H_ID);
            return Ok(ob);
        }



        [Route("TnaTaskGroup/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_TNA_TASK_GRPModel().Select(ID);
            return Ok(ob);
        }

        [Route("TnaTaskGroup/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/TnaTaskGroup/Save
        public IHttpActionResult Save([FromBody] MC_TNA_TASK_GRPModel ob)
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


        [Route("TnaTaskGroup/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/TnaTaskGroup/Update
        public IHttpActionResult Update([FromBody] MC_TNA_TASK_GRPModel ob)
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