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
    public class HR_EMPLOYEE_VM
    {
        public Int64? HR_EMPLOYEE_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string EMP_FULL_NAME_BN { get; set; }
        public Int64? HR_COMPANY_ID { get; set; }
        public Int64? HR_OFFICE_ID { get; set; }
        public Int64? HR_DEPARTMENT_ID { get; set; }
        public Int64? HR_DESIGNATION_ID { get; set; }
        public Int64? HR_GRADE_ID { get; set; }
        public Int64? LK_FLOOR_ID { get; set; }
        public Int64? LINE_NO { get; set; }
        public Int64? LK_JOB_STATUS_ID { get; set; }
        public DateTime? JOINING_DT { get; set; }      
        public Int64? LK_GENDER_ID { get; set; }
        public Int64? HR_SHIFT_PLAN_ID { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string LAST_ADV_REF { get; set; }
        public Int64? DESIG_ORDER { get; set; }
        public Int64? HR_ORGANO_DEPARTMENT_ID { get; set; }
        public decimal GROSS_SALARY { get; set; }
        public Int64? MIN_LEVEL { get; set; }
        public Int64? REQ_YEARS { get; set; }
        public Int64? SALARY_FOR_APPLY { get; set; }
        public Int64? MONTH_GAP { get; set; }
        public Int64? DSG_GRP_ORDER { get; set; }
        public Int64? RF_FISCAL_YEAR_ID { get; set; }
        public DateTime? ADV_RQST_DATE { get; set; }
        public Int64? MAX_NO_INSTL { get; set; }

        public List<HR_EMPLOYEE_VM> getEmployeeByUserId()
        {
            string sp = "pkg_hr.hr_employee_select";

            try
            {
                var obList = new List<HR_EMPLOYEE_VM>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pLK_JOB_STATUS_ID", Value = 30},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMPLOYEE_VM ob = new HR_EMPLOYEE_VM();
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.LAST_ADV_REF = (dr["LAST_ADV_REF"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LAST_ADV_REF"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.EMP_FULL_NAME_BN = (dr["EMP_FULL_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_BN"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["CORE_DEPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CORE_DEPT_ID"]); 
                    ob.HR_DESIGNATION_ID = (dr["HR_DESIGNATION_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DESIGNATION_ID"]);
                    ob.HR_GRADE_ID = (dr["HR_GRADE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_GRADE_ID"]);
                    if (dr["LK_FLOOR_ID"] != DBNull.Value)
                    { ob.LK_FLOOR_ID = Convert.ToInt64(dr["LK_FLOOR_ID"]); }
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.LK_JOB_STATUS_ID = (dr["LK_JOB_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_JOB_STATUS_ID"]);
                    if (dr["JOINING_DT"] != DBNull.Value)
                    { ob.JOINING_DT = Convert.ToDateTime(dr["JOINING_DT"]); }
                    ob.ADV_RQST_DATE = DateTime.Today;
                    ob.GROSS_SALARY = (dr["GROSS_SALARY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["GROSS_SALARY"]);
                    ob.LK_GENDER_ID = (dr["LK_GENDER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_GENDER_ID"]);
                    ob.REQ_YEARS = (dr["REQ_YEARS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_YEARS"]);
                    ob.MIN_LEVEL = (dr["MIN_LEVEL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MIN_LEVEL"]);
                    ob.SALARY_FOR_APPLY = (dr["SALARY_FOR_APPLY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SALARY_FOR_APPLY"]);
                    ob.MAX_NO_INSTL = (dr["NO_OF_INSTALLMENT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_INSTALLMENT"]);
                    ob.MONTH_GAP = (dr["MONTH_GAP"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MONTH_GAP"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DSG_GRP_ORDER"]);
                    ob.HR_SHIFT_PLAN_ID = (dr["HR_SHIFT_PLAN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SHIFT_PLAN_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
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