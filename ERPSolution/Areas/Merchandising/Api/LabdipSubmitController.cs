using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Merchandising.Api
{
    [RoutePrefix("api/mrc")]
    public class LabdipSubmitController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();

        [Route("LabdipSubmit/SelectAll")]
        [HttpGet]
        // GET :  mrc/api/LabdipSubmit/SelectAll
        public IHttpActionResult SelectAll()
        {
            var obList = new MC_LD_SUBMITModel().SelectAll();
            return Ok(obList);
        }

        [Route("LabdipSubmit/Select/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/LabdipSubmit/Select
        public IHttpActionResult Select(Int64 ID)
        {
            var ob = new MC_LD_SUBMITModel().Select(ID);
            return Ok(ob);
        }

        [Route("LabdipSubmit/Save")]
        [HttpPost]
        //[Authorize]
        // GET :  mrc/api/LabdipSubmit/Save
        public IHttpActionResult Save(MC_LD_SUBMITModel ob)
        {
            string jsonStr = "";
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
                return Ok(new { success = false, errors });
            }

            return Ok(new { success = true, jsonStr });

        }

        [Route("LabdipSubmit/saveLabdipData")]
        [HttpPost]
        //[Authorize]
        // GET :  mrc/api/LabdipSubmit/saveLabdipData
        public IHttpActionResult saveLabdipData(List<MC_LD_SUBMITModel> obList)
        {
            string jsonStr = "";
            var result = "";
            foreach (var ob in obList)
            {
                if (ob.IS_ACTIVE == "Y")
                {
                    ob.MC_TNA_TASK_STATUS_ID = 0;
                    jsonStr = ob.submitLabdipData();
                    if (result == "")
                    {
                        if (jsonStr.Contains(','))
                        {
                            result = jsonStr.Split(',')[1].Split(':')[1].Replace('}', ' ').Replace('"', ' ').Trim().TrimEnd().TrimStart();
                        }
                    }
                    else
                    {
                        if (jsonStr.Contains(','))
                        {
                            result = result + "," + jsonStr.Split(',')[1].Split(':')[1].Replace('}', ' ').Replace('"', ' ').Trim().TrimEnd().TrimStart();
                        }
                    }
                }
            }
            var session = HttpContext.Current.Session;
            session["MC_LD_SUBMIT_ID"] = result;

            Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }
        
        [Route("LabdipSubmit/submitLabdipData")]
        [HttpPost]
        //[Authorize]
        // GET :  mrc/api/LabdipSubmit/submitLabdipData
        public IHttpActionResult submitLabdipData(List<MC_LD_SUBMITModel> obList)
        {
            string jsonStr = "";
            foreach (var ob in obList)
            {
                if (ob.IS_ACTIVE == "Y")
                {
                    ob.MC_TNA_TASK_STATUS_ID = (ob.MC_TNA_TASK_STATUS_ID==4 || ob.MC_TNA_TASK_STATUS_ID==11) ? 5 : 0;
                    jsonStr = ob.updateLabdipData();
                }
            }
            Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("LabdipSubmit/updateLabdipData")]
        [HttpPut]
        //[Authorize]
        // GET :  mrc/api/LabdipSubmit/updateLabdipData
        public IHttpActionResult updateLabdipData(List<MC_LD_SUBMITModel> obList)
        {
            string jsonStr = "";

            foreach (var ob in obList)
            {
                if (ob.IS_ACTIVE == "Y")
                {
                    if (ob.MC_TNA_TASK_STATUS_ID != 10)
                    {
                        ob.MC_TNA_TASK_STATUS_ID = ob.MC_TNA_TASK_STATUS_ID;
                    }
                    jsonStr = ob.updateLabdipData();
                }
            }

            //if (obList[0].MC_LD_SUBMIT_ID > 0)
            //{
            //    foreach (var ob in obList)
            //    {
            //        if (ob.IS_ACTIVE == "Y")
            //        {
            //            if (ob.IS_LD_REF1 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF1;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.IS_LD_REF2 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF2;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.IS_LD_REF2 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF2;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.MC_TNA_TASK_STATUS_ID != 6)
            //            {
            //                ob.MC_TNA_TASK_STATUS_ID = ob.MC_TNA_TASK_STATUS_ID;
            //            }
            //            jsonStr = ob.updateLabdipData();
            //        }
            //    }
            //}
            //else
            //{
            //    var session = HttpContext.Current.Session;
            //    var result = session["MC_LD_SUBMIT_ID"];
            //    string[] spRes = result.ToString().Split(',');
            //    for (int i = 0; i < spRes.Length; i++)
            //    {
            //        var ob = new MC_LD_SUBMITModel().Select(Convert.ToInt64(spRes[i]));
            //        if (ob.IS_ACTIVE == "Y")
            //        {
            //            if (ob.IS_LD_REF1 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF1;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.IS_LD_REF2 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF2;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.IS_LD_REF3 == "Y")
            //            {
            //                ob.APRV_LD_REF = ob.LD_REF2;
            //                ob.MC_TNA_TASK_STATUS_ID = 6;
            //            }
            //            if (ob.MC_TNA_TASK_STATUS_ID != 6)
            //            {
            //                ob.MC_TNA_TASK_STATUS_ID = ob.MC_TNA_TASK_STATUS_ID;
            //            }
            //            jsonStr = ob.updateLabdipData();
            //        }
            //    }
            //}
            Hub.Clients.All.LabdipRequestProgramNotif();
            return Ok(new { success = true, jsonStr });
        }

        [Route("LabdipSubmit/Update")]
        [HttpPut]
        //[Authorize]
        // GET :  mrc/api/LabdipSubmit/Update
        public IHttpActionResult Update(MC_LD_SUBMITModel ob)
        {
            string jsonStr = "";

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Update();
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
                return Ok(new { success = false, errors });
            }

            return Ok(new { success = true, jsonStr });

        }

    }
}