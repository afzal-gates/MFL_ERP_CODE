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
    public class SCM_STR_ITM_ADJ_DModel
    {
        public Int64 SCM_STR_ITM_ADJ_D_ID { get; set; }
        public Int64 SCM_STR_ITM_ADJ_H_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Decimal STOCK_QTY { get; set; }
        public Decimal ADJ_STOCK_QTY { get; set; }
        public Decimal ADJ_QTY { get; set; }
        
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal COST_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "PKG_SCM_INV_ADJ.SCM_STR_ITM_ADJ_D_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_D_ID", Value = ob.SCM_STR_ITM_ADJ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_H_ID", Value = ob.SCM_STR_ITM_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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
            const string sp = "PKG_SCM_INV_ADJ.SCM_STR_ITM_ADJ_D_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_D_ID", Value = ob.SCM_STR_ITM_ADJ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_H_ID", Value = ob.SCM_STR_ITM_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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
            const string sp = "PKG_SCM_INV_ADJ.SCM_STR_ITM_ADJ_D_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_D_ID", Value = ob.SCM_STR_ITM_ADJ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_H_ID", Value = ob.SCM_STR_ITM_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pADJ_QTY", Value = ob.ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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

        public List<SCM_STR_ITM_ADJ_DModel> SelectAll()
        {
            string sp = "Select_SCM_STR_ITM_ADJ_D";
            try
            {
                var obList = new List<SCM_STR_ITM_ADJ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_ITM_ADJ_DModel ob = new SCM_STR_ITM_ADJ_DModel();
                    ob.SCM_STR_ITM_ADJ_D_ID = (dr["SCM_STR_ITM_ADJ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_D_ID"]);
                    ob.SCM_STR_ITM_ADJ_H_ID = (dr["SCM_STR_ITM_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
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


        public List<SCM_STR_ITM_ADJ_DModel> SelectByID(Int64? pSCM_STR_ITM_ADJ_H_ID = null)
        {
            string sp = "PKG_SCM_INV_ADJ.SCM_STR_ITM_ADJ_D_SELECT";
            try
            {
                var obList = new List<SCM_STR_ITM_ADJ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_H_ID", Value =pSCM_STR_ITM_ADJ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_ITM_ADJ_DModel ob = new SCM_STR_ITM_ADJ_DModel();
                    ob.SCM_STR_ITM_ADJ_D_ID = (dr["SCM_STR_ITM_ADJ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_D_ID"]);
                    ob.SCM_STR_ITM_ADJ_H_ID = (dr["SCM_STR_ITM_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);
                    ob.ADJ_STOCK_QTY = (dr["ADJ_STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_STOCK_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.TTL_VALUE = Math.Abs(ob.ADJ_QTY * ob.COST_PRICE);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_ITM_ADJ_DModel Select(long ID)
        {
            string sp = "Select_SCM_STR_ITM_ADJ_D";
            try
            {
                var ob = new SCM_STR_ITM_ADJ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_ITM_ADJ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_ITM_ADJ_D_ID = (dr["SCM_STR_ITM_ADJ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_D_ID"]);
                    ob.SCM_STR_ITM_ADJ_H_ID = (dr["SCM_STR_ITM_ADJ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_ITM_ADJ_H_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);
                    ob.ADJ_STOCK_QTY = (dr["ADJ_STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_STOCK_QTY"]);
                    ob.ADJ_QTY = (dr["ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
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

        public string MOU_CODE { get; set; }

        public decimal TTL_VALUE { get; set; }
    }
}