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
    public class DF_FAB_QC_RPTModel
    {
        public Int64 DF_FAB_QC_RPT_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public string RPT_REF_NO { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64 FAB_COLOR_ID { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64? HR_SCHEDULE_H_ID { get; set; }
        public DateTime? QC_DT { get; set; }
        public Int64? QC_BY { get; set; }
        public Int64? SHFT_IN_CHRGE { get; set; }
        public Int64 RQD_FIN_GSM { get; set; }
        public string RPT_SHORT_DESC { get; set; }
        public string QC_NOTE { get; set; }
        public string SHADE_NOTE { get; set; }
        public string IS_RPT_SPLIT { get; set; }
        public Int64 DF_BT_STS_TYPE_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64 LK_DIA_TYPE_ID { get; set; }
        public Int64? MAJ_QC_PARAM_TYPE_ID { get; set; }

        public string IS_FINALIZED { get; set; }

        public string XML_QC_D { get; set; }
        public string XML_ROLL_D { get; set; }
        public string XML_QC_FLT { get; set; }

        

        public string Save()
        {
            const string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID==0?null:ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pRPT_REF_NO", Value = ob.RPT_REF_NO},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pLK_DIA_TYPE_ID", Value = ob.LK_DIA_TYPE_ID},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID<=0?3:ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pRPT_SL_NO", Value = ob.RPT_SL_NO},                     
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY<=0?27549:ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE<=0?18682:ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pRQD_FIN_GSM", Value = ob.RQD_FIN_GSM==0?76:ob.RQD_FIN_GSM},
                     new CommandParameter() {ParameterName = "pRPT_SHORT_DESC", Value = ob.RPT_SHORT_DESC==null?"N/A":ob.RPT_SHORT_DESC},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pSHADE_NOTE", Value = ob.SHADE_NOTE},
                     new CommandParameter() {ParameterName = "pIS_RPT_SPLIT", Value = ob.IS_RPT_SPLIT==null?"N":ob.IS_RPT_SPLIT},
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID==0?1:ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMAJ_QC_PARAM_TYPE_ID", Value = ob.PARENT_ID==0?null:ob.MAJ_QC_PARAM_TYPE_ID},
                     //new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pXML_QC_D", Value = ob.XML_QC_D},
                     new CommandParameter() {ParameterName = "pXML_ROLL_D", Value = ob.XML_ROLL_D},
                     new CommandParameter() {ParameterName = "pXML_QC_FLT", Value = ob.XML_QC_FLT},
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


        public string SaveReport()
        {
            const string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_UPDATE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pXML_ROLL_D", Value = ob.XML_ROLL_D},
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
            const string sp = "SP_DF_FAB_QC_RPT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pRQD_FIN_GSM", Value = ob.RQD_FIN_GSM},
                     new CommandParameter() {ParameterName = "pRPT_SHORT_DESC", Value = ob.RPT_SHORT_DESC},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pSHADE_NOTE", Value = ob.SHADE_NOTE},
                     new CommandParameter() {ParameterName = "pIS_RPT_SPLIT", Value = ob.IS_RPT_SPLIT},
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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
            const string sp = "SP_DF_FAB_QC_RPT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value = ob.DF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pFAB_COLOR_ID", Value = ob.FAB_COLOR_ID},
                     new CommandParameter() {ParameterName = "pDYE_LOT_NO", Value = ob.DYE_LOT_NO},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pRQD_FIN_GSM", Value = ob.RQD_FIN_GSM},
                     new CommandParameter() {ParameterName = "pRPT_SHORT_DESC", Value = ob.RPT_SHORT_DESC},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pSHADE_NOTE", Value = ob.SHADE_NOTE},
                     new CommandParameter() {ParameterName = "pIS_RPT_SPLIT", Value = ob.IS_RPT_SPLIT},
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<DF_FAB_QC_RPTModel> SelectAll(string pDYE_BATCH_NO = null, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_FAB_PROD_ORD_H_ID = null, Int64? pMC_COLOR_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_Select";
            try
            {
                var obList = new List<DF_FAB_QC_RPTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value =pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value =pMC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_COLOR_ID", Value =pMC_COLOR_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_FAB_QC_RPTModel ob = new DF_FAB_QC_RPTModel();
                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    ob.RQD_FIN_GSM = (dr["RQD_FIN_GSM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FIN_GSM"]);
                    ob.RPT_SHORT_DESC = (dr["RPT_SHORT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_SHORT_DESC"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);
                    ob.SHADE_NOTE = (dr["SHADE_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_NOTE"]);
                    ob.IS_RPT_SPLIT = (dr["IS_RPT_SPLIT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RPT_SPLIT"]);
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RPT_SL_NO = (dr["RPT_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPT_SL_NO"]);

                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);
                    ob.BATCH_STATUS = (dr["BATCH_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BATCH_STATUS"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    ob.RPT_REF_NO = (dr["RPT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_REF_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FIN_DIA"]);
                    ob.LOT_QTY = (dr["LOT_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOT_QTY"]);

                    ob.RQD_GSM = (dr["RQD_GSM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RQD_GSM"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.LK_FBR_GRP_NAME = (dr["LK_FBR_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_FBR_GRP_NAME"]);
                    ob.FIB_COMP_NAME = (dr["FIB_COMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIB_COMP_NAME"]);
                    ob.QC_STS_TYP_NAME = (dr["QC_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_STS_TYP_NAME"]);

                    ob.QC_BY_NAME = (dr["QC_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_BY_NAME"]);
                    ob.SHIFT_IN_CHARGE = (dr["SHIFT_IN_CHARGE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIFT_IN_CHARGE"]);
                    ob.SCHEDULE_NAME_EN = (dr["SCHEDULE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SCHEDULE_NAME_EN"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.CREATED_BY_NAME = (dr["CREATED_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CREATED_BY_NAME"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_FAB_QC_RPTModel Select(Int64? pDF_FAB_QC_RPT_ID = null)
        {
            string sp = "PKG_DF_INSPECTION.DF_FAB_QC_RPT_Select";
            try
            {
                var ob = new DF_FAB_QC_RPTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_FAB_QC_RPT_ID", Value =pDF_FAB_QC_RPT_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    ob.DF_FAB_QC_RPT_ID = (dr["DF_FAB_QC_RPT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_FAB_QC_RPT_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.FAB_COLOR_ID = (dr["FAB_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FAB_COLOR_ID"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    ob.RQD_FIN_GSM = (dr["RQD_FIN_GSM"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_FIN_GSM"]);
                    ob.RPT_SHORT_DESC = (dr["RPT_SHORT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_SHORT_DESC"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);
                    ob.SHADE_NOTE = (dr["SHADE_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHADE_NOTE"]);
                    ob.IS_RPT_SPLIT = (dr["IS_RPT_SPLIT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_RPT_SPLIT"]);
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RPT_SL_NO = (dr["RPT_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RPT_SL_NO"]);
                    ob.LK_DIA_TYPE_ID = (dr["LK_DIA_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_DIA_TYPE_ID"]);
                    ob.MAJ_QC_PARAM_TYPE_ID = (dr["MAJ_QC_PARAM_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MAJ_QC_PARAM_TYPE_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.ACT_BATCH_QTY = (dr["ACT_BATCH_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ACT_BATCH_QTY"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.BUYER_NAME_EN = ob.BYR_ACC_GRP_NAME_EN;
                    ob.ORDER_NO = ob.MC_ORDER_NO_LST;
                    ob.DIA_TYPE_NAME = (dr["DIA_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DIA_TYPE_NAME"]);
                    ob.RPT_REF_NO = (dr["RPT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_REF_NO"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);

                    ob.DF_BT_QC_STATUS_ID = (dr["DF_BT_QC_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_QC_STATUS_ID"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long RPT_SL_NO { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string DIA_TYPE_NAME { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public int TOTAL_REC { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string ORDER_NO { get; set; }

        public long DF_BT_QC_STATUS_ID { get; set; }

        public long FIN_DIA { get; set; }

        public long LOT_QTY { get; set; }

        public string RQD_GSM { get; set; }

        public string SUB_LOT_NO { get; set; }

        public string FAB_TYPE_SNAME { get; set; }

        public string FIB_COMP_NAME { get; set; }

        public string LK_FBR_GRP_NAME { get; set; }

        public string QC_STS_TYP_NAME { get; set; }

        public decimal ACT_BATCH_QTY { get; set; }

        public string BATCH_STATUS { get; set; }

        public string QC_BY_NAME { get; set; }

        public string SHIFT_IN_CHARGE { get; set; }

        public string SCHEDULE_NAME_EN { get; set; }

        public string CREATED_BY_NAME { get; set; }
    }
}