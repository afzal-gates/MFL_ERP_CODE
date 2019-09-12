using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data;

namespace ERP.Model
{
    public class HR_SAL_TRAN_HModel //: IHR_SAL_TRAN_HModel
    {
        public Int64 HR_SAL_TRAN_H_ID { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 HR_DESIGNATION_ID { get; set; }
        public Int64 HR_GRADE_ID { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public Int64 LINE_NO { get; set; }
        public Int64 PERIOD_DAYS { get; set; }
        public Int64 PRE_DAYS { get; set; }
        public Int64 ABS_DAYS { get; set; }
        public Int64 LEAVE_DAYS { get; set; }
        public Int64 HOLI_DAYS { get; set; }

        public Int64 LATE_DAYS { get; set; }
        public Int64 MISSED_OUT_PUNCH_DAYS { get; set; }
        public decimal BASIC_PER_DAY { get; set; }            

        public string IS_SAL_OFF { get; set; }
        public string IS_ADV_DED_OFF { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string ACC_PAY_PERIOD_NAME { get; set; } //Created for Job Card Mail Notification by Aminul 

        public string SalaryProcess(Int64? pHR_COMPANY_ID, Int64? pACC_PAY_PERIOD_ID, Int64? pHR_OFFICE_ID, Int64? pHR_DEPARTMENT_ID,
            Int64? pHR_DESIGNATION_ID, Int64? pHR_SHIFT_TEAM_ID, Int64? pHR_EMPLOYEE_ID,
            Int64? pHR_MANAGEMENT_TYPE_ID, Int64? pLK_FLOOR_ID, Int64? pLINE_NO, Int64? pCORE_DEPT_ID)
        {
            const string sp = "pkg_salary.hr_salary_process";// "pkg_attendance.hr_attendance_process";            
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = pHR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pHR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = 0000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }                
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }


        public string PartialSalaryProcess(long? pHR_COMPANY_ID, long? pACC_PAY_PERIOD_ID, Int64? pHR_OFFICE_ID, long? pHR_DEPARTMENT_ID, long? pHR_DESIGNATION_ID, long? pHR_SHIFT_TEAM_ID,
            long? pHR_EMPLOYEE_ID, Int64? pLK_RELIGION_ID, long? pHR_MANAGEMENT_TYPE_ID, long? pLK_FLOOR_ID, long? pLINE_NO, long? pCORE_DEPT_ID, string pIS_INCLUDE_ADVANCE, string pIS_INCLUDE_OT,
            Int64? pROUND_AMOUNT, Int64? pROUND_TYPE_ID, Int64? pADDL_PRE_DAYS, string pHR_DESIGNATION_GRP_IDS, DateTime? pOT_START_DT, DateTime? pOT_END_DT)
        {
            const string sp = "pkg_salary.hr_part_salary_process";// "pkg_attendance.hr_attendance_process";            
            string vMsg = "";
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = pHR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = pHR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = pHR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_SHIFT_TEAM_ID", Value = pHR_SHIFT_TEAM_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = pLK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = pLINE_NO},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_TYPE_ID", Value = pHR_MANAGEMENT_TYPE_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pLK_RELIGION_ID", Value = pLK_RELIGION_ID},
                    new CommandParameter() {ParameterName = "pCORE_DEPT_ID", Value = pCORE_DEPT_ID},
                    new CommandParameter() {ParameterName = "pIS_INCLUDE_OT", Value = pIS_INCLUDE_OT},
                    new CommandParameter() {ParameterName = "pIS_INCLUDE_ADVANCE", Value = pIS_INCLUDE_ADVANCE},
                    new CommandParameter() {ParameterName = "pROUND_AMOUNT", Value = pROUND_AMOUNT},
                    new CommandParameter() {ParameterName = "pROUND_TYPE_ID", Value = pROUND_TYPE_ID},
                    new CommandParameter() {ParameterName = "pADDL_PRE_DAYS", Value = pADDL_PRE_DAYS},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_GRP_IDS", Value = pHR_DESIGNATION_GRP_IDS},
                    new CommandParameter() {ParameterName = "pOT_START_DT", Value = pOT_START_DT},
                    new CommandParameter() {ParameterName = "pOT_END_DT", Value = pOT_END_DT},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                    new CommandParameter() {ParameterName = "pOption", Value = 0000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }                
            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }
            return vMsg;
        }


        public HR_SAL_TRAN_HModel SalaryTranHdrData(Int64 pACC_PAY_PERIOD_ID, Int64 pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_salary.hr_sal_tran_h_select";
            
            try
            {
                var ob = this;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_SAL_TRAN_H_ID = (dr["HR_SAL_TRAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_TRAN_H_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.PERIOD_DAYS = (dr["PERIOD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERIOD_DAYS"]);
                    ob.PRE_DAYS = (dr["PRE_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DAYS"]);
                    ob.ABS_DAYS = (dr["ABS_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ABS_DAYS"]);
                    ob.LEAVE_DAYS = (dr["LEAVE_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAVE_DAYS"]);


                    ob.IS_SAL_OFF = (dr["IS_SAL_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAL_OFF"]);
                    ob.IS_ADV_DED_OFF = (dr["IS_ADV_DED_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ADV_DED_OFF"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                }
                return ob;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_SAL_TRAN_HModel AttenSummeryData(long pACC_PAY_PERIOD_ID, long pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_salary.hr_sal_tran_h_select";
            try
            {
                var ob = this;

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3003},                    
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_SAL_TRAN_H_ID = (dr["HR_SAL_TRAN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SAL_TRAN_H_ID"]);
                    ob.ACC_PAY_PERIOD_ID = (dr["ACC_PAY_PERIOD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_PAY_PERIOD_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.PERIOD_DAYS = (dr["PERIOD_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERIOD_DAYS"]);
                    ob.PRE_DAYS = (dr["PRE_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRE_DAYS"]);
                    ob.ABS_DAYS = (dr["ABS_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ABS_DAYS"]);
                    ob.LEAVE_DAYS = (dr["LEAVE_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LEAVE_DAYS"]);
                    ob.HOLI_DAYS = (dr["HOLI_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLI_DAYS"]);
                    ob.HOLI_DAYS = (dr["HOLI_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HOLI_DAYS"]);

                    ob.LATE_DAYS = (dr["LATE_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LATE_DAYS"]);
                    ob.MISSED_OUT_PUNCH_DAYS = (dr["MISSED_OUT_PUNCH_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MISSED_OUT_PUNCH_DAYS"]);
                    ob.BASIC_PER_DAY = (dr["BASIC_PER_DAY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BASIC_PER_DAY"]);

                    ob.IS_SAL_OFF = (dr["IS_SAL_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SAL_OFF"]);
                    ob.IS_ADV_DED_OFF = (dr["IS_ADV_DED_OFF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ADV_DED_OFF"]);
                }
                return ob;                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BatchSave(List<HR_SAL_TRAN_DModel> obList)
        {            
            string vMsg = "";
            string sp = "";
            Int64 vSalTranHID=0;
            Int32 vOption;

            var obHDR = this;

            try
            {
                if (obHDR.HR_SAL_TRAN_H_ID != null && obHDR.HR_SAL_TRAN_H_ID > 0)
                {
                    sp = "pkg_salary.hr_sal_tran_h_update";
                    vOption = 2000;
                }
                else
                {
                    sp = "pkg_salary.hr_sal_tran_h_insert";
                    vOption = 1000;
                }

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_SAL_TRAN_H_ID", Value = obHDR.HR_SAL_TRAN_H_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = obHDR.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = obHDR.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = obHDR.EMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = obHDR.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = obHDR.HR_OFFICE_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = obHDR.HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = obHDR.HR_DESIGNATION_ID},
                    new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = obHDR.HR_GRADE_ID},
                    new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = obHDR.LK_FLOOR_ID},
                    new CommandParameter() {ParameterName = "pLINE_NO", Value = obHDR.LINE_NO},
                    new CommandParameter() {ParameterName = "pPERIOD_DAYS", Value = obHDR.PERIOD_DAYS},
                    new CommandParameter() {ParameterName = "pPRE_DAYS", Value = obHDR.PRE_DAYS},
                    new CommandParameter() {ParameterName = "pABS_DAYS", Value = obHDR.ABS_DAYS},
                    new CommandParameter() {ParameterName = "pLEAVE_DAYS", Value = obHDR.LEAVE_DAYS},
                    new CommandParameter() {ParameterName = "pHOLI_DAYS", Value = obHDR.HOLI_DAYS},
                    new CommandParameter() {ParameterName = "pIS_SAL_OFF", Value = obHDR.IS_SAL_OFF},
                    new CommandParameter() {ParameterName = "pIS_ADV_DED_OFF", Value = obHDR.IS_ADV_DED_OFF},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                    new CommandParameter() {ParameterName = "pOption", Value = vOption},                    
                    new CommandParameter() {ParameterName = "pSAL_TRAN_HDR_ID", Value = 200, Direction = ParameterDirection.Output},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    if (dr["KEY"].ToString()=="PMSG")
                        vMsg = dr["VALUE"].ToString();
                    else if (dr["KEY"].ToString() == "PSAL_TRAN_HDR_ID")
                        vSalTranHID = Convert.ToInt64(dr["VALUE"]);
                }
                
                string vMsgStr = vMsg.Substring(0, 9);
                if (vMsgStr == "MULTI-001")
                {
                    foreach (HR_SAL_TRAN_DModel ob in obList)
                    {
                        if (ob.HR_SAL_TRAN_D_ID != null && ob.HR_SAL_TRAN_D_ID > 0)
                        {
                            sp = "pkg_salary.hr_sal_tran_d_update";
                            vOption = 2000;
                        }
                        else
                        {
                            sp = "pkg_salary.hr_sal_tran_d_insert";
                            vOption = 1000;
                        }

                        var ds1 = db.ExecuteStoredProcedure(new List<CommandParameter>()
                        {
                            new CommandParameter() {ParameterName = "pHR_SAL_TRAN_D_ID", Value = ob.HR_SAL_TRAN_D_ID},
                            new CommandParameter() {ParameterName = "pHR_SAL_TRAN_H_ID", Value = vSalTranHID},
                            new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                            new CommandParameter() {ParameterName = "pPAY_QTY_ACT", Value = ob.PAY_QTY_ACT},
                            new CommandParameter() {ParameterName = "pPAY_QTY_EXM", Value = ob.PAY_QTY_EXM},
                            new CommandParameter() {ParameterName = "pPAY_QTY", Value = ob.PAY_QTY},
                            new CommandParameter() {ParameterName = "pPAY_RATE", Value = ob.PAY_RATE},
                            new CommandParameter() {ParameterName = "pPAY_UNIT_FLAG", Value = ob.PAY_UNIT_FLAG},
                            new CommandParameter() {ParameterName = "pPAY_AMT", Value = ob.PAY_AMT},
                            new CommandParameter() {ParameterName = "pIS_ARREAR", Value = ob.IS_ARREAR},
                            new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                            new CommandParameter() {ParameterName = "pIS_PROCESSED", Value = ob.IS_PROCESSED},

                            new CommandParameter() {ParameterName = "pOption", Value = vOption},
                            new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                        }, sp);

                        foreach (DataRow dr in ds1.Tables["OUTPARAM"].Rows)
                        {
                            vMsg = dr["VALUE"].ToString();
                        }                        
                    }

                }

            }
            catch (Exception ex)
            {
                vMsg = "MULTI-005" + ex.Message;
            }

            return new { success = true, vMsg = vMsg, vSalTranHID = vSalTranHID };
        }



        public HR_SAL_TRAN_HModel getAccPayPeriodName(long? pACC_PAY_PERIOD_ID)
        {
            string sp = "pkg_salary.hr_sal_tran_h_select";

            try
            {
                var ob = this;
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.ACC_PAY_PERIOD_NAME = (dr["ACC_PAY_PERIOD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACC_PAY_PERIOD_NAME"]);
                }
                return ob;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}