using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Collections;
using System.Web.Configuration;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.IO;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrTaRawDataController : BaseController
    {
       // GET: Test/Country
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); 

            string vMenuID = "";
            string[] vQryStrSplit = { };
            string vQryStr = HttpContext.Request.Params["a"];
            ViewBag.PageName = "";
            if (vQryStr != null && vQryStr != "")
            {
                vQryStrSplit = vQryStr.Split('/');
                vMenuID = vQryStrSplit[0];

                if (vMenuID == "36")
                    ViewBag.PageName = "Import Raw Data";
                else if (vMenuID == "58")
                    ViewBag.PageName = "ID Card Print";
            }

            return View();
        }

        public PartialViewResult ImportRawdata()
        {
            return PartialView();
        }

        public PartialViewResult TaDeviceList()
        {
            return PartialView();
        }

        public PartialViewResult ImportRawdataList()
        {
            return PartialView();
        }

        public PartialViewResult IDCardPrint()
        {
            return PartialView();
        }

       
        //[HttpPost]
        //[MyValidateAntiForgeryToken]
        //public JsonResult ImportRawData(ImportFileModel ob)
        //{
        //    string vMsg = "";

        //    return Json(new { success = true, vMsg });
        //}

        [HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult uploadHOTextFile()
        {
            //var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;


                string savedFileName = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/PUNCH_DATA"), Path.GetFileName("ATTEN_FILE.txt"));
                System.IO.File.Delete(savedFileName);                            
                hpf.SaveAs(savedFileName);

                //r.Add(new UploadFilesResult()
                //{
                //    Name = hpf.FileName,
                //    Length = hpf.ContentLength,
                //    Type = hpf.ContentType
                //});
            }
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Import(int pIMPORT_TYPE)
        {
            string readFileName = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/PUNCH_DATA"), Path.GetFileName("ATTEN_FILE.txt"));
            

            string vMsg = "";

            try
            {
                vMsg = new HR_TA_RAW_DATAModel().Import(pIMPORT_TYPE, readFileName);
            }
            catch(Exception e)
            {
                vMsg = e.Message;
            }

            return Json(new { success = true, vMsg });
        }

        public JsonResult PunchListData()
        {            
            var ob = new HR_TA_RAW_DATAModel().PunchListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeviceListData(Int64 pLK_TA_DEV_TYPE_ID)
        {
            var ob = new HR_TA_DEVICE_CFGModel().DeviceListData(pLK_TA_DEV_TYPE_ID);            
            return Json(ob, JsonRequestBehavior.AllowGet);
        } 
        
    }
}