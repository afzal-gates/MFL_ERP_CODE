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
    public class KNT_SCO_BILL_HModel
    {
        public Int64 KNT_SCO_BILL_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string REF_BILL_NO { get; set; }
        public string BILL_NO { get; set; }
        public DateTime BILL_DT { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public Int64? NEXT_RF_ACTN_STATUS_ID { get; set; }

        public Int64? PARENT_ID { get; set; }
        public Int64? REVISION_NO { get; set; }
        public Decimal? PREV_BILL_AMT { get; set; }
        public Decimal? CURR_BILL_AMT { get; set; }
        public Decimal? REV_AMT { get; set; }
        public Int64? LK_BL_CR_RSN_TYP_ID { get; set; }
        public string OTH_RSN_DESC { get; set; }
        public string IS_DM_CM { get; set; }
        public string IS_ACTIVE { get; set; }

        public string MISC_DESC { get; set; }
        public Decimal? MISC_BILL_AMT { get; set; }
        
        public string SCO_BILL_D_XML { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }
        public string COMP_NAME_EN { get; set; }
        public string EVENT_NAME { get; set; }
        public string IS_FINALIZED { get; set; }
        public string CORR_TYPE_NAME { get; set; }
        public string ACTION_USER_TYPE { get; set; }



        public string BatchSave()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_bill_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_BILL_H_ID", Value = ob.KNT_SCO_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = ob.BILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = ob.BILL_DT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pMISC_DESC", Value = ob.MISC_DESC},
                     new CommandParameter() {ParameterName = "pMISC_BILL_AMT", Value = ob.MISC_BILL_AMT},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pNEXT_RF_ACTN_STATUS_ID", Value = ob.NEXT_RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSCO_BILL_D_XML", Value = ob.SCO_BILL_D_XML},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     
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

        public string SaveRevision()
        {
            const string sp = "pkg_knit_subcontract.knt_sco_bill_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_BILL_H_ID", Value = ob.KNT_SCO_BILL_H_ID},                     
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = ob.BILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = ob.BILL_DT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},                     
                     new CommandParameter() {ParameterName = "pLK_BL_CR_RSN_TYP_ID", Value = ob.LK_BL_CR_RSN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOTH_RSN_DESC", Value = ob.OTH_RSN_DESC},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                    
                     new CommandParameter() {ParameterName = "pOption", Value =1001},
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

        public object GetBillList(int pageNumber, int pageSize, Int64 pSCM_SUPPLIER_ID, string pBILL_NO = null, DateTime? pBILL_DT = null)
        {
            string sp = "pkg_knit_subcontract.knt_sco_bill_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SCO_BILL_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = pBILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = pBILL_DT},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SCO_BILL_HModel ob = new KNT_SCO_BILL_HModel();
                    ob.KNT_SCO_BILL_H_ID = (dr["KNT_SCO_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_BILL_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.BILL_NO = (dr["BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BILL_NO"]);
                    ob.BILL_DT = (dr["BILL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BILL_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    if (dr["REVISION_NO"] != DBNull.Value)
                    {
                        ob.REF_BILL_NO = (dr["REF_BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REF_BILL_NO"]);
                        ob.OTH_RSN_DESC = (dr["OTH_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTH_RSN_DESC"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    }

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    //ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                    if (vTotalRec < 1)
                    {
                        vTotalRec = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOTAL_REC"]);
                    }

                    obList.Add(ob);
                }

                obj.total = vTotalRec;
                obj.data = obList;
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SCO_BILL_HModel GetBillHdr(Int64 pKNT_SCO_BILL_H_ID)
        {
            string sp = "pkg_knit_subcontract.knt_sco_bill_h_select";
            try
            {
                var ob = new KNT_SCO_BILL_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_BILL_H_ID", Value =pKNT_SCO_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SCO_BILL_H_ID = (dr["KNT_SCO_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_BILL_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.BILL_NO = (dr["BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BILL_NO"]);
                    ob.BILL_DT = (dr["BILL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BILL_DT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.MISC_DESC = (dr["MISC_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MISC_DESC"]);
                    ob.MISC_BILL_AMT = (dr["MISC_BILL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["MISC_BILL_AMT"]);
                    
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

                    if (dr["PARENT_ID"] != DBNull.Value)
                    {

                        ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                        ob.IS_DM_CM = (dr["IS_DM_CM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DM_CM"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.LK_BL_CR_RSN_TYP_ID = (dr["LK_BL_CR_RSN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BL_CR_RSN_TYP_ID"]);
                        ob.CORR_TYPE_NAME = (dr["CORR_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CORR_TYPE_NAME"]);
                        ob.OTH_RSN_DESC = (dr["OTH_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTH_RSN_DESC"]);

                        ob.PREV_BILL_AMT = (dr["PREV_BILL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PREV_BILL_AMT"]);
                        ob.REV_AMT = (dr["REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_AMT"]);
                    }

                    ob.CURR_BILL_AMT = (dr["CURR_BILL_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["CURR_BILL_AMT"]);

                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.ACTION_USER_TYPE = (dr["ACTION_USER_TYPE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTION_USER_TYPE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public class KNT_SCO_BILL_USR_LAVELModel
    {
        public string USER_DRAFT_NAME { get; set; }
        public string USER_VERIFIER_NAME { get; set; }


        public KNT_SCO_BILL_USR_LAVELModel GetScoBillProcUserLavel()
        {
            string sp = "pkg_knit_subcontract.knt_sco_bill_h_select";
            try
            {
                var ob = new KNT_SCO_BILL_USR_LAVELModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.USER_DRAFT_NAME = (dr["USER_DRAFT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DRAFT_NAME"]);
                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
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