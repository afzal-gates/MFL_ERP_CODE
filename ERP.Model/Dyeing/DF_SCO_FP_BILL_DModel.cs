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
    public class DF_SCO_FP_BILL_DModel
    {
        public Int64 DF_SCO_FP_BILL_D_ID { get; set; }
        public Int64 DF_SCO_FP_BILL_H_ID { get; set; }
        public Int64 DF_SCO_CHL_RCV_H_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Decimal CHLN_QTY { get; set; }
        public Decimal DEDUCT_QTY { get; set; }
        public Decimal NET_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string DF_PROC_TYPE_LST { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SCO_FP_BILL_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_D_ID", Value = ob.DF_SCO_FP_BILL_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_H_ID", Value = ob.DF_SCO_FP_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHLN_QTY", Value = ob.CHLN_QTY},
                     new CommandParameter() {ParameterName = "pDEDUCT_QTY", Value = ob.DEDUCT_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
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
            const string sp = "SP_DF_SCO_FP_BILL_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_D_ID", Value = ob.DF_SCO_FP_BILL_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_H_ID", Value = ob.DF_SCO_FP_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHLN_QTY", Value = ob.CHLN_QTY},
                     new CommandParameter() {ParameterName = "pDEDUCT_QTY", Value = ob.DEDUCT_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
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
            const string sp = "SP_DF_SCO_FP_BILL_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_D_ID", Value = ob.DF_SCO_FP_BILL_D_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_H_ID", Value = ob.DF_SCO_FP_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_CHL_RCV_H_ID", Value = ob.DF_SCO_CHL_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHLN_QTY", Value = ob.CHLN_QTY},
                     new CommandParameter() {ParameterName = "pDEDUCT_QTY", Value = ob.DEDUCT_QTY},
                     new CommandParameter() {ParameterName = "pNET_QTY", Value = ob.NET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
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

        public List<DF_SCO_FP_BILL_DModel> SelectByID(Int64? pDF_SCO_FP_BILL_H_ID = null)
        {
            string sp = "PKG_DF_SC_PROGRAM.DF_SCO_FP_BILL_D_Select";
            try
            {
                var obList = new List<DF_SCO_FP_BILL_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_H_ID", Value =pDF_SCO_FP_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_FP_BILL_DModel ob = new DF_SCO_FP_BILL_DModel();
                    ob.DF_SCO_FP_BILL_D_ID = (dr["DF_SCO_FP_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FP_BILL_D_ID"]);
                    ob.DF_SCO_FP_BILL_H_ID = (dr["DF_SCO_FP_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FP_BILL_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.CHLN_QTY = (dr["CHLN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CHLN_QTY"]);
                    ob.DEDUCT_QTY = (dr["DEDUCT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDUCT_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CHALAN_DT = (dr["CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CHALAN_DT"]);
                    ob.CHALAN_NO = (dr["CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CHALAN_NO"]);
                    ob.DF_PROC_TYPE_NAME_LIST = (dr["DF_PROC_TYPE_NAME_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_NAME_LIST"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    ob.BILL_AMT = Math.Round(ob.PROD_RATE * ob.NET_QTY, 2);
                    ob.DELV_QTY = ob.CHLN_QTY;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SCO_FP_BILL_DModel Select(long ID)
        {
            string sp = "Select_DF_SCO_FP_BILL_D";
            try
            {
                var ob = new DF_SCO_FP_BILL_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_FP_BILL_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SCO_FP_BILL_D_ID = (dr["DF_SCO_FP_BILL_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FP_BILL_D_ID"]);
                    ob.DF_SCO_FP_BILL_H_ID = (dr["DF_SCO_FP_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_FP_BILL_H_ID"]);
                    ob.DF_SCO_CHL_RCV_H_ID = (dr["DF_SCO_CHL_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_CHL_RCV_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.CHLN_QTY = (dr["CHLN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CHLN_QTY"]);
                    ob.DEDUCT_QTY = (dr["DEDUCT_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DEDUCT_QTY"]);
                    ob.NET_QTY = (dr["NET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
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

        public string DIA_TYPE_NAME { get; set; }

        public string DF_PROC_TYPE_NAME_LIST { get; set; }

        public DateTime CHALAN_DT { get; set; }

        public string CHALAN_NO { get; set; }

        public decimal PROD_RATE { get; set; }

        public decimal BILL_AMT { get; set; }

        public decimal DELV_QTY { get; set; }
    }
}