using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ERPSolution.Areas.Planning.Controllers
{
    [SignInCheck]
    public class PlnController : BaseController
    {
        // GET: /Planning/pln/
        public ViewResult PlnReport()
        {
            return View();
        }

        public PartialViewResult _PlnReportParams()
        {
            return PartialView();
        }

        // GET: /Planning/pln/
        public ViewResult ProdCapctyClndr()
        {
            return View();
        }
        public PartialViewResult _ProdCapctyClndr()
        {
            return PartialView();
        }

        public ViewResult ProdCapctyMN()
        {
            return View();
        }
        public PartialViewResult _ProdCapctyMN()
        {
            return PartialView();
        }
        public PartialViewResult _ProdCapctyMnList()
        {
            return PartialView();
        }



        public ViewResult LearningCurv()
        {
            return View();
        }
        public PartialViewResult _LearningCurv()
        {
            return PartialView();
        }


        public ViewResult OrdVlumWiseCpmEff()
        {
            return View();
        }
        public PartialViewResult _OrdVlumWiseCpmEff()
        {
            return PartialView();
        }

        public ViewResult ThroughPutTime()
        {
            return View();
        }
        public PartialViewResult _ThroughPutTime()
        {
            return PartialView();
        }
        
        public ViewResult GmtPlanStatus()
        {
            return View();
        }
        public PartialViewResult _GmtPlanStatus()
        {
            return PartialView();
        }
        public ViewResult ItemPartMap()
        {
            return View();
        }
        public PartialViewResult _ItemPartMap()
        {
            return PartialView();
        }

        public ViewResult PartProcessSpec()
        {
            return View();
        }
        public PartialViewResult _PartProcessSpec()
        {
            return PartialView();
        }

        public ViewResult PlnCritProcessTemp()
        {
            return View();
        }
        public PartialViewResult _PlnCritProcessTemp()
        {
            return PartialView();
        }

        public ViewResult ProdTypeLineMap()
        {
            return View();
        }
        public PartialViewResult _ProdTypeLineMap()
        {
            return PartialView();
        }
        public PartialViewResult _LineGmtItemMapModal()
        {
            return PartialView();
        }

        public ViewResult PlnHolyDayModel()
        {
            return View();
        }
        public PartialViewResult _PlnHolyDayModel()
        {
            return PartialView();
        }

        public PartialViewResult _PlnHolyDayModal()
        {
            return PartialView();
        }
        public ViewResult GmtLineLoadPlan()
        {
            return View();
        }
        public PartialViewResult _GmtLineLoadPlan()
        {
            return PartialView();
        }



        public ViewResult ActivityPermission()
        {
            return View();
        }
        public PartialViewResult _ActivityPermissionH()
        {
            return PartialView();
        }
        public PartialViewResult _ActivityPermissionEventAccess()
        {
            return PartialView();
        }
        public PartialViewResult _ActivityPermissionResourceAccess()
        {
            return PartialView();
        }
        public PartialViewResult _ActivityPermissionOrderAccess()
        {
            return PartialView();
        }
        public PartialViewResult _OrderItemListLnPln()
        {
            return PartialView();
        }
        public PartialViewResult _OrderItemListLnPlnD()
        {
            return PartialView();
        }

        public PartialViewResult _ManualLineLoadingModal()
        {
            return PartialView();
        }
        public PartialViewResult _ManualLineLoadingLineSugModal()
        {
            return PartialView();
        }

        public ViewResult CapacityBkBoard()
        {
            return View();
        }
        public PartialViewResult _CapacityBkBoard()
        {
            return PartialView();
        }
        public PartialViewResult _CapacityBkBoardDtl()
        {
            return PartialView();
        }

        public PartialViewResult _CapacityBkBoardDtlChart()
        {
            return PartialView();
        }
        public PartialViewResult _GmtLineLoadingTune()
        {
            return PartialView();
        }
        public PartialViewResult _TnaDataListModal()
        {
            return PartialView();
        }
        public PartialViewResult _GmtLineLoadingEventOffset()
        {
            return PartialView();
        }
        public PartialViewResult _GmtProdMonitoringModal()
        {
            return PartialView();
        }
        public PartialViewResult _GmtPlanChangeModal()
        {
            return PartialView();
        }

        public PartialViewResult _GmtOrderMergingModal()
        {
            return PartialView();
        }


        public ViewResult ByrWiseCapctyAlloc()
        {
            return View();
        }
        public PartialViewResult _ByrWiseCapctyAlloc()
        {
            return PartialView();
        }
        

	}
}