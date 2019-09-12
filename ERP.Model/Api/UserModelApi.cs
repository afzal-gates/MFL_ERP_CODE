using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model
{
    public class UserModelApi
    {
        public Int64 SC_USER_ID { get; set; }
        public Int64 HR_EMPLOYEE_ID { get; set; }
        public string LOGIN_ID { get; set; }
        public string USER_NAME_EN { get; set; }
        public string USER_EMAIL { get; set; }
        public string USER_TYPE_DISPLAY { get; set; }

        public string EMPLOYEE_CODE { get; set; }
        public string PUSH_REGI_ID { get; set; }

        public Int64 APPROVAL_NOTI_COUNT { get; set; }

        public Int64 RF_FISCAL_YEAR_ID { get; set; }

        public Int64 HR_COMPANY_ID { get; set; }

        public Int64 HR_OFFICE_ID { get; set; }

        public Int64 DESIG_ORDER { get; set; }

        public Int64 HR_DEPARTMENT_ID { get; set; }

        public Int64 LK_FLOOR_ID { get; set; }

        public Int64 LINE_NO { get; set; }

        public Int64 SC_MENU_ID_LV { get; set; }

        public UserModelApi SelectDatabyLoginId(string LOGIN_ID)
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var ob = new UserModelApi();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3006},
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = LOGIN_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.RF_FISCAL_YEAR_ID = (dr["RF_FISCAL_YEAR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FISCAL_YEAR_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.DESIG_ORDER = (dr["DESIG_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DESIG_ORDER"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.LK_FLOOR_ID = (dr["LK_FLOOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FLOOR_ID"]);
                    ob.LINE_NO = (dr["LINE_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LINE_NO"]);
                    ob.SC_MENU_ID_LV = (dr["SC_MENU_ID_LV"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_MENU_ID_LV"]);
                    ob.APPROVAL_NOTI_COUNT = (dr["APPROVAL_NOTI_COUNT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APPROVAL_NOTI_COUNT"]);
                }

                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserModelApi registerPush(UserModelApi ob)
        {
            const string sp = "pkg_security.sc_user_update";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = ob.LOGIN_ID},
                    new CommandParameter() {ParameterName = "pPUSH_REGI_ID", Value = ob.PUSH_REGI_ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 2003},
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
            return ob;
        }

    }

}
