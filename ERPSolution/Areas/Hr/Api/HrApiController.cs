using ERP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Areas.Hr.Api
{
    [RoutePrefix("api/hr")]
    public class HrApiController : ApiController
    {

        [Route("SmsBroadcast2Emp/SendSms")]
        [HttpPost]
        [Authorize]
        //POST: /api/hr/SmsBroadcast2Emp/SendSms
        public IHttpActionResult SendSms([FromBody] SMS_BROADCAST2EMPModel ob)
        {
            string jsonStr = "";
            //string pPRJ_SERVER_PATH =  Server.MapPath("~/UPLOAD_DOCS");

            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.SendSms();
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

        
        [Route("Employee/GetGmtNomineeList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/Employee/GetGmtNomineeList
        public IHttpActionResult GetGmtNomineeList(long pHR_EMPLOYEE_ID)
        {
            var obList = new HR_EMP_NOMINEEModel().GetGmtNomineeList(pHR_EMPLOYEE_ID);
            return Ok(obList);
        }

        [Route("ShiftTeam/Save")]
        [HttpPost]
        [Authorize]
        //POST: /api/hr/ShiftTeam/Save
        public IHttpActionResult Save([FromBody] HrShiftTeamModel ob)
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
        
        [Route("EmpTrans/Save")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Save([FromBody] HR_EMP_TRNFRModel ob)
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

        [Route("EmpTrans/GetTransData")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/EmpTrans/GetTransData
        public IHttpActionResult GetTransData(int pageNumber, int pageSize, string pP_EMPLOYEE_CODE = null, string pN_EMPLOYEE_CODE = null, DateTime? pFROM_DT = null, DateTime? pTO_DT = null)
        {
            var obList = new HR_EMP_TRNFRModel().GetTransData(pageNumber, pageSize, pP_EMPLOYEE_CODE, pN_EMPLOYEE_CODE, pFROM_DT, pTO_DT);
            return Ok(obList);
        }

        [Route("EmpTrans/GetTransDataByID")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/EmpTrans/GetTransDataByID
        public IHttpActionResult GetTransDataByID(long pHR_EMP_TRNFR_ID)
        {
            var obList = new HR_EMP_TRNFRModel().GetTransDataByID(pHR_EMP_TRNFR_ID);
            return Ok(obList);
        }


        [Route("MbnBill/GetEmpAutoSearch")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/MbnBill/GetEmpAutoSearch
        public IHttpActionResult GetEmpAutoSearch(string pEMPLOYEE_CODE = null, string pLK_JOB_STATUS_ID = null)
        {
            var obList = new HR_MBN_BILL_EMP_AUTOModel().GetEmpAutoSearch(pEMPLOYEE_CODE, pLK_JOB_STATUS_ID);
            return Ok(obList);
        }

        [Route("MbnBill/MbnBillProcess")]
        [HttpPost]
        [Authorize]
        // POST :  /api/hr/MbnBill/MbnBillProcess
        public IHttpActionResult MbnBillProcess([FromBody] HR_MBN_BILL_HModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MbnBillProcess();
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

        [Route("MbnBill/GetMbnBillHdrList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/MbnBill/GetMbnBillList
        public IHttpActionResult GetMbnBillHdrList(Int64 pHR_EMPLOYEE_ID)
        {
            var obList = new HR_MBN_BILL_HModel().GetMbnBillHdrList(pHR_EMPLOYEE_ID);
            return Ok(obList);
        }

        [Route("MbnBill/GetMbnBillDtlList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/MbnBill/GetMbnBillDtlList
        public IHttpActionResult GetMbnBillDtlList(Int64 pHR_MBN_BILL_H_ID)
        {
            var obList = new HR_MBN_BILL_HModel().GetMbnBillDtlList(pHR_MBN_BILL_H_ID);
            return Ok(obList);
        }

        [Route("MbnBill/MbnBillPhaseUpdate")]
        [HttpPost]
        [Authorize]
        // POST :  /api/hr/MbnBill/MbnBillPhaseUpdate
        public IHttpActionResult MbnBillPhaseUpdate([FromBody] HR_MBN_BILL_DModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.MbnBillPhaseUpdate();
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



        [Route("OtSummeryApprove/Save")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Save([FromBody] HR_OT_SUMModel ob)
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

        [Route("OtSummeryApprove/Remove")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Remove([FromBody] HR_OT_SUMModel ob)
        {
            string jsonStr = "";
            if (ModelState.IsValid)
            {
                try
                {
                    jsonStr = ob.Remove();
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


        [Route("OtSummeryApprove/GetOtSummeryList")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetOtSummeryList(int pACC_PAY_PERIOD_ID)
        {
            var obList = new HR_OT_SUMModel().GetOtSummeryList(pACC_PAY_PERIOD_ID);
            return Ok(obList);
        }

        [Route("CompPayPeriod/GetCompPayPeriodList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/CompPayPeriod/GetCompPayPeriodList
        public IHttpActionResult GetCompPayPeriodList(Int64 pageNumber, Int64 pageSize, int? pHR_COMPANY_ID = null, int? pHR_PERIOD_TYPE_ID = null,
            string pIS_CLOSED = null, string pIS_SHOW4_RPT = null)
        {
            var obList = new ACC_PAY_PERIODModel().GetCompPayPeriodList(pageNumber, pageSize, pHR_COMPANY_ID, pHR_PERIOD_TYPE_ID, pIS_CLOSED, pIS_SHOW4_RPT);
            return Ok(obList);
        }

        [Route("CompPayPeriod/GetCompPayPeriodByID")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/CompPayPeriod/GetCompPayPeriodByID
        public IHttpActionResult GetCompPayPeriodByID(Int64 pACC_PAY_PERIOD_ID)
        {
            var obList = new ACC_PAY_PERIODModel().GetCompPayPeriodByID(pACC_PAY_PERIOD_ID);
            return Ok(obList);
        }

        [Route("GetFiscalYearList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/GetFiscalYearList
        public IHttpActionResult GetFiscalYearList(string pIS_CLOSED = null)
        {
            var obList = new RF_FISCAL_YEARModel().FiscalYearData(pIS_CLOSED);
            return Ok(obList);
        }

        [Route("Attendance/GetShiftTeamRestoreBatchList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/Attendance/GetShiftTeamRestoreBatchList
        public IHttpActionResult GetShiftTeamRestoreBatchList()
        {
            var obList = new HR_SHIFT_TEAM_RESTORE_POINTModel().GetShiftTeamRestoreBatchList();
            return Ok(obList);
        }

        [Route("Attendance/ScheduleByRandomPersonBatchSave")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult ScheduleByRandomPersonBatchSave([FromBody] HrScheduleAssignModel ob)
        {
            string jsonStr = "";
            //if (ModelState.IsValid)
            //{
                try
                {
                    jsonStr = ob.ScheduleByRandomPersonBatchSave();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Ok(new { success = false, errors });
            //}
            return Ok(new { success = true, jsonStr });
        }

        [Route("Attendance/SchdulRestoreByRandomBatchSave")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SchdulRestoreByRandomBatchSave([FromBody] HR_SHIFT_TEAM_RESTORE_POINTModel ob)
        {
            string jsonStr = "";
            //if (ModelState.IsValid)
            //{
            try
            {
                jsonStr = ob.SchdulRestoreByRandomBatchSave();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Ok(new { success = false, errors });
            //}
            return Ok(new { success = true, jsonStr });
        }

        [Route("Attendance/GetShiftTeamList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/Attendance/GetShiftTeamList?pHR_SHIFT_PLAN_ID
        public IHttpActionResult GetShiftTeamList(long? pHR_SHIFT_PLAN_ID = null)
        {
            var obList = new HrScheduleAssignModel().GetShiftTeamList(pHR_SHIFT_PLAN_ID);
            return Ok(obList);
        }

        [Route("Attendance/GetScheduleList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/Attendance/GetScheduleList?pHR_SHIFT_PLAN_ID
        public IHttpActionResult GetScheduleList(long? pHR_SHIFT_PLAN_ID = null)
        {
            var obList = new HR_SCHEDULE_HModel().GetScheduleList(pHR_SHIFT_PLAN_ID);
            return Ok(obList);
        }


        [Route("Leave/AnnualLeaveByRandomPersonBatchSave")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AnnualLeaveByRandomPersonBatchSave([FromBody] HR_LEAVE_BALModel ob)
        {
            string jsonStr = "";
            //if (ModelState.IsValid)
            //{
            try
            {
                jsonStr = ob.AnnualLeaveByRandomPersonBatchSave();
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            //}
            //else
            //{
            //    var errors = new Hashtable();
            //    foreach (var pair in ModelState)
            //    {
            //        if (pair.Value.Errors.Count > 0)
            //        {
            //            errors[pair.Key] = pair.Value.Errors.Select(error => error.ErrorMessage).ToList();
            //        }
            //    }
            //    return Ok(new { success = false, errors });
            //}
            return Ok(new { success = true, jsonStr });
        }


        [Route("GetProdFloorList")]
        [HttpGet]
        [Authorize]
        // GET :  hr/api/GetProdFloorList?pHR_COMPANY_ID&pLK_PFLR_TYP_ID&pOption
        public IHttpActionResult GetProdFloorList(Int16? pHR_COMPANY_ID = null, Int16? pLK_PFLR_TYP_ID = null, Int64? pOption = 3002)
        {
            var obList = new HR_PROD_FLRModel().GetProdFloorList(pHR_COMPANY_ID, pLK_PFLR_TYP_ID, pOption);
            return Ok(obList);
        }

        //[Route("Item/EmpPOSVerify/{pHR_EMPLOYEE_ID:int}/{pMEMO_DT:DateTime}")]
        [Route("EmpAutoDataList/{pEMPLOYEE_CODE}/{pLK_JOB_STATUS_ID}/{pHR_COMPANY_ID}")]
        [HttpGet]
        // GET :  hr/api/EmpAutoDataList
        public IHttpActionResult EmpAutoDataList(string pEMPLOYEE_CODE, string pLK_JOB_STATUS_ID, Int64? pHR_COMPANY_ID)
        {
            var obList = new HR_EMPLOYEEModel().EmployeeAutoData(pEMPLOYEE_CODE, pLK_JOB_STATUS_ID, pHR_COMPANY_ID);
            return Ok(obList);
        }


        
        [Route("GetEmpDataByID")]
        [HttpGet]
        // GET :  /api/hr/GetEmpDataByID
        public IHttpActionResult GetEmpDataByID(Int64 pHR_EMPLOYEE_ID)
        {
            var obList = new HR_EMPLOYEEModel().GetEmpDataByID(pHR_EMPLOYEE_ID);
            return Ok(obList);
        }


        [Route("GetEmployeeList")]
        [HttpGet]
        [Authorize]
        // GET :  hr/api/GetEmployeeList
        public IHttpActionResult GetEmployeeList(Int64? pHR_COMPANY_ID = null, Int64? pHR_DESIGNATION_GRP_ID = null, Int32? pLK_JOB_STATUS_ID = null, Int32? pCORE_DEPT_ID = null, string pEMPLOYEE_CODE = null)
        {
            var obList = new HR_EMPLOYEEModel().GetEmployeeList(pHR_COMPANY_ID, pHR_DESIGNATION_GRP_ID, pLK_JOB_STATUS_ID, pCORE_DEPT_ID, pEMPLOYEE_CODE);
            return Ok(obList);
        }

        [Route("GetShiftTypeList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/GetShiftTypeList
        public IHttpActionResult GetShiftTypeList()
        {
            var obList = new HrShiftTypeModel().ShiftTypeListData();
            return Ok(obList);
        }

        [Route("BatchSaveBkAccAssigWithEmp")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult BatchSaveBkAccAssigWithEmp(ACC_BK_ACCOUNTModel ob)
        {
            //if (ob.ITEM_CAT_LEVEL == 1 && ob.ITEM_CAT_PREFIX == null)
            //{
            //    ModelState.AddModelError("ITEM_CAT_PREFIX", "Please insert code prefix.");
            //}

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

        [Route("Country/Save")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult Save([FromBody] HR_COUNTRYModel ob)
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


        [Route("GetOfficeList")]
        [HttpGet]
        [Authorize]
        // GET :  /api/hr/GetOfficeList
        public IHttpActionResult GetOfficeList()
        {
            var obList = new HrOfficeModel().OfficeListData();
            return Ok(obList);
        }

        [Route("GetBuildingList")]
        [HttpGet]
        [Authorize]
        // GET :  hr/api/GetBuildingList?
        public IHttpActionResult GetBuildingList(Int64? pHR_PROD_BLDNG_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            var obList = new HR_PROD_BLDNGModel().Select(pHR_PROD_BLDNG_ID, pHR_OFFICE_ID);
            return Ok(obList);
        }


        [Route("SaveBuilding")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveBuilding([FromBody] HR_PROD_BLDNGModel ob)
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


        [Route("GetFloorList")]
        [HttpGet]
        [Authorize]
        // GET :  hr/api/GetFloorList?pHR_PROD_BLDNG_ID
        public IHttpActionResult GetFloorList(Int64? pHR_PROD_BLDNG_ID = null)
        {
            var obList = new HR_PROD_FLRModel().select(pHR_PROD_BLDNG_ID);
            return Ok(obList);
        }

        [Route("getFloorByProdCategory")]
        [HttpGet]
        [Authorize]
        // GET :  api/hr/getFloorByProdCategory?pGMT_PRODUCT_TYP_ID=1
        public IHttpActionResult getFloorByProdCategory(Int64 pGMT_PRODUCT_TYP_ID)
        {
            var obList = new HR_PROD_FLRModel().getFloorByProdCategory(pGMT_PRODUCT_TYP_ID);
            return Ok(obList);
        }


        [Route("SaveFloor")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveFloor([FromBody] HR_PROD_FLRModel ob)
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



        [Route("GetPrdLineList")]
        [HttpGet]
        [Authorize]
        // GET :  hr/api/GetPrdLineList?pHR_PROD_BLDNG_ID
        public IHttpActionResult GetPrdLineList(Int64? pHR_PROD_LINE_ID = null, Int64? pHR_PROD_FLR_ID = null, Int64? pHR_PROD_BLDNG_ID = null, Int64? pHR_OFFICE_ID = null)
        {
            var obList = new HR_PROD_LINEModel().SelectByID(pHR_PROD_LINE_ID, pHR_PROD_FLR_ID, pHR_PROD_BLDNG_ID, pHR_OFFICE_ID);
            return Ok(obList);
        }

        [Route("SaveLine")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult SaveLine([FromBody] HR_PROD_LINEModel ob)
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


        [Route("GetCompanyListByID")]
        [HttpGet]
        //[Authorize]
        // GET :  hr/api/GetCompanyListByID?pHR_OFFICE_ID
        public IHttpActionResult GetCompanyListByID(Int64? pHR_OFFICE_ID = null)
        {
            var obList = new HR_COMP_OFFModel().SelectAllOfficeID(pHR_OFFICE_ID);
            return Ok(obList);
        }


        

    }
}
