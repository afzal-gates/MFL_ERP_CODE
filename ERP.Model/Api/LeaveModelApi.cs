using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class LeaveModelApi
    {
        public string EMPLOYEE_CODE { get; set; }

        public Int64 HR_COMPANY_ID { get; set; }

        public string LEAVE_REF_NO { get; set; }

        public DateTime APPLY_DATE { get; set; }

        public String LV_TYPE_NAME_EN { get; set; }
        public string IS_DAY_OR_HR { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public Int64? NO_DAYS_APPL { get; set; }
        public Decimal? NO_HRS_APPL { get; set; }
        public DateTime NEXT_JOIN_DATE { get; set; }
        public DateTime? CREATION_DATE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public Int64 HR_LEAVE_APRVL_ID { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 DESIG_ORDER { get; set; }
        public Int64? LK_FLOOR_ID { get; set; }
        public Int64? LINE_NO { get; set; }
        public string REASON_DESC_EN { get; set; }

        public Int64 LK_LV_STATUS_ID { get; set; }

        public Int64 HR_OFFICE_ID { get; set; }

        public Int64 HR_LEAVE_TRANS_ID { get; set; }

        public Int64 RF_FISCAL_YEAR_ID { get; set; }

        public Int64 HR_EMPLOYEE_ID { get; set; }

        public Int64 HR_LEAVE_TYPE_ID { get; set; }

        public DateTime EDD_DT { get; set; }

        public string MSG { get; set; }

        public Int64 SC_MENU_ID { get; set; }

        public string LK_LV_STATUS { get; set; }

        public string APPROVED_BY { get; set; }

        public Int64 CREATED_BY { get; set; }

        public DateTime? LAST_LV_FROM_DATE { get; set; }

        public DateTime? LAST_LV_TO_DATE { get; set; }

        public Int64? LAST_LV_ENJOY { get; set; }

        public string REMARKS { get; set; }

        public DateTime JOINING_DT { get; set; }

        public string IS_VISIBLE { get; set; }

        public Int64? SL_BAL_DAY { get; set; }

        public Int64? SL_BAL_HR { get; set; }

        public Int64? CL_BAL_DAY { get; set; }

        public Int64? CL_BAL_HR { get; set; }

        public Int64? EL_BAL_DAY { get; set; }

        public Int64? EL_BAL_HR { get; set; }

        public Int64? OL_BAL_DAY { get; set; }

        public Int64? OL_BAL_HR { get; set; }



        public List<LeaveModelApi> GetRequesterData(string LOGIN_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<LeaveModelApi>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3019},
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = LOGIN_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LeaveModelApi ob = new LeaveModelApi();

                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.HR_LEAVE_APRVL_ID = (dr["HR_LEAVE_APRVL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_APRVL_ID"]);
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);

                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);



                    ob.LK_LV_STATUS_ID = (dr["LK_LV_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LV_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.LK_LV_STATUS = (dr["LK_LV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LV_STATUS"]);
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }

                    if (dr["FROM_DATE"] != DBNull.Value) { ob.FROM_DATE = Convert.ToDateTime(dr["FROM_DATE"]); }

                    if (dr["TO_DATE"] != DBNull.Value) { ob.TO_DATE = Convert.ToDateTime(dr["TO_DATE"]); }

                    if (dr["NEXT_JOIN_DATE"] != DBNull.Value) { ob.NEXT_JOIN_DATE = Convert.ToDateTime(dr["NEXT_JOIN_DATE"]); }

                    if (dr["APPLY_DATE"] != DBNull.Value) { ob.APPLY_DATE = Convert.ToDateTime(dr["APPLY_DATE"]); }

                    if (dr["JOINING_DT"] != DBNull.Value) { ob.JOINING_DT = Convert.ToDateTime(dr["JOINING_DT"]); }

                    if (dr["LAST_LV_FROM_DATE"] != DBNull.Value) { ob.LAST_LV_FROM_DATE = Convert.ToDateTime(dr["LAST_LV_FROM_DATE"]); }
                    if (dr["LAST_LV_TO_DATE"] != DBNull.Value) { ob.LAST_LV_TO_DATE = Convert.ToDateTime(dr["LAST_LV_TO_DATE"]); }



                    ob.LAST_LV_ENJOY = (dr["LAST_LV_ENJOY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_LV_ENJOY"]);

                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);

                    ob.SL_BAL_DAY = (dr["SL_BAL_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_BAL_DAY"]);
                    ob.SL_BAL_HR = (dr["SL_BAL_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_BAL_HR"]);

                    ob.CL_BAL_DAY = (dr["CL_BAL_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CL_BAL_DAY"]);
                    ob.CL_BAL_HR = (dr["CL_BAL_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CL_BAL_HR"]);

                    ob.EL_BAL_DAY = (dr["EL_BAL_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_BAL_DAY"]);
                    ob.EL_BAL_HR = (dr["EL_BAL_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EL_BAL_HR"]);

                    ob.OL_BAL_DAY = (dr["OL_BAL_DAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OL_BAL_DAY"]);
                    ob.OL_BAL_HR = (dr["OL_BAL_HR"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OL_BAL_HR"]);

                    ob.LV_TYPE_NAME_EN = (dr["LV_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LV_TYPE_NAME_EN"]);

                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);

                    ob.SC_MENU_ID = (dr["SC_MENU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID"]);

                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);




                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);



                    ob.REASON_DESC_EN = (dr["REASON_DESC_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC_EN"]);

                    ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_VISIBLE"]);
                    ob.IS_DAY_OR_HR = (dr["IS_DAY_OR_HR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DAY_OR_HR"]);

                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LeaveModelApi> getReqNotiData(string LOGIN_ID_ReqNoti)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                var obList = new List<LeaveModelApi>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3020},
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = LOGIN_ID_ReqNoti},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    LeaveModelApi ob = new LeaveModelApi();
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_LEAVE_TYPE_ID = (dr["HR_LEAVE_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TYPE_ID"]);

                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.LK_LV_STATUS = (dr["LK_LV_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_LV_STATUS"]);
                    ob.APPROVED_BY = (dr["APPROVED_BY"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["APPROVED_BY"]);
                    if (dr["APPLY_DATE"] != DBNull.Value) { ob.APPLY_DATE = Convert.ToDateTime(dr["APPLY_DATE"]); }
                    if (dr["CREATION_DATE"] != DBNull.Value) { ob.CREATION_DATE = Convert.ToDateTime(dr["CREATION_DATE"]); }
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 DateDiff(Int64 HR_COMPANY_ID, Int64 RF_FISCAL_YEAR_ID, Int64 HR_LEAVE_TYPE_ID, DateTime FROM_DATE, DateTime TO_DATE, Int64? HR_EMPLOYEE_ID)
        {
            string sp = "pkg_leave.hr_leave_trans_select";
            try
            {
                Int64 DateDiff = 0;
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
                    DateDiff = Convert.ToInt64(dr["DATE_DIFF"]);
                }
                return DateDiff;
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


    }
}
