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
    public class StyleDItemFabController : ApiController
    {
        [Route("StyleDItemFab/FabDataByBuyerAccId/{pMC_BYR_ACC_ID}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/FabDataByBuyerAccId/1
        public IHttpActionResult FabDataByBuyerAccId(Int64? pMC_BYR_ACC_ID)
        {
            var obList = new MC_STYLE_D_FABModel().FabDataByBuyerAccId(pMC_BYR_ACC_ID);
            return Ok(obList);
        }

        [Route("StyleDItemFab/FabDataByBuyerId/{pMC_BUYER_ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/FabDataByBuyerId/1
        public IHttpActionResult FabDataByBuyerId(Int64 pMC_BUYER_ID)
        {
            var obList = new MC_STYLE_D_FABModel().FabDataByBuyerId(pMC_BUYER_ID);
            return Ok(obList);
        }

        [Route("StyleDItemFab/SelectAll")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_STYLE_D_FABModel().SelectAll();
            return Ok(obList);
        }

        [Route("StyleDItemFab/SelectFabByStyleID/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/SelectFabByStyleID
        public IHttpActionResult SelectFabByStyleID(Int64 ID, Int16? pLK_FBR_GRP_ID = null, string pIS_ACTIVE = null)
        {
            var ob = new MC_STYLE_D_FABModel().SelectFabByStyleID(ID, pLK_FBR_GRP_ID, pIS_ACTIVE);
            return Ok(ob);
        }

        //[Route("StyleDItemFab/SelectFabByBuyerID/{pMC_BUYER_ID:int}")]
        //[HttpGet]
        //// GET :  api/mrc/StyleDItemFab/SelectFabByBuyerID
        //public IHttpActionResult SelectFabByBuyerID(long pMC_BUYER_ID)
        //{
        //    var ob = new MC_STYLE_D_FABModel().SelectFabByBuyerID(pMC_BUYER_ID);
        //    return Ok(ob);
        //}
        

        [Route("StyleDItemFab/FabDataByItemId/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/FabDataByItemId/1
        public IHttpActionResult FabDataByItemId(Int64 ID)
        {
            var obList = new MC_STYLE_D_FABModel().FabDataByItemId(ID);
            return Ok(obList);
        }


        [Route("StyleDItemFab/Select/{ID:int}")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_STYLE_D_FABModel().Select(ID);
            return Ok(ob);
        }


        [Route("StyleDItemFab/getFabricationYarn")]
        [HttpGet]
        // GET :  api/mrc/StyleDItemFab/getFabricationYarn
        public IHttpActionResult getFabricationYarn(Int64? pMC_STYLE_H_ID = null)
        {
            try
            {
                var ob = new MC_STYLE_D_FAB_YRNModel().queryDataFabScreen(pMC_STYLE_H_ID);
                return Ok(ob);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError,e.Message);
            }

        }





        [Route("StyleDItemFab/Save")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc//StyleDItemFab/Save
        public IHttpActionResult Save([FromBody] MC_STYLE_D_FABModel ob)
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



        [Route("StyleDItemFab/SaveFiberComposition")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleDItemFab/SaveFiberComposition
        public IHttpActionResult Save([FromBody] RF_FIB_COMPModel ob)
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


        [Route("StyleDItemFab/Update")]
        [HttpPut]
        [Authorize]

        // GET :  api/mrc/StyleDItemFab/Update
        public IHttpActionResult Update([FromBody] MC_STYLE_D_FABModel ob)
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

        [Route("StyleDItemFab/UpdateFromKnitting")]
        [HttpPost]
        [Authorize]

        // GET :  api/mrc/StyleDItemFab/UpdateFromKnitting
        public IHttpActionResult UpdateFromKnitting([FromBody] MC_STYLE_D_FABModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.UpdateFromKnitting();
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


        [Route("StyleDItemFab/Delete/{pMC_STYLE_D_FAB_ID:int}")]
        [HttpDelete]
        [Authorize]

        // GET :  api/mrc/StyleDItemFab/Delete
        public IHttpActionResult Delete( Int64 pMC_STYLE_D_FAB_ID )
        {
            string jsonStr = "";
            try
            {
                jsonStr = new MC_STYLE_D_FABModel().Delete(pMC_STYLE_D_FAB_ID);
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });

            }

            return Ok(new { success = true, jsonStr });

        }

        [Route("StyleDItemFab/makeActiveToggle/{pMC_STYLE_D_FAB_ID:int}")]
        [HttpDelete]
        [Authorize]

        // GET :  api/mrc/StyleDItemFab/Delete
        public IHttpActionResult makeActiveToggle(Int64 pMC_STYLE_D_FAB_ID)
        {
            string jsonStr = "";
            try
            {
                jsonStr = new MC_STYLE_D_FABModel().makeActiveToggle(pMC_STYLE_D_FAB_ID);
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });

            }

            return Ok(new { success = true, jsonStr });

        }

    }
}