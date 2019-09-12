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
    public class HR_MBN_BILL_HModel
    {
        public Int64 HR_MBN_BILL_H_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 HR_LEAVE_TRANS_ID { get; set; }
        public Int64 HR_PAY_ELEMENT_ID { get; set; }
        public string SAL_ACC_PAY_PERIOD_LST { get; set; }
        public string SAL_MONTH_NAME_LST { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_OFFICE_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 HR_DESIGNATION_ID { get; set; }
        public Int64 HR_GRADE_ID { get; set; }
        public Int64 LK_FLOOR_ID { get; set; }
        public Int64 LINE_NO { get; set; }
        
        public string LEAVE_REF_NO { get; set; }
        public DateTime? EDD_DT { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public DateTime? NEXT_JOIN_DATE { get; set; }
        public Int64 NO_DAYS_APPL { get; set; }
        public Int64 TOTAL_PAY_AMT { get; set; }

        

        public string MbnBillProcess()
        {
            const string sp = "pkg_leave.mbn_bill_process";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value = ob.HR_MBN_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                     new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                     new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = ob.EMPLOYEE_CODE},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                     new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ob.HR_GRADE_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
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

        public string Save()
        {
            const string sp = "SP_HR_MBN_BILL_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value = ob.HR_MBN_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pHR_LEAVE_TRANS_ID", Value = ob.HR_LEAVE_TRANS_ID},
                     new CommandParameter() {ParameterName = "pHR_PAY_ELEMENT_ID", Value = ob.HR_PAY_ELEMENT_ID},
                     new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = ob.EMPLOYEE_CODE},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_OFFICE_ID", Value = ob.HR_OFFICE_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pHR_DESIGNATION_ID", Value = ob.HR_DESIGNATION_ID},
                     new CommandParameter() {ParameterName = "pHR_GRADE_ID", Value = ob.HR_GRADE_ID},
                     new CommandParameter() {ParameterName = "pLK_FLOOR_ID", Value = ob.LK_FLOOR_ID},
                     new CommandParameter() {ParameterName = "pLINE_NO", Value = ob.LINE_NO},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
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



        public List<HR_MBN_BILL_HModel> GetMbnBillHdrList(Int64 pHR_EMPLOYEE_ID)
        {
            string sp = "pkg_leave.hr_mbn_bill_h_select";
            try
            {
                var obList = new List<HR_MBN_BILL_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = pHR_EMPLOYEE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_MBN_BILL_HModel ob = new HR_MBN_BILL_HModel();
                    ob.HR_MBN_BILL_H_ID = (dr["HR_MBN_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);

                    ob.SAL_MONTH_NAME_LST = (dr["SAL_MONTH_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SAL_MONTH_NAME_LST"]);
                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.EDD_DT = (dr["EDD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EDD_DT"]);
                    ob.FROM_DATE = (dr["FROM_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = (dr["TO_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TO_DATE"]);
                    ob.NEXT_JOIN_DATE = (dr["NEXT_JOIN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);
                    ob.TOTAL_PAY_AMT = (dr["TOTAL_PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_PAY_AMT"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_MBN_BILL_HModel Select(long ID)
        {
            string sp = "Select_HR_MBN_BILL_H";
            try
            {
                var ob = new HR_MBN_BILL_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_MBN_BILL_H_ID = (dr["HR_MBN_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_H_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.HR_PAY_ELEMENT_ID = (dr["HR_PAY_ELEMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_PAY_ELEMENT_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HR_MBN_BILL_DModel> GetMbnBillDtlList(Int64 pHR_MBN_BILL_H_ID)
        {
            string sp = "pkg_leave.hr_mbn_bill_h_select";
            try
            {
                var obList = new List<HR_MBN_BILL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_MBN_BILL_H_ID", Value = pHR_MBN_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value = 3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_MBN_BILL_DModel ob = new HR_MBN_BILL_DModel();
                    ob.HR_MBN_BILL_D_ID = (dr["HR_MBN_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_D_ID"]);
                    ob.HR_MBN_BILL_H_ID = (dr["HR_MBN_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_H_ID"]);
                    ob.MBN_BILL_NO = (dr["MBN_BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MBN_BILL_NO"]);
                    ob.PHASE_NO = (dr["PHASE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PHASE_NO"]);
                    ob.NOTICE_DT = (dr["NOTICE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NOTICE_DT"]);
                    ob.PAYMENT_DT = (dr["PAYMENT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PAYMENT_DT"]);
                    ob.PAY_AMT = (dr["PAY_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PAY_AMT"]);
                    ob.OT_RATE = (dr["OT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["OT_RATE"]);
                    ob.LAST_GROSS_SALARY = (dr["LAST_GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_GROSS_SALARY"]);
                    ob.LAST_BASIC_SALARY = (dr["LAST_BASIC_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LAST_BASIC_SALARY"]);
                    ob.IS_EDIT_ENABLED = false;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }











    public class HR_MBN_BILL_EMP_AUTOModel
    {
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string SECTION_NAME { get; set; }        
        public Int64 HR_LEAVE_TRANS_ID { get; set; }
        public string LEAVE_REF_NO { get; set; }
        public DateTime? EDD_DT { get; set; }
        public DateTime? FROM_DATE { get; set; }
        public DateTime? TO_DATE { get; set; }
        public DateTime? NEXT_JOIN_DATE { get; set; }
        public Int64? NO_DAYS_APPL { get; set; }
        public Int64? HR_MBN_BILL_H_ID { get; set; }
        public Int32? NO_OF_CHILD { get; set; }

        




        public List<HR_MBN_BILL_EMP_AUTOModel> GetEmpAutoSearch(string pEMPLOYEE_CODE, string pLK_JOB_STATUS_ID)
        {
            string sp = "PKG_LEAVE.hr_mbn_bill_h_select";
            try
            {
                var obList = new List<HR_MBN_BILL_EMP_AUTOModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pEMPLOYEE_CODE", Value = pEMPLOYEE_CODE},
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = pLK_JOB_STATUS_ID},
                    new CommandParameter() {ParameterName = "pOption", Value =3000},
                    new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_MBN_BILL_EMP_AUTOModel ob = new HR_MBN_BILL_EMP_AUTOModel();

                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.SECTION_NAME = (dr["SECTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SECTION_NAME"]);

                    ob.LEAVE_REF_NO = (dr["LEAVE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LEAVE_REF_NO"]);
                    ob.EDD_DT = (dr["EDD_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EDD_DT"]);
                    ob.FROM_DATE = (dr["FROM_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FROM_DATE"]);
                    ob.TO_DATE = (dr["TO_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TO_DATE"]);
                    ob.NEXT_JOIN_DATE = (dr["NEXT_JOIN_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["NEXT_JOIN_DATE"]);
                    
                    ob.HR_LEAVE_TRANS_ID = (dr["HR_LEAVE_TRANS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_LEAVE_TRANS_ID"]);
                    ob.NO_DAYS_APPL = (dr["NO_DAYS_APPL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_DAYS_APPL"]);

                    ob.HR_MBN_BILL_H_ID = (dr["HR_MBN_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_MBN_BILL_H_ID"]);
                    ob.NO_OF_CHILD = (dr["NO_OF_CHILD"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["NO_OF_CHILD"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }

}