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
    public class DF_BT_MVM_CUT_STR_HModel
    {
        public Int64 DF_BT_MVM_CUT_STR_H_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public DateTime RECEIVE_DT { get; set; }
        public Int64 RCV_NO_ROLL { get; set; }
        public Decimal RCV_FAB_QTY { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_LOT { get; set; }


        public string Save()
        {
            const string sp = "PKG_DF_FIN_FAB.DF_BT_MVM_CUT_STR_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_CUT_STR_H_ID", Value = ob.DF_BT_MVM_CUT_STR_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pRCV_NO_ROLL", Value = ob.RCV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pRCV_FAB_QTY", Value = ob.RCV_FAB_QTY},                     
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
            const string sp = "SP_DF_BT_MVM_CUT_STR_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_CUT_STR_H_ID", Value = ob.DF_BT_MVM_CUT_STR_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pRCV_NO_ROLL", Value = ob.RCV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pRCV_FAB_QTY", Value = ob.RCV_FAB_QTY},
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
            const string sp = "SP_DF_BT_MVM_CUT_STR_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_CUT_STR_H_ID", Value = ob.DF_BT_MVM_CUT_STR_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRECEIVE_DT", Value = ob.RECEIVE_DT},
                     new CommandParameter() {ParameterName = "pRCV_NO_ROLL", Value = ob.RCV_NO_ROLL},
                     new CommandParameter() {ParameterName = "pRCV_FAB_QTY", Value = ob.RCV_FAB_QTY},
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

        public List<DF_BT_MVM_CUT_STR_HModel> SelectAll()
        {
            string sp = "Select_DF_BT_MVM_CUT_STR_H";
            try
            {
                var obList = new List<DF_BT_MVM_CUT_STR_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_CUT_STR_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_MVM_CUT_STR_HModel ob = new DF_BT_MVM_CUT_STR_HModel();
                    ob.DF_BT_MVM_CUT_STR_H_ID = (dr["DF_BT_MVM_CUT_STR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_CUT_STR_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RECEIVE_DT = (dr["RECEIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RECEIVE_DT"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_FAB_QTY = (dr["RCV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FAB_QTY"]);
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

        public DF_BT_MVM_CUT_STR_HModel Select(long ID)
        {
            string sp = "Select_DF_BT_MVM_CUT_STR_H";
            try
            {
                var ob = new DF_BT_MVM_CUT_STR_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_MVM_CUT_STR_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_MVM_CUT_STR_H_ID = (dr["DF_BT_MVM_CUT_STR_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_MVM_CUT_STR_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RECEIVE_DT = (dr["RECEIVE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RECEIVE_DT"]);
                    ob.RCV_NO_ROLL = (dr["RCV_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_NO_ROLL"]);
                    ob.RCV_FAB_QTY = (dr["RCV_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_FAB_QTY"]);
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