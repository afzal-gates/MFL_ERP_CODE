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
    public class DF_SCO_PT_BILL_D1Model
    {
        public Int64 DF_SCO_PT_BILL_D1_ID { get; set; }
        public Int64 DF_SCO_PT_BILL_H_ID { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Decimal BILL_QTY { get; set; }
        public Int64 MC_FAB_PROC_RATE_ID { get; set; }
        public Decimal PROD_RATE { get; set; }
        public Int64 ACC_COA_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_DF_SCO_PT_BILL_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_D1_ID", Value = ob.DF_SCO_PT_BILL_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBILL_QTY", Value = ob.BILL_QTY},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pPROD_RATE", Value = ob.PROD_RATE},
                     new CommandParameter() {ParameterName = "pACC_COA_ID", Value = ob.ACC_COA_ID},
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
            const string sp = "SP_DF_SCO_PT_BILL_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_D1_ID", Value = ob.DF_SCO_PT_BILL_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBILL_QTY", Value = ob.BILL_QTY},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pPROD_RATE", Value = ob.PROD_RATE},
                     new CommandParameter() {ParameterName = "pACC_COA_ID", Value = ob.ACC_COA_ID},
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
            const string sp = "SP_DF_SCO_PT_BILL_D1";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_D1_ID", Value = ob.DF_SCO_PT_BILL_D1_ID},
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBILL_QTY", Value = ob.BILL_QTY},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_RATE_ID", Value = ob.MC_FAB_PROC_RATE_ID},
                     new CommandParameter() {ParameterName = "pPROD_RATE", Value = ob.PROD_RATE},
                     new CommandParameter() {ParameterName = "pACC_COA_ID", Value = ob.ACC_COA_ID},
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

        public List<DF_SCO_PT_BILL_D1Model> SelectAll()
        {
            string sp = "Select_DF_SCO_PT_BILL_D1";
            try
            {
                var obList = new List<DF_SCO_PT_BILL_D1Model>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_PT_BILL_D1Model ob = new DF_SCO_PT_BILL_D1Model();
                    ob.DF_SCO_PT_BILL_D1_ID = (dr["DF_SCO_PT_BILL_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_D1_ID"]);
                    ob.DF_SCO_PT_BILL_H_ID = (dr["DF_SCO_PT_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.BILL_QTY = (dr["BILL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BILL_QTY"]);
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.ACC_COA_ID = (dr["ACC_COA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COA_ID"]);
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

        public DF_SCO_PT_BILL_D1Model Select(long ID)
        {
            string sp = "Select_DF_SCO_PT_BILL_D1";
            try
            {
                var ob = new DF_SCO_PT_BILL_D1Model();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_D1_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SCO_PT_BILL_D1_ID = (dr["DF_SCO_PT_BILL_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_D1_ID"]);
                    ob.DF_SCO_PT_BILL_H_ID = (dr["DF_SCO_PT_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_H_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.BILL_QTY = (dr["BILL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BILL_QTY"]);
                    ob.MC_FAB_PROC_RATE_ID = (dr["MC_FAB_PROC_RATE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_RATE_ID"]);
                    ob.PROD_RATE = (dr["PROD_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PROD_RATE"]);
                    ob.ACC_COA_ID = (dr["ACC_COA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACC_COA_ID"]);
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