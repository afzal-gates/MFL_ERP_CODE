using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;

namespace ERP.Model
{
    public class HR_LEAVE_BALModel
    {
        public Int64 HR_LEAVE_BAL_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_FISCAL_YEAR_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 HR_LEAVE_TYPE_ID { get; set; }
        public Int64 BAL_DAY_CF { get; set; }
        public Decimal BAL_HR_CF { get; set; }
        public Int64 MAX_DAY_LV_CY { get; set; }
        public Int64 TAKE_DAY_CY { get; set; }
        public Decimal TAKE_HR_CY { get; set; }
        public Int64 ADJ_DAY_CY { get; set; }
        public Decimal ADJ_HR_CY { get; set; }
        public Int64 BAL_DAY_CY { get; set; }
        public Decimal BAL_HR_CY { get; set; }
        public string REMARKS { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }


        public String LV_TYPE_NAME_EN { get; set; }
        public String EMP_FULL_NAME_EN { get; set; }
        public String EMPLOYEE_CODE { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public Int64 EMPLOYEE_TYPE_ID { get; set; }
        public Int64 HR_MANAGEMENT_TYPE_ID { get; set; }
        public string EMP_XML { get; set; }


        public List<HR_LEAVE_BALModel> LeaveBalance(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID)
        {

            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<HR_LEAVE_BALModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_BALModel ob = new HR_LEAVE_BALModel();
                    ob.EMPLOYEE_TYPE_ID = (dr["EMPLOYEE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EMPLOYEE_TYPE_ID"]);
                    ob.HR_LEAVE_BAL_ID = (dr["HR_LEAVE_BAL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_BAL_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.BAL_DAY_CF = (dr["BAL_DAY_CF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_DAY_CF"]);
                    ob.BAL_HR_CF = (dr["BAL_HR_CF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_HR_CF"]);
                    ob.MAX_DAY_LV_CY = (dr["MAX_DAY_LV_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAY_LV_CY"]);
                    ob.TAKE_DAY_CY = (dr["TAKE_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TAKE_DAY_CY"]);
                    ob.TAKE_HR_CY = (dr["TAKE_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TAKE_HR_CY"]);
                    ob.ADJ_DAY_CY = (dr["ADJ_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADJ_DAY_CY"]);
                    ob.ADJ_HR_CY = (dr["ADJ_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_HR_CY"]);
                    ob.BAL_DAY_CY = (dr["BAL_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_DAY_CY"]);
                    ob.BAL_HR_CY = (dr["BAL_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_HR_CY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);

                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);
                    obList.Add(ob);

                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_LEAVE_BALModel> LeaveBalanceByType(long HR_COMPANY_ID, long RF_FISCAL_YEAR_ID, long HR_LEAVE_TYPE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";

            try
            {
                var obList = new List<HR_LEAVE_BALModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_LEAVE_BALModel ob = new HR_LEAVE_BALModel();
                    ob.HR_LEAVE_BAL_ID = (dr["HR_LEAVE_BAL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_BAL_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.BAL_DAY_CF = (dr["BAL_DAY_CF"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_DAY_CF"]);
                    ob.BAL_HR_CF = (dr["BAL_HR_CF"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_HR_CF"]);
                    ob.MAX_DAY_LV_CY = (dr["MAX_DAY_LV_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAX_DAY_LV_CY"]);
                    ob.TAKE_DAY_CY = (dr["TAKE_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TAKE_DAY_CY"]);
                    ob.TAKE_HR_CY = (dr["TAKE_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TAKE_HR_CY"]);
                    ob.ADJ_DAY_CY = (dr["ADJ_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADJ_DAY_CY"]);
                    ob.ADJ_HR_CY = (dr["ADJ_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_HR_CY"]);
                    ob.BAL_DAY_CY = (dr["BAL_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_DAY_CY"]);
                    ob.BAL_HR_CY = (dr["BAL_HR_CY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BAL_HR_CY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 LeaveBalanceByTypeEmp(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_EMPLOYEE_ID, Int64 HR_LEAVE_TYPE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                Int64 Balance = 0;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Balance = (dr["BAL_DAY_CY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BAL_DAY_CY"]);
                }
                return Balance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DateTime findNextWorkingDay(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, DateTime TO_DATE, Int64? HR_EMPLOYEE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                DateTime NextJoinDate = Convert.ToDateTime("01/01/1900");
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3007},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = TO_DATE},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NextJoinDate = Convert.ToDateTime(dr["CALNDR_DATE"]);
                }
                return NextJoinDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DateDiff(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID, DateTime FROM_DATE, DateTime TO_DATE, Int64? HR_EMPLOYEE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obj = new Object();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3009},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = TO_DATE},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    obj = new
                    {
                        DateDiff = Convert.ToInt64(dr["DATE_DIFF"]),
                        IS_APLY_TYM_EXP = Convert.ToString(dr["IS_APLY_TYM_EXP"])
                    };
                    

                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string AnnualLeaveByRandomPersonBatchSave()
        {
            const string sp = "pkg_leave.lv_bal_proc4random_emp";
            string jsonStr = "{";
            var ob = this;
            var i = 1;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                              
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},                    
                    new CommandParameter() {ParameterName = "pEMP_XML", Value = ob.EMP_XML},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += Convert.ToString('"') + dr["KEY"].ToString() + Convert.ToString('"') + ":" + Convert.ToString('"') + (dr["VALUE"].ToString().Replace(@"""", @"\""")) + Convert.ToString('"');
                    if (i < ds.Tables["OUTPARAM"].Rows.Count)
                    {
                        jsonStr += ",";
                    }
                    else
                    {
                        jsonStr += "}";
                    }
                    i++;
                }
                return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


    }
}