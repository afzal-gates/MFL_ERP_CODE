using ERP.Model;
using ERP.Model.Purchase;
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
namespace ERPSolution.Areas.Purchase.Api
{
    [RoutePrefix("api/purchase")]
    [Authorize]
    public class SupplierProfileController : ApiController
    {

        [Route("SupplierProfile/GetTypeWiseSuplier")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetTypeWiseSuplier
        public IHttpActionResult GetTypeWiseSuplier(int? pLK_SUP_TYPE_ID)
        {
            try
            {
                var obList = new SCM_SUPPLIERModel().GetTypeWiseSuplier(pLK_SUP_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("SupplierProfile/SelectAll")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/SelectAll
        public IHttpActionResult SelectAll()
        {
            try
            {
                var obList = new SCM_SUPPLIERModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("SupplierProfile/SelectAllByCat/{INV_ITEM_CAT_LST}")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/SelectAllByCat
        public IHttpActionResult SelectAllByCat(string INV_ITEM_CAT_LST)
        {
            try
            {
                var obList = new SCM_SUPPLIERModel().SelectAllByCat(INV_ITEM_CAT_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }


        [Route("SupplierProfile/GetSupplierInfo")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetSupplierInfo?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierInfo(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUPPLIERModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }
        [Route("SupplierProfile/GetSupplierAddressList")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetSupplierAddressList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierAddressList(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_ADDRESSModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }

        }

        [Route("SupplierProfile/GetSupplierContactList")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetSupplierContactList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierContactList(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_CPModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SupplierProfile/GetSupplierBrandList")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetSupplierBrandList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierBrandList(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_BRANDModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("SupplierProfile/SupplierListByItemCategory")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/SupplierListByItemCategory?pINV_ITEM_CAT_ID=1
        public IHttpActionResult SupplierListByItemCategory(Int64? pINV_ITEM_CAT_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_BRANDModel().SupplierListByItemCategory(pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("SupplierProfile/BrandListByItemCategory")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/BrandListByItemCategory?pINV_ITEM_CAT_ID=1
        public IHttpActionResult BrandListByItemCategory(Int64? pINV_ITEM_CAT_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_BRANDModel().BrandListByItemCategory(pINV_ITEM_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("SupplierProfile/GetCertificateList")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetCertificateList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetCertificateList(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_CERTModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SupplierProfile/GetSupplierBankList")]
        [HttpGet]
        // GET :  /api/purchase/SupplierProfile/GetSupplierBankList?pSCM_SUPPLIER_ID
        public IHttpActionResult GetSupplierBankList(Int64? pSCM_SUPPLIER_ID = null)
        {
            try
            {
                var obList = new SCM_SUP_BK_ACCModel().Select(pSCM_SUPPLIER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("SupplierProfile/SaveSupplierData")]
        [HttpPost]
        // GET :  purchase/api/SupplierProfile/SaveSupplierData
        public IHttpActionResult SaveSupplierData([FromBody] SCM_SUPPLIERModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveHrdData();
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



        [Route("SupplierProfile/SaveSupplierBankData")]
        [HttpPost]
        // GET :  purchase/api/SupplierProfile/SaveSupplierBankData
        public IHttpActionResult SaveSupplierBankData([FromBody] SCM_SUP_BK_ACCModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SaveBySupplier();
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