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
    public class CM_EXP_LC_HModel
    {
        public Int64 CM_EXP_LC_H_ID { get; set; }
        public string IS_SC_OR_LC { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public string EXP_LCSC_NO { get; set; }
        public DateTime ISSUE_DT { get; set; }
        public DateTime EXPIRY_DT { get; set; }
        public Int64 EXP_CNTRY_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal PCT_TOLR_AMT { get; set; }
        public Decimal PCT_BB_LMT { get; set; }
        public string AVAIL_WITH { get; set; }
        public Int64 RF_INCO_TERM_ID { get; set; }
        public Int64 RF_DELV_PLC_ID { get; set; }
        public Int64 RF_PAYM_TERM_ID { get; set; }
        public string PAYM_TERM_DESC { get; set; }
        public Int64 SND_BKBR_ID { get; set; }
        public Int64 RCV_BKBR_ID { get; set; }
        public Int64 LIN_BKBR_ID { get; set; }
        public string IS_PART_SHPM { get; set; }
        public string IS_TRAN_SHPM { get; set; }
        public Int64 LOAD_PORT_ID { get; set; }
        public string DSG_PORT_LST { get; set; }
        public DateTime LTST_SHPMNT_DT { get; set; }
        public string DRAFT_AT { get; set; }
        public string DRAWEE_DESC { get; set; }
        public string DOC_REQD { get; set; }
        public Decimal LC_TOT_VAL { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string CM_NOTIFY_PARTY_LST { get; set; }
        public string XML_PO { get; set; }
        public string IS_ADV_LC { get; set; }
        public string IS_LNK_PI_PO { get; set; }
        public string IS_LC_ADJ { get; set; }
        public Int64? CM_UD_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }
        public string REV_REASON { get; set; }


        public string STYLE_NO { get; set; }
        public string ORDER_NO { get; set; }
        public string IS_UPDATE { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string BUYER_NAME_EN { get; set; }
        public string COUNTRY_NAME_EN { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public Int64 PERMISSION { get; set; }
        public int TOTAL_REC { get; set; }

        public Decimal PO_TOT_VAL { get; set; }
        public string IS_IGNOR_DIFF { get; set; }
        public Decimal LC_TOT_CHG { get; set; }
        

        public string Save()
        {
            const string sp = "PKG_CM_EXPORT_LC.CM_EXP_LC_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pIS_SC_OR_LC", Value = ob.IS_SC_OR_LC},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pEXP_LCSC_NO", Value = ob.EXP_LCSC_NO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXP_CNTRY_ID", Value = ob.EXP_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
                     new CommandParameter() {ParameterName = "pPCT_BB_LMT", Value = ob.PCT_BB_LMT},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pSND_BKBR_ID", Value = ob.SND_BKBR_ID},
                     new CommandParameter() {ParameterName = "pRCV_BKBR_ID", Value = ob.RCV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pLIN_BKBR_ID", Value = ob.LIN_BKBR_ID},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pLTST_SHPMNT_DT", Value = ob.LTST_SHPMNT_DT},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_LST", Value = ob.CM_NOTIFY_PARTY_LST},
                     new CommandParameter() {ParameterName = "pIS_ADV_LC", Value = ob.IS_ADV_LC},
                     new CommandParameter() {ParameterName = "pIS_LNK_PI_PO", Value = ob.IS_LNK_PI_PO},
                     new CommandParameter() {ParameterName = "pIS_LC_ADJ", Value = ob.IS_LC_ADJ},
                     
                     new CommandParameter() {ParameterName = "pPO_TOT_VAL", Value = ob.PO_TOT_VAL},
                     new CommandParameter() {ParameterName = "pLC_TOT_CHG", Value = ob.LC_TOT_CHG},
                     new CommandParameter() {ParameterName = "pIS_IGNOR_DIFF", Value = ob.IS_IGNOR_DIFF},


                     new CommandParameter() {ParameterName = "pXML_PO", Value = ob.XML_PO},
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE},
                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opCM_EXP_LC_H_ID", Value =0, Direction = ParameterDirection.Output}
                     
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
            const string sp = "PKG_CM_EXPORT_LC.CM_EXP_LC_H_REVISE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pIS_SC_OR_LC", Value = ob.IS_SC_OR_LC},
                     new CommandParameter() {ParameterName = "pIS_ADV_LC", Value = ob.IS_ADV_LC},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pEXP_LCSC_NO", Value = ob.EXP_LCSC_NO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXP_CNTRY_ID", Value = ob.EXP_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pCM_UD_ID", Value = ob.CM_UD_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
                     new CommandParameter() {ParameterName = "pPCT_BB_LMT", Value = ob.PCT_BB_LMT},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pSND_BKBR_ID", Value = ob.SND_BKBR_ID},
                     new CommandParameter() {ParameterName = "pRCV_BKBR_ID", Value = ob.RCV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pLIN_BKBR_ID", Value = ob.LIN_BKBR_ID},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pLTST_SHPMNT_DT", Value = ob.LTST_SHPMNT_DT},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pIS_LNK_PI_PO", Value = ob.IS_LNK_PI_PO},
                     new CommandParameter() {ParameterName = "pIS_LC_ADJ", Value = ob.IS_LC_ADJ},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                                          
                     new CommandParameter() {ParameterName = "pPO_TOT_VAL", Value = ob.PO_TOT_VAL},
                     new CommandParameter() {ParameterName = "pLC_TOT_CHG", Value = ob.LC_TOT_CHG},
                     new CommandParameter() {ParameterName = "pIS_IGNOR_DIFF", Value = ob.IS_IGNOR_DIFF},

                     new CommandParameter() {ParameterName = "pCM_NOTIFY_PARTY_LST", Value = ob.CM_NOTIFY_PARTY_LST},                     
                     new CommandParameter() {ParameterName = "pXML_PO", Value = ob.XML_PO},

                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opCM_EXP_LC_H_ID", Value =0, Direction = ParameterDirection.Output}

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
            const string sp = "SP_CM_EXP_LC_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value = ob.CM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pIS_SC_OR_LC", Value = ob.IS_SC_OR_LC},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pEXP_LCSC_NO", Value = ob.EXP_LCSC_NO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXP_CNTRY_ID", Value = ob.EXP_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
                     new CommandParameter() {ParameterName = "pPCT_BB_LMT", Value = ob.PCT_BB_LMT},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pSND_BKBR_ID", Value = ob.SND_BKBR_ID},
                     new CommandParameter() {ParameterName = "pRCV_BKBR_ID", Value = ob.RCV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pLIN_BKBR_ID", Value = ob.LIN_BKBR_ID},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pLTST_SHPMNT_DT", Value = ob.LTST_SHPMNT_DT},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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

        public List<CM_EXP_LC_HModel> SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pMC_BUYER_ID = null,
            Int64? pRF_ACTN_STATUS_ID = null, string pEXP_LCSC_NO = null, string pSTYLE_ORDER_NO = null, string pISSUE_DT = null)
        {
            string sp = "PKG_CM_EXPORT_LC.CM_EXP_LC_H_SELECT";
            try
            {
                var obList = new List<CM_EXP_LC_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value =pCM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value =pRF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pEXP_LCSC_NO", Value =pEXP_LCSC_NO},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value =pISSUE_DT},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_EXP_LC_HModel ob = new CM_EXP_LC_HModel();
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.IS_SC_OR_LC = (dr["IS_SC_OR_LC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SC_OR_LC"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);
                    ob.EXP_CNTRY_ID = (dr["EXP_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXP_CNTRY_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
                    ob.PCT_BB_LMT = (dr["PCT_BB_LMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_BB_LMT"]);
                    ob.AVAIL_WITH = (dr["AVAIL_WITH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AVAIL_WITH"]);
                    ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                    ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_DESC = (dr["PAYM_TERM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_DESC"]);
                    ob.SND_BKBR_ID = (dr["SND_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SND_BKBR_ID"]);
                    ob.RCV_BKBR_ID = (dr["RCV_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_BKBR_ID"]);
                    ob.LIN_BKBR_ID = (dr["LIN_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LIN_BKBR_ID"]);
                    ob.IS_PART_SHPM = (dr["IS_PART_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PART_SHPM"]);
                    ob.IS_TRAN_SHPM = (dr["IS_TRAN_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRAN_SHPM"]);
                    ob.LOAD_PORT_ID = (dr["LOAD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOAD_PORT_ID"]);
                    ob.DSG_PORT_LST = (dr["DSG_PORT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DSG_PORT_LST"]);
                    ob.LTST_SHPMNT_DT = (dr["LTST_SHPMNT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LTST_SHPMNT_DT"]);
                    ob.DRAFT_AT = (dr["DRAFT_AT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAFT_AT"]);
                    ob.DRAWEE_DESC = (dr["DRAWEE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAWEE_DESC"]);
                    ob.DOC_REQD = (dr["DOC_REQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REQD"]);
                    ob.LC_TOT_VAL = (dr["LC_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_VAL"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CM_NOTIFY_PARTY_LST = (dr["CM_NOTIFY_PARTY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CM_NOTIFY_PARTY_LST"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);

                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    
                    ob.PO_TOT_VAL = (dr["PO_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_TOT_VAL"]);
                    ob.IS_IGNOR_DIFF = (dr["IS_IGNOR_DIFF"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_IGNOR_DIFF"]);
                    ob.LC_TOT_CHG = (dr["LC_TOT_CHG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_CHG"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_EXP_LC_HModel Select(Int64? pCM_EXP_LC_H_ID = null)
        {
            string sp = "PKG_CM_EXPORT_LC.CM_EXP_LC_H_SELECT";
            try
            {
                var ob = new CM_EXP_LC_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value =pCM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.IS_SC_OR_LC = (dr["IS_SC_OR_LC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SC_OR_LC"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);

                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);

                    ob.EXP_CNTRY_ID = (dr["EXP_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXP_CNTRY_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
                    ob.PCT_BB_LMT = (dr["PCT_BB_LMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_BB_LMT"]);
                    ob.AVAIL_WITH = (dr["AVAIL_WITH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AVAIL_WITH"]);
                    ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                    ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_DESC = (dr["PAYM_TERM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_DESC"]);
                    ob.SND_BKBR_ID = (dr["SND_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SND_BKBR_ID"]);
                    ob.RCV_BKBR_ID = (dr["RCV_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_BKBR_ID"]);
                    ob.LIN_BKBR_ID = (dr["LIN_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LIN_BKBR_ID"]);
                    ob.IS_PART_SHPM = (dr["IS_PART_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PART_SHPM"]);
                    ob.IS_TRAN_SHPM = (dr["IS_TRAN_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRAN_SHPM"]);
                    ob.LOAD_PORT_ID = (dr["LOAD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOAD_PORT_ID"]);
                    ob.DSG_PORT_LST = (dr["DSG_PORT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DSG_PORT_LST"]);
                    ob.LTST_SHPMNT_DT = (dr["LTST_SHPMNT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LTST_SHPMNT_DT"]);
                    ob.DRAFT_AT = (dr["DRAFT_AT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAFT_AT"]);
                    ob.DRAWEE_DESC = (dr["DRAWEE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAWEE_DESC"]);
                    ob.DOC_REQD = (dr["DOC_REQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REQD"]);
                    ob.LC_TOT_VAL = (dr["LC_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_VAL"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CM_NOTIFY_PARTY_LST = (dr["CM_NOTIFY_PARTY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CM_NOTIFY_PARTY_LST"]);

                    ob.IS_ADV_LC = (dr["IS_ADV_LC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ADV_LC"]);
                    ob.IS_LC_ADJ = (dr["IS_LC_ADJ"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LC_ADJ"]);
                    ob.IS_LNK_PI_PO = (dr["IS_LNK_PI_PO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LNK_PI_PO"]);
                    
                    ob.PO_TOT_VAL = (dr["PO_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_TOT_VAL"]);
                    ob.IS_IGNOR_DIFF = (dr["IS_IGNOR_DIFF"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_IGNOR_DIFF"]);
                    ob.LC_TOT_CHG = (dr["LC_TOT_CHG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_CHG"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CM_EXP_LC_HModel> SelectByBuyerID(Int64? pMC_BUYER_ID = null, Int64? pCM_EXP_LC_H_ID = null)
        {
            string sp = "PKG_CM_EXPORT_LC.CM_EXP_LC_H_SELECT";
            try
            {
                var list = new List<CM_EXP_LC_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pCM_EXP_LC_H_ID", Value =pCM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    var ob = new CM_EXP_LC_HModel();
                    ob.CM_EXP_LC_H_ID = (dr["CM_EXP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_LC_H_ID"]);
                    ob.IS_SC_OR_LC = (dr["IS_SC_OR_LC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_SC_OR_LC"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);

                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);

                    ob.EXP_CNTRY_ID = (dr["EXP_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXP_CNTRY_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
                    ob.PCT_BB_LMT = (dr["PCT_BB_LMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_BB_LMT"]);
                    ob.AVAIL_WITH = (dr["AVAIL_WITH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AVAIL_WITH"]);
                    ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                    ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_DESC = (dr["PAYM_TERM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_DESC"]);
                    ob.SND_BKBR_ID = (dr["SND_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SND_BKBR_ID"]);
                    ob.RCV_BKBR_ID = (dr["RCV_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_BKBR_ID"]);
                    ob.LIN_BKBR_ID = (dr["LIN_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LIN_BKBR_ID"]);
                    ob.IS_PART_SHPM = (dr["IS_PART_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PART_SHPM"]);
                    ob.IS_TRAN_SHPM = (dr["IS_TRAN_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRAN_SHPM"]);
                    ob.LOAD_PORT_ID = (dr["LOAD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOAD_PORT_ID"]);
                    ob.DSG_PORT_LST = (dr["DSG_PORT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DSG_PORT_LST"]);
                    ob.LTST_SHPMNT_DT = (dr["LTST_SHPMNT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LTST_SHPMNT_DT"]);
                    ob.DRAFT_AT = (dr["DRAFT_AT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAFT_AT"]);
                    ob.DRAWEE_DESC = (dr["DRAWEE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DRAWEE_DESC"]);
                    ob.DOC_REQD = (dr["DOC_REQD"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DOC_REQD"]);
                    ob.LC_TOT_VAL = (dr["LC_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_VAL"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CM_NOTIFY_PARTY_LST = (dr["CM_NOTIFY_PARTY_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CM_NOTIFY_PARTY_LST"]);

                    ob.IS_ADV_LC = (dr["IS_ADV_LC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ADV_LC"]);
                    ob.IS_LC_ADJ = (dr["IS_LC_ADJ"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_LC_ADJ"]);
                    ob.IS_LNK_PI_PO = (dr["IS_LNK_PI_PO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LNK_PI_PO"]);

                    ob.LC_VALUE = (dr["LC_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_VALUE"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    
                    ob.PO_TOT_VAL = (dr["PO_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PO_TOT_VAL"]);
                    ob.IS_IGNOR_DIFF = (dr["IS_IGNOR_DIFF"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_IGNOR_DIFF"]);
                    ob.LC_TOT_CHG = (dr["LC_TOT_CHG"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_TOT_CHG"]);

                    list.Add(ob);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal LC_VALUE { get; set; }

        public string CURR_NAME_EN { get; set; }
    }
}