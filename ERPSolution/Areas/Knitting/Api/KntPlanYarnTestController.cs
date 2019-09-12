using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERPSolution.Areas.Knitting.Api
{
    [RoutePrefix("api/Knit")]
    public class KntPlanYarnTestController : ApiController
    {
        [Route("KntPlanYarnTest/SelectAll/{pageNo}/{pageSize}")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/SelectAll
        public IHttpActionResult SelectAll(int pageNo, int pageSize, string pTEST_WO_NO = null, string pTEST_WO_DT = null, string pCOMP_NAME_EN = null, string pREQ_TYPE_NAME = null, string pPAY_MTHD_NAME = null, string pLOC_SRC_TYPE_NAME = null, string pREMARKS = null)
        {
            try
            {
                if (pTEST_WO_DT != null)
                    pTEST_WO_DT = Convert.ToDateTime(pTEST_WO_DT.Split('T')[0].ToString()).ToString("yyyy-MMM-dd");

                //var data = new KNT_YRN_LOT_TEST_HModel().SelectAll(pageNo, pageSize, pYRN_MRR_NO, pYRN_MRR_DT, pCOMP_NAME_EN, pREQ_TYPE_NAME, pPAY_MTHD_NAME, pLOC_SRC_TYPE_NAME, pREMARKS);
                var data = new KNT_YRN_LOT_TEST_HModel().SelectAll(pageNo, pageSize, pTEST_WO_NO, pTEST_WO_DT);
                int total = 0;
                if (data.Count > 0)
                    total = data.FirstOrDefault().TOTAL_REC;
                return Ok(new { total, data });

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntPlanYarnTest/GetYarnTest")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/GetYarnTest?pKNT_YRN_LOT_TEST_H_ID
        public IHttpActionResult GetYarnTest(Int64? pKNT_YRN_LOT_TEST_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_TEST_HModel().Select(pKNT_YRN_LOT_TEST_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntPlanYarnTest/GetYarnTestDtl")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/GetYarnTestDtl?pSCM_YRN_PURC_REQ_H_ID
        public IHttpActionResult GetYarnTestDtl(Int64? pKNT_YRN_LOT_TEST_H_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_TEST_D1Model().SelectByID(pKNT_YRN_LOT_TEST_H_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntPlanYarnTest/GetYarnTestDtl2")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/GetYarnTestDtl2?pKNT_YRN_LOT_TEST_D1_ID
        public IHttpActionResult GetYarnTestDtl2(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_TEST_D11Model().SelectByID(pKNT_YRN_LOT_TEST_D1_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntPlanYarnTest/GetYarnTestParam")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/GetYarnTestParam
        public IHttpActionResult GetYarnTestParam(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            try
            {
                var list = new RF_YRN_TST_PARAMModel().SelectByID(pKNT_YRN_LOT_TEST_D1_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("KntPlanYarnTest/SelectForDefectType")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/SelectForDefectType
        public IHttpActionResult SelectForDefectType(Int64? pKNT_YRN_LOT_TEST_D1_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_TEST_D11Model().SelectForDefectType(pKNT_YRN_LOT_TEST_D1_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("KntPlanYarnTest/SelectColorByCGID")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/SelectColorByCGID?pLK_COLR_GRP_ID=
        public IHttpActionResult SelectColorByCGID(Int64? pLK_COLR_GRP_ID = null)
        {
            try
            {
                var list = new RF_DFLT_MAP_COL_YCGModel().Select(pLK_COLR_GRP_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
         


        [Route("KntPlanYarnTest/Save")]
        [HttpPost]
        // GET :  api/knit/KntPlanYarnTest/Save
        public IHttpActionResult Save([FromBody] KNT_YRN_LOT_TEST_HModel ob)
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


        [Route("KntPlanYarnTest/Update")]
        [HttpPost]
        // GET :  api/knit/KntPlanYarnTest/Save
        public IHttpActionResult Update([FromBody] KNT_YRN_LOT_TEST_HModel ob)
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



        [Route("KntPlanYarnTest/SaveResultYT")]
        [HttpPost]
        // GET :  api/knit/KntPlanYarnTest/SaveResultYT
        public IHttpActionResult SaveResultYT([FromBody] KNT_YRN_LOT_TEST_R1Model ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Ok(new { success = true, jsonStr });
        }


        [Route("KntPlanYarnTest/SaveResultDF")]
        [HttpPost]
        // GET :  api/knit/KntPlanYarnTest/SaveResultDF
        public IHttpActionResult SaveResultDF([FromBody] KNT_YRN_LOT_TEST_R2Model ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return Ok(new { success = true, jsonStr });
        }

        [Route("KntPlanYarnTest/KnitYarnTestRegister")]
        [HttpGet]
        // GET :  /api/knit/KntPlanYarnTest/KnitYarnTestRegister?pRF_YRN_CNT_ID=&pRF_BRAND_ID=&pYRN_LOT_NO=&pLK_YRN_TST_TYPE_ID=
        public IHttpActionResult KnitYarnTestRegister(Int64? pRF_YRN_CNT_ID = null, Int64? pRF_BRAND_ID = null, string pYRN_LOT_NO = null, Int64? pLK_YRN_TST_TYPE_ID = null)
        {
            try
            {
                var list = new KNT_YRN_LOT_TEST_D11Model().KnitYarnTestRegister(pRF_YRN_CNT_ID, pRF_BRAND_ID, pYRN_LOT_NO, pLK_YRN_TST_TYPE_ID);
                return Ok(list);

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
