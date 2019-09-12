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
    public class CM_IMP_PO_D_YRNModel
    {
        public Int64 CM_IMP_PO_D_ID { get; set; }
        public Int64 CM_IMP_PO_H_ID { get; set; }
        public Int64 SCM_PURC_REQ_D_YRN_ID { get; set; }
        public Int64 SCM_PURC_REQ_D_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public Decimal PO_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal UNIT_PRICE { get; set; }
        public string LOT_REF_NO { get; set; }
        public DateTime DELV_TGT_DT { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_IMP_PO_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
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
            const string sp = "SP_CM_IMP_PO_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
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
            const string sp = "SP_CM_IMP_PO_D_YRN";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value = ob.CM_IMP_PO_D_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_YRN_ID", Value = ob.SCM_PURC_REQ_D_YRN_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pPO_QTY", Value = ob.PO_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pUNIT_PRICE", Value = ob.UNIT_PRICE},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pDELV_TGT_DT", Value = ob.DELV_TGT_DT},
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

        public List<CM_IMP_PO_D_YRNModel> SelectAll(Int64? pCM_IMP_PO_H_ID = null)
        {
            string sp = "PKG_SCM_PURCHASE.CM_IMP_PO_D_YRN_Select";
            try
            {
                var obList = new List<CM_IMP_PO_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value =pCM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PO_D_YRNModel ob = new CM_IMP_PO_D_YRNModel();
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                    ob.SCM_PURC_REQ_D_YRN_ID = (dr["SCM_PURC_REQ_D_YRN_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
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


        public List<CM_IMP_PO_D_YRNModel> SelectByID(Int64? pCM_IMP_PO_H_ID = null)
        {
            string sp = "PKG_SCM_PURCHASE.CM_IMP_PO_D_Select";
            try
            {
                var obList = new List<CM_IMP_PO_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value =pCM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PO_D_YRNModel ob = new CM_IMP_PO_D_YRNModel();
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                    ob.SCM_PURC_REQ_D_ID = (dr["SCM_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.MC_ORD_TRMS_ITEM_ID = (dr["MC_ORD_TRMS_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TRMS_ITEM_ID"]);

                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.LK_YRN_COLR_GRP_NAME = (dr["LK_YRN_COLR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YRN_COLR_GRP_NAME"]);

                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    ob.ORDER_DTL = ob.BUYER_NAME_EN + " | " + ob.STYLE_NO + " | " + ob.ORDER_NO;

                    ob.ITEM_SPEC_AUTO = (dr["ITEM_SPEC_AUTO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_SPEC_AUTO"]);
                    ob.ITEM_CODE = (dr["ITEM_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CODE"]);
                    ob.PARTICULARS = (dr["PARTICULARS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PARTICULARS"]);

                    //ob.SCM_PURC_REQ_D_YRN1_ID = (dr["SCM_PURC_REQ_D_YRN1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_YRN1_ID"]);
                    //ob.CONF_SUPPLIER_ID = (dr["CONF_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_SUPPLIER_ID"]);
                    //ob.CONF_BRAND_ID = (dr["CONF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CONF_BRAND_ID"]);
                    ob.CONF_RATE = ob.UNIT_PRICE;
                    ob.CONF_QTY = ob.PO_QTY;
                    ob.TTL_VALUE = ob.PO_QTY * ob.UNIT_PRICE;

                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<CM_IMP_PO_D_YRNModel> SelectForReceive(Int64? pCM_IMP_PO_H_ID = null)
        {
            string sp = "PKG_SCM_PURCHASE.CM_IMP_PO_D_Select";
            try
            {
                var obList = new List<CM_IMP_PO_D_YRNModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value =pCM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PO_D_YRNModel ob = new CM_IMP_PO_D_YRNModel();
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                    ob.SCM_PURC_REQ_D_ID = (dr["SCM_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    //ob.PURC_REQ_DT = (dr["PURC_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PURC_REQ_DT"]);
                    ob.PURC_REQ_NO = (dr["PURC_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PURC_REQ_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.SOURCE_NAME = (dr["SOURCE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SOURCE_NAME"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
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

        public CM_IMP_PO_D_YRNModel Select(long ID)
        {
            string sp = "Select_CM_IMP_PO_D_YRN";
            try
            {
                var ob = new CM_IMP_PO_D_YRNModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_IMP_PO_D_ID = (dr["CM_IMP_PO_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_D_ID"]);
                    ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                    ob.SCM_PURC_REQ_D_ID = (dr["SCM_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.PO_QTY = (dr["PO_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.UNIT_PRICE = (dr["UNIT_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["UNIT_PRICE"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.DELV_TGT_DT = (dr["DELV_TGT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DELV_TGT_DT"]);
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

        public string BRAND_NAME_EN { get; set; }

        public string LK_YRN_COLR_GRP_NAME { get; set; }

        public long MC_BUYER_ID { get; set; }

        public long MC_STYLE_H_EXT_ID { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string ORDER_DTL { get; set; }

        public long SCM_PURC_REQ_D_YRN1_ID { get; set; }

        public long CONF_SUPPLIER_ID { get; set; }

        public long CONF_BRAND_ID { get; set; }

        public decimal CONF_RATE { get; set; }

        public decimal CONF_QTY { get; set; }

        public string PURC_REQ_NO { get; set; }

        public long CM_IMP_PI_H_ID { get; set; }

        public string PI_NO_IMP { get; set; }

        public Decimal PACK_RCV_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Decimal RCV_QTY { get; set; }
        public Decimal LOOSE_QTY { get; set; }
        public Decimal PCT_ADDL_PRICE { get; set; }
        public Decimal COST_PRICE { get; set; }


        public string SOURCE_NAME { get; set; }

        public string MOU_CODE { get; set; }

        public string ITEM_SPEC_AUTO { get; set; }

        public string ITEM_CODE { get; set; }

        public string PARTICULARS { get; set; }

        public long MC_ORD_TRMS_ITEM_ID { get; set; }

        public decimal TTL_VALUE { get; set; }
    }
}