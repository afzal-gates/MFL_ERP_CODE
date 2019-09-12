using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Commercial.Controllers
{
    public class CmrController : BaseController
    {
        //
        // GET: /Commercial/Cmr/
        public ActionResult BuyerNotifyParty()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _BuyerNotifyParty()
        {
            return PartialView();
        }

        public PartialViewResult _BuyerNotifyPartyModal()
        {
            return PartialView();
        }

        public ActionResult ReferenceType()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ReferenceType()
        {
            return PartialView();
        }

        public PartialViewResult _IncoTerm()
        {
            return PartialView();
        }

        public PartialViewResult _PaymentTerm()
        {
            return PartialView();
        }

        public PartialViewResult _DeliveryPlace()
        {
            return PartialView();
        }

        public PartialViewResult _TransitPort()
        {
            return PartialView();
        }

        public PartialViewResult _LCType()
        {
            return PartialView();
        }

        public PartialViewResult _ShipMode()
        {
            return PartialView();
        }

        public PartialViewResult _GmtAsn()
        {
            return PartialView();
        }
        
        
        public ActionResult ExpLcContractList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ExpLcContractList()
        {
            return PartialView();
        }

        public ActionResult ExpLcContract()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ExpLcContract()
        {
            return PartialView();
        }

        public ActionResult ExpLcPIList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ExpLcPIList()
        {
            return PartialView();
        }

        public ActionResult ExpLcPI()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ExpLcPI()
        {
            return PartialView();
        }

        public ActionResult ImpLcPIList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ImpLcPIList()
        {
            return PartialView();
        }


        public ActionResult ImpLcPI()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ImpLcPI()
        {
            return PartialView();
        }
        

        public ActionResult ImpLcPOPIRevise()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ImpLcPOPIRevise()
        {
            return PartialView();
        }


        public ActionResult ImpLcList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ImpLcList()
        {
            return PartialView();
        }


        public ActionResult ImpLc()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ImpLc()
        {
            return PartialView();
        }


        public ActionResult LcUDList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _LcUDList()
        {
            return PartialView();
        }


        public ActionResult LcUD()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _LcUD()
        {
            return PartialView();
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult uploadPIDocs(CM_IMP_PI_HModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/IMP_PI_DOCS"), ob.RPT_PATH_URL);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);

        }
	}
}