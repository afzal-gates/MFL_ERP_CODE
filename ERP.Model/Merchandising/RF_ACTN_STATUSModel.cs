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
    public class RF_ACTN_STATUSModel
    {
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public Int64 RF_ACTN_TYPE_ID { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public Int64 ACTION_ORDER { get; set; }
        public string IS_REPEAT { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public Int64 APROVER_LVL_NO { get; set; }
        public string IS_NOTIFY_EMAIL { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_actn_status_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_STATUS_NAME", Value = ob.ACTN_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pACTION_SORT", Value = ob.ACTION_SORT},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pASSIGNED_TO_LST", Value = ob.ASSIGNED_TO_LST},
                     new CommandParameter() {ParameterName = "pALT_ASSIGNED_TO_ID", Value = ob.ALT_ASSIGNED_TO_ID},
                     new CommandParameter() {ParameterName = "pEXEC_BY_ID", Value = ob.EXEC_BY_ID},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value = ob.EVENT_NAME},
                     new CommandParameter() {ParameterName = "pNXT_ACTION_NAME", Value = ob.NXT_ACTION_NAME},
                     new CommandParameter() {ParameterName = "pACTN_ROLE_FLAG", Value = ob.ACTN_ROLE_FLAG},
                     new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                     new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
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

        public string Update()
        {
            const string sp = "SP_RF_ACTN_STATUS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_STATUS_NAME", Value = ob.ACTN_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pACTION_ORDER", Value = ob.ACTION_ORDER},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
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

        public string Delete()
        {
            const string sp = "SP_RF_ACTN_STATUS";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_STATUS_NAME", Value = ob.ACTN_STATUS_NAME},
                     new CommandParameter() {ParameterName = "pACTION_ORDER", Value = ob.ACTION_ORDER},
                     new CommandParameter() {ParameterName = "pIS_REPEAT", Value = ob.IS_REPEAT},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<RF_ACTN_STATUSModel> SelectAll()
        {
            string sp = "pkg_common.RF_ACTN_STATUS_select";
            try
            {
                var obList = new List<RF_ACTN_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTN_STATUSModel ob = new RF_ACTN_STATUSModel();
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_SORT = (dr["ACTION_SORT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_SORT"]);
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.ASSIGNED_TO_LST = (dr["ASSIGNED_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ASSIGNED_TO_LST"]);
                    ob.ALT_ASSIGNED_TO_ID = (dr["ALT_ASSIGNED_TO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_ASSIGNED_TO_ID"]);
                    ob.EXEC_BY_ID = (dr["EXEC_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXEC_BY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_ACTN_STATUSModel> RfActionStatusByID(Int64 RF_ACTN_TYPE_ID, Int64? PARENT_ID, Int64 CURRENT_USER)
        {
            string sp = "pkg_common.rf_actn_status_select";
            try
            {
                var obList = new List<RF_ACTN_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value =RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value =PARENT_ID},
                     new CommandParameter() {ParameterName = "pCURRENT_USER", Value =CURRENT_USER},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTN_STATUSModel ob = new RF_ACTN_STATUSModel();
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_SORT = (dr["ACTION_SORT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_SORT"]);
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.ASSIGNED_TO_LST = (dr["ASSIGNED_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ASSIGNED_TO_LST"]);
                    ob.ALT_ASSIGNED_TO_ID = (dr["ALT_ASSIGNED_TO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_ASSIGNED_TO_ID"]);
                    ob.EXEC_BY_ID = (dr["EXEC_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXEC_BY_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RF_ACTN_STATUSModel> Select(Int64? pRF_ACTN_TYPE_ID = null)
        {
            string sp = "pkg_common.rf_actn_status_select";
            try
            {
                var obList = new List<RF_ACTN_STATUSModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value =pRF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTN_STATUSModel ob = new RF_ACTN_STATUSModel();
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.ACTION_SORT = (dr["ACTION_SORT"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACTION_SORT"]);
                    ob.IS_REPEAT = (dr["IS_REPEAT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_REPEAT"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.ASSIGNED_TO_LST = (dr["ASSIGNED_TO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ASSIGNED_TO_LST"]);
                    ob.ALT_ASSIGNED_TO_ID = (dr["ALT_ASSIGNED_TO_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ALT_ASSIGNED_TO_ID"]);
                    ob.EXEC_BY_ID = (dr["EXEC_BY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXEC_BY_ID"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.ALT_USER_NAME = (dr["ALT_USER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ALT_USER_NAME"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    
                    obList.Add(ob);
                }
                return obList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long ACTION_SORT { get; set; }

        public Int64? PARENT_ID { get; set; }

        public Int64? HR_DEPARTMENT_ID { get; set; }

        public string ASSIGNED_TO_LST { get; set; }

        public long ALT_ASSIGNED_TO_ID { get; set; }

        public Int64? EXEC_BY_ID { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_TYPE_NAME { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string ALT_USER_NAME { get; set; }
    }
}