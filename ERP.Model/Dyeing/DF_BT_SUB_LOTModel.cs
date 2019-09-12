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
    public class DF_BT_SUB_LOTModel
    {
        public Int64 DF_BT_SUB_LOT_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public string SUB_LOT_NO { get; set; }
        public string IS_MAIN_LOT { get; set; }
        public Int64? LK_FBR_GRP_ID { get; set; }
        public Int64? LK_DIA_TYPE_ID { get; set; }
        public Int64? RF_FAB_TYPE_ID { get; set; }
        public Int64? RF_FIB_COMP_ID { get; set; }
        public string YARN_SPEC { get; set; }
        public Int64 LOT_QTY { get; set; }
        public Int64? QTY_MOU_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 DYE_STR_REQ_H_ID { get; set; }

        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public DateTime? QC_DT { get; set; }
        public Int64? QC_BY { get; set; }
        public Int64? SHFT_IN_CHRGE { get; set; }


        public string RQD_GSM { get; set; }
        public Int64 FIN_DIA { get; set; }
        public Int64 LK_SUB_LOT_GRP_ID { get; set; }
        public string IS_RP_RQD { get; set; }
        public Int64 MAJ_DFCT_TYPE_ID { get; set; }
        public string OTH_DFCT_TYPE_LST { get; set; }
        public Decimal FINIS_QTY { get; set; }
        public Int64 KNT_QC_STS_TYPE_ID { get; set; }
        public string IS_RP_DONE { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 DF_FAB_QC_RPT_ID { get; set; }
        public Int64 NO_OF_ROLL { get; set; }
        public Int64? ACT_NO_ROLL { get; set; }
        public Decimal? ACT_FINIS_QTY { get; set; }

        public string FAB_STR_POS { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string FAB_TYPE_NAME { get; set; }
        public string FIB_COMP_NAME { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string FIN_GSM { get; set; }
        public string DIA_TYPE_NAME { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string IS_SELECT { get; set; }
        public List<DF_BT_SUB_LOT_DFCTModel> DFCT_TYPE_LST { get; set; }

        public string Save()
        {
            const string sp = "PKG_DYE_BATCH_PROGRAM.DF_BT_SUB_LOT_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDYE_STR_REQ_H_ID", Value = ob.DYE_STR_REQ_H_ID},  
                     new CommandParameter() {ParameterName = "pSUB_LOT_NO", Value = ob.SUB_LOT_NO},
                     new CommandParameter() {ParameterName = "pIS_MAIN_LOT", Value = ob.IS_MAIN_LOT},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID==0?null:ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID==0?null:ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID==0?null:ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID==0?null:ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pNO_OF_ROLL", Value = ob.NO_OF_ROLL},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID==0?null:ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_BT_SUB_LOT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pSUB_LOT_NO", Value = ob.SUB_LOT_NO},
                     new CommandParameter() {ParameterName = "pIS_MAIN_LOT", Value = ob.IS_MAIN_LOT},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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
            const string sp = "SP_DF_BT_SUB_LOT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pSUB_LOT_NO", Value = ob.SUB_LOT_NO},
                     new CommandParameter() {ParameterName = "pIS_MAIN_LOT", Value = ob.IS_MAIN_LOT},
                     new CommandParameter() {ParameterName = "pLK_FBR_GRP_ID", Value = ob.LK_FBR_GRP_ID},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_TYPE_ID", Value = ob.RF_FAB_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_FIB_COMP_ID", Value = ob.RF_FIB_COMP_ID},
                     new CommandParameter() {ParameterName = "pYARN_SPEC", Value = ob.YARN_SPEC},
                     new CommandParameter() {ParameterName = "pLOT_QTY", Value = ob.LOT_QTY},
                     new CommandParameter() {ParameterName = "pQTY_MOU_ID", Value = ob.QTY_MOU_ID},
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

        public List<DF_BT_SUB_LOTModel> SelectAll()
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var obList = new List<DF_BT_SUB_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOTModel ob = new DF_BT_SUB_LOTModel();
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_SUB_LOT_GRP_ID = (dr["LK_SUB_LOT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUB_LOT_GRP_ID"]);
                    ob.IS_RP_RQD = (dr["IS_RP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_RQD"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    //ob.DFCT_TYPE_LST = (dr["DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_LST"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.IS_RP_DONE = (dr["IS_RP_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_DONE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);

                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.DFCT_TYPE_LST = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(ob.DF_BT_SUB_LOT_ID);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_BT_SUB_LOTModel> SelectByID(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var obList = new List<DF_BT_SUB_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value =pDF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOTModel ob = new DF_BT_SUB_LOTModel();
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    //ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    //ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    //ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    //ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    //ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    //ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_SUB_LOT_GRP_ID = (dr["LK_SUB_LOT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUB_LOT_GRP_ID"]);
                    ob.IS_RP_RQD = (dr["IS_RP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_RQD"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    //ob.DFCT_TYPE_LST = (dr["DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_LST"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.IS_RP_DONE = (dr["IS_RP_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_DONE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DF_FAB_QC_RPT_D_ID = (dr["DF_FAB_QC_RPT_D_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_D_ID"]);
                    //ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    //ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    //ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    //ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    //ob.FAB_STR_POS = (dr["FAB_STR_POS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_STR_POS"]);
                    ob.DFCT_TYPE_LST = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(ob.DF_BT_SUB_LOT_ID);
                    ob.KNIT_ITEM_LST = new DF_BT_SUB_LOT_FABModel().SelectByID(ob.DF_BT_SUB_LOT_ID);
                    
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_BT_SUB_LOTModel> SelectByBatchNo(string pDYE_BATCH_NO = null)
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var obList = new List<DF_BT_SUB_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOTModel ob = new DF_BT_SUB_LOTModel();
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_SUB_LOT_GRP_ID = (dr["LK_SUB_LOT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUB_LOT_GRP_ID"]);
                    ob.IS_RP_RQD = (dr["IS_RP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_RQD"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    //ob.DFCT_TYPE_LST = (dr["DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_LST"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.IS_RP_DONE = (dr["IS_RP_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_DONE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    
                    ob.DFCT_TYPE_LST = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(ob.DF_BT_SUB_LOT_ID);

                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.FAB_STR_POS = (dr["FAB_STR_POS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_STR_POS"]);

                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_QTY"]);

                    ob.FABRIC_GROUP_NAME = (dr["FABRIC_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_GROUP_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);
                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    ob.SUB_LOT_GRP_NAME = (dr["SUB_LOT_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_GRP_NAME"]);
                    
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_BT_SUB_LOTModel> SelectPendingLotRcv2DfStore(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, string pMONTHOF = null)
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var obList = new List<DF_BT_SUB_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMONTHOF", Value =pMONTHOF},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOTModel ob = new DF_BT_SUB_LOTModel();
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_SUB_LOT_GRP_ID = (dr["LK_SUB_LOT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUB_LOT_GRP_ID"]);
                    ob.IS_RP_RQD = (dr["IS_RP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_RQD"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    //ob.DFCT_TYPE_LST = (dr["DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_LST"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.IS_RP_DONE = (dr["IS_RP_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_DONE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);

                    ob.BP_DT = (dr["BP_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BP_DT"]);
                    ob.DF_DT = (dr["DF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["DF_DT"]);
                    
                    ob.KNT_STYL_FAB_ITEM_H_ID = (dr["KNT_STYL_FAB_ITEM_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_H_ID"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);

                    ob.DFCT_TYPE_LST = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(ob.DF_BT_SUB_LOT_ID);

                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.FAB_STR_POS = (dr["FAB_STR_POS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_STR_POS"]);

                    ob.LOT_TYPE = (dr["LOT_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_TYPE"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.FB_DFCT_TYPE_NAME = (dr["FB_DFCT_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FB_DFCT_TYPE_NAME"]);
                    
                    ob.FABRIC_GROUP_NAME = (dr["FABRIC_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_GROUP_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);

                    ob.GROUP_LST = "[Buyer: " + ob.BYR_ACC_GRP_NAME_EN + "];  [Style: " + ob.STYLE_NO + "];  [Order: " + ob.ORDER_NO + "];  [Color: " + ob.COLOR_NAME_EN+"]";// +" Batch #: " + ob.DYE_BATCH_NO;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_BT_SUB_LOTModel> SelectPendingLotRcv2CutStore(Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pRF_FAB_PROD_CAT_ID = null, string pMONTHOF = null)
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var obList = new List<DF_BT_SUB_LOTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value =pRF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pMONTHOF", Value =pMONTHOF},                     
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_BT_SUB_LOTModel ob = new DF_BT_SUB_LOTModel();
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LK_SUB_LOT_GRP_ID = (dr["LK_SUB_LOT_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_SUB_LOT_GRP_ID"]);
                    ob.IS_RP_RQD = (dr["IS_RP_RQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_RQD"]);
                    ob.MAJ_DFCT_TYPE_ID = (dr["MAJ_DFCT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);
                    //ob.DFCT_TYPE_LST = (dr["DFCT_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DFCT_TYPE_LST"]);
                    ob.FINIS_QTY = (dr["FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["FINIS_QTY"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);
                    ob.IS_RP_DONE = (dr["IS_RP_DONE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RP_DONE"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);
                    
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);

                    ob.DFCT_TYPE_LST = new DF_BT_SUB_LOT_DFCTModel().SelectByLotID(ob.DF_BT_SUB_LOT_ID);

                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.FAB_STR_POS = (dr["FAB_STR_POS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_STR_POS"]);

                    ob.LOT_TYPE = (dr["LOT_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LOT_TYPE"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);

                    ob.FABRIC_GROUP_NAME = (dr["FABRIC_GROUP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FABRIC_GROUP_NAME"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.LK_DIA_TYPE_NAME = (dr["LK_DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_DIA_TYPE_NAME"]);

                    ob.GROUP_LST = "[Buyer: " + ob.BYR_ACC_GRP_NAME_EN + "];  [Style: " + ob.STYLE_NO + "];  [Order: " + ob.ORDER_NO + "];  [Color: " + ob.COLOR_NAME_EN + "]";// +" Batch #: " + ob.DYE_BATCH_NO;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DF_BT_SUB_LOTModel Select(long ID)
        {
            string sp = "pkg_df_inspection.df_bt_sub_lot_select";
            try
            {
                var ob = new DF_BT_SUB_LOTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.IS_MAIN_LOT = (dr["IS_MAIN_LOT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_MAIN_LOT"]);
                    ob.LK_FBR_GRP_ID = (dr["LK_FBR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FBR_GRP_ID"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.RF_FAB_TYPE_ID = (dr["RF_FAB_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_TYPE_ID"]);
                    ob.RF_FIB_COMP_ID = (dr["RF_FIB_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FIB_COMP_ID"]);
                    ob.YARN_SPEC = (dr["YARN_SPEC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YARN_SPEC"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);
                    ob.QTY_MOU_ID = (dr["QTY_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QTY_MOU_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.NO_OF_ROLL = (dr["NO_OF_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NO_OF_ROLL"]);

                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);

                    ob.ACT_NO_ROLL = (dr["ACT_NO_ROLL"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ACT_NO_ROLL"]);
                    ob.ACT_FINIS_QTY = (dr["ACT_FINIS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_FINIS_QTY"]);
                    ob.FAB_STR_POS = (dr["FAB_STR_POS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_STR_POS"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long DF_FAB_QC_RPT_D_ID { get; set; }
        public long RQD_QTY { get; set; }
        public string FABRIC_GROUP_NAME { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public string LK_DIA_TYPE_NAME { get; set; }
        public string QC_STS_TYP_NAME { get; set; }
        public string FB_DFCT_TYPE_NAME { get; set; }
        public string SUB_LOT_GRP_NAME { get; set; }
        public string LOT_TYPE { get; set; }
        public string SUB_BATCH_NO { get; set; }
        public string REMARKS { get; set; }
        
        public List<DF_BT_SUB_LOT_FABModel> KNIT_ITEM_LST { get; set; }

        public long KNT_STYL_FAB_ITEM_H_ID { get; set; }

        public string FAB_ITEM_DESC { get; set; }

        public string GROUP_LST { get; set; }

        public DateTime BP_DT { get; set; }

        public DateTime DF_DT { get; set; }
    }
}