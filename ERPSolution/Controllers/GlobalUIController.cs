using ERP.Model;
using ERPSolution.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERPSolution.Controllers
{
    public class GlobalUIController : Controller
    {
        //
        // GET: /GlobalUI/
        public ActionResult BrandEntry()
        {
            return View("BrandEntry");
        }

        public ActionResult BankEntry()
        {
            return View("BankEntry");
        }

        public ActionResult BankBranchEntry()
        {
            return View("BankBranchEntry");
        }


        public ActionResult UploadOtherDocs()
        {
            return View("UploadOtherDocs");
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult UploadOtherDocs(RF_DOC_ARCVModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Save();

                    string vMsg=jsonStr.Substring(9,9);
                    if (vMsg == "MULTI-001")
                    {
                        string extension = null;
                        if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
                        {
                            extension = Path.GetExtension(ob.ATT_FILE.FileName);
                            string path = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/OTHER_DOCS"), ob.DOC_REF_NO + extension);
                            ob.ATT_FILE.SaveAs(path);
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            else
            {
                var errors = new Hashtable();
                foreach (var pair in ModelState)
                {
                    if (pair.Value.Errors.Count > 0)
                    {
                        errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
                    }
                }
                return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);            
        }

        

	}
}