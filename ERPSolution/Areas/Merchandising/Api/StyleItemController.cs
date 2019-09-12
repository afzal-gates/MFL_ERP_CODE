
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
    public class StyleItemController : ApiController
    {

        [Route("StyleItem/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_STYLE_ITEMModel().SelectAll();
            return Ok(obList);
        }

        [Route("StyleItem/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().Select(ID);
            return Ok(ob);
        }


        [Route("StyleItem/ItemByStyleID/{MC_STYLE_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/Select
        public IHttpActionResult ItemByStyleID(Int64 MC_STYLE_ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemByStyleID(MC_STYLE_ID);
            return Ok(ob);
        }


        [Route("StyleItem/CostHeadList/{MC_STYLE_ITEM_ID:int}/{pOption:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/CostHeadList/1
        public IHttpActionResult CostHeadList(Int64 MC_STYLE_ITEM_ID, Int64 pOption)
        {
            var ob = new MC_STYL_ITM_COSTModel().SelectAll(MC_STYLE_ITEM_ID, pOption);
            return Ok(ob);
        }


        [Route("StyleItem/saveCostHeadMappingData")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleItem/Save
        public IHttpActionResult saveCostHeadMappingData([FromBody] List<MC_STYL_ITM_COSTModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                jsonStr = ob.Save();
            }
            return Ok(new { success = true, jsonStr });

        }


        [Route("StyleItem/InvItemByParent/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/InvItemByParent/1
        public IHttpActionResult InvItemByParent(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().InvItemByParent(ID);
            return Ok(ob);
        }

        [Route("StyleItem/InvItemByLine")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/InvItemByLine?pPARENT_ID=1&pHR_PROD_LINE_ID=1;
        public IHttpActionResult InvItemByLine(Int64 pPARENT_ID, Int64 pHR_PROD_LINE_ID)
        {
            var ob = new MC_STYLE_ITEMModel().InvItemByLine(pPARENT_ID, pHR_PROD_LINE_ID);
            return Ok(ob);
        }



        [Route("StyleItem/ItemByStyleID2Level/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemByStyleID2Level/1
        public IHttpActionResult ItemByStyleID2Level(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemByStyleID2Level(ID);
            return Ok(ob);
        }


        [Route("StyleItem/ItemByStyleIdForSetCombo/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemByStyleIdForSetCombo/1
        public IHttpActionResult ItemByStyleIdForSetCombo(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemByStyleIdForSetCombo(ID);
            return Ok(ob);
        }








        [Route("StyleItem/ItemListForSetWithNoCombo/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemListForSetWithNoCombo/1
        public IHttpActionResult ItemListForSetWithNoCombo(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemListForSetWithNoCombo(ID);
            return Ok(ob);
        }


        [Route("StyleItem/ItemListForSetWithComboOpt/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemListForSetWithComboOpt/1
        public IHttpActionResult ItemListForSetWithComboOpt(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemListForSetWithComboOpt(ID);
            return Ok(ob);
        }


        [Route("StyleItem/ItemListForSetWithCombo/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemListForSetWithCombo/1
        public IHttpActionResult ItemListForSetWithCombo(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemListForSetWithCombo(ID);
            return Ok(ob);
        }


       

        [Route("StyleItem/ItemListForOtherWithNoCombo/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleItem/ItemListForOtherWithNoCombo/1
        public IHttpActionResult ItemListForOtherWithNoCombo(Int64 ID)
        {
            var ob = new MC_STYLE_ITEMModel().ItemListForOtherWithNoCombo(ID);
            return Ok(ob);
        }

        [Route("StyleItem/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleItem/Save
        public IHttpActionResult Save([FromBody] MC_STYLE_ITEMModel ob)
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



        [Route("StyleItem/saveStyleItemFabData")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleItem/saveStyleItemFabData
        public IHttpActionResult saveStyleItemFabData([FromBody] List<MC_STYLE_ITEMModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                try
                {
                    jsonStr = ob.saveStyleItemFabData();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return Ok(new { success = true, jsonStr });
        }


        [Route("StyleItem/saveStyleItemFabData2L")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleItem/saveStyleItemFabData2L
        public IHttpActionResult saveStyleItemFabData2L([FromBody] List<MC_STYLE_ITEMModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                try
                {
                    jsonStr = ob.saveStyleItemFabData2L();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return Ok(new { success = true, jsonStr });
        }



        [Route("StyleItem/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/StyleItem/Update
        public IHttpActionResult Update([FromBody] MC_STYLE_ITEMModel ob)
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