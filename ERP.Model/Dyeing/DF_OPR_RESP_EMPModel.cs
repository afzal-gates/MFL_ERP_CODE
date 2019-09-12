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
    public class DF_OPR_RESP_EMPModel
    {
        public Int64 DF_OPR_RESP_EMP_ID { get; set; }
        public Int64 LK_DF_EMP_TYPE_ID { get; set; }
        public Int64 RESP_EMP_ID { get; set; }
        public string PASS_CODE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_OPR_RESP_EMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_OPR_RESP_EMP_ID", Value = ob.DF_OPR_RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DF_EMP_TYPE_ID", Value = ob.LK_DF_EMP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRESP_EMP_ID", Value = ob.RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pPASS_CODE", Value = ob.PASS_CODE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_OPR_RESP_EMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_OPR_RESP_EMP_ID", Value = ob.DF_OPR_RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DF_EMP_TYPE_ID", Value = ob.LK_DF_EMP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRESP_EMP_ID", Value = ob.RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pPASS_CODE", Value = ob.PASS_CODE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_DF_OPR_RESP_EMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_OPR_RESP_EMP_ID", Value = ob.DF_OPR_RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pLK_DF_EMP_TYPE_ID", Value = ob.LK_DF_EMP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRESP_EMP_ID", Value = ob.RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pPASS_CODE", Value = ob.PASS_CODE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<DF_OPR_RESP_EMPModel> SelectAll(Int64? pLK_DF_EMP_TYPE_ID = null)
        {
            string sp = "PKG_DYE_COMMON.df_opr_resp_emp_select";
            try
            {
                var obList = new List<DF_OPR_RESP_EMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_OPR_RESP_EMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pLK_DF_EMP_TYPE_ID", Value =pLK_DF_EMP_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_OPR_RESP_EMPModel ob = new DF_OPR_RESP_EMPModel();
                    ob.DF_OPR_RESP_EMP_ID = (dr["DF_OPR_RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_OPR_RESP_EMP_ID"]);
                    ob.LK_DF_EMP_TYPE_ID = (dr["LK_DF_EMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DF_EMP_TYPE_ID"]);
                    ob.HR_EMPLOYEE_ID = (dr["HR_EMPLOYEE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_EMPLOYEE_ID"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);
                    ob.PASS_CODE = (dr["PASS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASS_CODE"]);
                    ob.EMPLOYEE_CODE = (dr["EMPLOYEE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMPLOYEE_CODE"]);                    
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_OPR_RESP_EMPModel Select(long ID)
        {
            string sp = "PKG_DYE_COMMON.df_opr_resp_emp_select";
            try
            {
                var ob = new DF_OPR_RESP_EMPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_OPR_RESP_EMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_OPR_RESP_EMP_ID = (dr["DF_OPR_RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_OPR_RESP_EMP_ID"]);
                    ob.LK_DF_EMP_TYPE_ID = (dr["LK_DF_EMP_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DF_EMP_TYPE_ID"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);
                    ob.PASS_CODE = (dr["PASS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PASS_CODE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string EMP_FULL_NAME_EN { get; set; }

        public long HR_EMPLOYEE_ID { get; set; }

        public string EMPLOYEE_CODE { get; set; }
    }
}