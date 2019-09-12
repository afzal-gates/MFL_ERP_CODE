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

namespace ERPSolution.Areas.Hr.Controllers
{
    [SignInCheck]
    public class HrLettersController : BaseController
    {        
        // GET: Test/Country
        public ActionResult Index()
        {            
            if (!IsMenuPermission()) return Redirect(Url.Content("~/")); return View();
        }

        public PartialViewResult LetterRejoin()
        {            
            return PartialView();
        }
        
        //public JsonResult AttenDataList(HR_TA_PROCESS_DATAModel ob)
        //{
        //    var obList = ob.AttenDataList();
        //    return Json(obList, JsonRequestBehavior.AllowGet);
        //}
     

    }
}