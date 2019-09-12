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
    public class SCM_STR_NDL_ISS_DModel
    {
        public Int64 SCM_STR_NDL_ISS_D_ID { get; set; }
        public Int64 SCM_STR_NDL_ISS_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Int64 ISS_QTY_PCS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_STR_NDL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_D_ID", Value = ob.SCM_STR_NDL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_PCS", Value = ob.ISS_QTY_PCS},
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
            const string sp = "SP_SCM_STR_NDL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_D_ID", Value = ob.SCM_STR_NDL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_PCS", Value = ob.ISS_QTY_PCS},
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
            const string sp = "SP_SCM_STR_NDL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_D_ID", Value = ob.SCM_STR_NDL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_PCS", Value = ob.ISS_QTY_PCS},
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

        public List<SCM_STR_NDL_ISS_DModel> SelectAll(Int64? pSCM_STR_NDL_ISS_H_ID = null)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_D_SELECT";
            try
            {
                var obList = new List<SCM_STR_NDL_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value =pSCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_NDL_ISS_DModel ob = new SCM_STR_NDL_ISS_DModel();
                    ob.SCM_STR_NDL_ISS_D_ID = (dr["SCM_STR_NDL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_D_ID"]);
                    ob.SCM_STR_NDL_ISS_H_ID = (dr["SCM_STR_NDL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SL_NO = (dr["SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_NO"]);
                    ob.ISS_QTY_PCS = (dr["ISS_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_PCS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                   
                    ob.BRK_QTY_PCS = (dr["BRK_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BRK_QTY_PCS"]);
                    ob.RQD_QTY_PCS = (dr["RQD_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY_PCS"]);
                   
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    //ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    //ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                    ob.CENTRAL_STR_STOCK = (dr["CENTRAL_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CENTRAL_STR_STOCK"]);
                    ob.SUB_STR_STOCK = (dr["SUB_STR_STOCK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SUB_STR_STOCK"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_NDL_ISS_DModel Select(Int64? pSCM_STR_NDL_ISS_H_ID = null)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_D_SELECT";
            try
            {
                var ob = new SCM_STR_NDL_ISS_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value =pSCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_NDL_ISS_D_ID = (dr["SCM_STR_NDL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_D_ID"]);
                    ob.SCM_STR_NDL_ISS_H_ID = (dr["SCM_STR_NDL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SL_NO = (dr["SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SL_NO"]);
                    ob.ISS_QTY_PCS = (dr["ISS_QTY_PCS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_QTY_PCS"]);
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

        public string ITEM_NAME_EN { get; set; }

        public string KNT_MACHINE_NO { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }

        public long CENTRAL_STR_STOCK { get; set; }

        public long SUB_STR_STOCK { get; set; }

        public long BRK_QTY_PCS { get; set; }

        public long RQD_QTY_PCS { get; set; }

        public decimal COST_PRICE { get; set; }

        public long SL_NO { get; set; }

        public decimal UNIT_PRICE { get; set; }
    }
}