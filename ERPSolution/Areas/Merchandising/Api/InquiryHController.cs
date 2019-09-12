
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
    public class InquiryHController : ApiController
    {

        [Route("InquiryH/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/InquiryH/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_INQR_HModel().SelectAll();
            return Ok(obList);
        }

        [Route("InquiryH/InquiryDataForList")]
        [HttpGet]
        // GET :  api/mrc/InquiryH/InquiryDataForList
        public IHttpActionResult InquiryDataForList()
        {
            var obList = new MC_INQR_HModel().InquiryDataForList();
            return Ok(obList);
        }


        [Route("InquiryH/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/InquiryH/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_INQR_HModel().Select(ID);
            return Ok(ob);
        }

        [Route("InquiryH/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc//InquiryH/Save
        public IHttpActionResult Save([FromBody] MC_INQR_HModel ob)
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


        [Route("InquiryH/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/InquiryH/Update
        public IHttpActionResult Update([FromBody] MC_INQR_HModel ob)
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