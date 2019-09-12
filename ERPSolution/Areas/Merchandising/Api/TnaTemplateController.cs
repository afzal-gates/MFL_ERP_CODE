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
    public class TnaTemplateController : ApiController
    {

        [Route("TnaTemplate/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_TNA_TMPLT_HModel().SelectAll();
            return Ok(obList);
        }

        [Route("TnaTemplate/FindTaskListByUser/{SC_USER_ID}")]
        [HttpGet]
        // GET :  api/mrc/TnaTemplate/FindTaskListByUser/SC_USER_ID
        public IHttpActionResult FindTaskListByUser(Int64? SC_USER_ID)
        {
            var obList = new MC_USR_TNA_TASKModel().FindTaskListByUser(SC_USER_ID);
            return Ok(obList);
        }

        [Route("TnaTemplate/FindTnaMappedUserList")]
        [HttpGet]
        // GET :  api/mrc/TnaTemplate/FindTnaMappedUserList
        public IHttpActionResult FindTaskListByUser()
        {
            var obList = new MC_USR_TNA_TASKModel().FindTnaMappedUserList();
            return Ok(obList);
        }


        [Route("TnaTemplate/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/SizeMaster/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_TNA_TMPLT_HModel().Select(ID);
            return Ok(ob);
        }

        [Route("TnaTemplate/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Save
        public IHttpActionResult Save([FromBody] MC_TNA_TMPLT_HModel ob)
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

        [Route("TnaTemplate/SaveTnaMappedTask")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/TnaTemplate/SaveTnaMappedTask
        public IHttpActionResult Save([FromBody] MC_USR_TNA_TASKModel ob)
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


        [Route("TnaTemplate/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/SizeMaster/Update
        public IHttpActionResult Update([FromBody] MC_TNA_TMPLT_HModel ob)
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