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
    public class HR_EMP_REJOINModel
    {
        public Int64 HR_EMP_REJOIN_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }

        [Required(ErrorMessage="Please select employee")]
        public Int64 HR_EMPLOYEE_ID { get; set; }
        [Required(ErrorMessage="Please select rejoin date")]
        public DateTime? REJOIN_DT { get; set; }
        public string REMARKS_EN { get; set; }
        public string REMARKS_BN { get; set; }
        //public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        //public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string EMPLOYEE_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public Int64 ACC_PAY_PERIOD_ID { get; set; }
        

        public string Save()
        {
            const string sp = "pkg_hr.hr_emp_rejoin_insert";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMP_REJOIN_ID", Value = ob.HR_EMP_REJOIN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pREJOIN_DT", Value = ob.REJOIN_DT},
                    new CommandParameter() {ParameterName = "pREMARKS_EN", Value = ob.REMARKS_EN},
                    new CommandParameter() {ParameterName = "pREMARKS_BN", Value = ob.REMARKS_BN},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public string Update()
        {
            const string sp = "pkg_hr.hr_emp_rejoin_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMP_REJOIN_ID", Value = ob.HR_EMP_REJOIN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pREJOIN_DT", Value = ob.REJOIN_DT},
                    new CommandParameter() {ParameterName = "pREMARKS_EN", Value = ob.REMARKS_EN},
                    new CommandParameter() {ParameterName = "pREMARKS_BN", Value = ob.REMARKS_BN},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    new CommandParameter() {ParameterName = "pOption", Value = 2000},
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

        public string Delete()
        {
            const string sp = "pkg_hr.hr_emp_rejoin_delete";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pHR_EMP_REJOIN_ID", Value = ob.HR_EMP_REJOIN_ID},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = ob.ACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pREJOIN_DT", Value = ob.REJOIN_DT},
                    new CommandParameter() {ParameterName = "pREMARKS_EN", Value = ob.REMARKS_EN},
                    new CommandParameter() {ParameterName = "pREMARKS_BN", Value = ob.REMARKS_BN},
                    new CommandParameter() {ParameterName = "pOption", Value = 4000},
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

        public List<HR_EMP_REJOINModel> SelectAll()
        {
            string sp = "Select_HR_EMP_REJOIN";

            try
            {
                var obList = new List<HR_EMP_REJOINModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_EMP_REJOIN_ID", Value = 0},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_REJOINModel ob = new HR_EMP_REJOINModel();
                    ob.HR_EMP_REJOIN_ID = (dr["HR_EMP_REJOIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_REJOIN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.REJOIN_DT = (dr["REJOIN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REJOIN_DT"]);
                    ob.REMARKS_EN = (dr["REMARKS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_EN"]);
                    ob.REMARKS_BN = (dr["REMARKS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_BN"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HR_EMP_REJOINModel Select(long ID)
        {
            string sp = "Select_HR_EMP_REJOIN";
            try
            {
                var ob = this;

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pHR_EMP_REJOIN_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_EMP_REJOIN_ID = (dr["HR_EMP_REJOIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_REJOIN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.REJOIN_DT = (dr["REJOIN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REJOIN_DT"]);
                    ob.REMARKS_EN = (dr["REMARKS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_EN"]);
                    ob.REMARKS_BN = (dr["REMARKS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_BN"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
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

        public List<HR_EMP_REJOINModel> RejoinListData(long pHR_COMPANY_ID, long pACC_PAY_PERIOD_ID)
        {
            string sp = "pkg_hr.hr_emp_rejoin_select";

            try
            {
                var obList = new List<HR_EMP_REJOINModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = pHR_COMPANY_ID},
                    new CommandParameter() {ParameterName = "pACC_PAY_PERIOD_ID", Value = pACC_PAY_PERIOD_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_EMP_REJOINModel ob = new HR_EMP_REJOINModel();
                    ob.HR_EMP_REJOIN_ID = (dr["HR_EMP_REJOIN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMP_REJOIN_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.REJOIN_DT = (dr["REJOIN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REJOIN_DT"]);
                    ob.REMARKS_EN = (dr["REMARKS_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_EN"]);
                    ob.REMARKS_BN = (dr["REMARKS_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_BN"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LAST_UPDATE_LOGIN = (dr["LAST_UPDATE_LOGIN"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATE_LOGIN"]);
                    ob.VERSION_NO = (dr["VERSION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["VERSION_NO"]);
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