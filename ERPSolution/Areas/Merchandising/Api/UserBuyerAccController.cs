
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
    public class UserBuyerAccController : ApiController
    {

        [Route("UserBuyerAcc/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/UserBuyerAcc/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_BYR_ACCModel().SelectAll();
            return Ok(obList);
        }

        [Route("UserBuyerAcc/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/UserBuyerAcc/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BYR_ACCModel().Select(ID);
            return Ok(ob);
        }


        [Route("UserBuyerAcc/getBuyerAccDataByByerId/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/UserBuyerAcc/getBuyerAccDataByByerId/:MC_BUYER_ID
        public IHttpActionResult getBuyerAccDataByByerId(Int64 MC_BUYER_ID)
        {
            var ob = new MC_BYR_ACCModel().getBuyerAccDataByByerId(MC_BUYER_ID);
            return Ok(ob);
        }


        [Route("UserBuyerAcc/GetUsersByByrAc/{pMC_BYR_ACC_IDS}")]
        [HttpGet]
        // GET :  mrc/api/UserBuyerAcc/GetUsersByByrAc/1,2,3
        public IHttpActionResult GetUsersByByrAc(string pMC_BYR_ACC_IDS)
        {
            var ob = new MC_USR_BYRACCModel().GetUsersByByrAc(pMC_BYR_ACC_IDS);
            return Ok(ob);
        }

        [Route("UserBuyerAcc/GetUsersByByrAcLst")]
        [HttpGet]
        // GET :  mrc/api/UserBuyerAcc/GetUsersByByrAcLst/1
        public IHttpActionResult GetUsersByByrAcLst(Int64 pMC_SMP_REQ_H_ID)
        {
            var ob = new MC_USR_BYRACCModel().GetUsersByByrAcLst(pMC_SMP_REQ_H_ID);
            return Ok(ob);
        }

        

        [Route("UserBuyerAcc/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/UserBuyerAcc/Save
        public IHttpActionResult Save([FromBody] List<MC_USR_BYRACCModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                jsonStr=ob.Save();
            }

            return Ok(new { success = true, jsonStr });

        }


        [Route("UserBuyerAcc/submitBuyerAcc")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/UserBuyerAcc/submitBuyerAcc
        public IHttpActionResult Save([FromBody] List<MC_BYR_ACCModel> obList)
        {
            string jsonStr = "";

            foreach (var ob in obList)
            {
                jsonStr = ob.submitBuyerAcc();
            }
            return Ok(new { success = true, jsonStr });

        }


        [Route("UserBuyerAcc/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/UserBuyerAcc/Update
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