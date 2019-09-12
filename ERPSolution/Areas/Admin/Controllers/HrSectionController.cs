using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Model;
using ERPSolution.Controllers;
using ERPSolution.Common;

namespace ERPSolution.Areas.Admin.Controllers
{
    [SignInCheck]
    public class HrSectionController : BaseController
    {
        public ActionResult Index()
        {
            var sections = new HrSectionModel().SelectAll();
            return View(sections);
        }

        public ActionResult AddNew()
        {
            HrSectionViewModel HrSectionViewModel = new HrSectionViewModel
            {
                HrSectionModel = new HrSectionModel {  }   
            };
            return View();
        }

        public SelectList ParentSection()
        {
            SelectList parentList = new HrSectionModel().getParentList();
            return parentList;
        }

        public JsonResult ChildSection(string id)
        {
            SelectList childList = new HrSectionModel().getChildList(id.ToString());
            return Json(childList, JsonRequestBehavior.AllowGet); 
        }
       
	}
}