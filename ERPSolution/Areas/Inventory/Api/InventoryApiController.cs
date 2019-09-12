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
//using System.Web.Mvc;


namespace ERPSolution.Areas.Inventory.Api
{
    
    [RoutePrefix("api/inv")]
    public class InventoryApiController : ApiController
    {        
        //=================== Start Item Category =========================
        [Route("ItemCategory/SelectAll")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/SelectAll
        public IHttpActionResult SelectAll()
        {
            //var vUsr = HttpContext.Current.Session["multiScUserId"].ToString();
            var obList = new INV_ITEM_CATModel().SelectAll();
            return Ok(obList);
        }

        [Route("ItemCategory/ItemCategTreeList")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/ItemCategTreeList
        public IHttpActionResult ItemCategTreeList(Int32? pSC_USER_ID=null)
        {
            //var vUsr = HttpContext.Current.Session["multiScUserId"].ToString();
            var obList = new INV_ITEM_CATModel().ItemCategTreeList(pSC_USER_ID);            
            return Ok(obList);
        }

        [Route("ItemCategory/ItemCategList")]
        [HttpGet]
        public IHttpActionResult ItemCategList()
        {
            var obList = new INV_ITEM_CATModel().ItemCategList();
            return Ok(obList);
        }


        [Route("ItemCategory/ItemCategList4LastLevel")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/ItemCategList4LastLevel
        public IHttpActionResult ItemCategList4LastLevel(int? pPARENT_ID = null)
        {
            var obList = new INV_ITEM_CATModel().ItemCategList4LastLevel(pPARENT_ID);
            return Ok(obList);
        }


        [Route("ItemCategory/LoginUserWiseItemCatList")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/LoginUserWiseItemCatTreeList
        public IHttpActionResult LoginUserWiseItemCatList()
        {
            //var vUsr = HttpContext.Current.Session["multiScUserId"].ToString();
            var obList = new INV_USER_ITMCATModel().LoginUserWiseItemCatList();
            return Ok(obList);
        }

        [Route("ItemCategory/LoginUserWiseItemCatTreeList")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/LoginUserWiseItemCatTreeList
        public IHttpActionResult LoginUserWiseItemCatTreeList()
        {
            //var vUsr = HttpContext.Current.Session["multiScUserId"].ToString();
            var obList = new INV_USER_ITMCATModel().LoginUserWiseItemCatTreeList();            
            return Ok(obList);
        }
        

        [Route("ItemCategory/Code/{ITEM_CAT_CODE}")]
        [HttpGet]
        public IHttpActionResult ItemCategList(String ITEM_CAT_CODE)
        {
            var obList = new INV_ITEM_CATModel().ItemCategListForDD(ITEM_CAT_CODE);
            return Ok(obList);
        }

        [Route("ItemCategory/getCategWiseBrandList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/inv/ItemCategory/getCategWiseBrandList?pINV_ITEM_CAT_ID=1
        public IHttpActionResult getCategWiseBrandList(int? pINV_ITEM_CAT_ID = null)
        {
            var obList = new INV_PROD_CAT_BRANDModel().getCategWiseBrandList(pINV_ITEM_CAT_ID);
            return Ok(obList);
        }


        [Route("ItemCategory/ItemCategorySave")]
        [HttpPost]
        [Authorize]
        // GET :  inventory/api/ItemCategory/ItemCategorySave
        public IHttpActionResult ItemCategorySave(INV_ITEM_CATModel ob)
        {
            if(ob.ITEM_CAT_LEVEL==1 && ob.ITEM_CAT_PREFIX==null)
            {
                ModelState.AddModelError("ITEM_CAT_PREFIX", "Please insert code prefix.");
            }

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

        [Route("ItemCategory/ItemCategoryUpdate")]
        [HttpPost]
        [Authorize]
        // GET :  inventory/api/ItemCategory/ItemCategoryUpdate
        public IHttpActionResult ItemCategoryUpdate(INV_ITEM_CATModel ob)
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


        [Route("ItemCategory/BatchSaveItemCatPermission")]
        [HttpPost]
        [Authorize]
        // GET :  inventory/api/ItemCategory/ItemCategorySave
        public IHttpActionResult BatchSaveItemCatPermission(INV_ITEM_CATModel ob)
        {            
            string jsonStr = "";
            jsonStr = ob.BatchSaveItemCatPermission();
            //if (ModelState.IsValid)
            //{
            //    try
            //    {

            //        jsonStr = ob.BatchSaveItemCatPermission();
            //    }
            //    catch (Exception e)
            //    {
            //        ModelState.AddModelError("", e.Message);
            //    }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Ok(new { success = false, errors });
            //}
            return Ok(new { success = true, jsonStr });
        }

        //=================== Start Item Category =========================


        //=================== Start Item =========================
        [Route("Item/ItemList")]
        [HttpGet]
        // GET :  inventory/api/ItemCategory/ItemList
        public IHttpActionResult ItemList(int? pINV_ITEM_CAT_ID)
        {            
            var obList = new INV_ITEMModel().ItemList(pINV_ITEM_CAT_ID);
            return Ok(obList);
        }

        [Route("Item/ItemListWithUserID")]
        [HttpGet]
        // GET :  /api/inventory/Item/ItemListWithUserID
        public IHttpActionResult ItemListWithUserID()
        {
            var obList = new INV_ITEMModel().ItemListWithUserID();
            return Ok(obList);
        }   

        [Route("Item/ItemSave")]
        [HttpPost]
        // GET :  inventory/api/Item/ItemSave
        public IHttpActionResult ItemSave(INV_ITEMModel ob)
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


        [Route("Item/ItemUpdate")]
        [HttpPost]
        // GET :  inventory/api/Item/ItemUpdate
        public IHttpActionResult ItemUpdate(INV_ITEMModel ob)
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
        //=================== End Item =========================


        //=================== Start Item Sales =========================
        [Route("Item/EmpPOSVerify/{pHR_EMPLOYEE_ID:int}/{pMEMO_DT:DateTime}")]
        [HttpGet]
        // GET :  inventory/api/ItemCategory/EmpPOSVerify
        public IHttpActionResult EmpPOSVerify(Int64? pHR_EMPLOYEE_ID, DateTime pMEMO_DT)
        {
            var obList = new PS_EMP_VERIFYModel().EmpPOSVerify(pHR_EMPLOYEE_ID, pMEMO_DT);
            return Ok(obList);
        }

        [Route("Item/ItemAutoDataList/{pITEM_CODE}")]
        [HttpGet]
        // GET :  inventory/api/ItemCategory/ItemAutoDataList
        public IHttpActionResult ItemAutoDataList(string pITEM_CODE)
        {
            var obList = new INV_ITEMModel().ItemAutoDataList(pITEM_CODE);
            return Ok(obList);
        }


        [Route("ItemCategory/getSalesStore")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/getSalesStore
        public IHttpActionResult getSalesStore()
        {
            var obList = new INV_ITEM_CATModel().getSalesStoreData();
            return Ok(obList);
        }

        [Route("ItemCategory/getSalesPoint")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/getSalesPoint
        public IHttpActionResult getSalesPoint()
        {
            var obList = new INV_ITEM_CATModel().getSalesPointData();
            return Ok(obList);
        }



        [Route("Item/BatchSavePOSItem")]
        [HttpPost]
        // GET :  inventory/api/Item/ItemSave
        public IHttpActionResult BatchSavePOSItem(PS_SALE_HModel ob)
        {
            if (((ob.CRD_LMT_EARNED - ob.ALREADY_SOLD_AMT) < ob.SOLD_AMT) && ob.LK_CUST_TYPE_ID == 590)
                ModelState.AddModelError("", "Sorry! You cannot exit your credit balance");

            if (ob.SalesDtlItem == null)
                ModelState.AddModelError("", "Sorry! You have no item to sold");

            if(ob.SOLD_AMT==0)
                ModelState.AddModelError("", "Sold amount must be grater than zero(0)");

            
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.BatchSavePOSItem();
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
        //=================== End Item Sales =========================

        #region Item MoU Conversion Rate Entry

        [Route("SaveMoUConvRate")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveMoUConvRate([FromBody] RF_ITM_MOU_CONVModel ob)
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

        [Route("DeleteMoUConvRate")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult DeleteMoUConvRate([FromBody] RF_ITM_MOU_CONVModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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

        [Route("GetItemMoUConvRate")]
        [HttpGet]
        //[Authorize]
        // GET :  /api/inv/GetItemMoUConvRate
        public IHttpActionResult GetItemMoUConvRate()
        {
            var obList = new RF_ITM_MOU_CONVModel().SelectAll();
            return Ok(obList);
        }


        [Route("ItemCategory/ItemCategListByUserID")]
        [HttpGet]
        // GET :  /api/inv/ItemCategory/ItemCategListByUserID
        public IHttpActionResult ItemCategListByUserID()
        {
            var obList = new INV_ITEM_CATModel().ItemCategListByUserID();
            return Ok(obList);
        }

        #endregion 


        [Route("Item/StrInactiveItemByID")]
        [HttpGet]
        // GET :  api/inv/Item/StrInactiveItemByID?pSCM_STORE_ID=
        public IHttpActionResult StrInactiveItemByID(int? pSCM_STORE_ID)
        {
            var obList = new DYE_CFG_SUB_STR_ITM_IAModel().SelectByID(pSCM_STORE_ID);
            return Ok(obList);
        }

        [Route("Item/StrInactiveItemSave")]
        [HttpPost]
        // GET :  inventory/api/Item/StrInactiveItemSave
        public IHttpActionResult StrInactiveItemSave(DYE_CFG_SUB_STR_ITM_IAModel ob)
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


        [Route("Item/StrInactiveItemDelete")]
        [HttpPost]
        // GET :  inventory/api/Item/StrInactiveItemDelete
        public IHttpActionResult StrInactiveItemDelete(DYE_CFG_SUB_STR_ITM_IAModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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
