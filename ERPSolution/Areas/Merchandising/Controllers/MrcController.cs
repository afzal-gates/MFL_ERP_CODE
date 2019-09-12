using ERP.Model;
using ERPSolution.Common;
using ERPSolution.Controllers;
using Postal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;



namespace ERPSolution.Areas.Merchandising.Controllers
{
    [SignInCheck]
    public class MrcController : BaseController
    {
        public static void MailSendOTP4OrderConfirm(Int64 pMC_ORDER_H_ID)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_ORDER_HModel ob = new MC_ORDER_HModel();
            var rptData = ob.GetData4SendOTP(pMC_ORDER_H_ID);

            var email = new MailSendOTP4OrderConfirmService
            {
                To = rptData.MAIL_ADD_LST,
                data = rptData
            };

            emailService.Send(email);
        }

        public JsonResult FireMailSendOTP4OrderConfirm(Int64 pMC_ORDER_H_ID)
        {
            MailSendOTP4OrderConfirm(pMC_ORDER_H_ID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public static void SendAddFabBkAprovMail(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_BLK_ADFB_REQ_RPTModel ob = new MC_BLK_ADFB_REQ_RPTModel();
            var rptData = ob.GetAddFabBkRpt(pMC_BLK_ADFB_REQ_H_ID);

            var email = new SendAddFabBkAprovMailService
            {
                To = rptData.finFabList[0].MAIL_ADD_LST,

                data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)

            };
         
           emailService.Send(email);
        }

        public JsonResult AddFabBkFireMail(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            SendAddFabBkAprovMail(pMC_BLK_ADFB_REQ_H_ID);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }        
        
        public static void send_order_confirmation_mail(String MAIL_LIST, String DESC, String REDIRECT_URL)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new OrderConfirmationMailService
            {
                MAIL_LIST = MAIL_LIST,
                DESC = DESC,
                REDIRECT_URL = REDIRECT_URL
            };

            emailService.Send(email);
        }

        public JsonResult FireOrderConfirmMail(Int64 pMC_ORDER_H_ID)
        {

          var ob =  new MC_STYLE_HModel().GetOrderCofirmationMailData(pMC_ORDER_H_ID);
          send_order_confirmation_mail(ob.MAIL_LIST, ob.DESC, ob.REDIRECT_URL);
          return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        public static void SendSmplProgAprovMail(Int64? pMC_SMP_REQ_H_ID, string pATTACH_FILE_PATH)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_SMP_REQ_HModel ob = new MC_SMP_REQ_HModel();
            var rptData = ob.Select(pMC_SMP_REQ_H_ID, "Y");

            if (rptData.MC_TNA_TASK_STATUS_ID == 14)
            {
                var email = new SendSmplProgAprovMailService
                {
                    To = rptData.EMAIL_TO_LST, //"dev.maruf@multi-fabs.com",
                    data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)
                };

                string savedFileName = pATTACH_FILE_PATH;  //"D:\\a\\multipleKnitCardFormat.pdf";
                // Create  the file attachment for this e-mail message.
                Attachment data = new Attachment(savedFileName, MediaTypeNames.Application.Octet);
                // Add time stamp information for the file.
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(savedFileName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(savedFileName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(savedFileName);
                // Add the file attachment to this e-mail message.
                email.Attachments.Add(data);

                try
                {
                    emailService.Send(email);
                    string vMsg = ob.MailSendSuccessfully4SmplProgAprvl(pMC_SMP_REQ_H_ID, "Y");
                }
                catch (SmtpFailedRecipientException ex)
                {
                    string vMsg = ob.MailSendSuccessfully4SmplProgAprvl(pMC_SMP_REQ_H_ID, "N");
                }

                System.IO.File.Delete(savedFileName);
            }
        }

        public static void SendBulkFabAprovMail(Int64? pMC_BLK_FAB_REQ_H_ID, string pATTACH_FILE_PATH)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_BLK_FAB_REQ_HModel ob = new MC_BLK_FAB_REQ_HModel();
            var rptData = ob.Select(pMC_BLK_FAB_REQ_H_ID, "Y");

            var email = new SendBulkFabAprovMailService
            {
                To = rptData.EMAIL_TO_LST, //"dev.maruf@multi-fabs.com",
                data = rptData //ob.GetSrtFabReqRpt(pKNT_SRT_FAB_REQ_H_ID)
            };

            string savedFileName = pATTACH_FILE_PATH;  //"D:\\a\\multipleKnitCardFormat.pdf";
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(savedFileName, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(savedFileName);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(savedFileName);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(savedFileName);
            // Add the file attachment to this e-mail message.
            email.Attachments.Add(data);
           
            try
            {
                emailService.Send(email);
                string vMsg = ob.MailSendSuccessfully4BulkBudgetAprvl(pMC_BLK_FAB_REQ_H_ID, "Y");
            }
            catch (SmtpFailedRecipientException ex)
            {
                string vMsg = ob.MailSendSuccessfully4BulkBudgetAprvl(pMC_BLK_FAB_REQ_H_ID, "N");
            }
            
            System.IO.File.Delete(savedFileName);
        }


        public static void SendProjectionMail(Int64? pMC_PROV_FAB_BK_H_ID, string pATTACH_FILE_PATH)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_PROV_FAB_BK_HModel ob = new MC_PROV_FAB_BK_HModel();
            var rptData = ob.Select(pMC_PROV_FAB_BK_H_ID);

            var email = new SendProjectionAproveMailService
            {
                To = rptData.EMAIL_TO_LST,
                data = rptData

            };

            string savedFileName = pATTACH_FILE_PATH;  //"D:\\a\\multipleKnitCardFormat.pdf";
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(savedFileName, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(savedFileName);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(savedFileName);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(savedFileName);
            // Add the file attachment to this e-mail message.
            email.Attachments.Add(data);

            emailService.Send(email);
            System.IO.File.Delete(savedFileName);
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _BulkBudgetAprvl()
        {
            return PartialView();
        }


        public ActionResult UploadStyleOrder()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult _UploadStyleOrder()
        {
            return PartialView();
        }
        
        [MyValidateAntiForgeryToken]
        public JsonResult ShowStyleOrderFileData(StyleOrderUploadModel ob)
        {
            var obList = new List<dynamic>();
            Int64 vTotLineCount = 0;

            if (ModelState.IsValid)
            {
                try
                {
                    string extension = null;
                    string uploadFileName = null;

                    if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
                    {
                        extension = Path.GetExtension(ob.ATT_FILE.FileName);
                        if (ob.UPLOAD_FORMAT_ID == 1)
                            uploadFileName = "StyleOrderFormat01";
                        else
                            uploadFileName = "StyleOrderFormat02";

                        string fileNameWithPath = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/IMP_STYL_ORD"), uploadFileName + extension);
                        ob.ATT_FILE.SaveAs(fileNameWithPath);

                        Int64 vLineCount = 0;                        
                        string[] lines = System.IO.File.ReadAllLines(fileNameWithPath);
                        vTotLineCount=lines.Count();

                        foreach (string line in lines)
                        {
                            if (vLineCount >= ob.SHOW_FROM_REC_NO && vLineCount < (ob.SHOW_FROM_REC_NO + ob.MAX_REC_SHOW))
                            {
                                string[] vResult = line.Split(',');

                                if (ob.UPLOAD_FORMAT_ID == 1)
                                    obList.Add(new
                                    {
                                        BYR_ACC_GRP_NAME_EN = vResult[0],
                                        BYR_ACC_NAME_EN = vResult[1],
                                        BUYER_NAME_EN = vResult[2],
                                        STYLE_NO = vResult[3],
                                        STYLE_DESC = vResult[4],
                                        STYLE_MOU_CODE = vResult[5],
                                        HAS_SET = vResult[6],
                                        PCS_PER_PACK = vResult[7],
                                        ORDER_NO = vResult[8],
                                        ORD_CONF_DT = vResult[9],
                                        SHIP_DT = vResult[10],
                                        SHIP_COUNTRY = vResult[11],
                                        REVISION_NO = vResult[12],
                                        REVISION_DT = vResult[13],
                                        REV_REASON = vResult[14],
                                        ITEM_CAT_NAME_EN = vResult[15],
                                        ITEM_NAME_EN = vResult[16],
                                        PARENT_ITEM_NAME_EN = vResult[17],
                                        GMT_TYPE = vResult[18],
                                        COLOR_NAME_EN = vResult[19],
                                        SIZE_CODE = vResult[20],
                                        SIZE_QTY = vResult[21]
                                    });
                                else if (ob.UPLOAD_FORMAT_ID == 2)
                                    obList.Add(new
                                    {
                                        BYR_ACC_GRP_NAME_EN = vResult[0],
                                        BYR_ACC_NAME_EN = vResult[1],
                                        BUYER_NAME_EN = vResult[2],
                                        STYLE_NO = vResult[3],
                                        ITEM_CAT_NAME_EN = vResult[5],
                                        FABRIC_DESC = vResult[6]
                                    });
                            }

                            vLineCount = vLineCount + 1;
                            if (vLineCount > (ob.SHOW_FROM_REC_NO + ob.MAX_REC_SHOW))
                                break;
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
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, data = obList, totalRecord = vTotLineCount }, JsonRequestBehavior.AllowGet);
        }
        
        [MyValidateAntiForgeryToken]
        public JsonResult UploadStyleOrderFileData(StyleOrderUploadModel ob)
        {

            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    string extension = null;
                    string uploadFileName = null;

                    if (ob.ATT_FILE != null && ob.ATT_FILE.ContentLength > 0)
                    {
                        extension = Path.GetExtension(ob.ATT_FILE.FileName);
                        if (ob.UPLOAD_FORMAT_ID == 1)
                            uploadFileName = "StyleOrderFormat01";
                        else
                            uploadFileName = "StyleOrderFormat02";

                        string fileNameWithPath = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/IMP_STYL_ORD"), uploadFileName + extension);
                        ob.ATT_FILE.SaveAs(fileNameWithPath);

                        jsonStr = ob.BatchSave(fileNameWithPath);
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
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult OrderInfo()
        {
            return View();
        }

        public PartialViewResult _OrderH()
        {
            return PartialView();
        }

        public PartialViewResult _OrderDtl()
        {
            return PartialView();
        }

        public PartialViewResult _OrderEntry()
        {
            return PartialView();
        }

        public PartialViewResult _OrderList()
        {
            return PartialView();
        }


        public ActionResult SmplFabBook()
        {
            return View();
        }
        public PartialViewResult _SmplFabBookEntry()
        {
            return PartialView();
        }
        public PartialViewResult _SmplFabBookEntryDtl()
        {
            return PartialView();
        }
        public PartialViewResult _SmplFabBookStyleItem()
        {
            return PartialView();
        }
        public PartialViewResult _SmplFabBookQty()
        {
            return PartialView();
        }
        public PartialViewResult _SmplFabBookList()
        {
            return PartialView();
        }
        public PartialViewResult _SmplProdEntry()
        {
            return PartialView();
        }
        public PartialViewResult _SmplProdEntryDtl()
        {
            return PartialView();
        }       
        public PartialViewResult _SmplBuyerFeedback()
        {
            return PartialView();
        }

        public ViewResult MrcReport()
        {
            return View();
        }

        public PartialViewResult _MrcReportParams()
        {
            return PartialView();
        }







        public ActionResult BuyerInfo()
        {
            return View();
        }



        public JsonResult Save(MC_STYLE_D_ITEMModel ob)
        {
            string jsonStr = "";

            if (ob.STYL_KEY_IMG_FILE != null && ob.STYL_KEY_IMG_FILE.ContentLength > 0)
            {
                var image = Image.FromStream(ob.STYL_KEY_IMG_FILE.InputStream);
                //var thumbnailBitmap = new Bitmap(270, 300);

                //var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //var imageRectangle = new Rectangle(0, 0, 270, 330);
                //thumbnailGraph.DrawImage(image, imageRectangle);
                //thumbnailBitmap.Save(ob.STYL_KEY_IMG_FILE.FileName, image.RawFormat);
                //thumbnailGraph.Dispose();
                //thumbnailBitmap.Dispose();
                //image.Dispose();

                ob.STYL_KEY_IMG = ImageToBase64(image, image.RawFormat);
            }

            if (ob.STYL_ALT_IMG_FILE != null && ob.STYL_ALT_IMG_FILE.ContentLength > 0)
            {
                var image = Image.FromStream(ob.STYL_ALT_IMG_FILE.InputStream);
                //var thumbnailBitmap = new Bitmap(270, 300);

                //var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //var imageRectangle = new Rectangle(0, 0, 270, 330);
                //thumbnailGraph.DrawImage(image, imageRectangle);
                //thumbnailBitmap.Save(ob.STYL_ALT_IMG_FILE.FileName, image.RawFormat);
                //thumbnailGraph.Dispose();
                //thumbnailBitmap.Dispose();
                //image.Dispose();

                ob.STYL_ALT_IMG = ImageToBase64(image, image.RawFormat);
            }



            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Save();
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
                return Json(new { success = false, errors });
            }

            return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Update(MC_STYLE_D_ITEMModel ob)
        {
            string jsonStr = "";

            if (ob.STYL_KEY_IMG_FILE != null && ob.STYL_KEY_IMG_FILE.ContentLength > 0)
            {
                var image = Image.FromStream(ob.STYL_KEY_IMG_FILE.InputStream);
                //var thumbnailBitmap = new Bitmap(270, 300);

                //var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //var imageRectangle = new Rectangle(0, 0, 270, 330);
                //thumbnailGraph.DrawImage(image, imageRectangle);
                //thumbnailBitmap.Save(ob.STYL_KEY_IMG_FILE.FileName, image.RawFormat);
                //thumbnailGraph.Dispose();
                //thumbnailBitmap.Dispose();
                //image.Dispose();



                MemoryStream ms = new MemoryStream();
                // Convert Image to byte[]
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                ob.STYL_KEY_IMG = Convert.ToBase64String(imageBytes);
                image.Dispose();



            }

            if (ob.STYL_ALT_IMG_FILE != null && ob.STYL_ALT_IMG_FILE.ContentLength > 0)
            {
                var image = Image.FromStream(ob.STYL_ALT_IMG_FILE.InputStream);
                //var thumbnailBitmap = new Bitmap(270, 300);

                //var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                //var imageRectangle = new Rectangle(0, 0, 270, 330);
                //thumbnailGraph.DrawImage(image, imageRectangle);
                //thumbnailBitmap.Save(ob.STYL_ALT_IMG_FILE.FileName, image.RawFormat);
                //thumbnailGraph.Dispose();
                //thumbnailBitmap.Dispose();
                //image.Dispose();

                MemoryStream ms = new MemoryStream();
                // Convert Image to byte[]
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                ob.STYL_ALT_IMG = Convert.ToBase64String(imageBytes);
                image.Dispose();
            }



            try
            {
                jsonStr = ob.Update();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }


            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        jsonStr = ob.Update();
            //    }
            //    catch (Exception e)
            //    {
            //        ModelState.AddModelError("", e.Message);
            //    }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Json(new { success = false, errors });
            //}

            return Json(new { success = true, jsonStr }, JsonRequestBehavior.AllowGet);

        }


        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public ActionResult OfficeInfo()
        {
            return View();
        }
        public ActionResult Size()
        {
            return View();
        }
        public ActionResult Colour()
        {
            return View();
        }
        public PartialViewResult _OfficeList()
        {
            return PartialView();
        }
        public PartialViewResult _ColourMaster()
        {
            return PartialView();
        }

        public PartialViewResult _SizeMaster()
        {
            return PartialView();
        }
        public PartialViewResult _BuyerEntry()
        {
            return PartialView();
        }

        public PartialViewResult _BuyerList()
        {
            return PartialView();
        }

        public PartialViewResult _OfficeEntry()
        {
            return PartialView();
        }

        public ActionResult Tna()
        {
            return View();
        }


        public ActionResult StyleMaster()
        {
            return View();
        }

        public ActionResult Task()
        {
            return View();
        }

        public PartialViewResult _StyleMaster(string viewName = "_StyleMaster")
        {
            return PartialView(viewName);
        }


        public PartialViewResult _StyleDItem(string viewName="_StyleDItem")
        {
            return PartialView(viewName);
        }


        public PartialViewResult _Task()
        {
            return PartialView();
        }

        public PartialViewResult _TnaTemplate()
        {
            return PartialView();
        }

        public PartialViewResult _TnaTemplateList()
        {
            return PartialView();
        }

        public PartialViewResult _TnaTemplateDtl()
        {
            return PartialView();
        }

        public ActionResult BuyerPermission()
        {
            return View();
        }

        public ActionResult Inquiry()
        {
            return View();
        }


        public PartialViewResult _InquiryList()
        {
            return PartialView();
        }

        public PartialViewResult _Inquiry()
        {
            return PartialView();
        }
        public PartialViewResult _InqStyle()
        {
            return PartialView();
        }

        public PartialViewResult _InqStyleList()
        {
            return PartialView();
        }

        public PartialViewResult _InqItem()
        {
            return PartialView();
        }

        public PartialViewResult _InqItemList()
        {
            return PartialView();
        }

        public PartialViewResult _InqComponent()
        {
            return PartialView();
        }

        public PartialViewResult _InqComponentList()
        {
            return PartialView();
        }




        public PartialViewResult _BuyerPermission()
        {
            return PartialView();
        }

        public ActionResult StyleList()
        {
            return View();
        }

        public PartialViewResult _StyleList(string viewName = "_StyleList")
        {
            return PartialView(viewName);
        }
        public ActionResult LabdipInfo()
        {
            return View();
        }
        public PartialViewResult _LabdipEntry()
        {
            return PartialView();
        }

        public PartialViewResult _LabdipList()
        {
            return PartialView();
        }
        public ActionResult LabdipSubmission()
        {
            return View();
        }

        public PartialViewResult _LabdipSubmission()
        {
            return PartialView();
        }
        public ActionResult PrintDesign()
        {
            return View();
        }
        public PartialViewResult _PrintStrikeOffEntry()
        {
            return PartialView();
        }

        public PartialViewResult _PrintStrikeOffList()
        {
            return PartialView();
        }
        public FileContentResult GetFileByDItem(Int64 id)
        {
            var obList = new MC_STYLE_D_ITEMModel().Select(id);
            if (obList != null)
            {
                //Convert Base64 to String
                byte[] byt = Convert.FromBase64String(obList.STYL_KEY_IMG);
                return File(byt, "Image/jpg");
            }
            return null;
        }
        public FileContentResult GetFile(Int64 id)
        {
            var obList = new MC_STYLE_ITEM_ViewModel().StyleWiseStyleList(id);
            if (obList.Count() > 0)
            {
                //Convert Base64 to String
                byte[] byt = Convert.FromBase64String(obList[0].STYL_KEY_IMG);
                return File(byt, "Image/jpg");

                //Convert String to Base64
                //byte[] byt = System.Text.Encoding.UTF8.GetBytes(obList[0].STYL_KEY_IMG);
                //return Convert.ToBase64String(byt);
            }
            return null;
        }

        public PartialViewResult _StyleDItemFabrication(string viewName = "_StyleDItemFabrication")
        {
            return PartialView(viewName);
        }


        public PartialViewResult _StyleDItemFabricationList()
        {
            return PartialView();
        }

        public ActionResult OrderCon()
        {
            return View();
        }

        public PartialViewResult _OrderConEntry()
        {
            return PartialView();
        }

        public PartialViewResult _OrderConDtlEntry()
        {
            return PartialView();
        }
        public PartialViewResult _StyleOrderCon(string viewName = "_StyleOrderCon")
        {
            return PartialView(viewName);
        }


        public ActionResult TnaTaskPermission()
        {
            return View();
        }

        public PartialViewResult _TnaTaskPermission()
        {
            return PartialView();
        }

        public PartialViewResult _StyleColRef(string viewName = "_StyleColRef")
        {
            return PartialView(viewName);
        }


        public ActionResult BuyerSample()
        {
            return View();
        }
        public PartialViewResult _BuyerSample()
        {
            return PartialView();
        }
        public ActionResult BulkFabBkEntry()
        {
            return View();
        }

        public PartialViewResult _BulkFabBkEntry()
        {
            return PartialView();
        }

        public PartialViewResult _BulkFabBkCadCon()
        {
            return PartialView();
        }

        public PartialViewResult _BulkFabBkProcessConsDtl()
        {
            return PartialView();
        }

        public PartialViewResult _BulkFabBkList()
        {
            return PartialView();
        }

        public ActionResult GsmCountConfig()
        {
            return View();
        }

        public PartialViewResult _GsmCountConfig()
        {
            return PartialView();
        }

        public ActionResult YarnItem()
        {
            return View();
        }

        public PartialViewResult _YarnItem()
        {
            return PartialView();
        }
        public PartialViewResult _StyleBom()
        {
            return PartialView();
        }

        public ViewResult BudgetSheet()
        {
            return View();
        }

        public PartialViewResult _BudgetSheet()
        {
            return PartialView();
        }

        public ViewResult RateChart()
        {
            return View();
        }

        public PartialViewResult _RateChart()
        {
            return PartialView();
        }

        public PartialViewResult _RateChartKnitting()
        {
            return PartialView();
        }

        public PartialViewResult _RateChartDyeing()
        {
            return PartialView();
        }

        public PartialViewResult _RateChartDFin()
        {
            return PartialView();
        }

        public PartialViewResult _RateChartYD()
        {
            return PartialView();
        }

        public PartialViewResult _RateChartAOP()
        {
            return PartialView();
        }

        public PartialViewResult _MktCostBreakDown()
        {
            return PartialView();
        }

        public PartialViewResult _CombFiberConfig()
        {
            return PartialView();
        }

        public ViewResult BuyerBom()
        {
            return View();
        }
        public PartialViewResult _BuyerBom()
        {
            return PartialView();
        }

        public ViewResult TnAFollowup()
        {
            return View();
        }

        public PartialViewResult _TnAFollowup()
        {
            return PartialView();
        }
        public PartialViewResult _UpdateTnAModal()
        {
            return PartialView();
        }

        /// <summary>
        /// Control Meta Data Configuration for Trim & Accessories Booking
        /// </summary>
        public ViewResult TempTaBk()
        {
            return View();
        }
        public PartialViewResult _TempTaBk_List()
        {
            return PartialView();
        }
        public PartialViewResult _StyleOrderConDtl(string viewName = "_StyleOrderConDtl")
        {
            return PartialView(viewName);
        }

        public ViewResult TaBooking()
        {
            return View();
        }
        public PartialViewResult _TaBooking()
        {
            return PartialView();
        }

        public PartialViewResult _TaBookingItem()
        {
            return PartialView();
        }
        public PartialViewResult _TaBookingList()
        {
            return PartialView();
        }

        public ViewResult TaBookingRpt(Int64 ID)
        {
            var ob = new MC_ACCS_PO_HModel().Select(ID);
            return View(ob);
        }
        
        public PartialViewResult _TaBookingRpt()
        {
            return PartialView();
        }
        public ActionResult TNATaskStatus()
        {
            return View();
        }
        public PartialViewResult _TNATaskStatus()
        {
            return PartialView();
        }

        public ActionResult OrderSCM()
        {
            return View();
        }

        public PartialViewResult _OrderSCM()
        {
            return PartialView();
        }

        public PartialViewResult _GenerateTnaTemplate()
        {
            return PartialView();
        }
        public PartialViewResult _GenerateDevTnaTemplate()
        {
            return PartialView();
        }

        public ViewResult CollarCuffManagement()
        {
            return View();
        }

        public PartialViewResult _CollarCuffManagement()
        {
            return PartialView();
        }


        public ViewResult StyleDashboard()
        {
            return View();
        }
        public PartialViewResult _StyleDashboard()
        {
            return PartialView();
        }

        public ViewResult TnaFollowupMatrix()
        {
            return View();
        }
        public PartialViewResult _TnaFollowupMatrix()
        {
            return PartialView();
        }

        public ViewResult DyeingProg4BulkBookingMnul()
        {
            return View();
        }
        public PartialViewResult _DyeingProg4BulkBookingMnul()
        {
            return PartialView();
        }


        public ViewResult StyleListFabDev()
        {
            return View();
        }

        public PartialViewResult _StyleListFabDev()
        {
            return PartialView();
        }
        
        public PartialViewResult _StyleMasterFabDev()
        {
            return PartialView();
        }

        public PartialViewResult _StyleDItemFabricationFabDev()
        {
            return PartialView();
        }

        public PartialViewResult _StyleDFabricBookingFabDev()
        {
            return PartialView();
        }

        public ViewResult pms()
        {
            return View();
        }

        public PartialViewResult _pms()
        {
            return PartialView();
        }

        public PartialViewResult _pmsD()
        {
            return PartialView();
        }

        public ViewResult StyleOrderFollowupRpt()
        {
            return View();
        }

        public PartialViewResult _StyleOrderFollowupRpt()
        {
            return PartialView();
        }

        public ViewResult TaBookingPoRpt(Int64 ID)
        {
            var ob = new SCM_PURC_REQ_HModel().Select(ID);
            //var ob = new MC_ACCS_PO_HModel().Select(ID);
            return View(ob);
        }

        public PartialViewResult _TaBookingPoRpt()
        {
            return PartialView();
        }


        public ViewResult ProjectionOrderList()
        {
            return View();
        }

        public PartialViewResult _ProjectionOrderList()
        {
            return PartialView();
        }

        public ViewResult ProjectionOrder()
        {
            return View();
        }

        public PartialViewResult _ProjectionOrder()
        {
            return PartialView();
        }

        public ViewResult AccessoriesBookingList()
        {
            return View();
        }

        public PartialViewResult _AccessoriesBookingList()
        {
            return PartialView();
        }
        public ViewResult AccessoriesBooking()
        {
            return View();
        }

        public PartialViewResult _AccessoriesBooking()
        {
            return PartialView();
        }


        public ViewResult AddFabBking()
        {
            return View();
        }
        public PartialViewResult _AddFabBkingH()
        {
            return PartialView();
        }
        public PartialViewResult _AddFabBkingD()
        {
            return PartialView();
        }
        public PartialViewResult _AddFabBkingList()
        {
            return PartialView();
        }
        
        public ViewResult AddFabBkingRpt()
        {
            return View();
        }
        public PartialViewResult _AddFabBkingRpt()
        {
            return PartialView();
        }

        public PartialViewResult _StyleDItemFabricationModal()
        {
            return PartialView();
        }

        public ViewResult pmsAcc()
        {
            return View();
        }

        public PartialViewResult _pmsAcc()
        {
            return PartialView();
        }

        public PartialViewResult _pmsAccD()
        {
            return PartialView();
        }


        public ViewResult RateChartForInquiry()
        {
            return View();
        }
        public PartialViewResult _RateChartForInquiry()
        {
            return PartialView();
        }


    }
}