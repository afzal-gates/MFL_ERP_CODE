using ERP.Model;
using ERPSolution.Hubs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Dyeing.Api
{

    [RoutePrefix("api/dye")]
    [NoCache]
    public class LabdipRecipeController : ApiController
    {
        [Route("LabdipRecipe/GetLabdipRecipeList")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetLabdipRecipeList
        public IHttpActionResult GetLabdipRecipeList(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_ID = null, string pLD_RECIPE_NO = null)
        {
            var obList = new MC_LD_RECIPE_HModel().SelectAll(pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_ID, pLD_RECIPE_NO);
            return Ok(obList);
        }

        [Route("LabdipRecipe/SelectLDRecipe/{pMC_LD_RECIPE_H_ID:int}/{pRECHECK_ID:int}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/SelectLDRecipe
        public IHttpActionResult SelectLDRecipe(Int64? pMC_LD_RECIPE_H_ID = null, Int64? pRECHECK_ID = null)
        {
            if (pRECHECK_ID == 2)
            {
                var obList = new MC_LD_RECIPE_HModel().SelectForREQ(pMC_LD_RECIPE_H_ID);
                return Ok(obList);
            }
            else
            {
                var obList = new MC_LD_RECIPE_HModel().Select(pMC_LD_RECIPE_H_ID);
                return Ok(obList);
            }
        }

        [Route("LabdipRecipe/SelectLD")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/SelectLD
        public IHttpActionResult SelectLD(Int64? pMC_LD_REQ_D_ID = null)
        {

            var obList = new MC_LD_RECIPE_HModel().SelectLD(pMC_LD_REQ_D_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetRecipeItemList")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetRecipeItemList
        public IHttpActionResult GetRecipeItemList(Int64? pMC_LD_RECIPE_H_ID = null)
        {
            var obList = new MC_LD_RECIPE_DModel().Select(pMC_LD_RECIPE_H_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetItemListByCatID")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetItemListByCatID
        public IHttpActionResult GetItemListByCatID(Int64? pINV_ITEM_CAT_ID = null)
        {
            var obList = new INV_ITEMModel().ItemDataListForLD(Convert.ToInt32(pINV_ITEM_CAT_ID.ToString()));
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetTrimsForLDByStyleID")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetTrimsForLDByStyleID?pMC_STYLE_H_ID=
        public IHttpActionResult GetTrimsForLDByStyleID(Int64? pMC_STYLE_H_ID = null)
        {
            var obList = new MC_ORD_TRMS_ITEMModel().GetTrimsForLDByStyleID(pMC_STYLE_H_ID);
            return Ok(obList);
        }



        [Route("LabdipRecipe/ItemDataListByCatList")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/ItemDataListByCatList?pINV_ITEM_CAT_LST
        public IHttpActionResult ItemDataListByCatList(string pINV_ITEM_CAT_LST)
        {
            var obList = new INV_ITEMModel().ItemDataListByCatList(pINV_ITEM_CAT_LST);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetPantonNoByStlClr/{MC_STYLE_H_ID}/{MC_COLOR_ID}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetPantonNoByStlClr
        public IHttpActionResult GetPantonNoByStlClr(Int64 MC_STYLE_H_ID, Int64 MC_COLOR_ID)
        {
            var jsonStr = new INV_ITEMModel().GetPantonNoByStlClr(MC_STYLE_H_ID, MC_COLOR_ID);
            return Ok(new { success = true, jsonStr });
        }

        [Route("LabdipRecipe/GetOrderNoByStl/{MC_STYLE_H_ID}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetOrderNoByStl
        public IHttpActionResult GetOrderNoByStl(Int64 MC_STYLE_H_ID)
        {
            var jsonStr = new MC_LD_RECIPE_HModel().GetOrderNoByStl(MC_STYLE_H_ID);
            return Ok(new { success = true, jsonStr });
        }


        [Route("LabdipRecipe/GetLDColorGroup")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetLDColorGroup
        public IHttpActionResult GetLDColorGroup(Decimal? pPERCENT_QTY = null)
        {
            var obList = new DYE_CFG_PCT_SHADEModel().SelectByValue(pPERCENT_QTY);
            return Ok(obList);
        }

        [Route("LabdipRecipe/Save")]
        [HttpPost]
        // GET :  /api/dye/LabdipRecipe/Save
        public IHttpActionResult Save([FromBody] MC_LD_RECIPE_HModel ob)
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




        [Route("LabdipRecipe/DyeProcessType")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/DyeProcessType
        public IHttpActionResult DyeProcessType()
        {
            var obList = new DYE_PROC_OPR_TYPEModel().SelectAll();
            return Ok(obList);
        }


        [Route("LabdipRecipe/DyePhaseType")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/DyePhaseType
        public IHttpActionResult DyePhaseType()
        {
            var obList = new DYE_PRD_PHASE_TYPEModel().SelectAll();
            return Ok(obList);
        }


        [Route("LabdipRecipe/GetLDDosingTemplete")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetLDDosingTemplete
        public IHttpActionResult GetLDDosingTemplete()
        {
            var obList = new DYE_DOSE_TMPLT_HModel().SelectAll();
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetLDDosingTempleteByColorGrpID")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetLDDosingTempleteByColorGrpID
        public IHttpActionResult GetLDDosingTempleteByColorGrpID(Int64? pMC_COLOR_GRP_ID, Int64? LK_DYE_MTHD_ID)
        {
            var obList = new DYE_DOSE_TMPLT_HModel().SelectByColorGrpID(pMC_COLOR_GRP_ID, LK_DYE_MTHD_ID);
            return Ok(obList);
        }


        [Route("LabdipRecipe/GetLDDosingTempleteDtl")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetLDDosingTempleteDtl
        public IHttpActionResult GetLDDosingTempleteDtl(Int64? pDYE_DOSE_TMPLT_H_ID = null, Int64? pMC_LD_RECIPE_H_ID = null, Int64? pOption = null, Int64? pSCM_STORE_ID = null)
        {
            var list = new DYE_DOSE_TMPLT_DModel().Select(pDYE_DOSE_TMPLT_H_ID, pMC_LD_RECIPE_H_ID, pOption);
            var obList = new INV_ITEMModel().ItemDataListByStoreNCatList("3,4",pSCM_STORE_ID);
            foreach (var x in list)
            {
                var itemLst = (from itm in obList.Where(itm => itm.INV_ITEM_CAT_ID == x.DC_AGENT_ID) select new { itm }).ToList();

                var item = itemLst.ToList().Select(
                 md => new INV_ITEMModel
                 {
                     ITEM_NAME_EN = md.itm.ITEM_NAME_EN,
                     INV_ITEM_ID = md.itm.INV_ITEM_ID,
                     ITEM_CAT_NAME_EN = md.itm.ITEM_CAT_NAME_EN,
                     MOU_CODE = md.itm.MOU_CODE,
                 });

                x.ItemList = item.ToList();
            }
            return Ok(list);
        }

        [Route("LabdipRecipe/DosSave")]
        [HttpPost]
        // GET :  /api/dye/LabdipRecipe/DosSave
        public IHttpActionResult DosSave([FromBody] DYE_DOSE_TMPLT_HModel ob)
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


        [Route("LabdipRecipe/SelectByLabNo/{LD_RECIPE_NO}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/SelectByLabNo
        public IHttpActionResult SelectByLabNo(string LD_RECIPE_NO)
        {
            var obList = new MC_LD_RECIPE_HModel().SelectByLabNo(LD_RECIPE_NO);
            return Ok(obList);
        }


        [Route("LabdipRecipe/CheckDuplicateBuyerLabNo/{pMC_BUYER_ID}/{pLD_RECIPE_NO}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/CheckDuplicateBuyerLabNo
        public IHttpActionResult CheckDuplicateBuyerLabNo(Int64 pMC_BUYER_ID, string pLD_RECIPE_NO)
        {
            var obList = new MC_LD_RECIPE_HModel().CheckDuplicateBuyerLabNo(pMC_BUYER_ID, pLD_RECIPE_NO);
            return Ok(obList);
        }



        [Route("LabdipRecipe/checkDuplicateRecipe/{pMC_STYLE_H_ID}/{pMC_COLOR_ID}/{pMC_STYLE_D_FAB_ID}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/checkDuplicateRecipe
        public IHttpActionResult checkDuplicateRecipe(Int64 pMC_STYLE_H_ID, Int64 pMC_COLOR_ID, Int64 pMC_STYLE_D_FAB_ID)
        {
            var obList = new MC_LD_RECIPE_HModel().checkDuplicateRecipe(pMC_STYLE_H_ID, pMC_COLOR_ID, pMC_STYLE_D_FAB_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/SelectByOrderStyleColor/{ORDER_NO}/{pMC_STYLE_H_ID}/{pMC_COLOR_ID}/{pMC_BYR_ACC_GRP_ID}")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/SelectByLabNo
        public IHttpActionResult SelectByOrderStyleColor(string ORDER_NO, Int64 pMC_STYLE_H_ID, Int64 pMC_COLOR_ID, Int64 pMC_BYR_ACC_GRP_ID)
        {
            if (ORDER_NO.ToUpper() == "NULL")
                ORDER_NO = null;
            var obList = new MC_LD_RECIPE_HModel().SelectByOrderStyleColor(ORDER_NO, pMC_STYLE_H_ID, pMC_COLOR_ID, pMC_BYR_ACC_GRP_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetPreviousRecipeItemList")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetPreviousRecipeItemList
        public IHttpActionResult GetPreviousRecipeItemList(Int64? pMC_LD_RECIPE_H_ID = null)
        {
            var obList = new MC_LD_RECIPE_DModel().GetPreviousRecipeItemList(pMC_LD_RECIPE_H_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetDyeMethodColorGroup")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetDyeMethodColorGroup
        public IHttpActionResult GetDyeMethodColorGroup(Int64? pMC_COLOR_GRP_ID = null, Int64? pLK_DYE_MTHD_ID = null)
        {
            var obList = new MC_MAP_CG_DMModel().Select(pMC_COLOR_GRP_ID, pLK_DYE_MTHD_ID);
            return Ok(obList);
        }

        [Route("LabdipRecipe/GetProdWithLotList")]
        [HttpGet]
        // GET :  api/dye/LabdipRecipe/GetProdWithLotList
        public IHttpActionResult GetProdWithLotList(Int32? pMC_COLOR_ID = null, Int32? pMC_STYLE_H_ID = null, Int32? pMC_STYLE_D_FAB_ID = null, Int32? pRF_FAB_TYPE_ID = null, Int32? pRF_FIB_COMP_ID = null)
        {
            var obList = new KNT_STYL_FAB_ITEMModel().GetProdWithLotList(pMC_COLOR_ID, pMC_STYLE_H_ID, pMC_STYLE_D_FAB_ID, pRF_FAB_TYPE_ID, pRF_FIB_COMP_ID);
            return Ok(obList);
        }


    }
}