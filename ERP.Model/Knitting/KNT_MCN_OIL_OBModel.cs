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
    public class KNT_MCN_OIL_OBModel
    {
        public Int64 KNT_MCN_OIL_OB_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public DateTime OB_RCV_DT { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 OB_RCV_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Decimal COST_PRICE { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal LOC_CONV_RATE { get; set; }
        
        public string Save()
        {
            const string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_OB_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_OB_ID", Value = ob.KNT_MCN_OIL_OB_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_DT", Value = ob.OB_RCV_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_QTY", Value = ob.OB_RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_OB_UPDATE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_OB_ID", Value = ob.KNT_MCN_OIL_OB_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_DT", Value = ob.OB_RCV_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_QTY", Value = ob.OB_RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_OB_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_OB_ID", Value = ob.KNT_MCN_OIL_OB_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_DT", Value = ob.OB_RCV_DT},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOB_RCV_QTY", Value = ob.OB_RCV_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLOC_CONV_RATE", Value = ob.LOC_CONV_RATE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<KNT_MCN_OIL_OBModel> SelectAll(Int64 pageNumber, Int64 pageSize,Int64? pHR_COMPANY_ID = null, Int64? pSCM_STORE_ID = null)
        {
            string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_OB_SELECT";
            try
            {
                var obList = new List<KNT_MCN_OIL_OBModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_OB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pageNumber", Value =pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_MCN_OIL_OBModel ob = new KNT_MCN_OIL_OBModel();
                    ob.KNT_MCN_OIL_OB_ID = (dr["KNT_MCN_OIL_OB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_OB_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.OB_RCV_DT = (dr["OB_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OB_RCV_DT"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.OB_RCV_QTY = (dr["OB_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OB_RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_MCN_OIL_OBModel Select(long ID)
        {
            string sp = "PKG_KNT_MCN_OIL.KNT_MCN_OIL_OB_SELECT";
            try
            {
                var ob = new KNT_MCN_OIL_OBModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_MCN_OIL_OB_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_MCN_OIL_OB_ID = (dr["KNT_MCN_OIL_OB_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_MCN_OIL_OB_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.OB_RCV_DT = (dr["OB_RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["OB_RCV_DT"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.OB_RCV_QTY = (dr["OB_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["OB_RCV_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
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

        public string COMP_NAME_EN { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string MOU_CODE { get; set; }

        public int TOTAL_REC { get; set; }

        public long INV_ITEM_CAT_ID { get; set; }
    }
}