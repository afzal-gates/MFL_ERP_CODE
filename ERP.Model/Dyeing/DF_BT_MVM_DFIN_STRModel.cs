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
    public class DF_BT_MVM_DFIN_STRModel
    {
        public Int64 DF_BT_MVM_DFIN_STR_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public DateTime RECEIVE_DT { get; set; }
        public DateTime ISSUE_DT { get; set; }
        public Int64 ACT_NO_ROLL { get; set; }
        public Decimal ACT_FINIS_QTY { get; set; }
        public Int64 CUT_STR_ISS_AUTH_BY { get; set; }
        public string IS_FINALIZED { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? NEXT_SCM_STORE_ID { get; set; }
        public string XML_LOT { get; set; }


        public string Save()
        {
            const string sp = "PKG_DF_FIN_FAB.DF_BT_MVM_DFIN_STR_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_DFIN_STR_ID", Value = ob.DF_BT_MVM_DFIN_STR_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pNEXT_SCM_STORE_ID", Value = ob.NEXT_SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pCUT_STR_ISS_AUTH_BY", Value = ob.CUT_STR_ISS_AUTH_BY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_LOT", Value = ob.XML_LOT},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_DF_BT_MVM_DFIN_STR";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_DFIN_STR_ID", Value = ob.DF_BT_MVM_DFIN_STR_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pCUT_STR_ISS_AUTH_BY", Value = ob.CUT_STR_ISS_AUTH_BY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "SP_DF_BT_MVM_DFIN_STR";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_DFIN_STR_ID", Value = ob.DF_BT_MVM_DFIN_STR_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pACT_NO_ROLL", Value = ob.ACT_NO_ROLL},
                     new CommandParameter() {ParameterName = "pACT_FINIS_QTY", Value = ob.ACT_FINIS_QTY},
                     new CommandParameter() {ParameterName = "pCUT_STR_ISS_AUTH_BY", Value = ob.CUT_STR_ISS_AUTH_BY},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<DF_BT_MVM_DFIN_STRModel> SelectAll()
        {
            string sp = "Select_DF_BT_MVM_DFIN_STR";
            try
            {
                var obList = new List<DF_BT_MVM_DFIN_STRModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_DFIN_STR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_MVM_DFIN_STRModel ob = new DF_BT_MVM_DFIN_STRModel();
                    ob.DF_BT_MVM_DFIN_STR_ID = (dr["DF_BT_MVM_DFIN_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_DFIN_STR_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RECEIVE_DT = (dr["RECEIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RECEIVE_DT"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.CUT_STR_ISS_AUTH_BY = (dr["CUT_STR_ISS_AUTH_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUT_STR_ISS_AUTH_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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

        public DF_BT_MVM_DFIN_STRModel Select(long ID)
        {
            string sp = "Select_DF_BT_MVM_DFIN_STR";
            try
            {
                var ob = new DF_BT_MVM_DFIN_STRModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_DFIN_STR_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_MVM_DFIN_STR_ID = (dr["DF_BT_MVM_DFIN_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_DFIN_STR_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RECEIVE_DT = (dr["RECEIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RECEIVE_DT"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.CUT_STR_ISS_AUTH_BY = (dr["CUT_STR_ISS_AUTH_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CUT_STR_ISS_AUTH_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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
    }
}