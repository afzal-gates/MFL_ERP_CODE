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
    public class LookupTablesController : BaseController
    {        //
        // GET: /Admin/LookupTables/
        [HttpGet]
        public ActionResult Index()
        {
            var lookupTables = new LookupTablesModel().SelectAll();
            return View(lookupTables);
        }

        //
        // GET: /Admin/LookupTables/Details/5
        public ActionResult LookupDataIndex(long id=0, long dataId=0)
        {
            var lookupTables = new LookupTablesModel().SelectAll();    
            if (id!=null)
            {
                var value = new LookupDataModel().SelectLookupDatas(id);
                ViewData["value"] = value;
                if (dataId != null)
                {
                    var data = new LookupDataModel().Select(dataId);
                    ViewData["data"] = data;

                }
            }

            return View("LookupDataIndex", lookupTables);
        }


        [HttpPost]
        public ActionResult LookupDataIndex(LookupDataModel ob)
        {
            
                // TODO: Add insert logic here
                //if (ModelState.IsValid)
                //{
                    if (ob.LOOKUP_DATA_ID<=0)
                    {
                        Session["vMsg"] = ob.Save();
                    }
                    else
                    {
                        Session["vMsg"] = ob.Update();
                    }
                    return RedirectToAction("LookupDataIndex", new { id=ob.LOOKUP_TABLE_ID });
                //}

                //return View(ob);
        }

        public ActionResult LookupDataAdd(int id)
        {
            
            return View();
        }

        public ActionResult AddNewCursor()
        {
            var lookupTables = new LookupTablesModel().SelectAll();
            return View(lookupTables);
        }

        //[HttpPost]  No Procedure/Sql found
        //public ActionResult AddNewCursor(List<LookupTablesModel> obList)
        //{
        //    LookupTablesModel ob = new LookupTablesModel();
        //    if (ModelState.IsValid )
        //    {

        //        obBLL.SaveList(obList);
        //        RedirectToAction("Index");
        //    }

        //    return View();
        //}

        
        //
        // GET: /Admin/LookupTables/Create
        public ActionResult AddNew()
        {
            LookupTablesModel ob = new LookupTablesModel();
            return View(ob);
        }

        //
        // POST: /Admin/LookupTables/Create
        [HttpPost]
        public string AddNew(LookupTablesModel ob)
        {
            try
            {
                string vMsg = "";
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    vMsg = ob.Save();
                    //return vMsg;
                }

                return vMsg;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        //
        // GET: /Admin/LookupTables/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var lookupTable = new LookupTablesModel().Select(id);
            return View(lookupTable);
        }

        //
        // POST: /Admin/LookupTables/Edit/5
        [HttpPost]
        public string Edit(LookupTablesModel ob)
        {
            try
            {
                string vMsg = "";
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //Session["vMsg"] = obBLL.Update(ob);
                    vMsg = ob.Update();

                    //return RedirectToAction("Index");
                    //Redirect("/Admin/LookupTables/Index");
                    return vMsg;
                }
                return vMsg;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public JsonResult GetCity(string id)
        {

            List<SelectListItem> City = new List<SelectListItem>();

            switch (id)
            {

                case "20":

                    City.Add(new SelectListItem { Text = "Select", Value = "0" });

                    City.Add(new SelectListItem { Text = "MUMBAI", Value = "1" });

                    City.Add(new SelectListItem { Text = "PUNE", Value = "2" });

                    City.Add(new SelectListItem { Text = "KOLHAPUR", Value = "3" });

                    City.Add(new SelectListItem { Text = "RATNAGIRI", Value = "4" });

                    City.Add(new SelectListItem { Text = "NAGPUR", Value = "5" });

                    City.Add(new SelectListItem { Text = "JALGAON", Value = "6" });

                    break;

            }

            return Json(new SelectList(City, "Value", "Text"));

        }

        //
        // GET: /Admin/LookupTables/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/LookupTables/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
