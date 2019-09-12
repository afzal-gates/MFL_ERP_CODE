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
    public class DF_PROC_QC_RPT_HModel
    {
        public Int64 DF_PROC_QC_RPT_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 DYE_BT_CARD_H_ID { get; set; }
        public Int64? DF_BT_SUB_LOT_ID { get; set; }
        public Int64 RP_SL_NO { get; set; }
        public string IS_SELF_QC { get; set; }
        public DateTime QC_DT { get; set; }
        public Int64 HR_SCHEDULE_H_ID { get; set; }
        public Int64 QC_BY { get; set; }
        public Int64 SHFT_IN_CHRGE { get; set; }
        public string QC_NOTE { get; set; }
        public Int64? RE_PROC_TYPE_ID { get; set; }
        public Int64? LK_RP_RTN_TYPE_ID { get; set; }
        public Decimal BATCH_RP_QTY { get; set; }
        public Int64 DF_BT_STS_TYPE_ID { get; set; }
        public Int64 LAST_DF_PROC_TYPE_ID { get; set; }
        public Int64? MAJ_DFCT_TYPE_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_QC_D { get; set; }
        public string XML_QC_DFCT { get; set; }

        
        public string Save()
        {
            const string sp = "PKG_DF_PRODUCTION.DF_PROC_QC_RPT_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID==0?null:ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_SELF_QC", Value = ob.IS_SELF_QC},
                     new CommandParameter() {ParameterName = "pIS_QC_OK", Value = ob.IS_QC_OK},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID==0?null:ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID==0?null:ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pKNT_QC_STS_TYPE_ID", Value = ob.KNT_QC_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pMAJ_DFCT_TYPE_ID", Value = ob.MAJ_DFCT_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLAST_DF_PROC_TYPE_ID", Value = ob.LAST_DF_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pXML_QC_D", Value = ob.XML_QC_D},
                     new CommandParameter() {ParameterName = "pXML_QC_DFCT", Value = ob.XML_QC_DFCT},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opDF_PROC_QC_RPT_H_ID", Value =500, Direction = ParameterDirection.Output}
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
            const string sp = "SP_DF_PROC_QC_RPT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_SELF_QC", Value = ob.IS_SELF_QC},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLAST_DF_PROC_TYPE_ID", Value = ob.LAST_DF_PROC_TYPE_ID},
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
            const string sp = "SP_DF_PROC_QC_RPT_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value = ob.DF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pDF_BT_SUB_LOT_ID", Value = ob.DF_BT_SUB_LOT_ID},
                     new CommandParameter() {ParameterName = "pRP_SL_NO", Value = ob.RP_SL_NO},
                     new CommandParameter() {ParameterName = "pIS_SELF_QC", Value = ob.IS_SELF_QC},
                     new CommandParameter() {ParameterName = "pQC_DT", Value = ob.QC_DT},
                     new CommandParameter() {ParameterName = "pHR_SCHEDULE_H_ID", Value = ob.HR_SCHEDULE_H_ID},
                     new CommandParameter() {ParameterName = "pQC_BY", Value = ob.QC_BY},
                     new CommandParameter() {ParameterName = "pSHFT_IN_CHRGE", Value = ob.SHFT_IN_CHRGE},
                     new CommandParameter() {ParameterName = "pQC_NOTE", Value = ob.QC_NOTE},
                     new CommandParameter() {ParameterName = "pRE_PROC_TYPE_ID", Value = ob.RE_PROC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLK_RP_RTN_TYPE_ID", Value = ob.LK_RP_RTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pBATCH_RP_QTY", Value = ob.BATCH_RP_QTY},
                     new CommandParameter() {ParameterName = "pDF_BT_STS_TYPE_ID", Value = ob.DF_BT_STS_TYPE_ID},
                     new CommandParameter() {ParameterName = "pLAST_DF_PROC_TYPE_ID", Value = ob.LAST_DF_PROC_TYPE_ID},
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

        public List<DF_PROC_QC_RPT_HModel> SelectAll()
        {
            string sp = "PKG_DF_PRODUCTION.DF_PROC_QC_RPT_H_Select";
            try
            {
                var obList = new List<DF_PROC_QC_RPT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_QC_RPT_HModel ob = new DF_PROC_QC_RPT_HModel();
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.IS_SELF_QC = (dr["IS_SELF_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SELF_QC"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.LAST_DF_PROC_TYPE_ID = (dr["LAST_DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_DF_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["MAJ_DFCT_TYPE_ID"] != DBNull.Value)
                        ob.MAJ_DFCT_TYPE_ID = Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);

                    ob.IS_QC_OK = (dr["IS_QC_OK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_QC_OK"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.LK_RP_RTN_TYPE_NAME = (dr["LK_RP_RTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_RP_RTN_TYPE_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_PROC_QC_RPT_HModel Select(Int64? pDF_PROC_QC_RPT_H_ID = null)
        {
            string sp = "PKG_DF_PRODUCTION.DF_PROC_QC_RPT_H_Select";
            try
            {
                var ob = new DF_PROC_QC_RPT_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_PROC_QC_RPT_H_ID", Value =pDF_PROC_QC_RPT_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.IS_SELF_QC = (dr["IS_SELF_QC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SELF_QC"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.LAST_DF_PROC_TYPE_ID = (dr["LAST_DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_DF_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["MAJ_DFCT_TYPE_ID"] != DBNull.Value)
                        ob.MAJ_DFCT_TYPE_ID = Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);

                    ob.IS_QC_OK = (dr["IS_QC_OK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_QC_OK"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.LK_RP_RTN_TYPE_NAME = (dr["LK_RP_RTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_RP_RTN_TYPE_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DF_PROC_QC_RPT_HModel> SelectForBatchReProcessByID(string pDYE_BATCH_NO = null)
        {
            string sp = "PKG_DF_PRODUCTION.DF_PROC_QC_RPT_H_Select";
            try
            {
                var obList = new List<DF_PROC_QC_RPT_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDYE_BATCH_NO", Value =pDYE_BATCH_NO},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_PROC_QC_RPT_HModel ob = new DF_PROC_QC_RPT_HModel();
                    ob.DF_PROC_QC_RPT_H_ID = (dr["DF_PROC_QC_RPT_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_PROC_QC_RPT_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DF_BT_SUB_LOT_ID = (dr["DF_BT_SUB_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_SUB_LOT_ID"]);
                    ob.RP_SL_NO = (dr["RP_SL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RP_SL_NO"]);
                    ob.IS_SELF_QC = (dr["IS_SELF_QC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SELF_QC"]);
                    ob.QC_DT = (dr["QC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QC_DT"]);
                    ob.HR_SCHEDULE_H_ID = (dr["HR_SCHEDULE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_SCHEDULE_H_ID"]);
                    ob.QC_BY = (dr["QC_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["QC_BY"]);
                    ob.SHFT_IN_CHRGE = (dr["SHFT_IN_CHRGE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SHFT_IN_CHRGE"]);
                    ob.QC_NOTE = (dr["QC_NOTE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QC_NOTE"]);
                    ob.RE_PROC_TYPE_ID = (dr["RE_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RE_PROC_TYPE_ID"]);
                    ob.LK_RP_RTN_TYPE_ID = (dr["LK_RP_RTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RP_RTN_TYPE_ID"]);
                    ob.BATCH_RP_QTY = (dr["BATCH_RP_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["BATCH_RP_QTY"]);
                    ob.DF_BT_STS_TYPE_ID = (dr["DF_BT_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_BT_STS_TYPE_ID"]);
                    ob.LAST_DF_PROC_TYPE_ID = (dr["LAST_DF_PROC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_DF_PROC_TYPE_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    if (dr["MAJ_DFCT_TYPE_ID"] != DBNull.Value)
                        ob.MAJ_DFCT_TYPE_ID = Convert.ToInt64(dr["MAJ_DFCT_TYPE_ID"]);

                    ob.IS_QC_OK = (dr["IS_QC_OK"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_QC_OK"]);
                    ob.KNT_QC_STS_TYPE_ID = (dr["KNT_QC_STS_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_QC_STS_TYPE_ID"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.COMP_SNAME = (dr["COMP_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_SNAME"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.SUB_BATCH_NO = (dr["SUB_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_BATCH_NO"]);
                    ob.SUB_LOT_NO = (dr["SUB_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUB_LOT_NO"]);
                    ob.LK_RP_RTN_TYPE_NAME = (dr["LK_RP_RTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LK_RP_RTN_TYPE_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.BT_STS_TYP_NAME = (dr["BT_STS_TYP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BT_STS_TYP_NAME"]);

                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COMP_NAME_EN { get; set; }

        public string COMP_SNAME { get; set; }

        public string DYE_BATCH_NO { get; set; }

        public string DYE_LOT_NO { get; set; }

        public string SUB_BATCH_NO { get; set; }

        public string SUB_LOT_NO { get; set; }

        public string LK_RP_RTN_TYPE_NAME { get; set; }

        public string BT_STS_TYP_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string COLOR_NAME_EN { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string IS_QC_OK { get; set; }

        public long KNT_QC_STS_TYPE_ID { get; set; }
        

    }
}