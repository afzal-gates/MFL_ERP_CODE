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
    public class KNT_YRN_STR_REQ_DModel
    {
        public Int64 KNT_YRN_STR_REQ_D_ID { get; set; }
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }
        public Int64 YARN_ITEM_ID { get; set; }
        public Int64 KNT_YRN_LOT_ID { get; set; }
        public Decimal PACK_RQD_QTY { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public Decimal QTY_PER_PACK { get; set; }
        public Decimal LOOSE_QTY { get; set; }
        public Decimal RQD_YRN_QTY { get; set; }
        public Int64 QTY_MOU_ID { get; set; }
        public string SP_NOTE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_KNT_YRN_STR_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_D_ID", Value = ob.KNT_YRN_STR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RQD_QTY", Value = ob.PACK_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRQD_YRN_QTY", Value = ob.RQD_YRN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_KNT_YRN_STR_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_D_ID", Value = ob.KNT_YRN_STR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RQD_QTY", Value = ob.PACK_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRQD_YRN_QTY", Value = ob.RQD_YRN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_KNT_YRN_STR_REQ_D";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_D_ID", Value = ob.KNT_YRN_STR_REQ_D_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = ob.YARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = ob.KNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pPACK_RQD_QTY", Value = ob.PACK_RQD_QTY},
                     new CommandParameter() {ParameterName = "pPACK_MOU_ID", Value = ob.PACK_MOU_ID},
                     new CommandParameter() {ParameterName = "pQTY_PER_PACK", Value = ob.QTY_PER_PACK},
                     new CommandParameter() {ParameterName = "pLOOSE_QTY", Value = ob.LOOSE_QTY},
                     new CommandParameter() {ParameterName = "pRQD_YRN_QTY", Value = ob.RQD_YRN_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<KNT_YRN_STR_REQ_DModel> GetJobCardListCollarCuffSco(Int64 pKNT_YRN_STR_REQ_H_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_d_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value =pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_DModel ob = new KNT_YRN_STR_REQ_DModel();
                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RQD_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);

                    ob.YRN_DETAILS = (dr["YRN_DETAILS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_DETAILS"]);
                    ob.ORDER_NO_LIST = (dr["ORDER_NO_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO_LIST"]);
                    ob.WORK_STYLE_NO = (dr["WORK_STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO"]);
                    ob.FAB_TYPE_NAME = (dr["FAB_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_NAME"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);

                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_DModel> Select(Int64? ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_d_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_DModel ob = new KNT_YRN_STR_REQ_DModel();
                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RQD_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                    ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_CONE_QTY"]);

                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STOCK_QTY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
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


        public List<KNT_YRN_STR_REQ_DModel> SelectForAllocDtl(Int64? pKNT_YRN_LOT_ID = null, Int64? pYARN_ITEM_ID = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_d_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_LOT_ID", Value = pKNT_YRN_LOT_ID},
                     new CommandParameter() {ParameterName = "pYARN_ITEM_ID", Value = pYARN_ITEM_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_DModel ob = new KNT_YRN_STR_REQ_DModel();
                    ob.KNT_YRN_STR_REQ_D_ID = (dr["KNT_YRN_STR_REQ_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_D_ID"]);
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.PACK_RQD_QTY = (dr["PACK_RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PACK_RQD_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.LOOSE_QTY = (dr["LOOSE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LOOSE_QTY"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY"]);
                    ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_CONE_QTY"]);
                    ob.ISS_YRN_QTY = (dr["ISS_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_YRN_QTY"]);

                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);

                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.SP_NOTE = (dr["SP_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_NOTE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STOCK_QTY"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.MOU_CODE = (dr["MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MOU_CODE"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                   
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<KNT_YRN_STR_REQ_DModel> getRequisitionYarnDetails(Int64? pKNT_JOB_CRD_H_ID, Int64? pKNT_YRN_ISS_H_ID, Int64? pKNT_YRN_STR_REQ_H_ID, Int64? pKNT_PLAN_H_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_d_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_DModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value = pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value = pKNT_PLAN_H_ID},

                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_H_ID", Value = pKNT_YRN_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_DModel ob = new KNT_YRN_STR_REQ_DModel();
                    
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);

                    ob.RQD_YRN_QTY_JC = (dr["RQD_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_YRN_QTY_JC"]);
                    ob.REQ_DN_YRN_QTY_JC = (dr["REQ_DN_YRN_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REQ_DN_YRN_QTY_JC"]);
                    ob.ISSUE_QTY_JC = (dr["ISSUE_QTY_JC"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_QTY_JC"]);

                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.ISSUE_RET_QTY = (dr["ISSUE_RET_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISSUE_RET_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long LK_YRN_COLR_GRP_ID { get; set; }

        public long RF_BRAND_ID { get; set; }

        public long STOCK_QTY { get; set; }

        public string YRN_LOT_NO { get; set; }

        public string BRAND_NAME_EN { get; set; }

        public string YRN_COLR_GRP { get; set; }

        public string MOU_CODE { get; set; }

        public string ITEM_NAME_EN { get; set; }

        public decimal RQD_CONE_QTY { get; set; }

        public long KNT_JOB_CRD_H_ID { get; set; }

        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }

        public string YRN_DETAILS { get; set; }

        public string ORDER_NO_LIST { get; set; }

        public string WORK_STYLE_NO { get; set; }

        public string FAB_TYPE_NAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string BYR_ACC_NAME_EN { get; set; }

        public long LK_REQ_STATUS_ID { get; set; }

        public decimal RQD_YRN_QTY_JC { get; set; }

        public decimal REQ_DN_YRN_QTY_JC { get; set; }

        public decimal ISSUE_QTY_JC { get; set; }

        public decimal ISSUE_RET_QTY { get; set; }

        public string BYR_ACC_GRP_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string MC_ORDER_NO_LST { get; set; }

        public string STR_REQ_NO { get; set; }

        public decimal ISS_YRN_QTY { get; set; }
    }
}