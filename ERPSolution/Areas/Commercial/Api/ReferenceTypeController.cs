using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Commercial.Api
{
    [RoutePrefix("api/cmr")]
    public class ReferenceTypeController : ApiController
    {
        [Route("ReferenceTyp/GetPayTerm")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetPayTerm
        public IHttpActionResult GetPayTerm()
        {
            try
            {
                var list = new RF_PAYM_TERMModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SavePT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SavePT
        public IHttpActionResult SavePT([FromBody] RF_PAYM_TERMModel ob)
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

        [Route("ReferenceTyp/DeletePT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeletePT
        public IHttpActionResult DeletePT([FromBody] RF_PAYM_TERMModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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


        [Route("ReferenceTyp/GetIncoTerm")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetIncoTerm
        public IHttpActionResult GetIncoTerm()
        {
            try
            {
                var list = new RF_INCO_TERMModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveIT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SavePT
        public IHttpActionResult SaveIT([FromBody] RF_INCO_TERMModel ob)
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

        [Route("ReferenceTyp/DeleteIT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteIT
        public IHttpActionResult DeleteIT([FromBody] RF_INCO_TERMModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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

        [Route("ReferenceTyp/GetDeliveryPlace")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetDeliveryPlace
        public IHttpActionResult GetDeliveryPlace()
        {
            try
            {
                var list = new RF_DELV_PLCModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveDP")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SaveDP
        public IHttpActionResult SaveDP([FromBody] RF_DELV_PLCModel ob)
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


        [Route("ReferenceTyp/DeleteDP")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteDP
        public IHttpActionResult DeleteDP([FromBody] RF_DELV_PLCModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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



        [Route("ReferenceTyp/GetTransitPort")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetTransitPort
        public IHttpActionResult GetTransitPort()
        {
            try
            {
                var list = new RF_TRAN_PORTModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveTP")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SaveTP
        public IHttpActionResult SaveTP([FromBody] RF_TRAN_PORTModel ob)
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


        [Route("ReferenceTyp/DeleteTP")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteTP
        public IHttpActionResult DeleteTP([FromBody] RF_TRAN_PORTModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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


        
        [Route("ReferenceTyp/GetLCType")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetLCType
        public IHttpActionResult GetLCType()
        {
            try
            {
                var list = new RF_LC_TYPEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveLT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SaveLT
        public IHttpActionResult SaveLT([FromBody] RF_LC_TYPEModel ob)
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


        [Route("ReferenceTyp/DeleteLT")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteLT
        public IHttpActionResult DeleteLT([FromBody] RF_LC_TYPEModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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


        [Route("ReferenceTyp/GetShipMode")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetShipMode
        public IHttpActionResult GetShipMode()
        {
            try
            {
                var list = new RF_SHIP_MODEModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveSM")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SaveSM
        public IHttpActionResult SaveSM([FromBody] RF_SHIP_MODEModel ob)
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


        [Route("ReferenceTyp/DeleteSM")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteSM
        public IHttpActionResult DeleteSM([FromBody] RF_SHIP_MODEModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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


        [Route("ReferenceTyp/GetGmtAsn")]
        [HttpGet]
        // GET :  /api/cmr/ReferenceTyp/GetGmtAsn
        public IHttpActionResult GetGmtAsn()
        {
            try
            {
                var list = new RF_GMT_ASNModel().SelectAll();
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("ReferenceTyp/SaveGA")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/SaveGA
        public IHttpActionResult SaveGA([FromBody] RF_GMT_ASNModel ob)
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


        [Route("ReferenceTyp/DeleteGA")]
        [HttpPost]
        // GET :  api/Cmr/ReferenceTyp/DeleteGA
        public IHttpActionResult DeleteGA([FromBody] RF_GMT_ASNModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();
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
