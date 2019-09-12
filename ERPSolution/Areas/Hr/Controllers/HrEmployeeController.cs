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
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrEmployeeController : BaseController
    {        
        [HttpPost]
        //[MyValidateAntiForgeryToken]
        public JsonResult UploadFiles()
        {
            //var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                if (hpf.ContentLength > 0)
                {
                    string savedFileName = Path.Combine(Server.MapPath("~/UPLOAD_DOCS/EMP_PHOTOS"), Path.GetFileName(Request.Form["EMPLOYEE_CODE"] + ".jpg"));
                    System.IO.File.Delete(savedFileName);

                    
                    //var image = Image.FromStream(hpf.InputStream);
                    //var thumbnailBitmap = new Bitmap(270, 300);

                    //var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
                    //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

                    //var imageRectangle = new Rectangle(0, 0, 270, 330);
                    //thumbnailGraph.DrawImage(image, imageRectangle);

                    //thumbnailBitmap.Save(savedFileName, image.RawFormat);
                    //thumbnailGraph.Dispose();
                    //thumbnailBitmap.Dispose();
                    //image.Dispose();

                    hpf.SaveAs(savedFileName);

                    //r.Add(new UploadFilesResult()
                    //{
                    //    Name = hpf.FileName,
                    //    Length = hpf.ContentLength,
                    //    Type = hpf.ContentType
                    //});
                }
            }
            //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            return Json(new { success = true },JsonRequestBehavior.AllowGet);
        }

        public void ResizeStream(int imageSize, Stream filePath, string outputPath)
        {
            var image = Image.FromStream(filePath);

            int thumbnailSize = imageSize;
            int newWidth, newHeight;

            if (image.Width > image.Height)
            {
                newWidth = thumbnailSize;
                newHeight = image.Height * thumbnailSize / image.Width;
            }
            else
            {
                newWidth = image.Width * thumbnailSize / image.Height;
                newHeight = thumbnailSize;
            }

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);

            var thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbnailGraph.DrawImage(image, imageRectangle);

            thumbnailBitmap.Save(outputPath, image.RawFormat);
            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
        }

        

        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult EmployeeEntry()
        {
            return PartialView();
        }

        public ActionResult EmployeeEntryPay()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }
        public PartialViewResult _EmployeeEntryPay()
        {
            return PartialView();
        }

        public PartialViewResult EmployeeList()
        {
            return PartialView();
        }

        public PartialViewResult ExchangeProxy()
        {
            return PartialView();
        }

        public PartialViewResult EmployeeRejoin()
        {
            return PartialView();
        }

        public PartialViewResult _EmployeePay()
        {
            return PartialView();
        }

        public PartialViewResult EmployeeSearch()
        {
            return PartialView();
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult Update(HR_EMPLOYEEModel ob)
        {
            string vMsg = "";
            //bool vSuccess = false;

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Update();
                    
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
        [MyValidateAntiForgeryToken]
        public JsonResult Save(HR_EMPLOYEEModel ob)
        {

            string jsonStr = "";
            //DateTime? vJoinDate = Convert.ToDateTime(ob.JOINING_DT).AddDays(3);

            //if (Convert.ToDateTime(ob.JOINING_DT) <= DateTime.Now.AddDays(-3) || Convert.ToDateTime(ob.JOINING_DT) > DateTime.Now)
            //{
            //    ModelState.AddModelError("JOINING_DT", "Joining date should be between " + string.Format("{0:dd/MMM/yyyy}", DateTime.Now.AddDays(-3)) + " and " + string.Format("{0:dd/MMM/yyyy}", DateTime.Now));
            //}

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

            return Json(new { success = true, jsonStr });
        }


        
        [HttpPost]
        [MyValidateAntiForgeryToken]
        public JsonResult ExchangeProxyUpdate(Int64 pHR_EMPLOYEE_ID, string pTA_PROXI_ID, string pTA_PROXI_ID_OLD)
        {
            
            string vMsg = "";
            
            if (pHR_EMPLOYEE_ID == null)
                ModelState.AddModelError("HR_EMPLOYEE_ID", "Please select an employee");
            if (pTA_PROXI_ID == null)
                ModelState.AddModelError("TA_PROXI_ID", "Please input new proxy ID");
            if (pTA_PROXI_ID == pTA_PROXI_ID_OLD)
                ModelState.AddModelError("TA_PROXI_ID", "Sorry! old and new proxy ID are same");
            if (pTA_PROXI_ID.Length!=10)
                ModelState.AddModelError("TA_PROXI_ID", "Please input 10 charecter for new proxy ID");
            //if (pTA_PROXI_ID_OLD.Length != 10)
            //    ModelState.AddModelError("TA_PROXI_ID_OLD", "Please input 10 charecter for old proxy ID");

            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = new HR_EMPLOYEEModel().ExchangeProxyUpdate(pHR_EMPLOYEE_ID, pTA_PROXI_ID);

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
        [MyValidateAntiForgeryToken]
        public JsonResult EmployeeRejoinSave(HR_EMP_REJOINModel ob)
        {
            string vMsg = "";
            
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = ob.Save();

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
        [MyValidateAntiForgeryToken]
        public JsonResult EmployeeRejoinDelete(HR_EMP_REJOINModel ob)
        {
            string vMsg = "";
            
            if (ModelState.IsValid)
            {
                try
                {
                    vMsg = new HR_EMP_REJOINModel().Delete();

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
        [MyValidateAntiForgeryToken]
        public JsonResult EmployeeGrossUpdate(HR_EMP_GROSS_UPDATEModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.EmployeeGrossUpdate();

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

            return Json(new { success = true, jsonStr });
        }


        public JsonResult EmployeeListData(Int64? pHR_DEPARTMENT_ID, Int64? pLK_JOB_STATUS_ID, string pEMPLOYEE_CODE, string pOLD_EMP_CODE)
        {

            var ob = new HR_EMPLOYEEModel().EmployeeListData(pHR_DEPARTMENT_ID, pLK_JOB_STATUS_ID, pEMPLOYEE_CODE, pOLD_EMP_CODE);            
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmployeeAutoData(string pEMPLOYEE_CODE, string pLK_JOB_STATUS_ID, Int64? pHR_COMPANY_ID)
        {
            var ob = new HR_EMPLOYEEModel().EmployeeAutoData(pEMPLOYEE_CODE, pLK_JOB_STATUS_ID, pHR_COMPANY_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LookupListData(int pLookupTableId)
        {           
            var ob = new LookupDataModel().LookupListData(pLookupTableId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LineListData()
        {
            //List<LookupDataModel> ob = new List<LookupDataModel>();
            List<SelectListItem> letterList = new List<SelectListItem>();

            int x = 1;
            while (x <= 50)
            {
                letterList.Add(new SelectListItem { Text = x.ToString(), Value = x.ToString() });
                x = x + 1;
            }

            return Json(letterList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CompanyListData()
        {
            var ob = new HrCompanyModel().CompanyListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OfficeListData()
        {
            var ob = new HrOfficeModel().OfficeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ParentDeptListData()
        {
            var ob = new HrDepartmentModel().ParentDeptListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeptListData(Int64? pPARENT_ID)
        {
            var ob = new HrDepartmentModel().DeptListData(pPARENT_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DesigListData(Int64? pOrganoDeptId)
        {
            var ob = new HrDesigModel().DesigListData(pOrganoDeptId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GradeListData()
        {
            var ob = new HrGradeModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PeriodTypeListData()
        {            
            var ob = new HrPeriodTypeModel().PeriodTypeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountryListData()
        {
            var ob = new HrCountryModel().CountryListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShiftPlanListData()
        {
            var ob = new HrShiftPlanModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShiftTeamListData(Int64? pHR_SHIFT_PLAN_ID)
        {
            var ob = new HrShiftTeamModel().ShiftTeamListData(pHR_SHIFT_PLAN_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GeoDivisionListData()
        {
            var ob = new HR_GEO_DIVISIONModel().SelectAll();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpozilaListData(Int64? pHR_GEO_DISTRICT_ID)
        {
            var ob = new HR_GEO_UPOZILAModel().UpozilaListData(pHR_GEO_DISTRICT_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RejoinListData(Int64 pHR_COMPANY_ID, Int64 pACC_PAY_PERIOD_ID)
        {
            var ob = new HR_EMP_REJOINModel().RejoinListData(pHR_COMPANY_ID, pACC_PAY_PERIOD_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PayElementListData(string pIS_ACTIVE = null, string pIS_CORE_SAL_PART = null, Int64? pLK_PAY_ELM_TYPE_ID = null)
        {
            var ob = new HR_PAY_ELEMENTModel().PayElementListData(pIS_ACTIVE, pIS_CORE_SAL_PART, pLK_PAY_ELM_TYPE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EmpPayListData(Int64? pHR_EMPLOYEE_ID)
        {
            var ob = new HR_EMP_PAYModel().EmpPayListData(pHR_EMPLOYEE_ID);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SalaryBreakupListData(Int64? pGROSS_SALARY)
        {
            var ob = new HR_PAY_ELEMENTModel().SalaryBreakupListData(pGROSS_SALARY);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }


    }
}