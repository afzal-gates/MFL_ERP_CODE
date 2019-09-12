using ERP.Model;
using ERPSolution.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Controllers
{

    [RoutePrefix("api/common")]
    [NoCache]
    public class CommonApiController : ApiController
    {
        private static IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<DashBoardHub>();


        [Route("GmtFabClassList")]
        [HttpGet]
        // GET :  /api/common/GmtFabClassList
        public IHttpActionResult GmtFabClassList()
        {
            var obList = new RF_FAB_CLASSModel().GmtFabClassList();
            return Ok(obList);
        }

        [Route("getReportDataListByUser/{pRF_REPORT_GRP_ID}")]
        [HttpGet]
        // GET :  api/common/getReportDataListByUser/1
        public IHttpActionResult getReportDataListByUser(int pRF_REPORT_GRP_ID)
        {
            //if (HttpContext.Current.Request.RawUrl != "/" && HttpContext.Current.Request.RawUrl.ToLower() != "/Home/Index/UserDashBoard".ToLower())
            //{

            //    string vMenuID = "";
            //    string[] vQryStrSplit = { };
            //    string vQryStr = HttpContext.Current.Request.Params["a"];
            //    if (vQryStr != null && vQryStr != "")
            //    {
            //        vQryStrSplit = vQryStr.Split('/');
            //        vMenuID = vQryStrSplit[0];
            //    }
            //}            

            var obList = new RF_REPORTModel().getReportDataListByUser(pRF_REPORT_GRP_ID);
            return Ok(obList);
        }

        [Route("GmtPartList")]
        [HttpGet]
        // GET :  /api/common/GmtPartList?pRF_GARM_PART_LST=1&pIS_PROC_GRP
        public IHttpActionResult GmtPartList(string pRF_GARM_PART_LST = null, string pIS_PROC_GRP=null)
        {
            var obList = new RF_GARM_PARTModel().SelectAll(pRF_GARM_PART_LST, pIS_PROC_GRP);
            return Ok(obList);
        }

        [Route("GmtPartListByStyle")]
        [HttpGet]
        // GET :  /api/common/GmtPartListByStyle
        public IHttpActionResult GmtPartListByStyle(Int64? pMC_STYLE_H_ID = null)
        {
            var obList = new RF_GARM_PARTModel().SelectByStyle(pMC_STYLE_H_ID);
            return Ok(obList);
        }



        [Route("CompanyList")]
        [HttpGet]
        // GET :  /api/common/CompanyList
        public IHttpActionResult CompanyList()
        {
            var obList = new HrCompanyModel().SelectAll();
            return Ok(obList);
        }


        [Route("OfficeList")]
        [HttpGet]
        // GET :  /api/common/OfficeList
        public IHttpActionResult OfficeList()
        {
            var obList = new HrOfficeModel().OfficeListData();
            return Ok(obList);
        }

        [Route("GetOfficeList")]
        [HttpGet]
        // GET :  /api/common/GetOfficeList?pHR_COMPANY_ID&pOption
        public IHttpActionResult GetOfficeList(Int32? pHR_COMPANY_ID = null, Int32? pOption = null)
        {
            var obList = new HrOfficeModel().GetOfficeList(pHR_COMPANY_ID, pOption);
            return Ok(obList);
        }


        [Route("LocationList")]
        [HttpGet]
        // GET :  /api/common/LocationList
        public IHttpActionResult LocationList()
        {
            var obList = new RF_LOCATIONModel().SelectAll();
            return Ok(obList);
        }

        [Route("LookupListData/{ID:int}")]
        [HttpGet]
        // GET :  mrc/api/common/LookupListData
        public IHttpActionResult LookupListData(Int64 ID)
        {
            var obList = new LookupDataModel().LookupListData(ID);
            return Ok(obList);
        }

        [Route("UserData")]
        [HttpGet]
        // GET :  mrc/api/common/UserData
        public IHttpActionResult UserData()
        {
            var obList = new ScUserModel().SelectAll();
            return Ok(obList);
        }

        [Route("SelectAllUserData")]
        [HttpGet]
        // GET :  mrc/api/common/SelectAllUserData
        public IHttpActionResult SelectAllUserData()
        {
            var obList = new ScUserModel().SelectAllUserData();
            return Ok(obList);
        }

        [Route("getUserData/TnaTask/{ID}")]
        [HttpGet]
        // GET :  api/common/getUserData/TnaTask/1
        public IHttpActionResult getUserData(Int64 ID)
        {
            var obList = new ScUserModel().getUserData(ID);
            return Ok(obList);
        }


        [Route("SelectAllSampleTypeData")]
        [HttpGet]
        // GET :  mrc/api/common/SelectAllSampleTypeData
        public IHttpActionResult SelectAllSampleTypeData()
        {
            var obList = new RF_SMPL_TYPEModel().SelectAll();
            return Ok(obList);
        }



        [Route("MOUList/{Default:alpha?}")]
        [HttpGet]
        // GET :  api/common/MOUList/Y
        public IHttpActionResult MOUList(String Default = "N")
        {
            var obList = new RF_MOUModel().SelectAll(Default);
            return Ok(obList);
        }

        [Route("CurrencyList")]
        [HttpGet]
        // GET :  api/Common/CurrencyList
        public IHttpActionResult CurrencyList()
        {
            var obList = new RF_CURRENCYModel().SelectAll();
            return Ok(obList);
        }

        [Route("GetCountryList")]
        [HttpGet]
        public IHttpActionResult GetCountryList()
        {
            var ob = new HR_COUNTRYModel().SelectAll();
            return Ok(ob);
        }

        [Route("BrandSave")]
        [HttpPost]
        // GET :  /api/common/BrandSave
        public IHttpActionResult BrandSave([FromBody] RF_BRANDModel ob)
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


        [Route("GetItemBrandList")]
        [HttpGet]
        public IHttpActionResult GetItemBrandList()
        {
            var ob = new RF_BRANDModel().SelectAll();
            return Ok(ob);
        }

        [Route("GetCategoryWiseBrandList/{pINV_ITEM_CORE_CAT_ID:int}")]
        [HttpGet]
        public IHttpActionResult GetCategoryWiseBrandList(int pINV_ITEM_CORE_CAT_ID, Int16? pOption = 3002, String pKNT_YRN_LOT_ID_LST = null, string pIS_SOLID = "S", Int64? pYARN_ITEM_ID = null)
        {
            var ob = new RF_BRANDModel().CategoryWiseBrandList(pINV_ITEM_CORE_CAT_ID, pOption, pKNT_YRN_LOT_ID_LST, pIS_SOLID, pYARN_ITEM_ID);
            return Ok(ob);
        }

        [Route("BankSave")]
        [HttpPost]
        // GET :  /api/common/BankSave
        public IHttpActionResult BankSave([FromBody] RF_BANKModel ob)
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

        [Route("BankDataList")]
        [HttpGet]
        public IHttpActionResult BankDataList()
        {
            var obList = new RF_BANKModel().SelectAll();
            return Ok(obList);
        }

        [Route("GetBankBranchList")]
        [HttpGet]
        public IHttpActionResult GetBankBranchList()
        {
            var obList = new RF_BANK_BRANCHModel().SelectAll();
            return Ok(obList);
        }

        [Route("BankBranchDataList/{pRF_BANK_ID}")]
        [HttpGet]
        public IHttpActionResult BankBranchDataList(int? pRF_BANK_ID)
        {
            var obList = new RF_BANK_BRANCHModel().BankBranchDataList(pRF_BANK_ID);
            return Ok(obList);
        }

        [Route("BankBranchSave")]
        [HttpPost]
        // GET :  /api/common/BankBranchSave
        public IHttpActionResult BankBranchSave([FromBody] RF_BANK_BRANCHModel ob)
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

        [Route("BankAccountAutoList")]
        [HttpGet]
        public IHttpActionResult BankAccountAutoList(string pIS_EMP_ACC, string pBK_ACC_NO, int? pRF_BANK_ID)
        {
            var obList = new ACC_BK_ACCOUNTModel().BankAccountAutoList(pIS_EMP_ACC, pBK_ACC_NO, pRF_BANK_ID);
            return Ok(obList);
        }



        [Route("YarnCategoryList")]
        [HttpGet]
        // GET :  api/Common/YarnCategoryList
        public IHttpActionResult YarnCategoryList()
        {
            var obList = new RF_YR_CATModel().SelectAll();

            return Ok(obList);
        }


        [Route("YarnCountList")]
        [HttpGet]
        // GET :  api/Common/YarnCountList
        public IHttpActionResult YarnCountList()
        {
            var obList = new RF_YRN_CNTModel().SelectAll();
            return Ok(obList);
        }


        [Route("FibreCompList")]
        [HttpGet]
        // GET :  api/Common/FibreCompList?pFIB_COMP_NAME&pOption
        public IHttpActionResult FibreCompList(string pFIB_COMP_NAME = null, int pOption = 3000)
        {
            var obList = new RF_FIB_COMPModel().SelectAll(pFIB_COMP_NAME, pOption);
            return Ok(obList);
        }


        [Route("FabricTypeList")]
        [HttpGet]
        // GET :  api/Common/FabricTypeList?pINV_ITEM_CAT_ID
        public IHttpActionResult FabricTypeList(Int64? pINV_ITEM_CAT_ID = null, string pIS_FLAT_CIR = null)
        {
            var obList = new RF_FAB_TYPEModel().SelectAll(pINV_ITEM_CAT_ID, pIS_FLAT_CIR);
            return Ok(obList);
        }

        [Route("RfActionStatusList")]
        [HttpGet]
        // GET :  api/common/RfActionStatusList
        public IHttpActionResult RfActionStatusList()
        {
            var obList = new RF_ACTN_STATUSModel().SelectAll();
            return Ok(obList);
        }

        [Route("RfActionStatusByID/{RF_ACTN_TYPE_ID}/{PARENT_ID}/{CURRENT_USER}")]
        [HttpGet]
        // GET :  api/common/RfActionStatusByID
        public IHttpActionResult RfActionStatusByID(Int64 RF_ACTN_TYPE_ID, Int64? PARENT_ID, Int64 CURRENT_USER)
        {
            var obList = new RF_ACTN_STATUSModel().RfActionStatusByID(RF_ACTN_TYPE_ID, PARENT_ID, CURRENT_USER);
            return Ok(obList);
        }

        [Route("OrderTnAData")]
        [HttpGet]
        // GET :  api/common/OrderTnAData
        public IHttpActionResult OrderTnAData(Int64 pMC_BYR_ACC_ID, Int64 pageNumber, Int64 pageSize, string pORDER_NO = null, string pWORK_STYLE_NO = null, string pORD_TYPE_NAME = null, Int64? pMC_ORDER_H_ID = null)
        {
            var obList = new OrderTnAViewModel().OrderTnaData(pMC_BYR_ACC_ID, pageNumber, pageSize, pORDER_NO, pWORK_STYLE_NO, pORD_TYPE_NAME, pMC_ORDER_H_ID);
            return Ok(obList);
        }

        [Route("OrderTnADataTask")]
        [HttpGet]
        // GET :  api/common/OrderTnADataTask
        public IHttpActionResult OrderTnADataTask(
            Int64 pMC_ORDER_H_ID,
            Int64 pageNumber,
            Int64 pageSize,
            string pTA_TASK_NAME_EN = null,
            string DpD2GList = null,
            string pPLAN_START_DT = null,
            string pACT_START_DT = null
            )
        {
            var obList = new OrderTnAViewModel().OrderTnADataTask(pMC_ORDER_H_ID, pageNumber, pageSize, pTA_TASK_NAME_EN, DpD2GList, pPLAN_START_DT, pACT_START_DT);
            return Ok(obList);
        }

        [Route("OrderTnATaskByCode")]
        [HttpGet]
        // GET :  api/common/OrderTnATaskByCode?pMC_ORDER_H_ID&pTA_TASK_CODE
        public IHttpActionResult OrderTnATaskByCode(
            Int64 pMC_ORDER_H_ID, string pTA_TASK_CODE)
        {
            var obList = new OrderTnAViewModel().OrderTnATaskByCode(pMC_ORDER_H_ID, pTA_TASK_CODE);
            return Ok(obList);
        }


        [Route("OrderTnADataTaskDashBord")]
        [HttpGet]
        // GET :  api/common/OrderTnADataTaskDashBord
        public IHttpActionResult OrderTnADataTaskDashBord(
            Int64 pageNumber,
            Int64 pageSize,
            string pTA_TASK_NAME_EN = null,
            string pPLAN_START_DT = null,
            string pWORK_STYLE = null,
            string pBASE_STYLE = null,
            string pORDER_NO = null
            )
        {
            var obList = new OrderTnAViewModel().OrderTnADataTaskDashBord(pageNumber, pageSize, pTA_TASK_NAME_EN, pPLAN_START_DT, pWORK_STYLE, pBASE_STYLE, pORDER_NO);
            return Ok(obList);
        }


        [Route("getTnAMatrixGridData")]
        [HttpGet]
        // GET :  api/common/getTnAMatrixGridData
        public IHttpActionResult getTnAMatrixGridData(
            Int64 pageNumber,
            Int64 pageSize,
            Int64? pMC_BYR_ACC_ID = null,
            string pMC_ORDER_H_ID_LST = null,
            DateTime? pFIRSTDATE = null,
            DateTime? pLASTDATE = null,
            Int64? pLK_ORD_TYPE_ID = null
            )
        {
            var obList = new TNA_CROSS_TABModel().queryData(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_ORDER_H_ID_LST, pFIRSTDATE, pLASTDATE, pLK_ORD_TYPE_ID);
            return Ok(obList);
        }



        [Route("getTnAMatrixRptData")]
        [HttpGet]
        // GET :  api/common/getTnAMatrixRptData
        public IHttpActionResult getTnAMatrixRptData()
        {
            var obList = new TNA_CROSS_TAB_RPTModel().queryData();
            return Ok(obList);
        }


        [Route("getTnAMatrixTextCode")]
        [HttpGet]
        // GET :  api/common/getTnAMatrixTextCode
        public IHttpActionResult getTnAMatrixTextCode()
        {
            var obList = new TNA_CROSS_TABModel().Select();
            return Ok(obList);
        }

        [Route("getTnAMatrixHideCode")]
        [HttpGet]
        // GET :  api/common/getTnAMatrixHideCode
        public IHttpActionResult getTnAMatrixHideCode()
        {
            var obList = new TNA_CROSS_TABModel().TNA_CODE_HIDE_SELECT();
            return Ok(obList);
        }



        [Route("GetDyeingMethodList")]
        [HttpGet]
        // GET :  api/common/GetDyeingMethodList
        public IHttpActionResult GetDyeingMethodList()
        {
            var obList = new LK_DYE_MTHDModel().SelectAll();
            return Ok(obList);
        }

        [Route("GetMachineGaugeList")]
        [HttpGet]
        // GET :  api/common/GetMachineGaugeList
        public IHttpActionResult GetMachineGaugeList(Decimal pFB_WT_MAX, Int64? pRF_FIB_COMP_ID = null, Int64? pRF_FAB_TYPE_ID = null)
        {
            try
            {
                var obList = new MC_YRN_CNT_CFGModel().GetMachineGaugeList(pFB_WT_MAX, pRF_FIB_COMP_ID, pRF_FAB_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Route("GetSpinProcesListByFibTyp")]
        [HttpGet]
        // GET :  api/common/GetSpinProcesListByFibTyp
        public IHttpActionResult GetSpinProcesListByFibTyp(Int64? LK_FIB_TYPE_ID = null)
        {
            try
            {
                var obList = new LOOKUP_DATA_VM().GetSpinProcesListByFibTyp(LK_FIB_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("SaveTnAData")]
        [HttpPost]
        [System.Web.Http.Authorize]
        public IHttpActionResult SaveTnAData([FromBody] MC_ORD_TNAModel ob)
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


        [Route("GetAccPayPeriod")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetAccPayPeriod
        public IHttpActionResult GetAccPayPeriod(int? pHR_COMPANY_ID = null, int? pHR_PERIOD_TYPE_ID = null, string pIS_CLOSED = null, string pIS_SHOW4_RPT = null)
        {
            try
            {
                var obList = new ACC_PAY_PERIODModel().GetAccPayPeriod(pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID, pIS_CLOSED, pIS_SHOW4_RPT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetPayPeriodType")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetPayPeriodType
        public IHttpActionResult GetPayPeriodType()
        {
            try
            {
                var obList = new HrPeriodTypeModel().PeriodTypeListData();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetPayFiscalYear")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetPayFiscalYear
        public IHttpActionResult GetPayFiscalYear(string pIS_CLOSED = null)
        {
            try
            {
                var obList = new RF_FISCAL_YEARModel().FiscalYearData(pIS_CLOSED);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SaveFiscalYear")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  api/common/SaveFiscalYear
        public IHttpActionResult SaveFiscalYear([FromBody] RF_FISCAL_YEARModel ob)
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

        [Route("GetMonthList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetMonthList
        public IHttpActionResult GetMonthList()
        {
            try
            {
                var obList = new RF_CAL_MONTHModel().MonthListData();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetIncrimentType")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetIncrimentType
        public IHttpActionResult GetIncrimentType()
        {
            try
            {
                var obList = new RF_INCR_TYPEModel().GetIncrimentType();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetPayMethod")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetPayMethod
        public IHttpActionResult GetPayMethod()
        {
            try
            {
                var obList = new RF_PAY_MTHDModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetReqSRC")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetReqSRC
        public IHttpActionResult GetReqSRC()
        {
            try
            {
                var obList = new RF_REQ_SRCModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetReqType")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetReqType
        public IHttpActionResult GetReqType()
        {
            try
            {
                var obList = new RF_REQ_TYPEModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetReqTypeByUser")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetReqTypeByUser
        public IHttpActionResult GetReqTypeByUser()
        {
            try
            {
                var obList = new RF_REQ_TYPEModel().SelectByUserID();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        

        [Route("GetStoreInfo")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetStoreInfo
        public IHttpActionResult GetStoreInfo(string pINV_ITEM_CAT_LST = null, Int64? pSC_USER_ID = null)
        {
            try
            {
                var obList = new SCM_STOREModel().SelectAll(pINV_ITEM_CAT_LST, pSC_USER_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("StoreListByOfcComCatID")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/StoreListByOfcComCatID?pHR_COMPANY_ID=
        public IHttpActionResult StoreListByOfcComCatID(Int64? pHR_OFFICE_ID = null, Int64? pHR_COMPANY_ID = null, string pINV_ITEM_CAT_LST = null)
        {
            try
            {
                var obList = new SCM_STOREModel().StoreListByOfcComCatID(pHR_OFFICE_ID, pHR_COMPANY_ID, pINV_ITEM_CAT_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetFabProdCat")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetFabProdCat
        public IHttpActionResult GetFabProdCat()
        {
            try
            {
                var obList = new RF_FAB_PROD_CATModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetSewingLineData")]
        [HttpGet]
        // GET :  api/common/GetSewingLineData
        public IHttpActionResult GetSewingLineData()
        {
            try
            {
                var obList = new HR_PROD_LINEModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("GetSewingFloorData")]
        [HttpGet]
        // GET :  api/common/GetSewingFloorData
        public IHttpActionResult GetSewingFloorData()
        {
            try
            {
                var obList = new HR_PROD_LINEModel().getFloorData();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("getFinishingProdEntryData")]
        [HttpGet]
        // GET :  api/common/getFinishingProdEntryData?pHR_PROD_FLR_ID_LST&pPROD_DT
        public IHttpActionResult getFinishingProdEntryData(String pHR_PROD_FLR_ID_LST, DateTime? pPROD_DT)
        {
            try
            {
                var obList = new GMT_FIN_PRODModel().SelectAll(pHR_PROD_FLR_ID_LST, pPROD_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("getLineLoadingPlanData")]
        [HttpGet]
        // GET :  api/common/getLineLoadingPlanData?pHR_PROD_FLR_LST&pHR_PROD_LINE_LST
        public IHttpActionResult getLineLoadingPlanData(String pHR_PROD_FLR_LST = null, String pHR_PROD_LINE_LST = null)
        {
            try
            {
                var obList = new GMT_LN_LOAD_PLANModel().SelectAll(pHR_PROD_FLR_LST, pHR_PROD_LINE_LST);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [Route("getLineLoadingPlanDataEntry")]
        [HttpGet]
        // GET :  api/common/getLineLoadingPlanDataEntry?pHR_PROD_FLR_LST&pHR_PROD_LINE_LST&pPROD_DT
        public IHttpActionResult getLineLoadingPlanDataEntry(String pHR_PROD_FLR_LST = null, String pHR_PROD_LINE_LST = null, DateTime? pPROD_DT = null)
        {
            try
            {
                var obList = new GMT_LN_LOAD_PLANModel().getLineLoadingPlanDataEntry(pHR_PROD_FLR_LST, pHR_PROD_LINE_LST, pPROD_DT);

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("getDataOfMapowerAdjustment")]
        [HttpGet]
        // GET :  api/common/getDataOfMapowerAdjustment?pHR_PROD_FLR_LST&pHR_PROD_LINE_LST&pPROD_DT
        public IHttpActionResult getDataOfMapowerAdjustment( DateTime pPROD_DT, String pHR_PROD_FLR_LST = null, String pHR_PROD_LINE_LST = null)
        {
            try
            {
                var obList = new GMT_LN_MP_ADJModel().Query(3000, pPROD_DT, pHR_PROD_LINE_LST, pHR_PROD_FLR_LST);

                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("getSewingProductionDashBoard")]
        [HttpGet]
        // GET :  api/common/getSewingProductionDashBoard?pHR_PROD_FLR_LST&pPROD_DT
        public IHttpActionResult getSewingProductionDashBoard(String pHR_PROD_FLR_LST = null, DateTime? pPROD_DT = null)
        {
            try
            {
                var obList = new HR_PROD_LINEModel().getSewingProdDashBoard(pHR_PROD_FLR_LST, pPROD_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SaveLineLoadingPlanData")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  api/common/SaveLineLoadingPlanData
        public IHttpActionResult SaveLineLoadingPlanData([FromBody] GMT_LN_LOAD_PLANModel ob)
        {
            try
            {
                var obList = ob.Save();
                Hub.Clients.All.executedFromServer();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("SaveGmtLineManpowerAdjustment")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  api/common/SaveGmtLineManpowerAdjustment
        public IHttpActionResult SaveGmtLineManpowerAdjustment([FromBody] GMT_LN_MP_ADJModel ob)
        {
            try
            {
                var obList = ob.Save();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("SaveFinishingData")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  api/common/SaveFinishingData
        public IHttpActionResult SaveFinishingData([FromBody] GMT_FIN_PRODModel ob)
        {
            try
            {
                var obList = ob.Save();
                Hub.Clients.All.executedFromServer();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("savePerformanceFaultReason")]
        [HttpPost]
        [System.Web.Http.Authorize]
        // GET :  api/common/savePerformanceFaultReason
        public IHttpActionResult savePerformanceFaultReason([FromBody] RF_PFLT_RSN_TYPEModel ob)
        {
            try
            {
                var obList = ob.Save();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }




        [Route("getOrderStyleDropDownData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getOrderStyleDropDownData?pMC_BYR_ACC_ID&pORDER_NO
        public IHttpActionResult getOrderStyleDropDownData(
              Int64? pMC_BYR_ACC_ID = null,
              String pORDER_NO = null,
              Int64? pMC_ORDER_H_ID = null,
              DateTime? pFIRSTDATE = null,
              DateTime? pLASTDATE = null,
              int? pOption = 3000,
              Int64? pRF_FAB_PROD_CAT_ID = null
            )
        {
            try
            {
                var obList = new MC_ORDER_STYLModel().getOrderStyleDropDownData(pMC_BYR_ACC_ID, pORDER_NO, pMC_ORDER_H_ID, pFIRSTDATE, pLASTDATE, pOption, pRF_FAB_PROD_CAT_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("getOrderStyleDropDownDataForPln")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getOrderStyleDropDownDataForPln
        public IHttpActionResult getOrderStyleDropDownDataForPln(
              Int64? pMC_BYR_ACC_ID = null,
              String pORDER_NO = null,
              DateTime? pFIRSTDATE = null,
              DateTime? pLASTDATE = null,
              Int64? pINV_ITEM_CAT_ID_P = null,
              Int64? pINV_ITEM_CAT_ID =null,
              Int64? pLK_ORD_TYPE_ID = null
            )
        {
            try
            {
                var obList = new MC_ORDER_STYLModel().getOrderStyleDropDownDataForPln(pMC_BYR_ACC_ID, pORDER_NO, pFIRSTDATE, pLASTDATE, pINV_ITEM_CAT_ID_P, pINV_ITEM_CAT_ID, pLK_ORD_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("getOrderStyleItemDropDownData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getOrderStyleItemDropDownData?pMC_ORDER_H_ID&pITEM_NAME_EN
        public IHttpActionResult getOrderStyleItemDropDownData(Int64? pMC_ORDER_H_ID = null, String pITEM_NAME_EN = null, Int64? pMC_ORDER_SHIP_ID=null)
        {
            try
            {
                var obList = new MC_ORDER_STYLModel().getOrderStyleItemDropDownData(pMC_ORDER_H_ID, pITEM_NAME_EN, pMC_ORDER_SHIP_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("getPerformanceFaultReasonData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getPerformanceFaultReasonData
        public IHttpActionResult getPerformanceFaultReasonData()
        {
            try
            {
                var obList = new RF_PFLT_RSN_TYPEModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("getDyeDfctTypeList")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getDyeDfctTypeList
        public IHttpActionResult getDyeDfctTypeList()
        {
            try
            {
                var obList = new RF_DY_DFCT_TYPEModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("NoOfWorkingDay")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/NoOfWorkingDay?pHR_COMPANY_ID&pFROM_DT&pTO_DT
        public IHttpActionResult getNoOfWorkingDay(int pHR_COMPANY_ID, DateTime? pFROM_DT, DateTime? pTO_DT)
        {
            try
            {
                var obList = new HrYrlyCalndrModel().getNoOfWorkingDay(pHR_COMPANY_ID, pFROM_DT, pTO_DT);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetPendingReqCountH")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetPendingReqCountH
        public IHttpActionResult getPendingReqCountH()
        {
            try
            {
                var obList = new RF_REQ_TYPEModel().getPendingReqCountH();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("GetPendingReqCountHD")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/GetPendingReqCountHD?pRF_REQ_TYPE_ID
        public IHttpActionResult getPendingReqCountHD(Int64 pRF_REQ_TYPE_ID)
        {
            try
            {
                var obList = new RF_REQ_TYPEModel().getPendingReqCountHD(pRF_REQ_TYPE_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("FindTnaProductionData")]
        [HttpGet]
        // GET :  api/common/FindTnaProductionData
        public IHttpActionResult FindTnaProductionData()
        {
            try
            {
                var obList = new KNT_BUYER_SHIP_MONTHModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("getLabelPrinter")]
        [HttpGet]
        // GET :  api/common/getLabelPrinter
        public IHttpActionResult getLabelPrinter()
        {
            try
            {
                var obList = new SC_RLBL_PRNTR_CFGModel().QueryDatas();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("getRespDeptList")]
        [HttpGet]
        // GET :  api/common/getRespDeptList
        public IHttpActionResult getRespDeptList()
        {
            try
            {
                var obList = new RF_RESP_DEPTModel().getRespDeptList();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SaveSrtFabBkRespDept")]
        [HttpPost]
        // GET :  api/common/SaveSrtFabBkRespDept
        public IHttpActionResult SaveSrtFabBkRespDept([FromBody] RF_RESP_DEPTModel ob)
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

        [Route("getSrtFabBkReasonTyp")]
        [HttpGet]
        // GET :  api/common/getSrtFabBkReasonTyp
        public IHttpActionResult getSrtFabBkReasonTyp()
        {
            try
            {
                var obList = new RF_SFAB_RSN_TYPEModel().getSrtFabBkReasonTyp();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("SaveSrtFabBkReasonTyp")]
        [HttpPost]
        // GET :  api/common/SaveSrtFabBkReasonTyp
        public IHttpActionResult SaveSrtFabBkReasonTyp([FromBody] RF_SFAB_RSN_TYPEModel ob)
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

        [Route("SaveCompPayPeriod")]
        [HttpPost]
        // GET :  api/common/SaveCompPayPeriod
        public IHttpActionResult SaveCompPayPeriod([FromBody] ACC_PAY_PERIODModel ob)
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

        [Route("SaveGmtPart")]
        [HttpPost]
        // GET :  api/common/SaveGmtPart
        public IHttpActionResult SaveGmtPart([FromBody] RF_GARM_PARTModel ob)
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

        [Route("getCompanyInsuranceList")]
        [HttpGet]
        // GET :  api/common/getCompanyInsuranceList
        public IHttpActionResult getCompanyInsuranceList()
        {
            try
            {
                var obList = new RF_INSURN_COMPModel().SelectAll();
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }



        [Route("GetUploadDocList")]
        [HttpGet]
        // GET :  /api/common/GetUploadDocList
        public IHttpActionResult GetUploadDocList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pDOC_REF_NO = null,
            string pSTYLE_NO = null, string pORDER_NO = null)
        {
            try
            {
                var obList = new RF_DOC_ARCVModel().SelectAll(pageNumber, pageSize, pMC_BYR_ACC_ID, pMC_STYLE_H_EXT_ID, pDOC_REF_NO, pSTYLE_NO, pORDER_NO);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("DeleteUploadedOtherDocs")]
        [HttpPost]
        // POST :  /api/common/DeleteUploadedOtherDocs
        public IHttpActionResult DeleteUploadedOtherDocs(RF_DOC_ARCVModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Delete();

                    string vMsg = jsonStr.Substring(9, 9);
                    if (vMsg == "MULTI-001")
                    {
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/OTHER_DOCS"), ob.DOC_PATH_URL);
                        System.IO.File.Delete(path);

                    }
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

        [Route("getOrderStyleDropDownDataGmt")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getOrderStyleDropDownDataGmt?pMC_BYR_ACC_GRP_ID=&pORDER_NO=&pGMT_DF_WASH_REQ_ID=
        public IHttpActionResult getOrderStyleDropDownDataGmt(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_ID = null, String pORDER_NO = null, Int64? pGMT_DF_WASH_REQ_ID = null)
        {
            try
            {
                var obList = new MC_ORDER_STYLModel().getOrderStyleDropDownDataGmt(pMC_BYR_ACC_GRP_ID, pMC_STYLE_H_ID, pORDER_NO, pGMT_DF_WASH_REQ_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("getOrderColorDataForGmt")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/getOrderColorDataForGmt?pMC_ORDER_H_ID
        public IHttpActionResult getOrderColorDataForGmt( Int64 pMC_ORDER_H_ID )
        {
            try
            {
                var obList = new MC_ORDER_COLModel().getOrderColorDataForGmt(pMC_ORDER_H_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Route("OpenPdf")]
        public HttpResponseMessage OpenPdfFile(string pdfName)
        {
            string path = HttpContext.Current.Server.MapPath("~/UPLOAD_DOCS/OTHER_DOCS/" + pdfName + ".pdf");
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            FileStream fileStream = File.OpenRead(path);
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return response;
        }


        [Route("FindOrderShipData")]
        [HttpGet]
        [System.Web.Http.Authorize]
        // GET :  api/common/FindOrderShipData?pMC_ORDER_SHIP_ID
        public IHttpActionResult FindOrderShipData(Int64 pMC_ORDER_SHIP_ID)
        {
            try
            {
                var obList = new MC_ORDER_SHIPModel().Select(pMC_ORDER_SHIP_ID);
                return Ok(obList);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Route("UpdateOrderShipData")]
        [HttpPost]
        // POST :  /api/common/UpdateOrderShipData
        public IHttpActionResult UpdateOrderShipData(MC_ORDER_SHIPModel ob)
        {
            string jsonStr = "";
            try
            {
                jsonStr = ob.Save();
                return Ok(new { success = true, jsonStr });
            }
            catch (Exception e)
            {
                return Ok(new { success = false, e.Message });
            }
        }
    }
}
