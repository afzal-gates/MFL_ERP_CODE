using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;


namespace ERPSolution.Areas.Security.Controllers
{
    public class MenuController : BaseController
    {
        //
        // GET: /Security/Menu/
        public ActionResult Index()
        {
            //List<ScMenuModel> obList = new List<ScMenuModel>();
            //obList = obBLL.ScMenuData();

            //ViewData["sideMenuList"] = obList;

            return View();
        }

        public ActionResult MenuCreation()
        {
            return View();
        }

        public JsonResult MenuData()
        {

            var obList = new ScMenuModel().ScMenuData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult Save(ScMenuModel ob)
        {
            string vMsg = "";
            var errors = new Hashtable();

            if (ob.TranMode == 1)
            {
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
            else
            {
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


            //    Session["vMsg"] = obBLL.Save(ob);
            //else
            //    Session["vMsg"] = obBLL.Update(ob);

            //return RedirectToAction("MenuCreation");
        }



        public JsonResult ScSubModuleData()
        {
           var obList = new ScSystemModel().ScSubModuleData();
           return Json(obList, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult ScSecurityLevelData()
        {
            var obList = new ScSecurityLevelModel().ScSecurityLevelData();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        
	}
}