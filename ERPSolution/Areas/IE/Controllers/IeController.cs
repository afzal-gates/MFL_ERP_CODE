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

namespace ERPSolution.Areas.Ie.Controllers
{
    [SignInCheck]
    public class IeController : BaseController
    {
        // GET: /IE/ie/
        public ViewResult IeReport()
        {
            return View();
        }

        public PartialViewResult _IeReportParams()
        {
            return PartialView();
        }

        // GET: /Planning/pln/
        public ViewResult GmtStylItmList()
        {
            return View();
        }
        public PartialViewResult _GmtStylItmList()
        {
            return PartialView();
        }
        public PartialViewResult _GmtStylItmListD()
        {
            return PartialView();
        }
        public PartialViewResult _GmtItemOprSpec()
        {
            return PartialView();
        }
        public PartialViewResult _GmtItemOprSpecD()
        {
            return PartialView();
        }

        public ViewResult GmtWashReqList()
        {
            return View();
        }
        public PartialViewResult _GmtWashReqList()
        {
            return PartialView();
        }
        public ViewResult GmtWashReq()
        {
            return View();
        }
        public PartialViewResult _GmtWashReq()
        {
            return PartialView();
        }

        public ViewResult SewingTargetSetting()
        {
            return View();
        }
        public PartialViewResult _SewingTargetSetting()
        {
            return PartialView();
        }
        public PartialViewResult _OrderAllocationCnfModal()
        {
            return PartialView();
        }

        public PartialViewResult _IeManMinsAdjustment()
        {
            return PartialView();
        }

        public PartialViewResult _GmtIeNptModal()
        {
            return PartialView();
        }
        public PartialViewResult _OutPlanProdEntryModal()
        {
            return PartialView();
        }

        public ViewResult IeGmtItemVariation()
        {
            return View();
        }
        public PartialViewResult _IeGmtItemVariation()
        {
            return PartialView();
        }

        
        
	}
}