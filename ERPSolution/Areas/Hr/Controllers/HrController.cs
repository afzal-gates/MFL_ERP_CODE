using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrController : BaseController
    {        
        
        public ActionResult IncrMemo()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _IncrMemoCreation()
        {
            return PartialView();
        }

        public ActionResult IncrProposal()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _IncrProposalH()
        {
            return PartialView();
        }
        public PartialViewResult _IncrProposalD()
        {
            return PartialView();
        }


        public PartialViewResult _SpecialIncrH()
        {
            return PartialView();
        }
        public PartialViewResult _SpecialIncrD()
        {
            return PartialView();
        }


        public PartialViewResult _IncrProposalBatchList()
        {
            return PartialView();
        }
        public PartialViewResult _IncrProcess()
        {
            return PartialView();
        }

        public ActionResult Country()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _Country()
        {
            return PartialView();
        }

        public ActionResult ProductionFloor()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ProductionFloor()
        {
            return PartialView();
        }

        public PartialViewResult FloorUI()
        {
            return PartialView();
        }

        public PartialViewResult BuildingUI()
        {
            return PartialView();
        }

        public ActionResult AssignScheduleByRandomPerson()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _AssignScheduleByRandomPerson()
        {
            return PartialView();
        }


        public ActionResult AnnualLeaveByRandomPerson()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _AnnualLeaveByRandomPerson()
        {
            return PartialView();
        }


        public ActionResult CompPayPeriod()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CompPayPeriod()
        {
            return PartialView();
        }

        public ActionResult OtSummeryApprove()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _OtSummeryApprove()
        {
            return PartialView();
        }

        public ActionResult MbnBillProcess()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _MbnBillProcess()
        {
            return PartialView();
        }


        public ActionResult EmpTrnfr()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _EmpTrnfr()
        {
            return PartialView();
        }

        public ActionResult SmsBrodcast2Emp()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _SmsBrodcast2Emp()
        {
            return PartialView();
        }

        public ActionResult IncrHistory4Emp()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _IncrHistory4Emp()
        {
            return PartialView();
        }

    }
}