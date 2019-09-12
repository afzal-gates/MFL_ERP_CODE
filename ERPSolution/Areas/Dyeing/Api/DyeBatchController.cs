using ERP.Model;
using ERPSolution.Hubs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Dyeing.Api
{
    [RoutePrefix("api/dye")]
    [Authorize]
    [NoCache]
    public class DyeBatchController : ApiController
    {

        [Route("DyeBatch/GetDataFabReqCal")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqCal?pMC_FAB_PROD_ORD_H_LST&pFAB_COLOR_ID&pRF_FAB_TYPE_ID&pRQD_PRD_QTY&pINV_ITEM_CAT_ID
        public IHttpActionResult GetDataFabReqCal(
            string pMC_FAB_PROD_ORD_H_LST,
            Int64 pFAB_COLOR_ID,
            Int64 pRF_FAB_TYPE_ID,
            decimal pRQD_PRD_QTY,
            Int64? pINV_ITEM_CAT_ID = 34,
            Int64? pDYE_BATCH_PLAN_ID = null,
            Int64? pDYE_BATCH_SCDL_ID = null,
            string pIS_BATCH_STORE = "N"
        )
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().GetDataFabReqCal(pMC_FAB_PROD_ORD_H_LST, pDYE_BATCH_SCDL_ID, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRQD_PRD_QTY, pINV_ITEM_CAT_ID, pDYE_BATCH_PLAN_ID, pIS_BATCH_STORE);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("DyeBatch/GetDataFabReqCalScP")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqCalScP?pMC_FAB_PROD_ORD_H_LST&pFAB_COLOR_ID&pRF_FAB_TYPE_ID&pRQD_PRD_QTY&pINV_ITEM_CAT_ID
        public IHttpActionResult GetDataFabReqCal(
            string pMC_FAB_PROD_ORD_H_LST,
            Int64 pFAB_COLOR_ID,
            Int64 pRF_FAB_TYPE_ID,
            decimal pRQD_PRD_QTY,
            Int64 pDYE_SC_PRG_ISS_ID,
            Int64? pINV_ITEM_CAT_ID = 34,
            Int64? pDYE_BATCH_PLAN_ID = null
         )
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().GetDataFabReqCalScP(pMC_FAB_PROD_ORD_H_LST, pFAB_COLOR_ID, pRF_FAB_TYPE_ID, pRQD_PRD_QTY, pINV_ITEM_CAT_ID, pDYE_BATCH_PLAN_ID, pDYE_SC_PRG_ISS_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }

        [Route("DyeBatch/GetDataFabReqCalProgram")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqCalProgram?pMC_FAB_PROD_ORD_H_LST&pFAB_COLOR_ID
        public IHttpActionResult GetDataFabReqCal(
            string pMC_FAB_PROD_ORD_H_LST,
            Int64 pFAB_COLOR_ID
         )
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().GetDataFabReqCalForProgram(pMC_FAB_PROD_ORD_H_LST, pFAB_COLOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("DyeBatch/GetDataFabReqCalProgramWithTrims")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqCalProgramWithTrims?pMC_FAB_PROD_ORD_H_LST&pFAB_COLOR_ID
        public IHttpActionResult GetDataFabReqCalWithTrims(
            string pMC_FAB_PROD_ORD_H_LST,
            Int64 pFAB_COLOR_ID
         )
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().GetDataFabReqCalForProgramWithTrims(pMC_FAB_PROD_ORD_H_LST, pFAB_COLOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("DyeBatch/GetDataFabReqCalProgramScP")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqCalProgramScP?pMC_FAB_PROD_ORD_H_LST&pFAB_COLOR_ID
        public IHttpActionResult GetDataFabReqCalScp(
            string pMC_FAB_PROD_ORD_H_LST,
            Int64 pFAB_COLOR_ID
         )
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().GetDataFabReqCalForProgramScP(pMC_FAB_PROD_ORD_H_LST, pFAB_COLOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }




        [Route("DyeBatch/GetDataFabReqDyeBatch")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetDataFabReqDyeBatch?pDYE_BATCH_SCDL_ID&pOption&pIS_SMP_BLK&pDYE_BATCH_NO
        public IHttpActionResult GetDataFabReqDyeBatch(Int64? pDYE_BATCH_SCDL_ID = null, Int16? pOption = 3000, string pIS_SMP_BLK = "B", string pDYE_BATCH_NO = "", Int64? pHR_COMPANY_ID = null)
        {
            try
            {
                var obList = new DYE_MACHINEModel().GetDataFabReqDyeBatch(pDYE_BATCH_SCDL_ID, pOption, pIS_SMP_BLK, pDYE_BATCH_NO, pHR_COMPANY_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);

            }
        }


        [Route("DyeBatch/GetBatchPlanDataByScProg")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetBatchPlanDataByScProg?pDYE_SC_PRG_ISS_ID
        public IHttpActionResult GetBatchPlanDataByScProg(Int64 pDYE_SC_PRG_ISS_ID)
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().getProgramData(null, null, 3004, null, pDYE_SC_PRG_ISS_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatch/GetBatchDataByID")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetBatchDataByID?pDYE_BATCH_NO=
        public IHttpActionResult GetBatchDataByID(string pDYE_BATCH_NO=null)
        {
            try
            {
                var obList = new DYE_BATCH_PLANModel().getProgramData(null, null, 3003, pDYE_BATCH_NO, null);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("DyeBatch/GetBatchListByID")]
        [HttpGet]
        // GET :  /api/dye/DyeBatch/GetBatchListByID?pMC_BYR_ACC_GRP_ID=&pMC_BYR_ACC_ID=&pMC_STYLE_H_ID=&pMC_FAB_PROD_ORD_H_ID=&pMC_STYLE_H_EXT_ID=&pMC_COLOR_ID=
        public IHttpActionResult GetBatchListByID(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, Int64? pMC_COLOR_ID = null)
        {
            try
            {
                var obList = new DYE_BT_CARD_HModel().SelectByID(pMC_BYR_ACC_GRP_ID, pMC_BYR_ACC_ID, pMC_STYLE_H_ID, pMC_FAB_PROD_ORD_H_ID, pMC_STYLE_H_EXT_ID, pMC_COLOR_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DyeBatch/SendOTP")]
        [HttpPost]
        public IHttpActionResult SendOTP(DYE_BATCH_PLANModel ob)
        {
            ERPSolution.Areas.Dyeing.Controllers.DyeController.SendMixLotMail(ob.DYE_BATCH_NO);
            return Ok();
        }

        [Route("DyeBatch/Save")]
        [HttpPost]
        // GET :  api/dye/DyeBatch/Save
        public IHttpActionResult Save([FromBody] DYE_BATCH_PLANModel ob)
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


        [Route("DyeBatch/Delete")]
        [HttpPost]
        // GET :  api/dye/DyeBatch/Delete
        public IHttpActionResult Delete([FromBody] DYE_BATCH_PLANModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Delete();
            }
            catch (Exception e)
            {
                jsonStr = e.Message;
                return Ok(new { success = false, jsonStr });
            }

            return Ok(new { success = true, jsonStr });
        }


        [Route("DyeBatch/CreateRequisition")]
        [HttpPost]
        // GET :  api/dye/DyeBatch/CreateRequisition
        public IHttpActionResult CreateRequisition([FromBody] DYE_BATCH_PLANModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.CreateRequisition();
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
