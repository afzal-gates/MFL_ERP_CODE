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
    public class SCM_YRN_PURC_REQ_DModel
    {
        public Int64 SCM_YRN_PURC_REQ_D_ID { get; set; }
        public Int64 SCM_YRN_PURC_REQ_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 LK_YRN_COLR_GRP_ID { get; set; }
        public Decimal BUFR_ALOC_QTY { get; set; }
        public Decimal CAL_RQD_QTY { get; set; }
        public Decimal PLAN_RQD_QTY { get; set; }
        public Decimal ORD_ADV_ALOC_QTY { get; set; }
        public Decimal NXT_BUFR_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_BRAND_ID { get; set; }
        public string LOT_REF_NO { get; set; }
        public string SP_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "pkg_fab_prod_order.scm_yrn_purc_req_d_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_D_ID", Value = ob.SCM_YRN_PURC_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_H_ID", Value = ob.SCM_YRN_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_RQD_QTY", Value = ob.PLAN_RQD_QTY},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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
            const string sp = "pkg_fab_prod_order.SP_SCM_YRN_PURC_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_D_ID", Value = ob.SCM_YRN_PURC_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_H_ID", Value = ob.SCM_YRN_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_RQD_QTY", Value = ob.PLAN_RQD_QTY},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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
            const string sp = "PKG_FAB_PROD_ORDER.SP_SCM_YRN_PURC_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_D_ID", Value = ob.SCM_YRN_PURC_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_H_ID", Value = ob.SCM_YRN_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pLK_YRN_COLR_GRP_ID", Value = ob.LK_YRN_COLR_GRP_ID},
                     new CommandParameter() {ParameterName = "pBUFR_ALOC_QTY", Value = ob.BUFR_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pCAL_RQD_QTY", Value = ob.CAL_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPLAN_RQD_QTY", Value = ob.PLAN_RQD_QTY},
                     new CommandParameter() {ParameterName = "pORD_ADV_ALOC_QTY", Value = ob.ORD_ADV_ALOC_QTY},
                     new CommandParameter() {ParameterName = "pNXT_BUFR_QTY", Value = ob.NXT_BUFR_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value = ob.RF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pLOT_REF_NO", Value = ob.LOT_REF_NO},
                     new CommandParameter() {ParameterName = "pSP_NOTE", Value = ob.SP_NOTE},
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

        public List<SCM_YRN_PURC_REQ_DModel> SelectAll()
        {
            string sp = "pkg_scm_purchase.scm_yrn_purc_req_d_select";
            try
            {
                var obList = new List<SCM_YRN_PURC_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_YRN_PURC_REQ_DModel ob = new SCM_YRN_PURC_REQ_DModel();
                    ob.SCM_YRN_PURC_REQ_D_ID = (dr["SCM_YRN_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_YRN_PURC_REQ_D_ID"]);
                    ob.SCM_YRN_PURC_REQ_H_ID = (dr["SCM_YRN_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_YRN_PURC_REQ_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.BUFR_ALOC_QTY = (dr["BUFR_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BUFR_ALOC_QTY"]);
                    ob.CAL_RQD_QTY = (dr["CAL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_RQD_QTY"]);
                    ob.PLAN_RQD_QTY = (dr["PLAN_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_RQD_QTY"]);
                    ob.ORD_ADV_ALOC_QTY = (dr["ORD_ADV_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORD_ADV_ALOC_QTY"]);
                    ob.NXT_BUFR_QTY = (dr["NXT_BUFR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NXT_BUFR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
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

        public List<SCM_YRN_PURC_REQ_DModel> Select(Int64? pSCM_YRN_PURC_REQ_H_ID)
        {
            string sp = "pkg_scm_purchase.scm_yrn_purc_req_d_select";
            try
            {
                var lst = new List<SCM_YRN_PURC_REQ_DModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_YRN_PURC_REQ_H_ID", Value =pSCM_YRN_PURC_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_YRN_PURC_REQ_DModel ob = new SCM_YRN_PURC_REQ_DModel();
                    ob.SCM_YRN_PURC_REQ_D_ID = (dr["SCM_YRN_PURC_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_YRN_PURC_REQ_D_ID"]);
                    ob.SCM_YRN_PURC_REQ_H_ID = (dr["SCM_YRN_PURC_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_YRN_PURC_REQ_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.BUFR_ALOC_QTY = (dr["BUFR_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BUFR_ALOC_QTY"]);
                    ob.CAL_RQD_QTY = (dr["CAL_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CAL_RQD_QTY"]);
                    ob.PLAN_RQD_QTY = (dr["PLAN_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PLAN_RQD_QTY"]);
                    ob.ORD_ADV_ALOC_QTY = (dr["ORD_ADV_ALOC_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ORD_ADV_ALOC_QTY"]);
                    ob.NXT_BUFR_QTY = (dr["NXT_BUFR_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["NXT_BUFR_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.LOT_REF_NO = (dr["LOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_REF_NO"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.LK_YRN_COLR_GRP_NAME = (dr["LK_YRN_COLR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_YRN_COLR_GRP_NAME"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    
                    lst.Add(ob);
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ITEM_NAME_EN { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string LK_YRN_COLR_GRP_NAME { get; set; }

        public string MOU_CODE { get; set; }
    }
}