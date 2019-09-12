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
    public class CM_IMP_PI_D_YRNModel
    {
        public Int64 CM_IMP_PI_D_ID { get; set; }
        public Int64 CM_IMP_PI_H_ID { get; set; }
        public Int64 CM_IMP_PO_D_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string HS_CODE { get; set; }
        public Decimal PI_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_IMP_PI_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_D_ID", Value = ob.CM_IMP_PI_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pPI_QTY", Value = ob.PI_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_CM_IMP_PI_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_D_ID", Value = ob.CM_IMP_PI_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pPI_QTY", Value = ob.PI_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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
            const string sp = "SP_CM_IMP_PI_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_D_ID", Value = ob.CM_IMP_PI_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pHS_CODE", Value = ob.HS_CODE},
                     new CommandParameter() {ParameterName = "pPI_QTY", Value = ob.PI_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
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

        public List<CM_IMP_PI_D_YRNModel> SelectByID(Int64? pCM_IMP_PI_H_ID = null)
        {
            string sp = "pkg_cm_import_pi.cm_imp_pi_d_select";
            try
            {
                var obList = new List<CM_IMP_PI_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value =pCM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PI_D_YRNModel ob = new CM_IMP_PI_D_YRNModel();
                    ob.CM_IMP_PI_D_ID = (dr["CM_IMP_PI_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_D_ID"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);
                    ob.PI_QTY = (dr["PI_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);


                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PO_QTY"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.COLOR_GRP_NAME = (dr["COLOR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_GRP_NAME"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.ORDER_DTL = ob.BUYER_NAME_EN + " | " + ob.STYLE_NO + " | " + ob.ORDER_NO;
                    ob.TTL_VALUE = ob.PI_QTY * ob.UNIT_PRICE;
                    ob.CONF_QTY = ob.PI_QTY;
                    ob.CONF_RATE = ob.UNIT_PRICE;
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_IMP_PI_D_YRNModel Select(Int64? pCM_IMP_PI_H_ID = null)
        {
            string sp = "pkg_cm_import_pi.cm_imp_pi_d_yrn_select";
            try
            {
                var ob = new CM_IMP_PI_D_YRNModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value =pCM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_IMP_PI_D_ID = (dr["CM_IMP_PI_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_D_ID"]);
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.HS_CODE = (dr["HS_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["HS_CODE"]);
                    ob.PI_QTY = (dr["PI_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
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

        public long PO_QTY { get; set; }

        public string LOT_REF_NO { get; set; }

        public DateTime DELV_TGT_DT { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string COLOR_GRP_NAME { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string MOU_CODE { get; set; }

        public decimal CONF_QTY { get; set; }

        public decimal CONF_RATE { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string ORDER_DTL { get; set; }

        public string PURC_REQ_NO { get; set; }

        public decimal TTL_VALUE { get; set; }
    }
}