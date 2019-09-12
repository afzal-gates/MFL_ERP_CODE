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
    public class CM_IMP_LC_HModel
    {
        public Int64 CM_IMP_LC_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public string IMP_LC_NO { get; set; }
        public DateTime IMP_LC_DT { get; set; }
        public Int64 RF_LC_TYPE_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public DateTime ISSUE_DT { get; set; }
        public DateTime EXPIRY_DT { get; set; }
        public Int64 EXPIRY_PLACE_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal PCT_TOLR_AMT { get; set; }
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
        public string LCA_NO { get; set; }
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
        public Int64 RF_INSURN_COMP_ID { get; set; }

        public string XML_LC_D { get; set; }
        public string IS_UPDATE { get; set; }


        public string Save()
        {
            const string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value = ob.IMP_LC_NO},
                     new CommandParameter() {ParameterName = "pIMP_LC_DT", Value = ob.IMP_LC_DT},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_PLACE_ID", Value = ob.EXPIRY_PLACE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
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
                     new CommandParameter() {ParameterName = "pLCA_NO", Value = ob.LCA_NO},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},                    
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_LC_D", Value = ob.XML_LC_D},                     
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE},                     
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
            const string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_H_REVISE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value = ob.IMP_LC_NO},
                     new CommandParameter() {ParameterName = "pIMP_LC_DT", Value = ob.IMP_LC_DT},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_PLACE_ID", Value = ob.EXPIRY_PLACE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
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
                     new CommandParameter() {ParameterName = "pLCA_NO", Value = ob.LCA_NO},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},                    
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_LC_D", Value = ob.XML_LC_D},   
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "PKG_CM_IMPORT_LC.CM_IMP_LC_H_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value = ob.CM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value = ob.IMP_LC_NO},
                     new CommandParameter() {ParameterName = "pIMP_LC_DT", Value = ob.IMP_LC_DT},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value = ob.ISSUE_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_DT", Value = ob.EXPIRY_DT},
                     new CommandParameter() {ParameterName = "pEXPIRY_PLACE_ID", Value = ob.EXPIRY_PLACE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_AMT", Value = ob.PCT_TOLR_AMT},
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
                     new CommandParameter() {ParameterName = "pLCA_NO", Value = ob.LCA_NO},
                     new CommandParameter() {ParameterName = "pDRAFT_AT", Value = ob.DRAFT_AT},
                     new CommandParameter() {ParameterName = "pDRAWEE_DESC", Value = ob.DRAWEE_DESC},
                     new CommandParameter() {ParameterName = "pDOC_REQD", Value = ob.DOC_REQD},
                     new CommandParameter() {ParameterName = "pLC_TOT_VAL", Value = ob.LC_TOT_VAL},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_INSURN_COMP_ID", Value = ob.RF_INSURN_COMP_ID},                    
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_LC_D", Value = ob.XML_LC_D},   
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

        public List<CM_IMP_LC_HModel> SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null,
                                            Int64? pLK_LC_STS_ID = null, string pIMP_LC_NO = null, string pLC_PI_NO = null, string pISSUE_DT = null, Int64? pRF_ACTN_STATUS_ID = null)
        {
            string sp = "pkg_cm_import_lc.cm_imp_lc_h_select";
            try
            {
                var obList = new List<CM_IMP_LC_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value =pRF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pIMP_LC_NO", Value =pIMP_LC_NO},
                     new CommandParameter() {ParameterName = "pLC_PI_NO", Value =pLC_PI_NO},
                     new CommandParameter() {ParameterName = "pISSUE_DT", Value =pISSUE_DT},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_LC_HModel ob = new CM_IMP_LC_HModel();
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.IMP_LC_DT = (dr["IMP_LC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IMP_LC_DT"]);
                    ob.RF_LC_TYPE_ID = (dr["RF_LC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LC_TYPE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);
                    ob.EXPIRY_PLACE_ID = (dr["EXPIRY_PLACE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXPIRY_PLACE_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
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
                    ob.LCA_NO = (dr["LCA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LCA_NO"]);
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
                    ob.RF_INSURN_COMP_ID = (dr["RF_INSURN_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INSURN_COMP_ID"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.LC_TYPE_NAME_EN = (dr["LC_TYPE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LC_TYPE_NAME_EN"]);

                    ob.EXP_LCSC_NO = (dr["EXP_LCSC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LCSC_NO"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);

                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CM_IMP_LC_HModel> SelectByID(Int64? pMC_BUYER_ID = null, Int64? pCM_EXP_LC_H_ID = null)
        {
            string sp = "pkg_cm_import_lc.cm_imp_lc_h_select";
            try
            {
                var obList = new List<CM_IMP_LC_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pCM_EXP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_LC_HModel ob = new CM_IMP_LC_HModel();
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.IMP_LC_DT = (dr["IMP_LC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IMP_LC_DT"]);
                    ob.RF_LC_TYPE_ID = (dr["RF_LC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LC_TYPE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);
                    ob.EXPIRY_PLACE_ID = (dr["EXPIRY_PLACE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXPIRY_PLACE_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
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
                    ob.LCA_NO = (dr["LCA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LCA_NO"]);
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
                    ob.RF_INSURN_COMP_ID = (dr["RF_INSURN_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INSURN_COMP_ID"]);

                    ob.LC_VALUE = (dr["LC_VALUE"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["LC_VALUE"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);                    

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_IMP_LC_HModel Select(Int64? pCM_IMP_LC_H_ID = null)
        {
            string sp = "pkg_cm_import_lc.cm_imp_lc_h_select";
            try
            {
                var ob = new CM_IMP_LC_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_LC_H_ID", Value =pCM_IMP_LC_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_IMP_LC_H_ID = (dr["CM_IMP_LC_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_LC_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.IMP_LC_NO = (dr["IMP_LC_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IMP_LC_NO"]);
                    ob.IMP_LC_DT = (dr["IMP_LC_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["IMP_LC_DT"]);
                    ob.RF_LC_TYPE_ID = (dr["RF_LC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LC_TYPE_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.ISSUE_DT = (dr["ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["ISSUE_DT"]);
                    ob.EXPIRY_DT = (dr["EXPIRY_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXPIRY_DT"]);
                    ob.EXPIRY_PLACE_ID = (dr["EXPIRY_PLACE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["EXPIRY_PLACE_ID"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.PCT_TOLR_AMT = (dr["PCT_TOLR_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_AMT"]);
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
                    ob.LCA_NO = (dr["LCA_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["LCA_NO"]);
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
                    ob.RF_INSURN_COMP_ID = (dr["RF_INSURN_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INSURN_COMP_ID"]);
                    ob.EXP_LC_NO_LST = (dr["EXP_LC_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EXP_LC_NO_LST"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TOTAL_REC { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string LC_TYPE_NAME_EN { get; set; }

        public long PERMISSION { get; set; }

        public string EXP_LCSC_NO { get; set; }

        public string PI_NO_IMP { get; set; }

        public string EXP_LC_NO_LST { get; set; }

        public decimal LC_VALUE { get; set; }

        public string CURR_NAME_EN { get; set; }
    }
}