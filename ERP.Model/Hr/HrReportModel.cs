using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ERP.Model
{
    public class HrReportModel //: IHrCompanyModel
    {
        public Int64? REPORT_TYPE_ID { get; set; }
        public string IS_EXCEL_FORMAT { get; set; }
        public string REPORT_CODE { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        //public string COMP_NAME_EN { get; set; }

        public Int64? HR_OFFICE_ID { get; set; }
        //public string OFFICE_NAME_EN { get; set; }
        public Int64? HR_MANAGEMENT_TYPE_ID { get; set; }
        public Int64? HR_DESIGNATION_GRP_ID { get; set; }
        public Int64? DSG_GRP_ORDER { get; set; }        
        public Int64? CORE_DEPT_ID { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? HR_OT_TYPE_ID { get; set; }
        public Int64? HR_SHIFT_TEAM_ID { get; set; }
        public Int64? HR_GRADE_ID { get; set; }

        public Int64? ASSIGN_OPERATOR_ID { get; set; }

        public Int64? LK_FLOOR_ID { get; set; }
        public string FLOOR_NAME { get; set; }
        public Int64? LINE_NO { get; set; }
        public Int64? LK_GENDER_ID { get; set; }
        public Int64? HR_EMPLOYEE_ID { get; set; }
        public string HR_EMPLOYEE_IDS { get; set; }
        public string TA_FLAG { get; set; }
        public string IS_SHIFT { get; set; }
        public Int64? CARD_PRINT_OPTION_ID { get; set; }

        public Int32? HR_PAY_ELEMENT_ID { get; set; }
        public Int32? PAY_TYPE_ID { get; set; }
        public Int64? LK_JOB_STATUS_ID { get; set; }
        public string LK_JOB_STATUS_LST { get; set; }
        
        public Int64? ACC_PAY_PERIOD_ID { get; set; }
        public Int64? ACC_PAY_PERIOD_ID_MONTH { get; set; }
        public string BONUS_TYPE_NAME { get; set; }
        public string IS_PREVIOUS_MONTH { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public string FROM_OUT_TIME_WT { get; set; }
        public string TO_OUT_TIME_WT { get; set; }
        public string OT_HR_TIME { get; set; }
        public string IS_FROM_OVERLAP { get; set; }
        public string IS_TO_OVERLAP { get; set; }
        public Int32? RF_BANK_ID { get; set; }

        public Int64? HR_LEAVE_TRANS_ID { get; set; }


        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public Int64? RF_CAL_MONTH_ID { get; set; }
        public Int64? HR_LEAVE_TYPE_ID { get; set; }

        public Int64? HR_SAL_ADVANCE_ID { get; set; }

        public Int64? HR_SAL_ADV_APRVL_ID { get; set; }

        public Int64? PAY_AMOUNT { get; set; }

        public Int64? NO_OF_DAYS { get; set; }

        public string MEMO_NO { get; set; }
        public string HAS_USER_ACCOUNT { get; set; }

        public string FY_NAME_EN { get; set; }
        
        public Int64? HR_DSPLN_ACT_TYPE_ID { get; set; }
        public string IS_SINGLE_PAGE { get; set; }
        public Int64? SALARY_PART_PCT { get; set; }
        public Int64? HR_INCR_MEMO_ID { get; set; }
        public Int64? INV_ITEM_ID { get; set; }
        public Int64? HR_MBN_BILL_H_ID { get; set; }
        public Int64? HR_MBN_BILL_D_ID { get; set; }
        public Int64? PS_STORE_ID { get; set; }
        public Int64? PS_COUNTR_ID { get; set; }
        public Int64? LK_CUST_TYPE_ID { get; set; }
        public Int64? INV_ITEM_CAT_ID { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public Int32? SAL_PART_ID { get; set; }


        


        public DataSet JobCard()
        {
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            var ob = this;

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pHAS_USER_ACCOUNT", Value = ob.HAS_USER_ACCOUNT},
		            new CommandParameter() {ParameterName = "pOption", Value = 3000}
                }, sp);
            
            ds.Tables[0].TableName = "JOB_CARD";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;                        
        }

        public DataSet JobCardByVirtualOT()
        {
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            var ob = this;

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pOT_HR_TIME", Value = ob.OT_HR_TIME},
                    new CommandParameter() {ParameterName = "pHAS_USER_ACCOUNT", Value = ob.HAS_USER_ACCOUNT},
		            new CommandParameter() {ParameterName = "pOption", Value = 3072}
                }, sp);

            ds.Tables[0].TableName = "JOB_CARD";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet DailyAttendanceSummery()
        {
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            var ob = this;

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    //new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3001}
                }, sp);

            ds.Tables[0].TableName = "DAILY_ATTEN_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet DailyAttendance()
        {
            var ob = this;

            Int32 vOption=0;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            if (ob.REPORT_CODE == "RPT-1002") //Daili attendance by floor line
                vOption = 3002;            
            else if (ob.REPORT_CODE == "RPT-1004") //Daili attendance by shift team
                vOption = 3003;

            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    //new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pFROM_OUT_TIME_WT", Value = ob.FROM_OUT_TIME_WT},
                    new CommandParameter() {ParameterName = "pTO_OUT_TIME_WT", Value = ob.TO_OUT_TIME_WT},
                    new CommandParameter() {ParameterName = "pIS_FROM_OVERLAP", Value = ob.IS_FROM_OVERLAP},
                    new CommandParameter() {ParameterName = "pIS_TO_OVERLAP", Value = ob.IS_TO_OVERLAP},

		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "DAILY_ATTEN";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet LeaveApplication()
        {
            var ob = this;
            string sp = "pkg_leave.hr_leave_trans_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 3008}
                }, sp);

            //ds.Tables[0].TableName = "DAILY_ATTEN";
            ds.Tables[1].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet MaternityLeaveData()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID },
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID },
		            new CommandParameter() {ParameterName = "pOption", Value = 3004}
                }, sp);

            ds.Tables[0].TableName = "Maternity Leave Application";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet OffdayLeaveApplicationData()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 3005}
                }, sp);

            ds.Tables[0].TableName = "Offday Leave Application";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet LeaveApplicationHr()
        {
            var ob = this;
            string sp = "pkg_leave.hr_leave_trans_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TYPE_ID", Value = ob.HR_LEAVE_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},

		            new CommandParameter() {ParameterName = "pOption", Value = 3017}
                }, sp);

            //ds.Tables[0].TableName = "DAILY_ATTEN";
            ds.Tables[1].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet onlineLeaveApplication()
        {
            var ob = this;
            string sp = "pkg_leave.hr_leave_trans_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
		            new CommandParameter() {ParameterName = "pOption", Value = 3018}
                }, sp);

            //ds.Tables[0].TableName = "DAILY_ATTEN";
            ds.Tables[1].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet UnassignProxy()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
		            new CommandParameter() {ParameterName = "pOption", Value = 3006}
                }, sp);

            ds.Tables[0].TableName = "UNASSIGN_PROXY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet SalaryAdvanceApplication()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_SAL_ADVANCE_ID", Value = ob.HR_SAL_ADVANCE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3007}
                }, sp);

            ds.Tables[0].TableName = "Loan/Salary Advance Application";
            return ds;
        }

        public DataSet DailyFloorWiseAtten()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                    
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3008}
                }, sp);

            ds.Tables[0].TableName = "DAILY_FLOOR_WISE_ATTEN";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet MonthlyAttenSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    //new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3009}
                }, sp);

            ds.Tables[0].TableName = "MONTHLY_ATTEN_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet OtSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    //new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    //new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pIS_SHIFT", Value = ob.IS_SHIFT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3010}
                }, sp);

            ds.Tables[0].TableName = "OT_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet AttenBySection()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},

                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    //new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pFROM_OUT_TIME_WT", Value = ob.FROM_OUT_TIME_WT},
                    new CommandParameter() {ParameterName = "pTO_OUT_TIME_WT", Value = ob.TO_OUT_TIME_WT},
                    new CommandParameter() {ParameterName = "pIS_FROM_OVERLAP", Value = ob.IS_FROM_OVERLAP},
                    new CommandParameter() {ParameterName = "pIS_TO_OVERLAP", Value = ob.IS_TO_OVERLAP},
                    new CommandParameter() {ParameterName = "pIS_SHIFT", Value = ob.IS_SHIFT},

		            new CommandParameter() {ParameterName = "pOption", Value = 3012}
                }, sp);

            ds.Tables[0].TableName = "ATTEN_BY_SECTION";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet LeaveSummarySheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},                    
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},

                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},

		            new CommandParameter() {ParameterName = "pOption", Value = 3011}
                }, sp);

            //ds.Tables[0].TableName = "ATTEN_BY_SECTION";
            return ds;
        }

        public DataSet LeaveDetailReport()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                //new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                   
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3013}
                }, sp);

            //ds.Tables[0].TableName = "ATTEN_BY_SECTION";
            return ds;
        }

        public DataSet TiffinBill()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3014}
                }, sp);

            ds.Tables[0].TableName = "TIFFIN_BILL";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet NigthBill()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3015}
                }, sp);

            ds.Tables[0].TableName = "NIGHT_BILL";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet OvertimeBill()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = (ob.HR_OT_TYPE_ID==4?3045:3016)} //3045=Festival
                }, sp);

            ds.Tables[0].TableName = "OVERTIME_BILL";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ExtraOvertimeBill4Cp()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pOT_HR_TIME", Value = ob.OT_HR_TIME},

		            new CommandParameter() {ParameterName = "pOption", Value = 3069}
                }, sp);

            ds.Tables[0].TableName = "OVERTIME_BILL";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet SalarySheet()
        {            
            var ob = this;
            int vOption=0;
            if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 2) 
                vOption = 3046; // 70% Salary Sheet for Staff
            else if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 8)
                vOption = 3017;
            else
                vOption = 3017; // Orginal Salary Sheet

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},

                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "SALARY_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet PaySlip()
        {            
            var ob = this;
            int vOption = 0;
            if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID==2)
                vOption = 3047; // 70% Pay Slip for Staff
            else if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 8)
                vOption = 3018; // 70% Pay Slip for Staff
            else
                vOption = 3018; // Orginal Pay Slip

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pHAS_USER_ACCOUNT", Value = ob.HAS_USER_ACCOUNT},
		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "PAY_SLIP";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet DailyAttenAllocSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            object[] myParams = new object[5];

            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3019}
                }, sp);

            ds.Tables[0].TableName = "DAILY_ATTEN_ALLOC_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet PartialSalary()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3020}
                }, sp);

            ds.Tables[0].TableName = "PARTIAL_SALARY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }
        
        public DataSet SalaryTopSheetBySection()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3022}
                }, sp);

            ds.Tables[0].TableName = "SALARY_TOP_SHEET";
            ds.Tables[1].TableName = "BANK_TOP_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet SalaryTopSheet01()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3053}
                }, sp);

            ds.Tables[0].TableName = "SALARY_TOP_SHEET01";
            ds.Tables[1].TableName = "FRIDAY_OT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet IDCardPrint(string pPRJ_SERVER_PATH)
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    //new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    //new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    //new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    //new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    //new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3023}
                }, sp);

            ds.Tables[0].TableName = "ID_CARD";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet TextileDailyAttenSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    //new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3024}
                }, sp);

            ds.Tables[0].TableName = "SHIFT_DAILY_ATTEN_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet DailyEstimateManpower()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    //new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    //new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3025}
                }, sp);

            ds.Tables[0].TableName = "DAILY_ESTIMATE_MANPOWER";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet BonusSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3021}
                }, sp);

            ds.Tables[0].TableName = "BONUS_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet BonusPaySlip()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3027}
                }, sp);

            ds.Tables[0].TableName = "BONUS_PAY_SLIP";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet BonusBankStatement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3028}
                }, sp);

            ds.Tables[0].TableName = "BONUS_BANK_STATEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet BonusTopSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    //new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    //new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    //new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    ////new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    //new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    //new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    ////new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3060}
                }, sp);

            ds.Tables[0].TableName = "BONUS_TOP_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet NightBillTopSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3026}
                }, sp);

            ds.Tables[0].TableName = "NIGHT_BILL_TOP_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet PartialSalaryPaySlip()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3029}
                }, sp);

            ds.Tables[0].TableName = "PARTIAL_SALARY_PAY_SLIP";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet SalaryBankStatement()
        {
            var ob = this;
            int vOption = 3030;
            //if (ob.SALARY_PART_PCT == 30 && ob.HR_MANAGEMENT_TYPE_ID == 2)
            //    vOption = 3048; // 30% Salary Cash Statement for Staff
            //else if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 8)
            //    vOption = 3030; // Salary Cash/Bank Statement
            //else
            //    vOption = 3030; // Orginal Salary Cash/Bank Statement

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "SALARY_BANK_STATEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet SalaryCashStatPart2Pct()
        {
            var ob = this;
            int vOption = 0;
            //if (ob.SALARY_PART_PCT == 30 && ob.HR_MANAGEMENT_TYPE_ID == 2)
            //    vOption = 3048; // 30% Salary Cash Statement for Staff
            //else if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 8)
            //    vOption = 3030; // Salary Cash/Bank Statement
            //else
                vOption = 3048; // Orginal Salary Cash/Bank Statement

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},

                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "SALARY_CASH_STATE_PART2PCT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet SalaryBankStatPart1Pct()
        {
            var ob = this;
            int vOption = 3049;
            //if (ob.SALARY_PART_PCT == 30 && ob.HR_MANAGEMENT_TYPE_ID == 2)
            //    vOption = 3048; // 30% Salary Cash Statement for Staff
            //else if (ob.SALARY_PART_PCT == 70 && ob.HR_MANAGEMENT_TYPE_ID == 8)
            //    vOption = 3030; // Salary Cash/Bank Statement
            //else
            //    vOption = 3030; // Orginal Salary Cash/Bank Statement

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = vOption}
                }, sp);

            ds.Tables[0].TableName = "SALARY_BANK_STAT_PART1PCT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet DailyMovement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    //new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3032}
                }, sp);

            ds.Tables[0].TableName = "DAILY_MOVEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet FloorWiseMovement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    //new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3071}
                }, sp);

            ds.Tables[0].TableName = "DAILY_MOVEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet TerminalWiseMovement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    //new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID_MONTH", Value = ob.ACC_PAY_PERIOD_ID_MONTH},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    //new CommandParameter() {ParameterName = "pPAY_AMOUNT", Value = ob.PAY_AMOUNT},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3066}
                }, sp);

            ds.Tables[0].TableName = "DAILY_MOVEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            ds.AcceptChanges();
            return ds;
        }
        public DataSet LoanAdvStatement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                { 
                    new CommandParameter() {ParameterName = "pOption", Value = 3033}
                }, sp);

            ds.Tables[0].TableName = "LOAN_ADV_STATEMENT";
            ds.AcceptChanges();
            return ds;
        }

        public DataSet AbsentSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3034}
                }, sp);

            ds.Tables[0].TableName = "ABSENT_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet EmployeeList()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_ID", Value = ob.HR_DESIGNATION_GRP_ID},
                    
                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ob.HR_GRADE_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3035}
                }, sp);

            ds.Tables[0].TableName = "EMPLOYEE_LIST";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet MonthlyLunchSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3031}
                }, sp);

            ds.Tables[0].TableName = "MONTHLY_LUNCH_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet DailyAttenAllocSummeryByLine()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3036}
                }, sp);

            ds.Tables[0].TableName = "DAILY_ATTEN_ALLOC_SUMMERY_BY_LINE";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet POSMemoPrint()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},

		            new CommandParameter() {ParameterName = "pOption", Value = 3037}
                }, sp);

            ds.Tables[0].TableName = "POS_MEMO_PRINT";
            return ds;
        }

        public DataSet IncrProposalList()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pMEMO_NO", Value = ob.MEMO_NO},

		            new CommandParameter() {ParameterName = "pOption", Value = 3038}
                }, sp);

            ds.Tables[0].TableName = "INCR_PROPOSAL_LIST";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }
        
        public DataSet OtherBillSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3039}
                }, sp);

            ds.Tables[0].TableName = "OTHER_BILL_SUMMERY";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet EmployeeProfile(string pPRJ_SERVER_PATH)
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	
                    new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_GENDER_ID", Value = ob.LK_GENDER_ID},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = ob.LK_JOB_STATUS_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3040}
                }, sp);

            ds.Tables[0].TableName = "EMPLOYEE_PROFILE";
            ds.Tables[1].TableName = "NOMINEE_INFO";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ElEncashmentSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3041}
                }, sp);

            ds.Tables[0].TableName = "EL_ENCASHMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet ElEncashmentBankStatement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3042}
                }, sp);

            ds.Tables[0].TableName = "EL_ENCASHMENT_BANK_STATEMENT";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet DailyOperatorHelperStatus()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pTA_FLAG", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pPAY_TYPE_ID", Value = ob.PAY_TYPE_ID},
                    new CommandParameter() {ParameterName = "pRF_BANK_ID", Value = ob.RF_BANK_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3043}
                }, sp);

            ds.Tables[0].TableName = "DAILY_OPERATOR_HELPER_STATUS";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet EmpListByDiscpActType()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pNO_OF_DAYS", Value = ob.NO_OF_DAYS},
                    new CommandParameter() {ParameterName = "pHR_DSPLN_ACT_TYPE_ID", Value = ob.HR_DSPLN_ACT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3044}
                }, sp);

            ds.Tables[0].TableName = "EMP_DISCP_ACT_TYPE";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet SalaryLoanAdvDeductStatement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3050}
                }, sp);

            ds.Tables[0].TableName = "SAL_LOAN_ADV_DEDUCT_STATE";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet IncrementProposalList()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},                    
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                    
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                                        
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3051}
                }, sp);

            ds.Tables[0].TableName = "INCR_PROPOSAL_LIST";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet IncrementLetter(string pPRJ_SERVER_PATH)
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},                    
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                    
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                                        
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},

		            new CommandParameter() {ParameterName = "pOption", Value = 3052}
                }, sp);

            ds.Tables[0].TableName = "INCR_LETTER";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet SalaryAnalysis()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    //new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3054}
                }, sp);

            ds.Tables[0].TableName = "SALARY_ANALYSIS";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet POSDailySales()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},  
                    new CommandParameter() {ParameterName = "pPS_STORE_ID", Value = ob.PS_STORE_ID},
                    new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                    new CommandParameter() {ParameterName = "pLK_CUST_TYPE_ID", Value = ob.LK_CUST_TYPE_ID}, 
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
 
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},  
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3055}
                }, sp);

            ds.Tables[0].TableName = "DAILY_SALES";
            ds.Tables[1].TableName = "STORE_INFO";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet POSItemWiseSalesSummery()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
	                new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},  
                    new CommandParameter() {ParameterName = "pPS_STORE_ID", Value = ob.PS_STORE_ID},
                    new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                    new CommandParameter() {ParameterName = "pLK_CUST_TYPE_ID", Value = ob.LK_CUST_TYPE_ID}, 
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
 
                    new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},  
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3070}
                }, sp);

            ds.Tables[0].TableName = "FS_ITEM_SALES_SUMMERY";
            ds.Tables[1].TableName = "STORE_INFO";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }

        public DataSet POSSalesSummeryDateWise()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                 
                    new CommandParameter() {ParameterName = "pPS_STORE_ID", Value = ob.PS_STORE_ID},                     
                    new CommandParameter() {ParameterName = "pPS_COUNTR_ID", Value = ob.PS_COUNTR_ID},
                    new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3064}
                }, sp);

            ds.Tables[0].TableName = "DATE_WISE_SALES_SUMMERY";
            ds.Tables[1].TableName = "STORE_INFO";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }
                
        public DataSet SalaryTopSheet4Cash()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
		            new CommandParameter() {ParameterName = "pOption", Value = 3056}

                }, sp);

            ds.Tables[0].TableName = "SALARY_TOP_SHEET4CASH";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet BkashData4Payment()
        {
            var ob = this;
            //Int64 vOption = 0;

            //if(pHR_PAY_ELEMENT_ID==12) // Tiffin Bill
            //    vOption=0;
            //else if (ob.HR_PAY_ELEMENT_ID == 13) // Night Bill
            //    vOption = 3065;
            //else if (ob.HR_PAY_ELEMENT_ID == 1) //Salary Payment
            //    vOption = 3065;

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},                    
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_IDS", Value = ob.HR_EMPLOYEE_IDS},                    
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    

		            new CommandParameter() {ParameterName = "pOption", Value = 3065}

                }, sp);

            ds.Tables[0].TableName = "BKASH_DATA4PAY";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet SalaryTopSheetBySection4Cash(string pPRJ_SERVER_PATH)
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pRF_FISCAL_YEAR_ID", Value = ob.RF_FISCAL_YEAR_ID},
                    new CommandParameter() {ParameterName = "pRF_CAL_MONTH_ID", Value = ob.RF_CAL_MONTH_ID},
                    new CommandParameter() {ParameterName = "pHR_MANAGEMENT_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OT_TYPE_ID", Value = ob.HR_OT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pPRJ_SERVER_PATH", Value = pPRJ_SERVER_PATH},
                    new CommandParameter() {ParameterName = "pSAL_PART_ID", Value = ob.SAL_PART_ID},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3063}

                }, sp);

            ds.Tables[0].TableName = "SALARY_TOP_SHEET4CASH";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet IncrTopSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                    
                    new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3057}

                }, sp);

            ds.Tables[0].TableName = "INCR_TOP_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet IncrCheckSheet()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},                    
                    new CommandParameter() {ParameterName = "pHR_INCR_MEMO_ID", Value = ob.HR_INCR_MEMO_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                  
		            new CommandParameter() {ParameterName = "pOption", Value = 3058}

                }, sp);

            ds.Tables[0].TableName = "INCR_CHECK_SHEET";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet MaternityBenefitBill()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value = ob.HR_MBN_BILL_H_ID},
                    new CommandParameter() {ParameterName = "pHR_MBN_BILL_D_ID", Value = ob.HR_MBN_BILL_D_ID},          
		            new CommandParameter() {ParameterName = "pOption", Value = 3059}

                }, sp);

            ds.Tables[0].TableName = "MBN_BILL";
            ds.Tables[1].TableName = "MBN_SAL_DATA";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet EmpTaxPayStatement()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},          
		            new CommandParameter() {ParameterName = "pOption", Value = 3061}

                }, sp);

            ds.Tables[0].TableName = "TEX_PAY_STATE";
            ds.Tables[2].TableName = "COMPANY_INFO";
            
            return ds;
        }

        public DataSet AcHeadWiseSalaryDeduct()
        {
            var ob = this;
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},

                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    //new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    //new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = ob.TA_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    //new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    //new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},

		            new CommandParameter() {ParameterName = "pOption", Value = 3062}
                }, sp);

            ds.Tables[0].TableName = "AC_HEAD_WISE_DEDUCT";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet DailyOutPunchWiseMP()
        {
            var ob = this;
            
            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},                    
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_IDS", Value = ob.HR_EMPLOYEE_IDS},                    
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = ob.HR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    
		            new CommandParameter() {ParameterName = "pOption", Value = 3067}
                }, sp);

            ds.Tables[0].TableName = "DAILY_OUTPUNCH_WISE_MP";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet FairshopAdvDeducDtl()
        {
            var ob = this;

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},                                      
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},                    
                                        
		            new CommandParameter() {ParameterName = "pOption", Value = 3068}
                }, sp);

            ds.Tables[0].TableName = "FS_ADV_DEDUC_DTL";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet EmployeeList4BKMEA()
        {
            var ob = this;

            string sp = "pkg_reports.hr_rpt_select";
            //DataSet ds = new DataSet();
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},
                                        
		            new CommandParameter() {ParameterName = "pOption", Value = 3073}
                }, sp);

            ds.Tables[0].TableName = "EmployeeList4BKMEA";
            ds.Tables[2].TableName = "COMPANY_INFO";

            return ds;
        }

        public DataSet EmployeeList4MonthlySalary()
        {
            var ob = this;            
            string sp = "pkg_reports.hr_rpt_select";
            
            OraDatabase db = new OraDatabase();
            var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   	                
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = ob.HR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = ob.CORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},

                    new CommandParameter() {ParameterName = "pDSG_GRP_ORDER", Value = ob.DSG_GRP_ORDER},
                    new CommandParameter() {ParameterName = "pASSIGN_OPERATOR_ID", Value = ob.ASSIGN_OPERATOR_ID},

                    new CommandParameter() {ParameterName = "pREPORT_TYPE_ID", Value = ob.REPORT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},                    
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_LST", Value = ob.LK_JOB_STATUS_LST},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pFROM_DATE", Value = ob.FROM_DATE},
                    new CommandParameter() {ParameterName = "pTO_DATE", Value = ob.TO_DATE},

		            new CommandParameter() {ParameterName = "pOption", Value = 3074}
                }, sp);

            ds.Tables[0].TableName = "EmployeeList4MonthlySalary";
            ds.Tables[2].TableName = "COMPANY_INFO";
            return ds;
        }


    }
}
