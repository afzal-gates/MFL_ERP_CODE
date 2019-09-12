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

namespace ERPSolution.Areas.Garments.Controllers
{
    [SignInCheck]
    public class GmtController : BaseController
    {
        // GET: /Garments/Gmt/
        public ViewResult GmtReport()
        {
            return View();
        }

        public PartialViewResult _GmtReportParams()
        {
            return PartialView();
        }

        // GET: /Garments/Gmt/
        public ViewResult SwMcnSpec()
        {
            return View();
        }

        public PartialViewResult _SwMcnSpec()
        {
            return PartialView();
        }

        public ViewResult SewingOutput()
        {
            return View();
        }

        public PartialViewResult _SewingOutPut()
        {
            return PartialView();
        }
        public PartialViewResult _SewingOutPutDefectRejectModal()
        {
            return PartialView();
        }

        public ViewResult GmtWash()
        {
            return View();
        }

        public PartialViewResult _GmtWashSent()
        {
            return PartialView();
        }
        public PartialViewResult _GmtWashRecv()
        {
            return PartialView();
        }

        public PartialViewResult _GmtWashDelChallanModal()
        {
            return PartialView();
        }

        public ViewResult GmtFinIron()
        {
            return View();
        }
        public PartialViewResult _GmtFinIron()
        {
            return PartialView();
        }

        public ViewResult GmtFinPoly()
        {
            return View();
        }
        public PartialViewResult _GmtFinPoly()
        {
            return PartialView();
        }

        public ViewResult GmtFinFold()
        {
            return View();
        }
        public PartialViewResult _GmtFinFold()
        {
            return PartialView();
        }
        
        public ViewResult ScInShipLeftOver()
        {
            return View();
        }
        public PartialViewResult _ScInShipLeftOver()
        {
            return PartialView();
        }

        public ViewResult AsSample()
        {
            return View();
        }
        public PartialViewResult _AsSampleSent()
        {
            return PartialView();
        }
        public PartialViewResult _AsSampleRecv()
        {
            return PartialView();
        }

        public PartialViewResult _GmtAsSampleSearchModal()
        {
            return PartialView();
        }
	}
}