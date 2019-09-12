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
    public class CM_UD_DModel
    {
        public Int64 CM_UD_D_ID { get; set; }
        public Int64 CM_UD_H_ID { get; set; }
        public Int64 CM_EXP_LC_H_ID { get; set; }
        public Int64 CM_IMP_LC_H_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_UD_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_D_ID", Value = ob.CM_UD_D_ID},
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
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
            const string sp = "SP_CM_UD_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_D_ID", Value = ob.CM_UD_D_ID},
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
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
            const string sp = "SP_CM_UD_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_D_ID", Value = ob.CM_UD_D_ID},
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value = ob.CM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
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

        public List<CM_UD_DModel> SelectImpUD(Int64? pCM_UD_H_ID = null)
        {
            string sp = "PKG_CM_UD.CM_UD_D_Select";
            try
            {
                var obList = new List<CM_UD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value =pCM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_UD_DModel ob = new CM_UD_DModel();
                    ob.CM_UD_D_ID = (dr["CM_UD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_D_ID"]);
                    ob.CM_UD_H_ID = (dr["CM_UD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
                    ob.LC_VALUE = (dr["LC_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_VALUE"]);

                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CM_UD_DModel> SelectExpUD(Int64? pCM_UD_H_ID = null)
        {
            string sp = "PKG_CM_UD.CM_UD_D_Select";
            try
            {
                var obList = new List<CM_UD_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_H_ID", Value =pCM_UD_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_UD_DModel ob = new CM_UD_DModel();
                    ob.CM_UD_D_ID = (dr["CM_UD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_D_ID"]);
                    ob.CM_UD_H_ID = (dr["CM_UD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
                    ob.LC_VALUE = (dr["LC_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_VALUE"]);
                    
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_UD_DModel Select(long ID)
        {
            string sp = "Select_CM_UD_D";
            try
            {
                var ob = new CM_UD_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_UD_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_UD_D_ID = (dr["CM_UD_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_D_ID"]);
                    ob.CM_UD_H_ID = (dr["CM_UD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_UD_H_ID"]);
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
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

        public DateTime ISSUE_DT { get; set; }

        public decimal PCT_TOLR_AMT { get; set; }

        public string EXP_LCSC_NO { get; set; }

        public string CURR_NAME_EN { get; set; }

        public decimal LC_VALUE { get; set; }

        public string IMP_LC_NO { get; set; }
    }
}