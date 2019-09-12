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
    public class StyleDItemController : ApiController
    {
        [Route("StyleDItem/ChildItemListByStyle/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/ChildItemListByStyle/1
        public IHttpActionResult ChildItemListByStyle(Int64 pMC_STYLE_H_ID)
        {
            var obList = new MC_STYLE_D_ITEMModel().ChildItemListByStyle(pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("StyleDItem/StyleDtlParentItemList/{pMC_STYLE_H_ID:int}/{pIS_GET_CHILD}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/StyleDtlParentItemList
        public IHttpActionResult StyleDtlParentItemList(Int64? pMC_STYLE_H_ID, string pIS_GET_CHILD)
        {
            var obList = new MC_STYLE_D_ITEMModel().StyleDtlParentItemList(pMC_STYLE_H_ID, pIS_GET_CHILD);
            return Ok(obList);
        }

        [Route("StyleDItem/StyleDtlItemList/{pMC_STYLE_H_ID}/{pIS_GET_CHILD}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/StyleDtlItemList/1
        public IHttpActionResult StyleDtlItemList(Int64? pMC_STYLE_H_ID, string pIS_GET_CHILD)
        {
            var obList = new MC_STYLE_D_ITEMModel().StyleDtlItemList(pMC_STYLE_H_ID, pIS_GET_CHILD);
            return Ok(obList);
        }

        [Route("StyleDItem/StyleWiseItemList/{pMC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/StyleWiseItemList
        public IHttpActionResult StyleWiseItemList(Int64 pMC_STYLE_H_ID)
        {
            var obList = new MC_STYLE_D_ITEMModel().StyleWiseItemList(pMC_STYLE_H_ID);
            return Ok(obList);
        }

        [Route("StyleDItem/OrderWiseItemListForPln")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/OrderWiseItemListForPln?pMC_ORDER_SHIP_ID
        public IHttpActionResult OrderWiseItemListForPln(Int64 pMC_ORDER_SHIP_ID, int? pOption = null,Int64? pMC_ORDER_STYL_ID = null)
        {
            var obList = new MC_STYLE_D_ITEMModel().OrderWiseItemListForPln(pMC_ORDER_SHIP_ID, pOption, pMC_ORDER_STYL_ID);
            return Ok(obList);
        }

        [Route("StyleDItem/GmtItemListForPln")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/GmtItemListForPln?pMC_STYLE_H_ID&pMC_ORDER_ITEM_PLN_LST
        public IHttpActionResult GmtItemListForPln(Int64 pMC_STYLE_H_ID, string pMC_ORDER_ITEM_PLN_LST)
        {
            var obList = new MC_STYLE_D_ITEMModel().GmtItemListForPln(pMC_STYLE_H_ID, pMC_ORDER_ITEM_PLN_LST);
            return Ok(obList);
        }

        [Route("StyleDItem/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_STYLE_D_ITEMModel().SelectAll();
            return Ok(obList);
        }

        [Route("StyleDItem/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_STYLE_D_ITEMModel().Select(ID);
            return Ok(ob);
        }


        [Route("StyleDItem/ParentItemList/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItem/ParentItemList/1
        public IHttpActionResult ParentItemList(Int64 ID)
        {
            var ob = new MC_STYLE_D_ITEMModel().ParentItemList(ID);
            return Ok(ob);
        }



        [Route("StyleDItem/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc//StyleDItem/Save
        public IHttpActionResult Save([FromBody] MC_STYLE_D_ITEMModel ob)
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


        [Route("StyleDItem/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/StyleDItem/Update
        public IHttpActionResult Update([FromBody] MC_STYLE_D_ITEMModel ob)
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