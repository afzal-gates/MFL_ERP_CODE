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
    public class RF_ITM_MOU_CONVModel
    {
        public Int64 RF_ITM_MOU_CONV_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 FRM_MOU_ID { get; set; }
        public Int64 TO_MOU_ID { get; set; }
        public Decimal CONV_FACTOR { get; set; }
        public Int64 LK_ROUND_MTHD_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string LK_ROUND_MTHD_NAME { get; set; }
        public string FRM_MOU_CODE { get; set; }
        public string TO_MOU_CODE { get; set; }
        public string ITEM_NAME_EN { get; set; }


        public string Save()
        {
            const string sp = "pkg_inventory.RF_ITM_MOU_CONV_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ITM_MOU_CONV_ID", Value = ob.RF_ITM_MOU_CONV_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFRM_MOU_ID", Value = ob.FRM_MOU_ID},
                     new CommandParameter() {ParameterName = "pTO_MOU_ID", Value = ob.TO_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONV_FACTOR", Value = ob.CONV_FACTOR},
                     new CommandParameter() {ParameterName = "pLK_ROUND_MTHD_ID", Value = ob.LK_ROUND_MTHD_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opRF_ITM_MOU_CONV_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_inventory.RF_ITM_MOU_CONV_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ITM_MOU_CONV_ID", Value = ob.RF_ITM_MOU_CONV_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFRM_MOU_ID", Value = ob.FRM_MOU_ID},
                     new CommandParameter() {ParameterName = "pTO_MOU_ID", Value = ob.TO_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONV_FACTOR", Value = ob.CONV_FACTOR},
                     new CommandParameter() {ParameterName = "pLK_ROUND_MTHD_ID", Value = ob.LK_ROUND_MTHD_ID},
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
            const string sp = "pkg_inventory.RF_ITM_MOU_CONV_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ITM_MOU_CONV_ID", Value = ob.RF_ITM_MOU_CONV_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pFRM_MOU_ID", Value = ob.FRM_MOU_ID},
                     new CommandParameter() {ParameterName = "pTO_MOU_ID", Value = ob.TO_MOU_ID},
                     new CommandParameter() {ParameterName = "pCONV_FACTOR", Value = ob.CONV_FACTOR},
                     new CommandParameter() {ParameterName = "pLK_ROUND_MTHD_ID", Value = ob.LK_ROUND_MTHD_ID},
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

        public List<RF_ITM_MOU_CONVModel> SelectAll()
        {
            string sp = "pkg_inventory.RF_ITM_MOU_CONV_Select";
            try
            {
                var obList = new List<RF_ITM_MOU_CONVModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ITM_MOU_CONV_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ITM_MOU_CONVModel ob = new RF_ITM_MOU_CONVModel();
                    ob.RF_ITM_MOU_CONV_ID = (dr["RF_ITM_MOU_CONV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITM_MOU_CONV_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.FRM_MOU_ID = (dr["FRM_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_MOU_ID"]);
                    ob.TO_MOU_ID = (dr["TO_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_MOU_ID"]);
                    ob.CONV_FACTOR = (dr["CONV_FACTOR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONV_FACTOR"]);
                    ob.LK_ROUND_MTHD_ID = (dr["LK_ROUND_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ROUND_MTHD_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.FRM_MOU_CODE = (dr["FRM_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FRM_MOU_CODE"]);
                    ob.TO_MOU_CODE = (dr["TO_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_MOU_CODE"]);
                    ob.LK_ROUND_MTHD_NAME = (dr["LK_ROUND_MTHD_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_ROUND_MTHD_NAME"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public RF_ITM_MOU_CONVModel Select(long ID)
        {
            string sp = "pkg_inventory.RF_ITM_MOU_CONV_Select";
            try
            {
                var ob = new RF_ITM_MOU_CONVModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ITM_MOU_CONV_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_ITM_MOU_CONV_ID = (dr["RF_ITM_MOU_CONV_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ITM_MOU_CONV_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.FRM_MOU_ID = (dr["FRM_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_MOU_ID"]);
                    ob.TO_MOU_ID = (dr["TO_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_MOU_ID"]);
                    ob.CONV_FACTOR = (dr["CONV_FACTOR"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONV_FACTOR"]);
                    ob.LK_ROUND_MTHD_ID = (dr["LK_ROUND_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_ROUND_MTHD_ID"]);
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

        public Int64? INV_ITEM_CAT_ID { get; set; }
    }
}