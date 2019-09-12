using ERP.Model;
using Newtonsoft.Json.Linq;
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
    public class StyleDBOMController : ApiController
    {
        [Route("StyleDBOM/TapeItemList/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  /api/mrc/StyleDBOM/TapeItemList/1
        public IHttpActionResult TapeItemList(Int64 pMC_STYLE_H_ID)
        {
            var obList = new MC_STYLE_D_BOMModel().TapeItemList(pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("StyleDBOM/BOMListByID/{MC_STYLE_H_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDBOM/BOMListByID/1
        public IHttpActionResult BOMListByID(Int64 MC_STYLE_H_ID)
        {

            try
            {
                var obList = new MC_STYLE_D_BOMModel().BOMListByID(MC_STYLE_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }


        
    }
}
