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
    public class KNT_MCN_OIL_ISS_DModel
    {
        public Int64 KNT_MCN_OIL_ISS_D_ID { get; set; }
        public Int64 KNT_MCN_OIL_ISS_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 KNT_MACHINE_ID { get; set; }
        public Int64 OPERATOR_ID { get; set; }
        public Decimal ISS_QTY_KG { get; set; }
        public Decimal ISS_QTY_LTR { get; set; }
        public Decimal KG_COST_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_MCN_OIL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_D_ID", Value = ob.KNT_MCN_OIL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_H_ID", Value = ob.KNT_MCN_OIL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pISS_QTY_LTR", Value = ob.ISS_QTY_LTR},
                     new CommandParameter() {ParameterName = "pKG_COST_PRICE", Value = ob.KG_COST_PRICE},
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
            const string sp = "SP_KNT_MCN_OIL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_D_ID", Value = ob.KNT_MCN_OIL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_H_ID", Value = ob.KNT_MCN_OIL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pISS_QTY_LTR", Value = ob.ISS_QTY_LTR},
                     new CommandParameter() {ParameterName = "pKG_COST_PRICE", Value = ob.KG_COST_PRICE},
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
            const string sp = "SP_KNT_MCN_OIL_ISS_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_D_ID", Value = ob.KNT_MCN_OIL_ISS_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_H_ID", Value = ob.KNT_MCN_OIL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_MACHINE_ID", Value = ob.KNT_MACHINE_ID},
                     new CommandParameter() {ParameterName = "pOPERATOR_ID", Value = ob.OPERATOR_ID},
                     new CommandParameter() {ParameterName = "pISS_QTY_KG", Value = ob.ISS_QTY_KG},
                     new CommandParameter() {ParameterName = "pISS_QTY_LTR", Value = ob.ISS_QTY_LTR},
                     new CommandParameter() {ParameterName = "pKG_COST_PRICE", Value = ob.KG_COST_PRICE},
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

        public List<KNT_MCN_OIL_ISS_DModel> SelectAll()
        {
            string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_ISS_D_SELECT";
            try
            {
                var obList = new List<KNT_MCN_OIL_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_OIL_ISS_DModel ob = new KNT_MCN_OIL_ISS_DModel();
                    ob.KNT_MCN_OIL_ISS_D_ID = (dr["KNT_MCN_OIL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_D_ID"]);
                    ob.KNT_MCN_OIL_ISS_H_ID = (dr["KNT_MCN_OIL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_KG"]);
                    ob.ISS_QTY_LTR = (dr["ISS_QTY_LTR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_LTR"]);
                    ob.KG_COST_PRICE = (dr["KG_COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KG_COST_PRICE"]);
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

        public KNT_MCN_OIL_ISS_DModel Select(long ID)
        {
            string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_ISS_D_SELECT";
            try
            {
                var ob = new KNT_MCN_OIL_ISS_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MCN_OIL_ISS_D_ID = (dr["KNT_MCN_OIL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_D_ID"]);
                    ob.KNT_MCN_OIL_ISS_H_ID = (dr["KNT_MCN_OIL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_KG"]);
                    ob.ISS_QTY_LTR = (dr["ISS_QTY_LTR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_LTR"]);
                    ob.KG_COST_PRICE = (dr["KG_COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KG_COST_PRICE"]);
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

        public List<KNT_MCN_OIL_ISS_DModel> SelectByID(Int64 pKNT_MCN_OIL_ISS_H_ID)
        {
            string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_ISS_D_SELECT";
            try
            {
                var obList = new List<KNT_MCN_OIL_ISS_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_ISS_H_ID", Value =pKNT_MCN_OIL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_OIL_ISS_DModel ob = new KNT_MCN_OIL_ISS_DModel();
                    ob.KNT_MCN_OIL_ISS_D_ID = (dr["KNT_MCN_OIL_ISS_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_D_ID"]);
                    ob.KNT_MCN_OIL_ISS_H_ID = (dr["KNT_MCN_OIL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_ISS_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.KNT_MACHINE_ID = (dr["KNT_MACHINE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MACHINE_ID"]);
                    ob.OPERATOR_ID = (dr["OPERATOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OPERATOR_ID"]);
                    ob.ISS_QTY_KG = (dr["ISS_QTY_KG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_KG"]);
                    ob.ISS_QTY_LTR = (dr["ISS_QTY_LTR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY_LTR"]);
                    ob.KG_COST_PRICE = (dr["KG_COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["KG_COST_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.KNT_MACHINE_NO = (dr["KNT_MACHINE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["KNT_MACHINE_NO"]);
                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }

        public string KNT_MACHINE_NO { get; set; }

        public string EMP_FULL_NAME_EN { get; set; }
    }
}