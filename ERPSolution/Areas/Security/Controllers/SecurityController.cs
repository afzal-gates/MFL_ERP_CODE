using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Security.Controllers
{
    public class SecurityController : BaseController
    {
        //
        // GET: /Security/Security/

        
        public ActionResult RequestApprovalWorkFlow()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _RequestApprovalWorkFlow()
        {
            return PartialView();
        }

        public ActionResult UserStoreMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _UserStoreMap()
        {
            return PartialView();
        }

        public ActionResult UserOfficeMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _UserOfficeMap()
        {
            return PartialView();
        }


        public ActionResult ReportTemplate()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _ReportTemplate()
        {
            return PartialView();
        }

        public ActionResult GlobalNotification()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GlobalNotification()
        {
            return PartialView();
        }

        public ActionResult serverMaintenance()
        {

            //if (!IsMenuPermission()) 
            //    return Redirect(Url.Content("~/")); 
            return View();
        }
        public PartialViewResult _serverMaintenance()
        {
            return PartialView();
        }

        public ActionResult UserProdCatMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _UserProdCatMap()
        {
            return PartialView();
        }


        public ActionResult FabDfctTypeMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _FabDfctTypeMap()
        {
            return PartialView();
        }

        public ActionResult BuyerStoreMap()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _BuyerStoreMap()
        {
            return PartialView();
        }
        

	}


}