using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class ActionConfigController : BaseController
    {
        public ActionResult Index()
        {
            var obList = new RF_ACTION_CFG_D1Model().SelectAll();
            return View(obList);
        }

        public JsonResult PeriodTypeData()
        {
            var objList = new HrPeriodTypeModel().PeriodTypeListData();
            return Json(objList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActionTypeListData()
        {
            var obList = new RF_ACTION_CFG_HModel().ActionTypeList();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ActionConfigData(Int64 RF_ACTION_CFG_H_ID)
        {
            var obList = new RF_ACTION_CFG_D1Model().ActionConfigData(RF_ACTION_CFG_H_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLvlData(Int64 RF_ACTION_CFG_H_ID)
        {
            var obList = new RF_ACTION_CFG_LVLModel().getLvlData(RF_ACTION_CFG_H_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getApproverLvlData(Int64 RF_ACTION_CFG_H_ID, Int64 RF_ACTION_CFG_D1_ID)
        {
            var obList = new RF_ACTION_CFG_LVLModel().getApproverLvlData(RF_ACTION_CFG_H_ID, RF_ACTION_CFG_D1_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(RF_ACTION_CFG_D1Model ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Update();

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
                return Json(new { success = false, errors });
            }
            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Save(RF_ACTION_CFG_D1Model ob)
        {
            string vMsg = "";
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Save();
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
                return Json(new { success = false, errors, vMsg });
            }

            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        public JsonResult submitLvlData(List<RF_ACTION_CFG_LVLModel> obList)
        {
            string vMsg = "";
            try
            {
                vMsg = new RF_ACTION_CFG_LVLModel().submitLvlData(obList);

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return Json(new { success = true, vMsg });
        }




        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult LvlUpdate(RF_ACTION_CFG_LVLModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.LvlUpdate();

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
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, vMsg });

        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult LvlSave(RF_ACTION_CFG_LVLModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.LvlSave();

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
                return Json(new { success = false, errors, vMsg });
            }

            return Json(new { success = true, vMsg });
        }



        public PartialViewResult ActionApprover()
        {
            return PartialView();
        }

        public PartialViewResult ActionType()
        {
            return PartialView();
        }

        public PartialViewResult ActionConfig()
        {
            return PartialView();
        }


        public PartialViewResult ActionApproverAdd()
        {
            return PartialView();
        }
    }
}