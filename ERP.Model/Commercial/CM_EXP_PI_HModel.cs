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
    public class CM_EXP_PI_HModel
    {
        public Int64 CM_EXP_PI_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public string PI_NO_EXP { get; set; }
        public DateTime PI_DT_EXP { get; set; }
        public string ATTN_BUYER { get; set; }
        public DateTime EXP_DELV_DT { get; set; }
        public Int64 MC_BUYER_ID { get; set; }
        public Int64 PARENT_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime? REVISION_DT { get; set; }        
        public string REV_REASON { get; set; }
        public string DELV_DESC { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 RF_INCO_TERM_ID { get; set; }
        public Int64 RF_DELV_PLC_ID { get; set; }
        public Int64 RF_PAYM_TERM_ID { get; set; }
        public string PAYM_TERM_DESC { get; set; }
        public string IS_PART_SHPM { get; set; }
        public string IS_TRAN_SHPM { get; set; }
        public Int64 LOAD_PORT_ID { get; set; }
        public string DSG_PORT_LST { get; set; }
        public Int64 ORG_CNTRY_ID { get; set; }
        public Int64 RF_LC_TYPE_ID { get; set; }
        public string PAYMENT { get; set; }
        public Int64 PI_VALIDITY { get; set; }
        public string PI_VALIDITY_DESC { get; set; }
        public string INSPECTION { get; set; }
        public DateTime LTST_SHPMNT_DT { get; set; }
        public string SHIP_SCHDL { get; set; }
        public Int64 RF_SHIP_MODE_ID { get; set; }
        public string SHIP_TERM { get; set; }
        public string PACKING { get; set; }
        public Decimal PCT_TOLR_LMT { get; set; }
        public string TOLR_LMT_DESC { get; set; }
        public string AVAIL_WITH { get; set; }
        public string INSURANCE { get; set; }
        public Int64 ADV_BKBR_ID { get; set; }
        public Decimal PI_TOT_VAL { get; set; }
        public string IS_DISC_P_A { get; set; }
        public Decimal DISC_AMT { get; set; }
        public Decimal PI_NET_AMT { get; set; }
        public string ADDL_TERM { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }        
        public string ITEM_DESC { get; set; }
        public string IS_UPDATE { get; set; }
        public string XML_PO { get; set; }
        public string RPT_PATH_URL { get; set; }
        
        public string Save()
        {
            const string sp = "PKG_CM_EXPORT_PI.CM_EXP_PI_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_PI_H_ID", Value = ob.CM_EXP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_EXP", Value = ob.PI_NO_EXP},
                     new CommandParameter() {ParameterName = "pPI_DT_EXP", Value = ob.PI_DT_EXP},
                     new CommandParameter() {ParameterName = "pATTN_BUYER", Value = ob.ATTN_BUYER},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pITEM_DESC", Value = ob.ITEM_DESC},
                     new CommandParameter() {ParameterName = "pDELV_DESC", Value = ob.DELV_DESC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pORG_CNTRY_ID", Value = ob.ORG_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPAYMENT", Value = ob.PAYMENT},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY", Value = ob.PI_VALIDITY},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY_DESC", Value = ob.PI_VALIDITY_DESC},
                     new CommandParameter() {ParameterName = "pINSPECTION", Value = ob.INSPECTION},
                     new CommandParameter() {ParameterName = "pLTST_SHPMNT_DT", Value = ob.LTST_SHPMNT_DT},
                     new CommandParameter() {ParameterName = "pSHIP_SCHDL", Value = ob.SHIP_SCHDL},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pSHIP_TERM", Value = ob.SHIP_TERM},
                     new CommandParameter() {ParameterName = "pPACKING", Value = ob.PACKING},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_LMT", Value = ob.PCT_TOLR_LMT},
                     new CommandParameter() {ParameterName = "pTOLR_LMT_DESC", Value = ob.TOLR_LMT_DESC},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pINSURANCE", Value = ob.INSURANCE},
                     new CommandParameter() {ParameterName = "pADV_BKBR_ID", Value = ob.ADV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pPI_TOT_VAL", Value = ob.PI_TOT_VAL},
                     new CommandParameter() {ParameterName = "pIS_DISC_P_A", Value = ob.IS_DISC_P_A},
                     new CommandParameter() {ParameterName = "pDISC_AMT", Value = ob.DISC_AMT},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pADDL_TERM", Value = ob.ADDL_TERM},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},                     
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE},                     
                     new CommandParameter() {ParameterName = "pXML_PO", Value = ob.XML_PO},
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
            const string sp = "PKG_CM_EXPORT_PI.CM_EXP_PI_H_REVISE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_PI_H_ID", Value = ob.CM_EXP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_EXP", Value = ob.PI_NO_EXP},
                     new CommandParameter() {ParameterName = "pPI_DT_EXP", Value = ob.PI_DT_EXP},
                     new CommandParameter() {ParameterName = "pATTN_BUYER", Value = ob.ATTN_BUYER},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pITEM_DESC", Value = ob.ITEM_DESC},
                     new CommandParameter() {ParameterName = "pDELV_DESC", Value = ob.DELV_DESC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pORG_CNTRY_ID", Value = ob.ORG_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPAYMENT", Value = ob.PAYMENT},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY", Value = ob.PI_VALIDITY},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY_DESC", Value = ob.PI_VALIDITY_DESC},
                     new CommandParameter() {ParameterName = "pINSPECTION", Value = ob.INSPECTION},
                     new CommandParameter() {ParameterName = "pLTST_SHPMNT_DT", Value = ob.LTST_SHPMNT_DT},
                     new CommandParameter() {ParameterName = "pSHIP_SCHDL", Value = ob.SHIP_SCHDL},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pSHIP_TERM", Value = ob.SHIP_TERM},
                     new CommandParameter() {ParameterName = "pPACKING", Value = ob.PACKING},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_LMT", Value = ob.PCT_TOLR_LMT},
                     new CommandParameter() {ParameterName = "pTOLR_LMT_DESC", Value = ob.TOLR_LMT_DESC},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pINSURANCE", Value = ob.INSURANCE},
                     new CommandParameter() {ParameterName = "pADV_BKBR_ID", Value = ob.ADV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pPI_TOT_VAL", Value = ob.PI_TOT_VAL},
                     new CommandParameter() {ParameterName = "pIS_DISC_P_A", Value = ob.IS_DISC_P_A},
                     new CommandParameter() {ParameterName = "pDISC_AMT", Value = ob.DISC_AMT},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pADDL_TERM", Value = ob.ADDL_TERM},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},                     
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_PO", Value = ob.XML_PO},
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

        public string Delete()
        {
            const string sp = "SP_CM_EXP_PI_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_PI_H_ID", Value = ob.CM_EXP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_EXP", Value = ob.PI_NO_EXP},
                     new CommandParameter() {ParameterName = "pPI_DT_EXP", Value = ob.PI_DT_EXP},
                     new CommandParameter() {ParameterName = "pATTN_BUYER", Value = ob.ATTN_BUYER},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value = ob.MC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pDELV_DESC", Value = ob.DELV_DESC},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pPAYM_TERM_DESC", Value = ob.PAYM_TERM_DESC},
                     new CommandParameter() {ParameterName = "pIS_PART_SHPM", Value = ob.IS_PART_SHPM},
                     new CommandParameter() {ParameterName = "pIS_TRAN_SHPM", Value = ob.IS_TRAN_SHPM},
                     new CommandParameter() {ParameterName = "pLOAD_PORT_ID", Value = ob.LOAD_PORT_ID},
                     new CommandParameter() {ParameterName = "pDSG_PORT_LST", Value = ob.DSG_PORT_LST},
                     new CommandParameter() {ParameterName = "pORG_CNTRY_ID", Value = ob.ORG_CNTRY_ID},
                     new CommandParameter() {ParameterName = "pRF_LC_TYPE_ID", Value = ob.RF_LC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pPAYMENT", Value = ob.PAYMENT},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY", Value = ob.PI_VALIDITY},
                     new CommandParameter() {ParameterName = "pPI_VALIDITY_DESC", Value = ob.PI_VALIDITY_DESC},
                     new CommandParameter() {ParameterName = "pSHIP_SCHDL", Value = ob.SHIP_SCHDL},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pPACKING", Value = ob.PACKING},
                     new CommandParameter() {ParameterName = "pPCT_TOLR_LMT", Value = ob.PCT_TOLR_LMT},
                     new CommandParameter() {ParameterName = "pTOLR_LMT_DESC", Value = ob.TOLR_LMT_DESC},
                     new CommandParameter() {ParameterName = "pAVAIL_WITH", Value = ob.AVAIL_WITH},
                     new CommandParameter() {ParameterName = "pINSPECTION", Value = ob.INSPECTION},
                     new CommandParameter() {ParameterName = "pADV_BKBR_ID", Value = ob.ADV_BKBR_ID},
                     new CommandParameter() {ParameterName = "pPI_TOT_VAL", Value = ob.PI_TOT_VAL},
                     new CommandParameter() {ParameterName = "pIS_DISC_P_A", Value = ob.IS_DISC_P_A},
                     new CommandParameter() {ParameterName = "pDISC_AMT", Value = ob.DISC_AMT},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
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

        public List<CM_EXP_PI_HModel> SelectAll(int pageNo, int pageSize, Int64? pHR_COMPANY_ID = null, Int64? pMC_BUYER_ID = null,
            Int64? pRF_ACTN_STATUS_ID = null, string pPI_NO_EXP = null, string pSTYLE_ORDER_NO = null, string pPI_DT_EXP = null)
        {
            string sp = "PKG_CM_EXPORT_PI.CM_EXP_PI_H_SELECT";
            try
            {
                var obList = new List<CM_EXP_PI_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     //new CommandParameter() {ParameterName = "pCM_EXP_PI_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID", Value =pMC_BUYER_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value =pRF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_EXP", Value =pPI_NO_EXP},
                     new CommandParameter() {ParameterName = "pSTYLE_ORDER_NO", Value =pSTYLE_ORDER_NO},
                     new CommandParameter() {ParameterName = "pPI_DT_EXP", Value =pPI_DT_EXP},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_EXP_PI_HModel ob = new CM_EXP_PI_HModel();
                    ob.CM_EXP_PI_H_ID = (dr["CM_EXP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_PI_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.PI_NO_EXP = (dr["PI_NO_EXP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_EXP"]);
                    ob.PI_DT_EXP = (dr["PI_DT_EXP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PI_DT_EXP"]);
                    ob.ATTN_BUYER = (dr["ATTN_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ATTN_BUYER"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.DELV_DESC = (dr["DELV_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_DESC"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                    ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_DESC = (dr["PAYM_TERM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_DESC"]);
                    ob.ITEM_DESC = (dr["ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_DESC"]);
                    ob.IS_PART_SHPM = (dr["IS_PART_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PART_SHPM"]);
                    ob.IS_TRAN_SHPM = (dr["IS_TRAN_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRAN_SHPM"]);
                    ob.LOAD_PORT_ID = (dr["LOAD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOAD_PORT_ID"]);
                    ob.DSG_PORT_LST = (dr["DSG_PORT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DSG_PORT_LST"]);
                    ob.ORG_CNTRY_ID = (dr["ORG_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORG_CNTRY_ID"]);
                    ob.RF_LC_TYPE_ID = (dr["RF_LC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LC_TYPE_ID"]);
                    ob.PAYMENT = (dr["PAYMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYMENT"]);
                    ob.PI_VALIDITY = (dr["PI_VALIDITY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PI_VALIDITY"]);
                    ob.PI_VALIDITY_DESC = (dr["PI_VALIDITY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_VALIDITY_DESC"]);
                    ob.INSPECTION = (dr["INSPECTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSPECTION"]);
                    ob.LTST_SHPMNT_DT = (dr["LTST_SHPMNT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LTST_SHPMNT_DT"]);
                    ob.SHIP_SCHDL = (dr["SHIP_SCHDL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_SCHDL"]);
                    ob.RF_SHIP_MODE_ID = (dr["RF_SHIP_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SHIP_MODE_ID"]);
                    ob.SHIP_TERM = (dr["SHIP_TERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_TERM"]);
                    ob.PACKING = (dr["PACKING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACKING"]);
                    ob.PCT_TOLR_LMT = (dr["PCT_TOLR_LMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_LMT"]);
                    ob.TOLR_LMT_DESC = (dr["TOLR_LMT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TOLR_LMT_DESC"]);
                    ob.AVAIL_WITH = (dr["AVAIL_WITH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AVAIL_WITH"]);
                    ob.INSURANCE = (dr["INSURANCE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSURANCE"]);
                    ob.ADV_BKBR_ID = (dr["ADV_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADV_BKBR_ID"]);
                    ob.PI_TOT_VAL = (dr["PI_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_TOT_VAL"]);
                    ob.IS_DISC_P_A = (dr["IS_DISC_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISC_P_A"]);
                    ob.DISC_AMT = (dr["DISC_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DISC_AMT"]);
                    ob.PI_NET_AMT = (dr["PI_NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_NET_AMT"]);
                    ob.ADDL_TERM = (dr["ADDL_TERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDL_TERM"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.COUNTRY_NAME_EN = (dr["COUNTRY_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COUNTRY_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);

                    ob.RPT_PATH_URL = (dr["RPT_PATH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_PATH_URL"]);

                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
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

        public CM_EXP_PI_HModel Select(Int64? pCM_EXP_PI_H_ID = null)
        {
            string sp = "PKG_CM_EXPORT_PI.CM_EXP_PI_H_SELECT";
            try
            {
                var ob = new CM_EXP_PI_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_EXP_PI_H_ID", Value = pCM_EXP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_EXP_PI_H_ID = (dr["CM_EXP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_EXP_PI_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.PI_NO_EXP = (dr["PI_NO_EXP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_EXP"]);
                    ob.PI_DT_EXP = (dr["PI_DT_EXP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PI_DT_EXP"]);
                    ob.ATTN_BUYER = (dr["ATTN_BUYER"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ATTN_BUYER"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.DELV_DESC = (dr["DELV_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DELV_DESC"]);
                    ob.MC_BUYER_ID = (dr["MC_BUYER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BUYER_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    if(dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                    ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                    ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                    ob.PAYM_TERM_DESC = (dr["PAYM_TERM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYM_TERM_DESC"]);
                    ob.ITEM_DESC = (dr["ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_DESC"]);
                    ob.IS_PART_SHPM = (dr["IS_PART_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_PART_SHPM"]);
                    ob.IS_TRAN_SHPM = (dr["IS_TRAN_SHPM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_TRAN_SHPM"]);
                    ob.LOAD_PORT_ID = (dr["LOAD_PORT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LOAD_PORT_ID"]);
                    ob.DSG_PORT_LST = (dr["DSG_PORT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DSG_PORT_LST"]);
                    ob.ORG_CNTRY_ID = (dr["ORG_CNTRY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ORG_CNTRY_ID"]);
                    ob.RF_LC_TYPE_ID = (dr["RF_LC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_LC_TYPE_ID"]);
                    ob.PAYMENT = (dr["PAYMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAYMENT"]);
                    ob.PI_VALIDITY = (dr["PI_VALIDITY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PI_VALIDITY"]);
                    ob.PI_VALIDITY_DESC = (dr["PI_VALIDITY_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_VALIDITY_DESC"]);
                    ob.INSPECTION = (dr["INSPECTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSPECTION"]);
                    ob.LTST_SHPMNT_DT = (dr["LTST_SHPMNT_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LTST_SHPMNT_DT"]);
                    ob.SHIP_SCHDL = (dr["SHIP_SCHDL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_SCHDL"]);
                    ob.RF_SHIP_MODE_ID = (dr["RF_SHIP_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SHIP_MODE_ID"]);
                    ob.SHIP_TERM = (dr["SHIP_TERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SHIP_TERM"]);
                    ob.PACKING = (dr["PACKING"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PACKING"]);
                    ob.PCT_TOLR_LMT = (dr["PCT_TOLR_LMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_TOLR_LMT"]);
                    ob.TOLR_LMT_DESC = (dr["TOLR_LMT_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TOLR_LMT_DESC"]);
                    ob.AVAIL_WITH = (dr["AVAIL_WITH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AVAIL_WITH"]);
                    ob.INSURANCE = (dr["INSURANCE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INSURANCE"]);
                    ob.ADV_BKBR_ID = (dr["ADV_BKBR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ADV_BKBR_ID"]);
                    ob.PI_TOT_VAL = (dr["PI_TOT_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_TOT_VAL"]);
                    ob.IS_DISC_P_A = (dr["IS_DISC_P_A"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DISC_P_A"]);
                    ob.DISC_AMT = (dr["DISC_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DISC_AMT"]);
                    ob.PI_NET_AMT = (dr["PI_NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_NET_AMT"]);
                    ob.ADDL_TERM = (dr["ADDL_TERM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ADDL_TERM"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
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

        public int TOTAL_REC { get; set; }

        public string STYLE_NO { get; set; }

        public string ORDER_NO { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string COUNTRY_NAME_EN { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public long PERMISSION { get; set; }
    }
}