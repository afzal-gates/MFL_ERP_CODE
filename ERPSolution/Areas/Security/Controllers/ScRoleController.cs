using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;

using ERPSolution.Controllers;
using ERPSolution.Common;

namespace ERPSolution.Areas.Security.Controllers
{
    [SignInCheck]
    public class ScRoleController : BaseController
    {
        //
        // GET: /Security/ScRole/
        public ActionResult Index()
        {
            return View();
        }

        
        public string Save(ScRoleModel ob)
        {
            string vMsg = "";
            if(ModelState.IsValid)
                vMsg = ob.Save();

            return vMsg;
        }

        public string Update(ScRoleModel ob)
        {
            string vMsg = "";
            if (ModelState.IsValid)
                vMsg = ob.Update();

            return vMsg;
        }

        public JsonResult RoleListData()
        {
            var ob = new ScRoleModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUniqueNameE(ScRoleModel ob)
        {
            try
            {
                //obBLL.SignIn().Where(m => m.LOGIN_ID == ob.LOGIN_ID && m.PASSWORD_HASH == ob.PASSWORD_HASH).ToList()[0]
                ScRoleModel obUser = ob.SelectAll().Where(m => m.ROLE_NAME_EN.ToLower() == ob.ROLE_NAME_EN.ToLower()).ToList()[0];
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch 
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public PartialViewResult ScRoleMenu()
        {
            return PartialView();
        }





	}
}