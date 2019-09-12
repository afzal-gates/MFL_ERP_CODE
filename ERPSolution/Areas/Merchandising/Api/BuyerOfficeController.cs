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
    public class BuyerOfficeController : ApiController
    {

        [Route("BuyerOffice/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/buyer/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_BUYER_OFFModel().SelectAll();
            return Ok(obList);
        }

        [Route("BuyerOffice/GetTimeZoneList")]
        [HttpGet]
        // GET :  mrc/api/buyer/SelectAll
        public IHttpActionResult GetTimeZoneList()
        {
            var obList = new MC_BUYER_OFFModel().GetTimeZoneList();
            return Ok(obList);
        }


        [Route("BuyerOffice/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/buyer/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BUYER_OFFModel().Select(ID);
            return Ok(ob);
        }

        [Route("BuyerOffice/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/buyer/Save
        public IHttpActionResult Save([FromBody] MC_BUYER_OFFModel ob)
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

        [Route("BuyerOffice/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/buyer/Update
        public IHttpActionResult Update([FromBody] MC_BUYER_OFFModel ob)
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

        [Route("BuyerOffice/OfficeDatasByUserID")]
        [HttpGet]
        // GET :  mrc/api/buyer/OfficeDatasByUserID
        public IHttpActionResult OfficeDatasByUserID()
        {
            var ob = new MC_BUYER_OFFModel().OfficeDatasByUserID();
            return Ok(ob);
        }

    }
}
