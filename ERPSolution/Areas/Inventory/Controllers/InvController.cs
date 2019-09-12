using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Inventory.Controllers
{
    [SignInCheck]
    public class InvController : BaseController
    {
        //========== Start for Item Category
        public ActionResult ItemCategory()
        {
            return View();
        }

        public PartialViewResult _ItemCategoryEntry()
        {
            return PartialView();
        }

        public PartialViewResult _ItemCategoryPermission()
        {
            return PartialView();
        }
        //========== End for Item Category


        //========== Start for Item Information
        public ActionResult Item()
        {
            return View();
        }

        public PartialViewResult _ItemList()
        {
            return PartialView();
        }

        public PartialViewResult _ItemSales()
        {
            return PartialView();
        }

        //========== End for Item Information


        //========== Start for Role Category
        public ActionResult AssignUserToCatogory()
        {
            return View();
        }

        public PartialViewResult _AssignUserToCategory()
        {
            return PartialView();
        }
        //========== End for Role Information

        public ViewResult StoreProfile()
        {
            return View();
        }
        public PartialViewResult _StoreProfile()
        {
            return PartialView();
        }

        public ViewResult ScmStoreReceiveList()
        {
            return View();
        }

        public PartialViewResult _ScmStoreReceiveList()
        {
            return PartialView();
        }

        public ViewResult ScmStoreReceive()
        {
            return View();
        }

        public PartialViewResult _ScmStoreReceive()
        {
            return PartialView();
        }

        public ViewResult MoUConversion()
        {
            return View();
        }

        public PartialViewResult _MoUConversion()
        {
            return PartialView();
        }

        public ViewResult ScmReceiveReturnList()
        {
            return View();
        }

        public PartialViewResult _ScmReceiveReturnList()
        {
            return PartialView();
        }

        public ViewResult ScmReceiveReturn()
        {
            return View();
        }

        public PartialViewResult _ScmReceiveReturn()
        {
            return PartialView();
        }

        public ViewResult GeneralStoreTransferList()
        {
            return View();
        }

        public PartialViewResult _GeneralStoreTransferList()
        {
            return PartialView();
        }

        public ViewResult GeneralStoreTransfer()
        {
            return View();
        }

        public PartialViewResult _GeneralStoreTransfer()
        {
            return PartialView();
        }

        public ViewResult GeneralStoreTransferIssue()
        {
            return View();
        }

        public PartialViewResult _GeneralStoreTransferIssue()
        {
            return PartialView();
        }


        public ViewResult ScmGenStrReqList()
        {
            return View();
        }

        public PartialViewResult _ScmGenStrReqList()
        {
            return PartialView();
        }

        public ViewResult ScmGenStrReq()
        {
            return View();
        }

        public PartialViewResult _ScmGenStrReq()
        {
            return PartialView();
        }
        

        public ViewResult ScmGenStrIssueList()
        {
            return View();
        }

        public PartialViewResult _ScmGenStrIssueList()
        {
            return PartialView();
        }

        public ViewResult ScmGenStrIssue()
        {
            return View();
        }

        public PartialViewResult _ScmGenStrIssue()
        {
            return PartialView();
        }


        public ActionResult OpeningBal()
        {
            if (!IsMenuPermission()) 
                return Redirect(Url.Content("~/")); 
            return View();
        }
        public PartialViewResult _OpeningBal()
        {
            return PartialView();
        }

        public ActionResult InvReport()
        {
            if (!IsMenuPermission()) 
                return Redirect(Url.Content("~/")); 
            return View();
        }

        public PartialViewResult _InvReportParams()
        {
            return PartialView();
        }

        public ActionResult StrInActiveItem()
        {
            if (!IsMenuPermission()) 
                return Redirect(Url.Content("~/")); 
            return View();
        }

        public PartialViewResult _StrInActiveItem()
        {
            return PartialView();
        }


        public ActionResult StrIssRtnList()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _StrIssRtnList()
        {
            return PartialView();
        }

        public ActionResult StrIssRtn()
        {
            if (!IsMenuPermission())
                return Redirect(Url.Content("~/"));
            return View();
        }

        public PartialViewResult _StrIssRtn()
        {
            return PartialView();
        }
    }
}