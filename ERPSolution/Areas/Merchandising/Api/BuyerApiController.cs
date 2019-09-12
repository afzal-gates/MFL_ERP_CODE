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
    public class BuyerApiController : ApiController
    {
        [Route("buyer/BuyerByUserList")]
        [HttpGet]
        // GET :  mrc/api/buyer/SelectAll
        public IHttpActionResult BuyerByUserList()
        {
            var obList = new MC_BUYERModel().BuyerByUserList();
            return Ok(obList);
        }
        [Route("buyer/getBuyerDropdownList")]
        [HttpGet]
        // GET :  mrc/api/buyer/getBuyerDropdownList
        public IHttpActionResult getBuyerDropdownList(Int64? MC_BYR_ACC_ID)
        {
            var obList = new MC_BUYERModel().getBuyerDropdownList(MC_BYR_ACC_ID);
            return Ok(obList);
        }

        [Route("buyer/getBuyerDropdownListByID/{MC_BYR_ACC_ID}")]
        [HttpGet]
        // GET :  mrc/api/buyer/getBuyerDropdownList
        public IHttpActionResult getBuyerDropdownListByID(Int64? MC_BYR_ACC_ID)
        {
            var obList = new MC_BUYERModel().getBuyerDropdownList(MC_BYR_ACC_ID);
            return Ok(obList);
        }

        
        [Route("buyer/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/buyer/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_BUYERModel().SelectAll();
            return Ok(obList);
        }

        [Route("buyer/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/buyer/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_BUYERModel().Select(ID);
            return Ok(ob);
        }

        [Route("buyer/GetCountryList")]
        [HttpGet]
        public IHttpActionResult GetCountryList()
        {
            var ob = new HR_COUNTRYModel().SelectAll();
            return Ok(ob);
        }

        [Route("buyer/MappedBuyerList")]
        [HttpGet]
        public IHttpActionResult MappedBuyerList(Int64 pOption = 3005)
        {
            var obList = new MC_BUYERModel().MappedBuyerList(pOption);
            return Ok(obList);
        }


        [Route("buyer/SampleListByBuyer/{MC_BUYER_ID}")]
        [HttpGet]
        public IHttpActionResult SampleListByBuyer(Int64  MC_BUYER_ID)
        {
            var obList = new RF_SMPL_TYPEModel().SampleListByBuyer(MC_BUYER_ID);
            return Ok(obList);
        }


        [Route("buyer/SampleListByBuyerID/{MC_BUYER_ID}")]
        [HttpGet]
        public IHttpActionResult SampleListByBuyerID(Int64 MC_BUYER_ID)
        {
            var obList = new RF_SMPL_TYPEModel().SampleListByBuyerID(MC_BUYER_ID);
            return Ok(obList);
        }


        [Route("buyer/BuyerDatasByColourId/{MC_COLOR_ID}")]
        [HttpGet]
        public IHttpActionResult BuyerDatasByColourId(Int64 MC_COLOR_ID)
        {
            var ob = new MC_BUYERModel().BuyerDatasByColourId(MC_COLOR_ID);
            return Ok(ob);
        }


        

        [Route("buyer/{MC_BUYER_ID:int}/Offices")]
        [HttpGet]
        public IHttpActionResult OfficeDatasByBuyerId(Int64 MC_BUYER_ID)
        {
            var ob = new MC_BUYER_OFFModel().OfficeDatasByBuyerId(MC_BUYER_ID);
            return Ok(ob);
        }

        [Route("buyer/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/buyer/Save
        public IHttpActionResult Save(MC_BUYERModel ob)
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


        [Route("buyer/SaveMapData")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/buyer/Save/SaveMapData
        public IHttpActionResult SaveMapData([FromBody] List<MC_BU_OFF_MAPModel> obList)
        {
            string jsonStr = "";
                try
                {
                    jsonStr = new MC_BU_OFF_MAPModel().Save(obList);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            return Ok(new { success = true, jsonStr });

        }


        [Route("buyer/SaveSampleMappedData")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/buyer/SaveSampleMappedData
        public IHttpActionResult SaveSampleMappedData([FromBody] MC_BUYERModel ob)
        {
            string jsonStr = "";
                try
                {
                    jsonStr = ob.SaveSampleMappedData();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            return Ok(new { success = true, jsonStr });

        }


        



        [Route("buyer/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/buyer/Update
        public IHttpActionResult Update([FromBody] MC_BUYERModel ob)
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
