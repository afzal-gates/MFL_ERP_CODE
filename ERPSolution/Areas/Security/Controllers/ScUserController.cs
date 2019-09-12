using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;
using System.Drawing.Text;
using System.Web.Routing;
using System.Collections;
using System.Net;
using Newtonsoft.Json.Linq;
using FluentScheduler;
using System.Web.Hosting;
using Postal;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERPSolution.Areas.Security.Controllers
{
    public struct Number
    {
        private int n;

        public Number(int value)
        {
            n = value;
        }

        public int Value
        {
            get { return n; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Number))
                return false;
            else
                return n == ((Number)obj).n;
        }

        public override int GetHashCode()
        {
            return n;
        }

        public override string ToString()
        {
            return n.ToString();
        }
    }



    public class ScUserController : BaseController
    {
        //ScUserBLL obBLL = new ScUserBLL();



        [SignInCheck]
        public ActionResult Index()
        {
            return View();
        }



        ///////////////////////////////////////

        //[MyValidateAntiForgeryToken]
        public FileContentResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            //generate new question 
            int a = rand.Next(10, 99);
            int b = rand.Next(1, 9);
            var captcha = string.Format("{0} + {1} = ?", a, b);

            //store answer 
            Session["multiCaptcha" + prefix] = a + b;

            //image stream 
            FileContentResult img = null;

            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(130, 40))
            using (var gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.Bisque, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise 
                if (noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)),
                        (rand.Next(0, 255)));

                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);

                        //gfx.DrawEllipse(pen, (x – r), (y – r), r, r); 
                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question 
                gfx.DrawString(captcha, new Font("Tahoma", 17), Brushes.Gray, 2, 3);

                //render as Jpeg 
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
            //return PartialView("_CaptchaImage", img);
        }


        public PartialViewResult _CaptchaImage()
        {
            return PartialView();
        }



        public PartialViewResult _CreateUser()
        {
            return PartialView();
        }

        public PartialViewResult _ChangePassword()
        {
            return PartialView();
        }




        [HttpPost]
        public JsonResult SaveUser(ScUserModel ob1, string p)
        {
            string vMsg = "";


            string hasspw = GenerateHashWithSalt(p, ob1.LOGIN_ID.ToUpper());
            ob1.PASSWORD_HASH = hasspw;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob1.SaveUser();

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

            return Json(new { success = true, vMsg });
        }


        [HttpPost]
        public JsonResult UpdateUser(ScUserModel ob2, string p)
        {
            string vMsg = "";


            string hasspw = GenerateHashWithSalt(p, ob2.LOGIN_ID.ToUpper());
            ob2.PASSWORD_HASH = hasspw;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob2.Update();

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

            return Json(new { success = true, vMsg });
        }




        //[HttpPost]
        //[SignInCheck]
        //public ActionResult SignUp(ScUserModel ob)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            obBLL.Save(ob);

        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            return View(ob);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return View(ob);
        //    }
        //}

        public ActionResult SignIn()
        {
            Session.Clear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            HttpContext.Cache.Remove("MultiTEXCache");

            return View();
        }

        private string vLoginId { get; set; }
        private string vPass { get; set; }

        [HttpPost]
        public ActionResult SignIn(LoginModel ob)
        {
            try
            {
                string vCapthaValue = Session["multiCaptcha"].ToString();

                // Remove the following line after development
                //ob.CAPTCHA_VALUE = vCapthaValue;

                if (ob.CAPTCHA_VALUE != vCapthaValue)
                {
                    ModelState.AddModelError("CAPTCHA_VALUE", "Please input how much displayed below image");
                    ModelState["CAPTCHA_VALUE"].Value = null;
                    ob.CAPTCHA_VALUE = "";

                    return View(ob);
                }

                string vPassHashSalt = "";

                vLoginId = ob.LOGIN_ID.ToUpper();
                vPass = ob.PASSWORD_HASH;

                vPassHashSalt = GenerateHashWithSalt(vPass, vLoginId);


                bool vIsUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper() && m.PASSWORD_HASH == vPassHashSalt);


                //vIsUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper() && m.PASSWORD_HASH == vPassHashSalt);





                //ScUserModel obUser = new ScUserModel();                
                //ScUserModel obUser = obBLL.SelectAll().Where(m => m.LOGIN_ID == ob.LOGIN_ID).ToList()[0];
                //SessionHandler.SetRole(obUser.Role.ToString());

                //HttpContext.Session.Add("multiLoginId",ob.LOGIN_ID);




                if (vIsUser)
                {
                    var obUser = new ScUserModel().SelectAll().Where(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper()).ToList()[0];


                    Session["multiLoginEmpCompId"] = obUser.HR_COMPANY_ID;
                    Session["multiLoginEmpParentDeptId"] = obUser.PARENT_DEPARTMENT_ID;
                    Session["multiLoginEmpCoreDeptId"] = obUser.CORE_DEPARTMENT_ID;
                    
                    Session["multiLoginEmpId"] = obUser.HR_EMPLOYEE_ID;
                    Session["multiLoginDeptId"] = obUser.HR_DEPARTMENT_ID;
                    Session["multiLoginEmpCode"] = obUser.EMPLOYEE_CODE;
                    Session["multiLoginEmpName"] = obUser.EMP_FULL_NAME_EN;
                    Session["multiLoginEmpDesigName"] = obUser.DESIGNATION_NAME_EN;
                    Session["multiLoginEmpDesigGrpOrder"] = obUser.DSG_GRP_ORDER;
                    Session["multiLoginEmpCoreDeptName"] = obUser.CORE_DEPARTMENT_NAME_EN;
                    Session["multiLoginEmpSectionName"] = obUser.CORE_DEPARTMENT_NAME_EN + '-' + obUser.DEPARTMENT_NAME_EN;


                    Session["multiScUserId"] = obUser.SC_USER_ID;
                    
                    Session["multiUserType"] = obUser.USER_TYPE;
                    Session["multiUserPic"] = obUser.EMPLOYEE_CODE + ".jpg";
                    Session["multiLoginId"] = ob.LOGIN_ID;
                    Session["multiUserPosCounterId"] = obUser.PS_COUNTR_ID;
                    Session["multiUserPosCounterNo"] = obUser.COUNTER_NO;
                    Session["multiUserPosStoreId"] = obUser.PS_STORE_ID;
                    Session["multiUserPosStoreNameEn"] = obUser.STORE_NAME_EN;
                    Session["multiValidMenuId"] = "";
                    Session["access_token"] = getOauthToken(ob.LOGIN_ID, ob.PASSWORD_HASH);

                    Session["multiCurrGroupNameE"] = "MULTIFABS LIMITED";
                    Session["multiCurrGroupNameB"] = "MULTIFABS LIMITED";

                    Session["multiCurrCompanyNameE"] = "MULTIFABS LIMITED";// "Multifabs Factory Office (Unit-1)";
                    Session["multiCurrCompanyAddressE"] = "Nayapara, Kashimpur, Gazipur.";
                    Session["multiCurrCompanyNameB"] = "মাল্টিফ্যাবস লিমিটেড";// "মাল্টিফ্যাবস লিমিটেড";
                    Session["multiCurrCompanyAddressB"] = "নয়াপাড়া, কাশিমপুর. গাজীপুর।";

                    Session["multiServerCurrDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    Session["multiLoginTime"] = DateTime.Now.ToString("yyyyMMddHHmm");
                    Session["scRoleId"] = obUser.SC_ROLE_ID;
                    //string a = Session["multiLoginId"].ToString();
                    //return RedirectToAction("Index", "Home", new { area = "" }); 

                    
                    
                    //============ Redirect memorable word page ================
                    if (obUser.IS_PWD_CNG_LOGON == "Y")
                    {
                        return RedirectToAction("Index", "Home", new { area = "" });
                        //return RedirectToAction("_ChangePasswordModal", "Home", new { area = "" });
                    }
                    else
                    {
                        //return RedirectToAction("LoginMemorable", "ScUser", new { area = "Security" });
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }

                }
                else
                {
                    ModelState.AddModelError("PASSWORD_HASH", "Please input valid password");
                    //ModelState.Remove("PASSWORD_HASH");
                    //ModelState.Clear();
                    ModelState["PASSWORD_HASH"].Value = null;
                    ModelState["CAPTCHA_VALUE"].Value = null;
                    ob.PASSWORD_HASH = "";
                    ob.CAPTCHA_VALUE = "";

                    return View(ob);
                }

                //Add to cookie
                //HttpCookie myCookie = new HttpCookie("multiLoginId", obUser.LOGIN_ID);               
                //Response.Cookies.Add(myCookie);


                //Get data from cookie
                //string x = "";
                //HttpCookie myCookie = new HttpCookie("multiLoginId");
                //myCookie = Request.Cookies["multiLoginId"];
                //x = myCookie.Value;

                //Random rnd = new Random();
                //int randomN = rnd.Next(Int32.MinValue, Int32.MaxValue);
                //Number n = new Number(randomN);


                // a = string.Format("n = {0,12}, hash code = 0x{1:X8}", n, n.GetHashCode());

                //for (int ctr = 0; ctr <= 9; ctr++)
                //{
                //    int randomN = rnd.Next(Int32.MinValue, Int32.MaxValue);
                //    Number n = new Number(randomN);
                //    //Console.WriteLine("n = {0,12}, hash code = {1,12}", n, n.GetHashCode());

                //    a = string.Format("n = {0,12}, hash code = 0x{1:X8}", n, n.GetHashCode());
                //}  



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("PASSWORD_HASH", ex.Message);
                ModelState["PASSWORD_HASH"].Value = null;
                ModelState["CAPTCHA_VALUE"].Value = null;
                ob.PASSWORD_HASH = "";
                ob.CAPTCHA_VALUE = "";

                return View(ob);
                //throw ex;
            }
        }


        [HttpPost]
        public ActionResult PromptSignIn(LoginModel ob)
        {
            try
            {
                string vPassHashSalt = "";

                vLoginId = ob.LOGIN_ID.ToUpper();
                vPass = ob.PASSWORD_HASH;

                vPassHashSalt = GenerateHashWithSalt(vPass, vLoginId);

                bool vIsUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper() && m.PASSWORD_HASH == vPassHashSalt);

                if (vIsUser)
                {
                    var obUser = new ScUserModel().SelectAll().Where(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper()).ToList()[0];

                    Session["multiLoginEmpCompId"] = obUser.HR_COMPANY_ID;
                    Session["multiLoginEmpParentDeptId"] = obUser.PARENT_DEPARTMENT_ID;
                    Session["multiLoginEmpCoreDeptId"] = obUser.CORE_DEPARTMENT_ID;

                    Session["multiLoginEmpId"] = obUser.HR_EMPLOYEE_ID;
                    Session["multiLoginDeptId"] = obUser.HR_DEPARTMENT_ID;
                    Session["multiLoginEmpCode"] = obUser.EMPLOYEE_CODE;
                    Session["multiLoginEmpName"] = obUser.EMP_FULL_NAME_EN;
                    Session["multiLoginEmpDesigName"] = obUser.DESIGNATION_NAME_EN;
                    Session["multiLoginEmpDesigGrpOrder"] = obUser.DSG_GRP_ORDER;
                    Session["multiLoginEmpCoreDeptName"] = obUser.CORE_DEPARTMENT_NAME_EN;
                    Session["multiLoginEmpSectionName"] = obUser.CORE_DEPARTMENT_NAME_EN + '-' + obUser.DEPARTMENT_NAME_EN;


                    Session["multiScUserId"] = obUser.SC_USER_ID;

                    Session["multiUserType"] = obUser.USER_TYPE;
                    Session["multiUserPic"] = obUser.EMPLOYEE_CODE + ".jpg";
                    Session["multiLoginId"] = ob.LOGIN_ID;
                    Session["multiUserPosCounterId"] = obUser.PS_COUNTR_ID;
                    Session["multiUserPosCounterNo"] = obUser.COUNTER_NO;
                    Session["multiUserPosStoreId"] = obUser.PS_STORE_ID;
                    Session["multiUserPosStoreNameEn"] = obUser.STORE_NAME_EN;
                    Session["multiValidMenuId"] = "";
                    Session["access_token"] = getOauthToken(ob.LOGIN_ID, ob.PASSWORD_HASH);

                    Session["multiCurrGroupNameE"] = "MULTIFABS LIMITED";
                    Session["multiCurrGroupNameB"] = "MULTIFABS LIMITED";

                    Session["multiCurrCompanyNameE"] = "MULTIFABS LIMITED";// "Multifabs Factory Office (Unit-1)";
                    Session["multiCurrCompanyAddressE"] = "Nayapara, Kashimpur, Gazipur.";
                    Session["multiCurrCompanyNameB"] = "মাল্টিফ্যাবস লিমিটেড";// "মাল্টিফ্যাবস লিমিটেড";
                    Session["multiCurrCompanyAddressB"] = "নয়াপাড়া, কাশিমপুর. গাজীপুর।";

                    Session["multiServerCurrDate"] = DateTime.Now.ToString("dd-MMM-yyyy");
                    Session["multiLoginTime"] = DateTime.Now.ToString("yyyyMMddHHmm");
                    Session["scRoleId"] = obUser.SC_ROLE_ID;

                    return Content("Success");

                }
                else
                {
                    ModelState.AddModelError("PASSWORD_HASH", "Please input valid password");
                    ModelState["PASSWORD_HASH"].Value = null;
                    ModelState["CAPTCHA_VALUE"].Value = null;
                    ob.PASSWORD_HASH = "";
                    ob.CAPTCHA_VALUE = "";
                    return Content("Error");
        }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("PASSWORD_HASH", ex.Message);
                ModelState["PASSWORD_HASH"].Value = null;
                ModelState["CAPTCHA_VALUE"].Value = null;
                ob.PASSWORD_HASH = "";
                ob.CAPTCHA_VALUE = "";

                return Content("Error");
            }
        }



        public static void AutoMail4CapBk(string pATTACH_FILE_PATH)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_ORDER_HModel ob = new MC_ORDER_HModel();
            var rptData = ob.GetCapBkData4MailAutoSend();

            var email = new AutoMail4CapBkService
            {
                To = rptData.MAIL_ADD_LST,
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
                
        public static void AutoMailSend4OrderConfirmList()
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            MC_ORDER_HModel ob = new MC_ORDER_HModel();
            var rptData = ob.GetOrdCnfrmList4MailAutoSend();

            var email = new AutoMailSend4OrderConfirmListService
            {                
                data = rptData
            };

            emailService.Send(email);
        }

        public static void SendTnASummaryMail(String To, List<TnASummaryMail> TnASumData)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new TnASummaryMailService
            {
                To = To,
                data = TnASumData

            };

            emailService.Send(email);
        }

        public static void SendDevelopmentOrderMail(String To, List<TNA_CROSS_TAB_RPTModel> orderData)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new DevelopmentOrderMailService
            {
                To = To,
                data = orderData

            };

            emailService.Send(email);
        }

        public static void SendTnATaskFailureMail(String To, List<VM_TNA_TASK_FAILURE_MAILModel> orderData, List<VM_TNA_TASK_FAILURE_MAILModel> orderDataBlk)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new TnATaskFailureMailService
            {
                To = To,
                data = orderData,
                data1 = orderDataBlk
            };

            emailService.Send(email);
        }

        public static void SendPlanChangeMail(List<GMT_PLN_STYL_CNGE_LOGModel> log, List<GMT_PLN_STYL_CNG_DEPTModel> ResDept)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new PlanChangeMailService
            {
                To = "dev.aminul@multi-fabs.com",
                log = log,
                logDept = ResDept
            };

            emailService.Send(email);
        }


        public static void SendTnAProductionMail(String To, List<KNT_BUYER_SHIP_MONTHModel> data)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));
            var emailService = new EmailService(engines);

            var email = new TnAProductionMailService
            {
                To = To,
                data = data
            };

            emailService.Send(email);
        }



        [HttpGet]
        public JsonResult TestMailDevelopmentOrder()
        {
            try
            {


                List<GMT_PLN_STYL_CNGE_LOGModel> log = new GMT_PLN_STYL_CNGE_LOGModel().getPlanChangeLogForMail();
                List<GMT_PLN_STYL_CNG_DEPTModel> logDept = new GMT_PLN_STYL_CNGE_LOGModel().getPlanChangeDeptForMail();

                SendPlanChangeMail(log, logDept);
                return Json(new { SUCCESS = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return View(ob);
                throw ex;
            }
        }



        [HttpGet]
        public JsonResult fireMail()
        {
            try
            {
                //===== For New Confirm Order Entry List =========
                AutoMailSend4OrderConfirmList();

                //===== Start mail send for Production capacity and order booking summery ==========
                string dayName = DateTime.Today.ToString("dddd");
                //if (dayName.ToUpper() == "WEDNESDAY")
                //{
                ERPSolution.Areas.Planning.Api.PlnReportController plnRptCtrl = new Planning.Api.PlnReportController();
                PlanReportModel obCapBk = new PlanReportModel();
                obCapBk.REPORT_CODE = "RPT-5003";
                obCapBk.FROM_MONTH = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                obCapBk.IS_EXCEL_FORMAT = "N";
                obCapBk.IS_EXPORT_TO_DISK = "Y";

                plnRptCtrl.PreviewReportRDLC(obCapBk);
                //}
                //===== End mail send for Production capacity and order booking summery ==========

                
                
                List<GMT_PLN_STYL_CNGE_LOGModel> log = new GMT_PLN_STYL_CNGE_LOGModel().getPlanChangeLogForMail();
                List<GMT_PLN_STYL_CNG_DEPTModel> logDept = new GMT_PLN_STYL_CNGE_LOGModel().getPlanChangeDeptForMail();
                SendPlanChangeMail(log, logDept);


                var obj = new TnASummaryMail().getUserAndMailAddress();
                if (obj.Count > 0)
                {
                    foreach (var item in obj)
                    {
                        var data = new TnASummaryMail().SelectAll(item.SC_USER_ID);
                        if (data.Count > 0)
                        {
                            SendTnASummaryMail(item.MAIL_ADDRESS, data);
                        }
                    }
                }

                //List<TNA_CROSS_TAB_RPTModel> obList = new TNA_CROSS_TAB_RPTModel().queryData();
                //if (obList.Count > 0)
                //{
                //    SendDevelopmentOrderMail("dev.aminul@multi-fabs.com", obList);
                //}

                List<VM_TNA_TASK_FAILURE_MAILModel> obList1 = new VM_TNA_TASK_FAILURE_MAILModel().SelectAll(3004);
                List<VM_TNA_TASK_FAILURE_MAILModel> obList2 = new VM_TNA_TASK_FAILURE_MAILModel().SelectAll(3005);
                if (obList1.Count > 0 || obList2.Count > 0)
                {
                    SendTnATaskFailureMail("dev.aminul@multi-fabs.com", obList1, obList2);
                }

                
                return Json(new { SUCCESS = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //return View(ob);
                throw ex;
            }
        }

        [HttpGet]
        [SignInCheck]
        public ActionResult LoginMemorable()
        {
            int x = 65;
            List<SelectListItem> letterList = new List<SelectListItem>();
            letterList.Add(new SelectListItem { Text = "Select", Value = "" });
            while (x <= 90)
            {
                char c = (char)x;

                letterList.Add(new SelectListItem { Text = c.ToString(), Value = c.ToString() });
                x = x + 1;
            }
            x = 0;
            while (x < 10)
            {
                letterList.Add(new SelectListItem { Text = x.ToString(), Value = x.ToString() });
                x = x + 1;
            }

            Random rnd = new Random();
            int randomN = rnd.Next(1, 8);
            ViewBag.firstLeterNumber = randomN;
            randomN = rnd.Next(1, 8);
            ViewBag.secondLeterNumber = randomN;
            randomN = rnd.Next(1, 8);
            ViewBag.thirdLeterNumber = randomN;

            ViewData["letterListData"] = letterList;

            return View();
        }

        [HttpPost]
        [SignInCheck]
        public ActionResult LoginMemorable(LoginMemorableModel ob)
        {
            //List<ScUserModel> u = new List<ScUserModel>();
            //u = obBLL.LoginMemorable();
            //string a = "BANGLADESH".Substring(2, 1);
            if (Session["multiAttemptNumber"] != null)
            {
                Session["multiAttemptNumber"] = Convert.ToInt32(Session["multiAttemptNumber"]) + 1;
            }
            else
            {
                Session["multiAttemptNumber"] = 1;
            }

            bool obUser = ob.LoginMemorable().Any(m => m.LOGIN_ID == Session["multiLoginId"].ToString() &&
                                                          m.MEMORABLE_TEXT.Substring(ob.FIRST_LETTER_NUMBER - 1, 1) == ob.FIRST_LETTER &&
                                                          m.MEMORABLE_TEXT.Substring(ob.SECOND_LETTER_NUMBER - 1, 1) == ob.SECOND_LETTER &&
                                                          m.MEMORABLE_TEXT.Substring(ob.THIRD_LETTER_NUMBER - 1, 1) == ob.THIRD_LETTER);

            if (obUser)
                return RedirectToAction("Index", "Home", new { area = "" });
            else if (Convert.ToInt32(Session["multiAttemptNumber"]) >= 3)
            {
                Session["multiLoginId"] = "";
                Session["multiAttemptNumber"] = 0;
                return RedirectToAction("SignIn", "ScUser", new { area = "Security" });
            }

            /////////////////////////////////////////////////////////////////////////////
            int x = 65;
            List<SelectListItem> letterList = new List<SelectListItem>();
            letterList.Add(new SelectListItem { Text = "Select", Value = "" });
            while (x <= 90)
            {
                char c = (char)x;

                letterList.Add(new SelectListItem { Text = c.ToString(), Value = c.ToString() });
                x = x + 1;
            }
            x = 0;
            while (x < 10)
            {
                letterList.Add(new SelectListItem { Text = x.ToString(), Value = x.ToString() });
                x = x + 1;
            }

            Random rnd = new Random();
            int randomN = rnd.Next(1, 8);
            ViewBag.firstLeterNumber = randomN;
            randomN = rnd.Next(1, 8);
            ViewBag.secondLeterNumber = randomN;
            randomN = rnd.Next(1, 8);
            ViewBag.thirdLeterNumber = randomN;

            ViewData["letterListData"] = letterList;
            ////////////////////////////////////////////////////////////////////////////

            return View();

        }

        public JsonResult IsValidLoginID(LoginModel ob)
        {
            //obBLL.SignIn().Where(m => m.LOGIN_ID == ob.LOGIN_ID && m.PASSWORD_HASH == ob.PASSWORD_HASH).ToList()[0]
            bool obUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == ob.LOGIN_ID.ToUpper());
            return Json(obUser, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UserDataList()
        {
            var obList = new ScUserModel().UserDataList();
            return Json(obList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckValidUserID(string Username)
        {
            bool obUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == Username.ToUpper());
            return Json(obUser, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult SaveChangePassword(ChangePasswordModel ob)
        {
            string vMsg = "";

            string vPassHashSalt = "";

            vLoginId = Convert.ToString(Session["multiLoginId"]).ToUpper();
            vPass = ob.PASSWORD_HASH_OLD;

            vPassHashSalt = GenerateHashWithSalt(vPass, vLoginId);
            bool vIsUser = new ScUserModel().SignIn().Any(m => m.LOGIN_ID.ToUpper() == vLoginId && m.PASSWORD_HASH == vPassHashSalt);

            if (vIsUser == false)
            {
                ModelState.AddModelError("PASSWORD_HASH_OLD", "Invalid old password");
            }

            vPass = ob.PASSWORD_HASH;
            vPassHashSalt = GenerateHashWithSalt(vPass, vLoginId);

            ob.SC_USER_ID = Convert.ToInt64(Session["multiScUserId"]);
            ob.PASSWORD_HASH = vPassHashSalt;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.SaveChangePassword();

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

            return Json(new { success = true, vMsg });
        }




        public JsonResult saveRoleData(Int64 SC_USER_ID, Int64 SC_ROLE_ID)
        {
            string vMsg = "";
            vMsg = new ScUserModel().saveRoleData(SC_USER_ID, SC_ROLE_ID);
            return Json(new { success = true, vMsg });
        }


        public JsonResult updateRoleData(Int64 SC_USER_ID, Int64 SC_ROLE_ID, Int64 SC_USER_ROLE_ID)
        {
            string vMsg = "";
            vMsg = new ScUserModel().updateRoleData(SC_USER_ID, SC_ROLE_ID, SC_USER_ROLE_ID);
            return Json(new { success = true, vMsg });
        }


        public JsonResult RolesByUserId(Int64 SC_USER_ID)
        {
            var obList = new ScUserModel().RolesByUserId(SC_USER_ID);
            return Json(obList, JsonRequestBehavior.AllowGet);

        }



        public PartialViewResult _UserList()
        {
            return PartialView();
        }

        //public JsonResult IsValidPassword(LoginModel ob)
        //{
        //    string vPassHashSalt = "";

        //    vLoginId = ob.LOGIN_ID;
        //    vPass = ob.PASSWORD_HASH;

        //    vPassHashSalt = GenerateHashWithSalt(vPass, vLoginId);

        //    bool obUser = obBLL.SignIn().Any(m => m.LOGIN_ID == ob.LOGIN_ID && m.PASSWORD_HASH == vPassHashSalt);
        //    return Json(obUser, JsonRequestBehavior.AllowGet);
        //}



        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }


        public string getOauthToken(string UserName, string Password)
        {
            var hostname = "http://" + Request.Url.Authority + "/token"; ;
            try
            {
                WebRequest tRequest;
                tRequest = WebRequest.Create(hostname);
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded";
                string postData = "grant_type=password&username=" + UserName + "&password=" + Password;

                Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();

                JObject o = JObject.Parse(sResponseFromServer);
                string token = o["access_token"].ToString();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();

                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}