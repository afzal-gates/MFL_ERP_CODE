
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
    public class BuyerAccController : ApiController
    {

        [Route("BuyerAcc/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/BuyerAcc/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_BYR_ACCModel().SelectAll();
            return Ok(obList);
        }

        [Route("BuyerAcc/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/BuyerAcc/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BYR_ACCModel().Select(ID);
            return Ok(ob);
        }


        [Route("BuyerAcc/getBuyerAccDataByByerId/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/BuyerAcc/getBuyerAccDataByByerId/:MC_BUYER_ID
        public IHttpActionResult getBuyerAccDataByByerId(Int64 MC_BUYER_ID)
        {
            var ob = new MC_BYR_ACCModel().getBuyerAccDataByByerId(MC_BUYER_ID);
            return Ok(ob);
        }


        [Route("BuyerAcc/getBuyerAccListByUserId/{SC_USER_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/BuyerAcc/getBuyerAccDataByByerId/:MC_BUYER_ID
        public IHttpActionResult getBuyerAccListByUserId(Int64 SC_USER_ID)
        {
            var ob = new MC_BYR_ACCModel().getBuyerAccListByUserId(SC_USER_ID);
            return Ok(ob);
        }

        [Route("BuyerAcc/getBuyerAccListByUserId")]
        [HttpGet]
        // GET :  /api/mrc/BuyerAcc/getBuyerAccListByUserId
        public IHttpActionResult getBuyerAccListByUserId()
        {
            var ob = new MC_BYR_ACCModel().getBuyerAccListByUserId();
            return Ok(ob);
        }

        [Route("BuyerAcc/GetBuyerAccGrpList")]
        [HttpGet]
        // GET :  /api/mrc/BuyerAcc/GetBuyerAccGrpList
        public IHttpActionResult GetBuyerAccGrpList()
        {
            var ob = new MC_BYR_ACC_GRPModel().GetBuyerAccGrpList();
            return Ok(ob);
        }

        [Route("BuyerAcc/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/BuyerAcc/Save
        public IHttpActionResult Save([FromBody] MC_BYR_ACCModel ob)
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


        [Route("BuyerAcc/submitBuyerAcc")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/BuyerAcc/submitBuyerAcc
        public IHttpActionResult Save([FromBody] List<MC_BYR_ACCModel> obList)
        {
            string jsonStr = "";

            foreach (var ob in obList)
            {
                jsonStr = ob.submitBuyerAcc();
            }
            return Ok(new { success = true, jsonStr });

        }


        [Route("BuyerAcc/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/BuyerAcc/Update
        public IHttpActionResult Update([FromBody] MC_BYR_ACCModel ob)
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