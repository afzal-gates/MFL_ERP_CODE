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
    public class KNT_YRN_STR_RPL_REQ_DModel
    {
        public Int64 KNT_YRN_STR_RPL_REQ_D_ID { get; set; }
        public Int64 KNT_YRN_STR_RPL_REQ_H_ID { get; set; }
        public Int64 CM_IMP_LC_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Decimal ACT_ISS_QTY { get; set; }
        public Decimal REQ_RTN_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public Decimal COST_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        
        public string IMP_LC_NO { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string YRN_LOT_NO { get; set; }
        public decimal STOCK_QTY { get; set; }
        public string LK_YRN_COLR_GRP_NAME { get; set; }
        public decimal LOC_CONV_RATE { get; set; }
        public decimal PCT_ADDL_PRICE { get; set; }
        public decimal RCV_QTY { get; set; }
        public decimal PACK_RCV_QTY { get; set; }
        public string PACK_MOU_CODE { get; set; }
        public string MOU_CODE { get; set; }
        public long CONE_QTY { get; set; }
        public decimal CONE_WT { get; set; }
        public decimal TOTAL_AMT { get; set; }
        public long LK_YRN_COLR_GRP_ID { get; set; }
        public long RF_BRAND_ID { get; set; }



        public string Save()
        {
            const string sp = "SP_KNT_YRN_STR_RPL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_D_ID", Value = ob.KNT_YRN_STR_RPL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_H_ID", Value = ob.KNT_YRN_STR_RPL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pREQ_RTN_QTY", Value = ob.REQ_RTN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_KNT_YRN_STR_RPL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_D_ID", Value = ob.KNT_YRN_STR_RPL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_H_ID", Value = ob.KNT_YRN_STR_RPL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pREQ_RTN_QTY", Value = ob.REQ_RTN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_KNT_YRN_STR_RPL_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_D_ID", Value = ob.KNT_YRN_STR_RPL_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_H_ID", Value = ob.KNT_YRN_STR_RPL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pREQ_RTN_QTY", Value = ob.REQ_RTN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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

        public List<KNT_YRN_STR_RPL_REQ_DModel> SelectAll()
        {
            string sp = "PKG_KNT_RCV_RTN.KNT_YRN_STR_RPL_REQ_D_SELECT";
            try
            {
                var obList = new List<KNT_YRN_STR_RPL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_RPL_REQ_DModel ob = new KNT_YRN_STR_RPL_REQ_DModel();
                    ob.KNT_YRN_STR_RPL_REQ_D_ID = (dr["KNT_YRN_STR_RPL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_D_ID"]);
                    ob.KNT_YRN_STR_RPL_REQ_H_ID = (dr["KNT_YRN_STR_RPL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.REQ_RTN_QTY = (dr["REQ_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_RTN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
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


        public List<KNT_YRN_STR_RPL_REQ_DModel> SelectByID(Int64? pKNT_YRN_STR_RPL_REQ_H_ID = null)
        {
            string sp = "PKG_KNT_RCV_RTN.KNT_YRN_STR_RPL_REQ_D_SELECT";
            try
            {
                var obList = new List<KNT_YRN_STR_RPL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_H_ID", Value =pKNT_YRN_STR_RPL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_RPL_REQ_DModel ob = new KNT_YRN_STR_RPL_REQ_DModel();
                    ob.KNT_YRN_STR_RPL_REQ_D_ID = (dr["KNT_YRN_STR_RPL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_D_ID"]);
                    ob.KNT_YRN_STR_RPL_REQ_H_ID = (dr["KNT_YRN_STR_RPL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.REQ_RTN_QTY = (dr["REQ_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_RTN_QTY"]);
                    ob.ACT_ISS_QTY = (dr["ACT_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);

                    ob.LK_YRN_COLR_GRP_NAME = (dr["LK_YRN_COLR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YRN_COLR_GRP_NAME"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADDL_PRICE"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);

                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.PACK_RCV_QTY = (dr["PACK_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RCV_QTY"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.CONE_QTY = (dr["CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONE_QTY"]);
                    ob.CONE_WT = (dr["CONE_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONE_WT"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.TOTAL_AMT = ob.UNIT_PRICE * ob.RCV_QTY;
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_YRN_STR_RPL_REQ_DModel> SelectBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "PKG_KNT_RCV_RTN.KNT_YRN_STR_RPL_REQ_D_SELECT";
            try
            {
                var obList = new List<KNT_YRN_STR_RPL_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_RPL_REQ_DModel ob = new KNT_YRN_STR_RPL_REQ_DModel();
                    ob.KNT_YRN_STR_RPL_REQ_D_ID = (dr["KNT_YRN_STR_RPL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_D_ID"]);
                    ob.KNT_YRN_STR_RPL_REQ_H_ID = (dr["KNT_YRN_STR_RPL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.REQ_RTN_QTY = (dr["REQ_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_RTN_QTY"]);
                    ob.ACT_ISS_QTY = (dr["ACT_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_ISS_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);                    
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);

                    ob.LK_YRN_COLR_GRP_NAME = (dr["LK_YRN_COLR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YRN_COLR_GRP_NAME"]);
                    ob.LOC_CONV_RATE = (dr["LOC_CONV_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOC_CONV_RATE"]);
                    ob.PCT_ADDL_PRICE = (dr["PCT_ADDL_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_ADDL_PRICE"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["STOCK_QTY"]);

                    ob.RCV_QTY = (dr["RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RCV_QTY"]);
                    ob.PACK_RCV_QTY = (dr["PACK_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RCV_QTY"]);
                    ob.PACK_MOU_CODE = (dr["PACK_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACK_MOU_CODE"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.CONE_QTY = (dr["CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONE_QTY"]);
                    ob.CONE_WT = (dr["CONE_WT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONE_WT"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.TOTAL_AMT = ob.UNIT_PRICE * ob.RCV_QTY;
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_STR_RPL_REQ_DModel Select(long ID)
        {
            string sp = "PKG_KNT_RCV_RTN.KNT_YRN_STR_RPL_REQ_D_SELECT";
            try
            {
                var ob = new KNT_YRN_STR_RPL_REQ_DModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_RPL_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_STR_RPL_REQ_D_ID = (dr["KNT_YRN_STR_RPL_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_D_ID"]);
                    ob.KNT_YRN_STR_RPL_REQ_H_ID = (dr["KNT_YRN_STR_RPL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_RPL_REQ_H_ID"]);
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.REQ_RTN_QTY = (dr["REQ_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_RTN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
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


        public string STR_REQ_NO { get; set; }

        public DateTime STR_REQ_DT { get; set; }
    }
}