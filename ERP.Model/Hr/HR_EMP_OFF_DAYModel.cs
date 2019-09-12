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
    public class HR_EMP_OFF_DAYModel
    {
        public Int64 HR_EMP_OFF_DAY_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public Int64 RF_CALENDAR_DAY_ID { get; set; }
        public Int64 HR_DAY_TYPE_ID { get; set; }
        public DateTime? EFFECTIVE_FROM { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public DateTime? EXPIRED_ON { get; set; }
        public string REMARKS { get; set; }
        public Int64 CREATED_BY { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }

        public Int64 DEFAULT_OFFDAY { get; set; }

        public List<HR_EMP_OFF_DAYModel> getEmployeeDataByDept(Int64? HR_COMPANY_ID, Int64? HR_DEPARTMENT_ID, Int64? HR_EMPLOYEE_ID)
        {
            string sp = "pkg_hr.hr_emp_off_day_select";
            try
            {
                var obList = new List<HR_EMP_OFF_DAYModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = HR_DEPARTMENT_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_OFF_DAYModel ob = new HR_EMP_OFF_DAYModel();
                    ob.HR_EMP_OFF_DAY_ID = (dr["HR_EMP_OFF_DAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_OFF_DAY_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.RF_CALENDAR_DAY_ID = (dr["RF_CALENDAR_DAY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CALENDAR_DAY_ID"]);
                    ob.HR_DAY_TYPE_ID = (dr["HR_DAY_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DAY_TYPE_ID"]);
                    if (dr["EFFECTIVE_FROM"] != DBNull.Value) { ob.EFFECTIVE_FROM = Convert.ToDateTime(dr["EFFECTIVE_FROM"]); }
                    if (dr["EXPIRED_ON"] != DBNull.Value) { ob.EXPIRED_ON = Convert.ToDateTime(dr["EXPIRED_ON"]); }
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.DEFAULT_OFFDAY = (dr["DEFAULT_OFFDAY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DEFAULT_OFFDAY"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
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

        public string Submit(List<HR_EMP_OFF_DAYModel> obList, Int64 pOption)
        {

            string vMsg = "";


            string sp = "";

            if (pOption == 1000)
            {
                sp = "pkg_hr.hr_emp_off_day_insert";
            }
            else if (pOption == 2000)
            {
                sp = "pkg_hr.hr_emp_off_day_update";
            }

            foreach (var ob in obList)
            {

                OraDatabase db = new OraDatabase();
                try
                {
                    var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMP_OFF_DAY_ID", Value = ob.HR_EMP_OFF_DAY_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pRF_CALENDAR_DAY_ID", Value = ob.RF_CALENDAR_DAY_ID},
                    new CommandParameter() {ParameterName = "pHR_DAY_TYPE_ID", Value = 2},
                    new CommandParameter() {ParameterName = "pEFFECTIVE_FROM", Value = ob.EFFECTIVE_FROM},
                    new CommandParameter() {ParameterName = "pEXPIRED_ON", Value = ob.EXPIRED_ON},
                    new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pLAST_UPDATE_LOGIN", Value = ob.LAST_UPDATE_LOGIN},
                    new CommandParameter() {ParameterName = "pOption", Value = pOption},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);
                    foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                    {
                        vMsg = dr["VALUE"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return vMsg;
        }
    }
}