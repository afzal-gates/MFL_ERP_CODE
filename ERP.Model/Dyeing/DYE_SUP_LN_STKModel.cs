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
    public class DYE_SUP_LN_STKModel
    {
        public Int64 DYE_SUP_LN_STK_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 DC_ITEM_ID { get; set; }
        public Decimal LN_RCV_QTY { get; set; }
        public Decimal LN_PAY_QTY { get; set; }
        public Decimal LN_PAY_ADJ_QTY { get; set; }
        public Decimal LN_ISS_QTY { get; set; }
        public Decimal LN_ISS_RTN_QTY { get; set; }
        public Decimal LN_RTN_ADJ_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_OP_BAL { get; set; }
        public Int64? ADJ_ITEM_ID { get; set; }

        public DateTime? OP_BAL_DT { get; set; }
        public string IS_FINALIZED { get; set; }



        public string Save()
        {
            const string sp = "pkg_dye_dc_issue.dye_sup_ln_stk_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SUP_LN_STK_ID", Value = ob.DYE_SUP_LN_STK_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     //new CommandParameter() {ParameterName = "pDYE_DC_RCV_H_ID", Value = ob.DYE_DC_RCV_H_ID},
                     //new CommandParameter() {ParameterName = "pDYE_DC_ISS_H_ID", Value = ob.DYE_DC_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLN_RCV_QTY", Value = ob.LN_RCV_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_QTY", Value = ob.LN_PAY_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_ADJ_QTY", Value = ob.LN_PAY_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_QTY", Value = ob.LN_ISS_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_RTN_QTY", Value = ob.LN_ISS_RTN_QTY},
                     new CommandParameter() {ParameterName = "pLN_RTN_ADJ_QTY", Value = ob.LN_RTN_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     //new CommandParameter() {ParameterName = "pADJ_ITEM_ID", Value = ob.ADJ_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOP_BAL_DT", Value = ob.OP_BAL_DT},
                     new CommandParameter() {ParameterName = "pIS_OP_BAL", Value = ob.IS_OP_BAL},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opDYE_SUP_LN_STK_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "SP_DYE_SUP_LN_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SUP_LN_STK_ID", Value = ob.DYE_SUP_LN_STK_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLN_RCV_QTY", Value = ob.LN_RCV_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_QTY", Value = ob.LN_PAY_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_ADJ_QTY", Value = ob.LN_PAY_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_QTY", Value = ob.LN_ISS_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_RTN_QTY", Value = ob.LN_ISS_RTN_QTY},
                     new CommandParameter() {ParameterName = "pLN_RTN_ADJ_QTY", Value = ob.LN_RTN_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pIS_OP_BAL", Value = ob.IS_OP_BAL},
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
            const string sp = "SP_DYE_SUP_LN_STK";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SUP_LN_STK_ID", Value = ob.DYE_SUP_LN_STK_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pDC_ITEM_ID", Value = ob.DC_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLN_RCV_QTY", Value = ob.LN_RCV_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_QTY", Value = ob.LN_PAY_QTY},
                     new CommandParameter() {ParameterName = "pLN_PAY_ADJ_QTY", Value = ob.LN_PAY_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_QTY", Value = ob.LN_ISS_QTY},
                     new CommandParameter() {ParameterName = "pLN_ISS_RTN_QTY", Value = ob.LN_ISS_RTN_QTY},
                     new CommandParameter() {ParameterName = "pLN_RTN_ADJ_QTY", Value = ob.LN_RTN_ADJ_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pIS_OP_BAL", Value = ob.IS_OP_BAL},
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

        public List<DYE_SUP_LN_STKModel> SelectAll()
        {
            string sp = "Select_DYE_SUP_LN_STK";
            try
            {
                var obList = new List<DYE_SUP_LN_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_SUP_LN_STK_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_SUP_LN_STKModel ob = new DYE_SUP_LN_STKModel();
                    ob.DYE_SUP_LN_STK_ID = (dr["DYE_SUP_LN_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SUP_LN_STK_ID"]);
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.DYE_DC_ISS_H_ID = (dr["DYE_DC_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.LN_RCV_QTY = (dr["LN_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RCV_QTY"]);
                    ob.LN_PAY_QTY = (dr["LN_PAY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_QTY"]);
                    ob.LN_PAY_ADJ_QTY = (dr["LN_PAY_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_ADJ_QTY"]);
                    ob.LN_ISS_QTY = (dr["LN_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_QTY"]);
                    ob.LN_ISS_RTN_QTY = (dr["LN_ISS_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_RTN_QTY"]);
                    ob.LN_RTN_ADJ_QTY = (dr["LN_RTN_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RTN_ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.IS_OP_BAL = (dr["IS_OP_BAL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_OP_BAL"]);
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

        public List<DYE_SUP_LN_STKModel> SelectBySuppID(Int64? SCM_SUPPLIER_ID, Int64? REQ_STORE_ID = null)
        {
            string sp = "PKG_DYE_DC_ISSUE.DYE_SUP_LN_STK_SELECT";
            try
            {
                var obList = new List<DYE_SUP_LN_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pREQ_STORE_ID", Value =REQ_STORE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_SUP_LN_STKModel ob = new DYE_SUP_LN_STKModel();
                    //ob.DYE_SUP_LN_STK_ID = (dr["DYE_SUP_LN_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SUP_LN_STK_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    //ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    //ob.DYE_DC_ISS_H_ID = (dr["DYE_DC_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);


                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.LN_RCV_QTY = (dr["LN_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RCV_QTY"]);
                    ob.LN_PAY_QTY = (dr["LN_PAY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_QTY"]);
                    ob.LN_PAY_ADJ_QTY = (dr["LN_PAY_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_ADJ_QTY"]);
                    ob.LN_ISS_QTY = (dr["LN_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_QTY"]);
                    ob.LN_ISS_RTN_QTY = (dr["LN_ISS_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_RTN_QTY"]);
                    ob.LN_RTN_ADJ_QTY = (dr["LN_RTN_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RTN_ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    //ob.IS_OP_BAL = (dr["IS_OP_BAL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_OP_BAL"]);
                    //ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    //ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    //ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    //ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);

                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);

                    ob.IS_ITEM_ADJ = "N";

                    ob.LN_PAY_BAL_QTY = ob.LN_RCV_QTY - (ob.LN_PAY_QTY + ob.LN_PAY_ADJ_QTY);
                    ob.LN_RTN_BAL_QTY = ob.LN_ISS_QTY - (ob.LN_ISS_RTN_QTY + ob.LN_RTN_ADJ_QTY);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DYE_SUP_LN_STKModel> SelectOpeningLN(Int64? SCM_SUPPLIER_ID)
        {
            string sp = "PKG_DYE_DC_ISSUE.DYE_SUP_LN_STK_SELECT";
            try
            {
                var obList = new List<DYE_SUP_LN_STKModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DYE_SUP_LN_STKModel ob = new DYE_SUP_LN_STKModel();
                    ob.DYE_SUP_LN_STK_ID = (dr["DYE_SUP_LN_STK_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_SUP_LN_STK_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.DYE_DC_RCV_H_ID = (dr["DYE_DC_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_RCV_H_ID"]);
                    ob.DYE_DC_ISS_H_ID = (dr["DYE_DC_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_DC_ISS_H_ID"]);


                    ob.DC_ITEM_ID = (dr["DC_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DC_ITEM_ID"]);
                    ob.LN_RCV_QTY = (dr["LN_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RCV_QTY"]);
                    ob.LN_PAY_QTY = (dr["LN_PAY_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_QTY"]);
                    ob.LN_PAY_ADJ_QTY = (dr["LN_PAY_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_PAY_ADJ_QTY"]);
                    ob.LN_ISS_QTY = (dr["LN_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_QTY"]);
                    ob.LN_ISS_RTN_QTY = (dr["LN_ISS_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_ISS_RTN_QTY"]);
                    ob.LN_RTN_ADJ_QTY = (dr["LN_RTN_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LN_RTN_ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.IS_OP_BAL = (dr["IS_OP_BAL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_OP_BAL"]);

                    if (dr["OP_BAL_DT"] != DBNull.Value)
                        ob.OP_BAL_DT = Convert.ToDateTime(dr["OP_BAL_DT"]);

                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    

                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);

                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);

                    ob.IS_ITEM_ADJ = "N";

                    ob.LN_PAY_BAL_QTY = ob.LN_RCV_QTY - (ob.LN_PAY_QTY + ob.LN_PAY_ADJ_QTY);
                    ob.LN_RTN_BAL_QTY = ob.LN_ISS_QTY - (ob.LN_ISS_RTN_QTY + ob.LN_RTN_ADJ_QTY);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MOU_CODE { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public decimal LN_RTN_BAL_QTY { get; set; }

        public decimal LN_PAY_BAL_QTY { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public decimal STOCK_QTY { get; set; }

        public string IS_ITEM_ADJ { get; set; }

        public Int64 INV_ITEM_CAT_ID { get; set; }

        public Int64? PACK_MOU_ID { get; set; }

        public decimal QTY_PER_PACK { get; set; }

        public string PACK_MOU_CODE { get; set; }

        public decimal COST_PRICE { get; set; }

        public Int64? DYE_DC_RCV_H_ID { get; set; }

        public Int64? DYE_DC_ISS_H_ID { get; set; }
    }
}