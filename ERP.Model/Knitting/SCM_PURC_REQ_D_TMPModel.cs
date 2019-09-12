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
    public class SCM_PURC_REQ_D_TMPModel
    {
        public Int64 SCM_PURC_REQ_D_TMP_ID { get; set; }
        public Int64 SCM_PURC_REQ_ORD_ID { get; set; }
        public Int64 RF_REQ_SRC_ID { get; set; }
        public Int64 INV_ITEM_CAT_ID { get; set; }
        public Int64 INV_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Decimal BUFR_ALOC_QTY { get; set; }
        public Decimal CAL_RQD_QTY { get; set; }
        public Decimal CONS_DOZ { get; set; }
        public Decimal RQD_QTY { get; set; }
        public Decimal MKT_RATE { get; set; }
        public Decimal ORD_ADV_ALOC_QTY { get; set; }
        public Decimal NXT_BUFR_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 LK_PRIORITY_ID { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string LOT_REF_NO { get; set; }
        public string SHADE_REF_NO { get; set; }
        public DateTime TARGET_DT { get; set; }
        public string SP_NOTE { get; set; }
        public string IS_DELETED { get; set; }
        public Int64 RPL_ITEM_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_SCM_PURC_REQ_D_TMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_TMP_ID", Value = ob.SCM_PURC_REQ_D_TMP_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_ORD_ID", Value = ob.SCM_PURC_REQ_ORD_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pCONS_DOZ", Value = ob.CONS_DOZ},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pMKT_RATE", Value = ob.MKT_RATE},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSHADE_REF_NO", Value = ob.SHADE_REF_NO},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
                     new CommandParameter() {ParameterName = "pRPL_ITEM_ID", Value = ob.RPL_ITEM_ID},
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
            const string sp = "SP_SCM_PURC_REQ_D_TMP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_TMP_ID", Value = ob.SCM_PURC_REQ_D_TMP_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_ORD_ID", Value = ob.SCM_PURC_REQ_ORD_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pCONS_DOZ", Value = ob.CONS_DOZ},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pMKT_RATE", Value = ob.MKT_RATE},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSHADE_REF_NO", Value = ob.SHADE_REF_NO},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
                     new CommandParameter() {ParameterName = "pRPL_ITEM_ID", Value = ob.RPL_ITEM_ID},
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
            const string sp = "pkg_scm_purchase.scm_purc_req_d_tmp_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_TMP_ID", Value = ob.SCM_PURC_REQ_D_TMP_ID},
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_ORD_ID", Value = ob.SCM_PURC_REQ_ORD_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_SRC_ID", Value = ob.RF_REQ_SRC_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_ID", Value = ob.INV_ITEM_CAT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_ID", Value = ob.INV_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pCONS_DOZ", Value = ob.CONS_DOZ},
                     new CommandParameter() {ParameterName = "pRQD_QTY", Value = ob.RQD_QTY},
                     new CommandParameter() {ParameterName = "pMKT_RATE", Value = ob.MKT_RATE},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_PRIORITY_ID", Value = ob.LK_PRIORITY_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSHADE_REF_NO", Value = ob.SHADE_REF_NO},
                     new CommandParameter() {ParameterName = "pTARGET_DT", Value = ob.TARGET_DT},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
                     new CommandParameter() {ParameterName = "pIS_DELETED", Value = ob.IS_DELETED},
                     new CommandParameter() {ParameterName = "pRPL_ITEM_ID", Value = ob.RPL_ITEM_ID},
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

        public List<SCM_PURC_REQ_D_TMPModel> SelectAll()
        {
            string sp = "Select_SCM_PURC_REQ_D_TMP";
            try
            {
                var obList = new List<SCM_PURC_REQ_D_TMPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_TMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_PURC_REQ_D_TMPModel ob = new SCM_PURC_REQ_D_TMPModel();
                    ob.SCM_PURC_REQ_D_TMP_ID = (dr["SCM_PURC_REQ_D_TMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_TMP_ID"]);
                    ob.SCM_PURC_REQ_ORD_ID = (dr["SCM_PURC_REQ_ORD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_ORD_ID"]);
                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.BUFR_ALOC_QTY = (dr["BUFR_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BUFR_ALOC_QTY"]);
                    ob.CAL_RQD_QTY = (dr["CAL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_RQD_QTY"]);
                    ob.CONS_DOZ = (dr["CONS_DOZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_DOZ"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.MKT_RATE = (dr["MKT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MKT_RATE"]);
                    ob.ORD_ADV_ALOC_QTY = (dr["ORD_ADV_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORD_ADV_ALOC_QTY"]);
                    ob.NXT_BUFR_QTY = (dr["NXT_BUFR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NXT_BUFR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.SHADE_REF_NO = (dr["SHADE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_REF_NO"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.IS_DELETED = (dr["IS_DELETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETED"]);
                    ob.RPL_ITEM_ID = (dr["RPL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPL_ITEM_ID"]);
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

        public SCM_PURC_REQ_D_TMPModel Select(long ID)
        {
            string sp = "Select_SCM_PURC_REQ_D_TMP";
            try
            {
                var ob = new SCM_PURC_REQ_D_TMPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_PURC_REQ_D_TMP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_PURC_REQ_D_TMP_ID = (dr["SCM_PURC_REQ_D_TMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_D_TMP_ID"]);
                    ob.SCM_PURC_REQ_ORD_ID = (dr["SCM_PURC_REQ_ORD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_PURC_REQ_ORD_ID"]);
                    ob.RF_REQ_SRC_ID = (dr["RF_REQ_SRC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_SRC_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.INV_ITEM_ID = (dr["INV_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.BUFR_ALOC_QTY = (dr["BUFR_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BUFR_ALOC_QTY"]);
                    ob.CAL_RQD_QTY = (dr["CAL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_RQD_QTY"]);
                    ob.CONS_DOZ = (dr["CONS_DOZ"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CONS_DOZ"]);
                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.MKT_RATE = (dr["MKT_RATE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MKT_RATE"]);
                    ob.ORD_ADV_ALOC_QTY = (dr["ORD_ADV_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORD_ADV_ALOC_QTY"]);
                    ob.NXT_BUFR_QTY = (dr["NXT_BUFR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NXT_BUFR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_PRIORITY_ID = (dr["LK_PRIORITY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PRIORITY_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.SHADE_REF_NO = (dr["SHADE_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_REF_NO"]);
                    ob.TARGET_DT = (dr["TARGET_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["TARGET_DT"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.IS_DELETED = (dr["IS_DELETED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DELETED"]);
                    ob.RPL_ITEM_ID = (dr["RPL_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPL_ITEM_ID"]);
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