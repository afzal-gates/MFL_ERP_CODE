using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrOfficeController : BaseController
    {
        // GET: /Hr/HrOffice/
        public ActionResult Index()
        {
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        [HttpPost]
        public void Test(HttpPostedFileBase file)
        {
            string Name = Request.Form[1];
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file1 = Request.Files[0];
            }
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public string Update(HrOfficeModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
                vMsg = ob.Update();

            return vMsg;
        }

        [HttpPost]
        [MyValidateAntiForgeryToken]
        public string Save(HrOfficeModel ob)
        {
            string vMsg = "";

            if (ModelState.IsValid)
                vMsg = ob.Save();

            return vMsg;
        }


        public JsonResult CompanyGroupListData()
        {
            var ob = new HrCompanyGrpModel().CompanyGroupListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LookupListData()
        {
            Int64 lookupTableId = Convert.ToInt64(HttpContext.Request.Params["pLookupTableId"]);
            
            var ob = new LookupDataModel().LookupListData(lookupTableId);
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CountryListData()
        {
            var ob = new HrCountryModel().CountryListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimezoneListData()
        {
            var ob = new HrTimezoneModel().TimezoneListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DistrictListData(Int64? pHR_GEO_DIVISION_ID)
        {
            var ob = new HrGeoDistrictModel().DistrictListData(pHR_GEO_DIVISION_ID);

            return Json(ob, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OfficeListData()
        {
            var ob = new HrOfficeModel().OfficeListData();
            return Json(ob, JsonRequestBehavior.AllowGet);
        }



        public PartialViewResult OfficeEntry()
        {
            return PartialView();
        }


        public PartialViewResult OfficeList()
        {
            return PartialView();
        }




	}


}