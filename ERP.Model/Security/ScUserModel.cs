using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;
using System.Web;
//using ERPSolution.Common;


namespace ERP.Model
{
    public class ChangePasswordModel
    {
        public Int64 SC_USER_ID { get; set; }

        [Required(ErrorMessage = "Please input password")]
        public string PASSWORD_HASH { get; set; }
        [Required(ErrorMessage = "Please input old password")]
        public string PASSWORD_HASH_OLD { get; set; }

        public string SaveChangePassword()
        {
            const string sp = "pkg_security.sc_user_update";
            string vMsg = "";
            var ob = this;

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                    new CommandParameter() {ParameterName = "pPASSWORD_HASH", Value = ob.PASSWORD_HASH},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Please input login ID")]
        [Remote("IsValidLoginID", "ScUser", ErrorMessage = "Please input valid user")]
        //[RemoteClientServerIsValid("IsValidLoginID", "ScUser", ErrorMessage = "Please input valid user")]
        public string LOGIN_ID { get; set; }

        [Required(ErrorMessage = "Please input password")]
        //[Remote("IsValidPassword", "ScUser", ErrorMessage = "Please input valid password for the user")]
        public string PASSWORD_HASH { get; set; }

        [Required(ErrorMessage = "Please input how much displayed below")]
        public string CAPTCHA_VALUE { get; set; }



    }

    public class LoginMemorableModel
    {
        public Int64 SC_USER_ID { get; set; }
        public int FIRST_LETTER_NUMBER { get; set; }
        public int SECOND_LETTER_NUMBER { get; set; }
        public int THIRD_LETTER_NUMBER { get; set; }

        public string FIRST_LETTER { get; set; }
        public string SECOND_LETTER { get; set; }
        public string THIRD_LETTER { get; set; }

        public List<ScUserModel> LoginMemorable()
        {
            string sp = "pkg_security.sc_user_select";

            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3003}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);
                    ob.PASSWORD_HASH = (dr["PASSWORD_HASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSWORD_HASH"]);
                    ob.MEMORABLE_TEXT = (dr["MEMORABLE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMORABLE_TEXT"]);
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

    public class ScUserVMModel
    {
        public Int64? SC_USER_ID { get; set; }
        public Int64? HR_EMPLOYEE_ID { get; set; }
        public string LOGIN_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
    }

    public class ScUserModel //: IUsersModel
    {
        public Int64 SC_USER_ID { get; set; }

        public Int64? HR_EMPLOYEE_ID { get; set; }

        [Required(ErrorMessage = "Please input login ID")]
        //[Remote("IsValidLogin","ScUser",ErrorMessage="Please input valid user")]
        public string LOGIN_ID { get; set; }
        [Required]
        public string LOGON_FLAG { get; set; }
        [Required]
        public string LOCKED_FLAG { get; set; }
        [Required]
        public int LOGIN_ATTEMPTS { get; set; }
        [Required(ErrorMessage = "Please input user name")]
        public string USER_NAME_EN { get; set; }


        public string USER_NAME_BN { get; set; }
        public string USER_DESC { get; set; }

        [Required(ErrorMessage = "Please input Mail Address")]
        public string USER_EMAIL { get; set; }

        public string IS_PWD_SET_BY_ADMIN { get; set; }

        [Required]
        public string IS_SYS_ADMIN { get; set; }
        [Required]
        public string USER_TYPE { get; set; }
        [Required(ErrorMessage = "Please choose user role")]
        public int SC_USER_STATUS_ID { get; set; }


        public DateTime? CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime? LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LAST_UPDATE_LOGIN { get; set; }
        public Int64 VERSION_NO { get; set; }

        public string IS_USER_NEVER_EXPIRE { get; set; }
        public DateTime USER_EXPIRE_ON { get; set; }
        public string IS_PWD_CNG_LOGON { get; set; }
        public string IS_PWD_NEVER_EXPIRE { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string MEMORABLE_TEXT { get; set; }

        public Int64 SC_ROLE_ID { get; set; }

        public Int64 SC_USER_ROLE_ID { get; set; }

        public string USER_TYPE_DISPLAY { get; set; }


        public Int64? HR_COMPANY_ID { get; set; }
        public string EMPLOYEE_CODE { get; set; }
        public string PASSWORD_HASH_OLD { get; set; }
        public Int32? PS_STORE_ID { get; set; }
        public string STORE_NAME_EN { get; set; }
        public Int32? PS_COUNTR_ID { get; set; }
        public Int32? COUNTER_NO { get; set; }

        //Added by Motahar
        public string EMP_FULL_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public Int32? DSG_GRP_ORDER { get; set; }
        //  End by Motahar

        public int HR_DEPARTMENT_ID { get; set; }

        public List<ScUserModel> SignIn()
        {
            string sp = "pkg_security.sc_user_select";

            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {  
                    new CommandParameter() {ParameterName = "pOption", Value = 3002},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}

                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);
                    ob.PASSWORD_HASH = (dr["PASSWORD_HASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSWORD_HASH"]);
                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ScUserModel CheckUserSession()
        {
            ScUserModel ob = new ScUserModel();
            try
            {
                ob.SC_USER_ID = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"].ToString());
                return ob;
            }
            catch (Exception ex)
            {
                ob.SC_USER_ID = 0;
                return ob;
            }
        }

        //public List<ScUserModel> SignIn()
        //{
        //    string sp = "pkg_security.sc_user_select";
        //    try
        //    {
        //        List<ScUserModel> obList = new List<ScUserModel>();
        //        using (DbCommand cmd = Database.GetStoredProcCommand(sp))
        //        {
        //            Database.AddInParameter(cmd, "pOption", DbType.Int64, 3002);
        //            Database.AddInParameter(cmd, "pMsg", DbType.String);
        //            //Database.AddInParameter(cmd, "pHR_COMPANY_ID", DbType.Int64, 1);
        //            //Database.AddInParameter(cmd, "pRF_FISCAL_YEAR_ID", DbType.Int64, 1);

        //            OracleParameter p = new OracleParameter("pResult", OracleType.Cursor);
        //            p.Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add(p);

        //            IDataReader dr = Database.ExecuteReader(cmd);
        //            using (dr)
        //            {
        //                while (dr.Read())
        //                {
        //                    ScUserModel ob = new ScUserModel();

        //                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
        //                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
        //                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
        //                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
        //                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
        //                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
        //                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
        //                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
        //                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
        //                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
        //                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
        //                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);

        //                    ob.PASSWORD_HASH = (dr["PASSWORD_HASH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASSWORD_HASH"]);

        //                    obList.Add(ob);
        //                }
        //            }
        //        }
        //        return obList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public string SaveUser()
        {
            const string sp = "pkg_security.sc_user_insert";
            string vMsg = "";
            var ob = this;
            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = ob.LOGIN_ID},
                    new CommandParameter() {ParameterName = "pLOGON_FLAG", Value = ob.LOGON_FLAG},
                    new CommandParameter() {ParameterName = "pLOCKED_FLAG", Value = ob.LOCKED_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pUSER_NAME_EN", Value = ob.USER_NAME_EN},
                    new CommandParameter() {ParameterName = "pUSER_NAME_BN", Value = ob.USER_NAME_BN},
                    new CommandParameter() {ParameterName = "pUSER_DESC", Value = ob.USER_DESC},
                    new CommandParameter() {ParameterName = "pUSER_EMAIL", Value = ob.USER_EMAIL},
                    new CommandParameter() {ParameterName = "pIS_PWD_SET_BY_ADMIN", Value = ob.IS_PWD_SET_BY_ADMIN==null?"Y":ob.IS_PWD_SET_BY_ADMIN},
                    new CommandParameter() {ParameterName = "pIS_SYS_ADMIN", Value = ob.IS_SYS_ADMIN==null?"N":ob.IS_SYS_ADMIN},
                    new CommandParameter() {ParameterName = "pUSER_TYPE", Value = ob.USER_TYPE},
                    new CommandParameter() {ParameterName = "pSC_USER_STATUS_ID", Value = ob.SC_USER_STATUS_ID},
                    new CommandParameter() {ParameterName = "pPASSWORD_HASH", Value = ob.PASSWORD_HASH},
                    new CommandParameter() {ParameterName = "pMEMORABLE_TEXT", Value = ob.MEMORABLE_TEXT},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_USER_NEVER_EXPIRE", Value = ob.IS_USER_NEVER_EXPIRE==null?"Y":ob.IS_USER_NEVER_EXPIRE},
                    new CommandParameter() {ParameterName = "pUSER_EXPIRE_ON", Value = ob.USER_EXPIRE_ON},
                    new CommandParameter() {ParameterName = "pIS_PWD_CNG_LOGON", Value = ob.IS_PWD_CNG_LOGON==null?"Y":ob.IS_PWD_CNG_LOGON},
                    new CommandParameter() {ParameterName = "pIS_PWD_NEVER_EXPIRE", Value = ob.IS_PWD_NEVER_EXPIRE==null?"Y":ob.IS_PWD_NEVER_EXPIRE},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = "Y"},
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
                vMsg = ex.ToString();
            }
            return vMsg;
        }
        public string Update()
        {
            const string sp = "pkg_security.sc_user_update"; ;
            var ob = this;
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = ob.SC_USER_ID},
                    new CommandParameter() {ParameterName = "pLOGIN_ID", Value = ob.LOGIN_ID},
                    new CommandParameter() {ParameterName = "pLOGON_FLAG", Value = ob.LOGON_FLAG},
                    new CommandParameter() {ParameterName = "pHR_EMPLOYEE_ID", Value = ob.HR_EMPLOYEE_ID},
                    new CommandParameter() {ParameterName = "pLOCKED_FLAG", Value = ob.LOCKED_FLAG},
                    new CommandParameter() {ParameterName = "pLOGIN_ATTEMPTS", Value = ob.LOGIN_ATTEMPTS},
                    new CommandParameter() {ParameterName = "pUSER_NAME_EN", Value = ob.USER_NAME_EN},
                    new CommandParameter() {ParameterName = "pUSER_NAME_BN", Value = ob.USER_NAME_BN},
                    new CommandParameter() {ParameterName = "pUSER_DESC", Value = ob.USER_DESC},
                    new CommandParameter() {ParameterName = "pUSER_EMAIL", Value = ob.USER_EMAIL},
                    new CommandParameter() {ParameterName = "pIS_PWD_SET_BY_ADMIN", Value = ob.IS_PWD_SET_BY_ADMIN},
                    new CommandParameter() {ParameterName = "pIS_SYS_ADMIN", Value = ob.IS_SYS_ADMIN},
                    new CommandParameter() {ParameterName = "pUSER_TYPE", Value = ob.USER_TYPE},
                    new CommandParameter() {ParameterName = "pSC_USER_STATUS_ID", Value = ob.SC_USER_STATUS_ID},
                    new CommandParameter() {ParameterName = "pPASSWORD_HASH", Value = ob.PASSWORD_HASH},
                    new CommandParameter() {ParameterName = "pMEMORABLE_TEXT", Value = ob.MEMORABLE_TEXT},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pIS_USER_NEVER_EXPIRE", Value = ob.IS_USER_NEVER_EXPIRE},
                    new CommandParameter() {ParameterName = "pUSER_EXPIRE_ON", Value = ob.USER_EXPIRE_ON},
                    new CommandParameter() {ParameterName = "pIS_PWD_CNG_LOGON", Value = ob.IS_PWD_CNG_LOGON},
                    new CommandParameter() {ParameterName = "pIS_PWD_NEVER_EXPIRE", Value = ob.IS_PWD_NEVER_EXPIRE},
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
                vMsg = ex.ToString();
            }

            return vMsg;
        }
        public List<ScUserModel> SelectAll()
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3000},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_PWD_CNG_LOGON = (dr["IS_PWD_CNG_LOGON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_CNG_LOGON"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);
                    ob.PARENT_DEPARTMENT_ID = (dr["PARENT_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PARENT_DEPARTMENT_ID"]);
                    ob.CORE_DEPARTMENT_ID = (dr["CORE_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["CORE_DEPARTMENT_ID"]);

                    if (dr["HR_COMPANY_ID"] != DBNull.Value)
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);

                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.DSG_GRP_ORDER = (dr["DSG_GRP_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["DSG_GRP_ORDER"]);
                    ob.CORE_DEPARTMENT_NAME_EN = (dr["CORE_DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CORE_DEPARTMENT_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);

                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);

                    if (dr["COUNTER_NO"] != DBNull.Value)
                    {
                        ob.PS_COUNTR_ID = Convert.ToInt32(dr["PS_COUNTR_ID"]);
                        ob.COUNTER_NO = Convert.ToInt32(dr["COUNTER_NO"]);
                        ob.PS_STORE_ID = Convert.ToInt32(dr["PS_STORE_ID"]);
                    }

                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["HR_DEPARTMENT_ID"]); // Newly Added By AMinul
                    ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_ROLE_ID"]); // Newly Added By AMinul
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScUserModel> SelectAllUserData()
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3007},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_PWD_CNG_LOGON = (dr["IS_PWD_CNG_LOGON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_CNG_LOGON"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
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

        public List<ScUserModel> UserDataList()
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3004},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();

                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);

                    ob.SC_ROLE_ID = (dr["SC_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_ROLE_ID"]);

                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOCKED_FLAG = (dr["LOCKED_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOCKED_FLAG"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.MEMORABLE_TEXT = (dr["MEMORABLE_TEXT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MEMORABLE_TEXT"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    if (ob.USER_TYPE == "I")
                    {
                        ob.USER_TYPE_DISPLAY = "Internal";
                    }
                    else if (ob.USER_TYPE == "E")
                    {
                        ob.USER_TYPE_DISPLAY = "External";
                    }
                    else if (ob.USER_TYPE == "G")
                    {
                        ob.USER_TYPE_DISPLAY = "Guest";
                    }
                    else if (ob.USER_TYPE == "B")
                    {
                        ob.USER_TYPE_DISPLAY = "Built in";
                    }
                    ob.IS_USER_NEVER_EXPIRE = (dr["IS_USER_NEVER_EXPIRE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_USER_NEVER_EXPIRE"]);
                    ob.USER_EXPIRE_ON = (dr["USER_EXPIRE_ON"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["USER_EXPIRE_ON"]);
                    ob.IS_PWD_CNG_LOGON = (dr["IS_PWD_CNG_LOGON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_CNG_LOGON"]);
                    ob.IS_PWD_NEVER_EXPIRE = (dr["IS_PWD_NEVER_EXPIRE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_NEVER_EXPIRE"]);

                    ob.ROLE_NAME_EN = (dr["ROLE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROLE_NAME_EN"]);
                    ob.USER_STATUS_NAME = (dr["USER_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_STATUS_NAME"]);

                    obList.Add(ob);

                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string saveRoleData(Int64 SC_USER_ID, Int64 SC_ROLE_ID)
        {
            const string sp = "pkg_security.sc_user_insert";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = SC_USER_ID},
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = "Y"},
                    new CommandParameter() {ParameterName = "pCREATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 1001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }

            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }

            return vMsg;
        }

        public string updateRoleData(Int64 SC_USER_ID, Int64 SC_ROLE_ID, Int64 SC_USER_ROLE_ID)
        {
            const string sp = "pkg_security.sc_user_update";
            string vMsg = "";

            OraDatabase db = new OraDatabase();
            try
            {
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = SC_USER_ID},
                    new CommandParameter() {ParameterName = "pSC_ROLE_ID", Value = SC_ROLE_ID},
                    new CommandParameter() {ParameterName = "pSC_USER_ROLE_ID", Value = SC_USER_ROLE_ID},
                    new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])},
                    new CommandParameter() {ParameterName = "pOption", Value = 2001},
                    new CommandParameter() {ParameterName = "pMsg", Value = 200, Direction = ParameterDirection.Output}
                }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    vMsg = dr["VALUE"].ToString();
                }
            }
            catch (Exception ex)
            {
                vMsg = ex.ToString();
            }
            return vMsg;
        }

        public List<SC_USER_ROLEModel> RolesByUserId(Int64 SC_USER_ID)
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var obList = new List<SC_USER_ROLEModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   new CommandParameter() {ParameterName = "pOption", Value = 3005},
                    new CommandParameter() {ParameterName = "pSC_USER_ID", Value = SC_USER_ID},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SC_USER_ROLEModel ob = new SC_USER_ROLEModel();
                    ob.SC_ROLE_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.SC_USER_ROLE_ID = (dr["SC_USER_ROLE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ROLE_ID"]);
                    ob.ROLE_NAME_EN = (dr["ROLE_NAME_EN"] == DBNull.Value) ? String.Empty : Convert.ToString(dr["ROLE_NAME_EN"]);
                    obList.Add(ob);
                }

                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ScUserModel> getUserData(Int64 ID)
        {
            string sp = "pkg_security.sc_user_select";
            try
            {
                var obList = new List<ScUserModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {   
                    new CommandParameter() {ParameterName = "pMC_TNA_TASK_ID", Value = ID},
                    new CommandParameter() {ParameterName = "pOption", Value = 3008},
                    new CommandParameter() {ParameterName = "pMsg", Value = 500, Direction = ParameterDirection.Output}
                }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ScUserModel ob = new ScUserModel();
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SC_USER_ID"]);
                    ob.LOGIN_ID = (dr["LOGIN_ID"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGIN_ID"]);
                    ob.LOGON_FLAG = (dr["LOGON_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOGON_FLAG"]);
                    ob.LOGIN_ATTEMPTS = (dr["LOGIN_ATTEMPTS"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LOGIN_ATTEMPTS"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.USER_NAME_BN = (dr["USER_NAME_BN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_BN"]);
                    ob.USER_DESC = (dr["USER_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DESC"]);
                    ob.USER_EMAIL = (dr["USER_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_EMAIL"]);
                    ob.IS_PWD_SET_BY_ADMIN = (dr["IS_PWD_SET_BY_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_SET_BY_ADMIN"]);
                    ob.IS_PWD_CNG_LOGON = (dr["IS_PWD_CNG_LOGON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PWD_CNG_LOGON"]);
                    ob.IS_SYS_ADMIN = (dr["IS_SYS_ADMIN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SYS_ADMIN"]);
                    ob.USER_TYPE = (dr["USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_TYPE"]);
                    ob.SC_USER_STATUS_ID = (dr["SC_USER_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_STATUS_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
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





        public string ROLE_NAME_EN { get; set; }

        public string USER_STATUS_NAME { get; set; }

        public int PARENT_DEPARTMENT_ID { get; set; }

        public int CORE_DEPARTMENT_ID { get; set; }
    }
}
