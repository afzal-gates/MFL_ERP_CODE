using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Security.Api
{
    [RoutePrefix("api/security")]
    public class UserStoreOfficeMappingController : ApiController
    {
        [Route("UserMap/GetStoreUserByID")]
        [HttpGet]
        // GET :  /api/security/UserMap/GetStoreUserByID
        public IHttpActionResult GetStoreUserByID(Int64? pSC_USER_ID = null)
        {
            try
            {
                var list = new SC_MAP_STR_USRModel().SelectByID(pSC_USER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UserMap/SaveSM")]
        [HttpPost]
        // GET :  /api/security/UserMap/SaveSM
        public IHttpActionResult SaveSM([FromBody] SC_MAP_STR_USRModel ob)
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

        [Route("UserMap/GetOfficeUserByID")]
        [HttpGet]
        // GET :  /api/security/UserMap/GetOfficeUserByID
        public IHttpActionResult GetOfficeUserByID(Int64? pSC_USER_ID = null)
        {
            try
            {
                var list = new KNT_MAP_USR_COMPModel().SelectByID(pSC_USER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UserMap/SaveOM")]
        [HttpPost]
        // GET :  /api/security/UserMap/SaveOM
        public IHttpActionResult SaveOM([FromBody] KNT_MAP_USR_COMPModel ob)
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


        [Route("UserMap/GetProdCatUserByID")]
        [HttpGet]
        // GET :  /api/security/UserMap/GetProdCatUserByID
        public IHttpActionResult GetProdCatUserByID(Int64? pSC_USER_ID = null)
        {
            try
            {
                var list = new SC_MAP_USR_PRD_CATModel().SelectByID(pSC_USER_ID);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UserMap/SavePCM")]
        [HttpPost]
        // GET :  /api/security/UserMap/SavePCM
        public IHttpActionResult SavePCM([FromBody] SC_MAP_USR_PRD_CATModel ob)
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



        [Route("UserMap/GetFabDfctTypeMapByID")]
        [HttpGet]
        // GET :  /api/security/UserMap/GetFabDfctTypeMapByID?pLK_FAB_INSP_TYPE_ID=&pOption=
        public IHttpActionResult GetFabDfctTypeMapByID(Int64? pLK_FAB_INSP_TYPE_ID = null, Int64? pOption = null)
        {
            try
            {
                var list = new SC_MAP_FAB_DFCT_TYPModel().SelectByID(pLK_FAB_INSP_TYPE_ID, pOption);
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UserMap/SaveFDT")]
        [HttpPost]
        // GET :  /api/security/UserMap/SaveFDT
        public IHttpActionResult SaveFDT([FromBody] SC_MAP_FAB_DFCT_TYPModel ob)
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

        [Route("UserMap/GetBuyerStoreByID")]
        [HttpGet]
        // GET :  /api/security/UserMap/GetBuyerStoreByID
        public IHttpActionResult GetBuyerStoreByID(Int64? pSCM_STORE_ID = null)
        {
            try
            {
                var list = new SC_MAP_BYR_STRModel().SelectByID(pSCM_STORE_ID).OrderBy(x=>x.BYR_ACC_GRP_NAME_EN).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("UserMap/SaveBSM")]
        [HttpPost]
        // GET :  /api/security/UserMap/SaveBSM
        public IHttpActionResult SaveBSM([FromBody] SC_MAP_BYR_STRModel ob)
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

    }
}
