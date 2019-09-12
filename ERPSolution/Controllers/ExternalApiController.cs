using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace ERPSolution.Controllers
{

    [RoutePrefix("api/ext")]
    public class ExternalApiController : ApiController
    {
        [Route("GetOrderByBookingDate")]
        [HttpGet]
        [ExternalReqAuthorize]
        // GET :  /api/ext/GetOrderByBookingDate?pBOOKING_DT=date
        public IHttpActionResult GetOrderByBookingDate(DateTime? pBOOKING_DT)
        {
            var obList = new BlkSmpModel().GetOrderByBookingDate(pBOOKING_DT);
            return Ok(obList);
        }




    }
}
