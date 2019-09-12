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
using System.Web.Hosting;
using Postal;
using System.Net.Mail;
using System.Net.Mime;

namespace ERPSolution.Areas.Cutting.Controllers
{
    [SignInCheck]
    public class CuttingController : BaseController
    {
        //
        // GET: /Cutting/Cutting/


        public ViewResult CutTableTgt()
        {
            return View();
        }
        public PartialViewResult _CutTableTgt()
        {
            return PartialView();
        }


        public ViewResult CutReport()
        {
            return View();
        }
        public PartialViewResult _CutReportParams()
        {
            return PartialView();
        }


        public ViewResult MrkrReq()
        {
            return View();
        }
        public PartialViewResult _MrkrReqH()
        {
            return PartialView();
        }
        public PartialViewResult _MrkrReqList()
        {
            return PartialView();
        }

        public ViewResult Mrkr()
        {
            return View();
        }
        public PartialViewResult _MrkrH()
        {
            return PartialView();
        }

        public ViewResult LayChart()
        {
            return View();
        }
        public PartialViewResult _LayChartH()
        {
            return PartialView();
        }

        public ActionResult CuttingList()
        {
            return View();
        }
        public PartialViewResult _CuttingList()
        {
            return PartialView();
        }

        public ActionResult StrRcv()
        {
            return View();
        }
        public PartialViewResult _StrRcv()
        {
            return PartialView();
        }

        public ViewResult CutPanelInspection()
        {
            return View();
        }
        public PartialViewResult _CutPanelInspection()
        {
            return PartialView();
        }
        public PartialViewResult _CutPanelInspectionModal()
        {
            return PartialView();
        }

        public ViewResult CutPanelRecut()
        {
            return View();
        }

        public PartialViewResult _CutPanelRecut()
        {
            return PartialView();
        }
        public PartialViewResult _CutPanelRecutModal()
        {
            return PartialView();
        }

        public ActionResult BndlCardPrintParam()
        {
            return View();
        }
        public PartialViewResult _BndlCardPrintParam()
        {
            return PartialView();
        }

        public ActionResult ReScan4SewInput()
        {
            return View();
        }
        public PartialViewResult _ReScan4SewInput()
        {
            return PartialView();
        }

        public ViewResult StoreRecv()
        {
            return View();
        }
        public PartialViewResult _StoreRecv()
        {
            return PartialView();
        }
        public PartialViewResult _BundleStatusModal()
        {
            return PartialView();
        }
        public PartialViewResult _StoreRecvOptModal()
        {
            return PartialView();
        }
        public ViewResult DeliveryChallan()
        {
            return View();
        }
        public PartialViewResult _SewingDeliveryChallanList()
        {
            return PartialView();
        }
        public PartialViewResult _DeliveryChallanModal()
        {
            return PartialView();
        }
        public PartialViewResult _SewDelvChaln4Split()
        {
            return PartialView();
        }


        public ViewResult StoreRecvPrintEmbr()
        {
            return View();
        }
        public PartialViewResult _StoreRecvPrintEmbr()
        {
            return PartialView();
        }
        public PartialViewResult _StoreRecvOptPrintEmbrModal()
        {
            return PartialView();
        }
        public PartialViewResult _StoreRecvPrintEmbrModal()
        {
            return PartialView();
        }

        public ViewResult CutBundleCardAmend()
        {
            return View();
        }

        public PartialViewResult _CutBundleCardAmend()
        {
            return PartialView();
        }

        public PartialViewResult _CutBundleCardAmendModal()
        {
            return PartialView();
        }
        public PartialViewResult _SewingDeliveryChallanModal()
        {
            return PartialView();
        }

        public PartialViewResult _SewingScDeliveryChallanModal()
        {
            return PartialView();
        }

        public PartialViewResult _StoreRecvPrintEmbrRecvChlnModal()
        {
            return PartialView();
        }

        public PartialViewResult _SewingPrintEmbrDeliveryChallanList()
        {
            return PartialView();
        }
        public PartialViewResult _SewingScDeliveryChallanList()
        {
            return PartialView();
        }

        public PartialViewResult _DeliveryChallanSscSearchModal()
        {
            return PartialView();
        }

        public PartialViewResult _DeliveryChallanSewSearchModal()
        {
            return PartialView();
        }
        public PartialViewResult _DeliveryChallanPrintEmbrSearchModal()
        {
            return PartialView();
        }
        public PartialViewResult _EmblsmntDelivChalnSearchModal4Merge()
        {
            return PartialView();
        }

        public ViewResult CutFabReqList()
        {
            return View();
        }

        public PartialViewResult _CutFabReqList()
        {
            return PartialView();
        }


        public ViewResult CutFabReq()
        {
            return View();
        }

        public PartialViewResult _CutFabReq()
        {
            return PartialView();
        }

        
	}
}