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
    public class KNT_YRN_ISS_RET_DModel
    {
        public Int64 KNT_YRN_ISS_RET_D_ID { get; set; }
        public Int64 KNT_YRN_ISS_RET_H_ID { get; set; }
        public Int64 KNT_JOB_CRD_H_ID { get; set; }
        public Int64 KNT_YRN_ISS_H_ID { get; set; }

        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal PACK_RET_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Decimal ADV_ADJ_QTY { get; set; }
        
        public Decimal LOOSE_QTY { get; set; }
        public Decimal RET_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public Decimal COST_PRICE { get; set; }
        public string SP_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_YRN_ISS_RET_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_D_ID", Value = ob.KNT_YRN_ISS_RET_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RET_QTY", Value = ob.PACK_RET_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRET_QTY", Value = ob.RET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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
            const string sp = "SP_KNT_YRN_ISS_RET_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_D_ID", Value = ob.KNT_YRN_ISS_RET_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RET_QTY", Value = ob.PACK_RET_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRET_QTY", Value = ob.RET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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
            const string sp = "SP_KNT_YRN_ISS_RET_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_D_ID", Value = ob.KNT_YRN_ISS_RET_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = ob.KNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RET_QTY", Value = ob.PACK_RET_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRET_QTY", Value = ob.RET_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
                     new CommandParameter() {ParameterName = "pCOST_PRICE", Value = ob.COST_PRICE},
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

        public List<KNT_YRN_ISS_RET_DModel> SelectAll()
        {
            string sp = "Select_KNT_YRN_ISS_RET_D";
            try
            {
                var obList = new List<KNT_YRN_ISS_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_D_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_RET_DModel ob = new KNT_YRN_ISS_RET_DModel();
                    ob.KNT_YRN_ISS_RET_D_ID = (dr["KNT_YRN_ISS_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_D_ID"]);
                    ob.KNT_YRN_ISS_RET_H_ID = (dr["KNT_YRN_ISS_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_H_ID"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.ADV_ADJ_QTY = (dr["ADV_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_ADJ_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
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

        public List<KNT_YRN_ISS_RET_DModel> GetIssueInfoForReturn(Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pRF_BRAND_ID = null, Int64? pKNT_YRN_LOT_ID = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID=null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_for_iss_rtn_select";
            try
            {
                var obList = new List<KNT_YRN_ISS_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value =pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value =pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value =pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value =pKNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value =pYRN_LOT_NO},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_RET_DModel ob = new KNT_YRN_ISS_RET_DModel();
                    
                    ob.ORDER_INFO = (dr["ORDER_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_INFO"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.JOB_CRD_NO = (dr["JOB_CRD_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["JOB_CRD_NO"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.ISS_CHALAN_DT = (dr["ISS_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISS_CHALAN_DT"]);

                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_PER_PACK"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);

                    ob.RET_QTY = dr["RET_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.ADV_ADJ_QTY = (dr["ADV_ADJ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_ADJ_QTY"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RQD_QTY"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                    ob.ISS_YRN_QTY = (dr["ISS_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_YRN_QTY"]);

                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_ISS_RET_DModel> GetIssuedYarnList(Int64? pKNT_YRN_STR_REQ_H_ID = null, Int64? pYARN_ITEM_ID = null, Int64? pRF_BRAND_ID = null, Int64? pKNT_YRN_LOT_ID = null, string pYRN_LOT_NO = null, Int64? pRF_REQ_TYPE_ID = null)
        {
            string sp = "pkg_knit_yarn_issue.Knt_yrn_iss_item_select";
            try
            {
                var obList = new List<KNT_YRN_ISS_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value =pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value =pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pRF_BRAND_ID", Value =pRF_BRAND_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value =pKNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYRN_LOT_NO", Value =pYRN_LOT_NO},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_RET_DModel ob = new KNT_YRN_ISS_RET_DModel();

                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);

                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_ISS_RET_DModel> Select(Int64? pKNT_YRN_ISS_RET_H_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_ret_d_select";
            try
            {
                var list = new List<KNT_YRN_ISS_RET_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value =pKNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new KNT_YRN_ISS_RET_DModel();
                    ob.KNT_YRN_ISS_RET_D_ID = (dr["KNT_YRN_ISS_RET_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_D_ID"]);
                    ob.KNT_YRN_ISS_RET_H_ID = (dr["KNT_YRN_ISS_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_H_ID"]);
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RET_QTY = (dr["PACK_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RET_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.ISSUE_QTY = (dr["ISSUE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY"]);
                    ob.RET_QTY = (dr["RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RET_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.COST_PRICE = (dr["COST_PRICE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["COST_PRICE"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ORDER_INFO = (dr["ORDER_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_INFO"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    //ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    //ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);

                    ob.RCV_QTY = ob.RET_QTY;

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ORDER_INFO { get; set; }

        public string STR_REQ_NO { get; set; }

        public string JOB_CRD_NO { get; set; }

        public string ISS_CHALAN_NO { get; set; }

        public string YRN_LOT_NO { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string YRN_COLR_GRP { get; set; }

        public string MOU_CODE { get; set; }

        public DateTime ISS_CHALAN_DT { get; set; }

        public long LK_YRN_COLR_GRP_ID { get; set; }

        public long RF_BRAND_ID { get; set; }

        public long KNT_YRN_STR_REQ_D_ID { get; set; }

        public long KNT_YRN_STR_REQ_H_ID { get; set; }

        public decimal PACK_RQD_QTY { get; set; }

        public decimal RQD_YRN_QTY { get; set; }

        public decimal ISS_YRN_QTY { get; set; }

        public decimal ISSUE_QTY { get; set; }

        public decimal RCV_QTY { get; set; }
    }
}