using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{

    [RoutePrefix("api/mrc")]
    public class ColourMasterController : ApiController
    {

        [Route("ColourMaster/GetAnyAvailColorList")]
        [HttpGet]
        // GET :  mrc/api/ColourMaster/GetAnyAvailColorList
        public IHttpActionResult GetAnyAvailColorList()
        {
            var obList = new MC_COLORModel().GetAnyAvailColorList();
            return Ok(obList);
        }

        [Route("ColourMaster/ColourBuyerListData")]
        [HttpGet]
        // GET :  mrc/api/ColourMaster/ColourBuyerListData
        public IHttpActionResult ColourBuyerListData()
        {
            var obList = new MC_COLORModel().ColourBuyerListData();
            return Ok(obList);
        }


        [Route("ColourMaster/ColourListByBuyerId/{MC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  mrc/api/ColourMaster/ColourListByBuyerId
        public IHttpActionResult ColourListByBuyerId(Int64 MC_BUYER_ID)
        {
            var obList = new MC_COLORModel().ColourListByBuyerId(MC_BUYER_ID);
            return Ok(obList);
        }


        [Route("ColourMaster/ColourMappDataByBuyerId/{MC_STYLE_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/ColourMaster/ColourMappDataByBuyerId/1?pLK_COL_TYPE_LIST=358,407
        public IHttpActionResult ColourMappDataByBuyerId(Int64 MC_STYLE_H_ID, string pLK_COL_TYPE_LIST = null, string pIS_DUMMY_COLOR = null)
        {
            try
            {
                var obList = new MC_COLORModel().ColourMappDataByBuyerId(MC_STYLE_H_ID, pLK_COL_TYPE_LIST, pIS_DUMMY_COLOR);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        [Route("ColourMaster/GetColorListAfterDyeByID")]
        [HttpGet]
        // GET :  api/mrc/ColourMaster/GetColorListAfterDyeByID?pMC_FAB_PROD_ORD_H_ID=1&pMC_BYR_ACC_GRP_ID=3&pMC_STYLE_H_EXT_ID=2
        public IHttpActionResult GetColorListAfterDyeByID(Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            var obList = new MC_COLORModel().GetColorListAfterDyeByID(pMC_FAB_PROD_ORD_H_ID, pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_EXT_ID);
            return Ok(obList);
        }



        [Route("ColourMaster/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/ColourMaster/SelectAll?pCOLOR_NAME_EN&pOption=3000
        public IHttpActionResult SelectAll(String pCOLOR_NAME_EN = null, int pOption = 3000)
        {
            var obList = new MC_COLORModel().SelectAll(pCOLOR_NAME_EN, pOption);
            return Ok(obList);
        }

        [Route("ColourMaster/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/ColourMaster/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_COLORModel().Select(ID);
            return Ok(ob);
        }

        [Route("ColourMaster/ColourGroupList")]
        [HttpGet]
        // GET :  api/mrc/ColourMaster/ColourGroupList
        public IHttpActionResult ColourGroupList()
        {
            var obList = new MC_COLOR_GRPModel().SelectAll();
            return Ok(obList);
        }

        [Route("ColourMaster/Save")]
        [HttpPost]
        [Authorize]

        // GET :  mrc/api/ColourMaster/Save
        public IHttpActionResult Save([FromBody] MC_COLORModel ob)
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


        [Route("ColourMaster/BuyerColourMapData")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult BuyerColourMapData([FromBody] List<MC_BU_COL_REFModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                jsonStr = ob.Save();
            }
            return Ok(new { success = true, jsonStr });
        }


        [Route("ColourMaster/Update")]
        [HttpPut]
        [Authorize]

        // GET :  mrc/api/ColourMaster/Update
        public IHttpActionResult Update([FromBody] MC_COLORModel ob)
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

        [Route("ColourMaster/UpdateWithXML")]
        [HttpPost]
        [Authorize]

        public IHttpActionResult UpdateWithXML([FromBody] MC_COLORModel ob)
        {
            string jsonStr = "";

            try
            {
                jsonStr = ob.UpdateWithXML();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Ok(new { success = true, jsonStr });

        }


        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}
