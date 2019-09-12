using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Areas.Purchase.Controllers
{
    public class PurchaseController : BaseController
    {
        public ActionResult SupplierMaster()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _SupplierMaster()
        {
            return PartialView();
        }

        public ActionResult SupplierList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _SupplierList()
        {
            return PartialView();
        }

        public ActionResult GeneratePOList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _GeneratePOList()
        {
            return PartialView();
        }
        public ActionResult GeneratePO()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _GeneratePO()
        {
            return PartialView();
        }

        public ActionResult PurchaseRequisitionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _PurchaseRequisitionList()
        {
            return PartialView();
        }

        public ActionResult PurchaseRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _PurchaseRequisition()
        {
            return PartialView();
        }


        public ActionResult FundRequisitionList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FundRequisitionList()
        {
            return PartialView();
        }

        public ActionResult FundRequisition()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _FundRequisition()
        {
            return PartialView();
        }

        public ActionResult GeneralPOList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GeneralPOList()
        {
            return PartialView();
        }

        public ActionResult GeneralPO()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _GeneralPO()
        {
            return PartialView();
        }

        public ActionResult ReceiveList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _ReceiveList()
        {
            return PartialView();
        }

        public ActionResult Receive()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _Receive()
        {
            return PartialView();
        }

        public ActionResult CertificateType()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CertificateType()
        {
            return PartialView();
        }

        public ActionResult CertificateList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _CertificateList()
        {
            return PartialView();
        }

        public ActionResult Certificate()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _Certificate()
        {
            return PartialView();
        }

        public ActionResult AddFabBkingRpt()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _AddFabBkingRpt()
        {
            return PartialView();
        }

        public ActionResult InvAdjList()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _InvAdjList()
        {
            return PartialView();
        }

        public ActionResult InvAdj()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _InvAdj()
        {
            return PartialView();
        }
        

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult uploadAuditCertificate(HR_AUD_CERT_REGModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/AUD_CRT_DOCS"), ob.APP_DOC_PATH);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult uploadAuditCertificateOrg(HR_AUD_CERT_REGModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/AUD_CRT_DOCS"), ob.ISS_DOC_PATH);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult uploadAuditCertificateCap(HR_AUD_CERT_CAPModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/AUD_CRT_DOCS"), ob.CAP_DOC_PATH);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult uploadDocLogo(RF_AUD_CERT_TYPEModel ob)
        {
            string extension = null;
            if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
            {
                extension = Path.GetExtension(ob.ATT_FILE.FileName);
                string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/AUD_CRT_DOCS"), ob.CERT_LOGO);
                ob.ATT_FILE.SaveAs(path);
            }
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

    }
}