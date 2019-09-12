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
    public class KNT_YRN_STR_REQ_HModel
    {
        public Int64 KNT_YRN_STR_REQ_H_ID { get; set; }
        public Int64? KNT_JOB_CRD_H_ID { get; set; }
        public Int64? KNT_SC_PRG_ISS_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public Int64 STR_REQ_BY { get; set; }
        public Int64 STR_REQ_TO { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? KNT_YRN_ISS_H_ID { get; set; }

        public Int64? KNT_SCO_CLCF_PRG_H_ID { get; set; }

        public string COMP_NAME_EN { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string REQ_STATUS { get; set; }
        public int TOTAL_REC { get; set; }
        public string USER_NAME_EN { get; set; }
        public string REQ_TYPE_NAME { get; set; }

        public long YARN_ITEM_ID { get; set; }
        public long KNT_YRN_LOT_ID { get; set; }
        public long LK_YRN_COLR_GRP_ID { get; set; }
        public long RF_BRAND_ID { get; set; }
        public string ITEM_NAME_EN { get; set; }
        public string BRAND_NAME_EN { get; set; }
        public string YRN_LOT_NO { get; set; }
        public string YRN_COLR_GRP { get; set; }
        public long RQD_YRN_QTY { get; set; }
        public long STOCK_QTY { get; set; }
        public long REQ_QTY { get; set; }
        public string XML_REQ_D { get; set; }

        public List<KNT_YRN_STR_REQ_DModel> yarn_list { get; set; }

        public string Save()
        {
            const string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pKNT_SC_PRG_ISS_ID", Value = ob.KNT_SC_PRG_ISS_ID==0?null:ob.KNT_SC_PRG_ISS_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO==0?1:ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pLK_REQ_STATUS_ID", Value = ob.LK_REQ_STATUS_ID==0?229:ob.LK_REQ_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opKNT_YRN_STR_REQ_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_knit_yarn_issue.knt_yrn_iss_h_ststus_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
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

        

        public List<KNT_YRN_STR_REQ_HModel> SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null,
            string pREQ_TYPE_NAME = null, string pBYR_ACC_NAME_EN = null, string pMC_ORDER_NO_LST = null, string pYR_COUNT_NO = null, 
            string pUSER_NAME_EN = null, string pEVENT_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value =0},

                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value =pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value =pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value =pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pUSER_NAME_EN", Value =pUSER_NAME_EN},
                     new CommandParameter() {ParameterName = "pBYR_ACC_NAME_EN", Value =pBYR_ACC_NAME_EN},
                     new CommandParameter() {ParameterName = "pMC_ORDER_NO_LST", Value =pMC_ORDER_NO_LST},
                     new CommandParameter() {ParameterName = "pYR_COUNT_NO", Value =pYR_COUNT_NO},
                     new CommandParameter() {ParameterName = "pEVENT_NAME", Value =pEVENT_NAME},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3000:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PERMISSION"]);

                    if (dr["ACTN_ROLE_FLAG"] != DBNull.Value)
                        ob.ACTN_ROLE_FLAG = Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.TTL_ISU_QTY = (dr["TTL_ISU_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISU_QTY"]);
                    ob.TTL_REQ_QTY = (dr["TTL_REQ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_REQ_QTY"]);

                    ob.BALANCE_QTY = ob.TTL_REQ_QTY - ob.TTL_ISU_QTY;
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);

                    if (dr["ITEM_NAME_EN"] != DBNull.Value)
                        ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    if (dr["YR_COUNT_NO"] != DBNull.Value)
                        ob.YR_COUNT_NO = (dr["YR_COUNT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YR_COUNT_NO"]);
                    
                    if (dr["BUYER_NAME_EN"] != DBNull.Value)
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


        public List<KNT_YRN_STR_REQ_HModel> GetCollarCuffScoReqList(Int64 pKNT_SCO_CLCF_PRG_H_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SCO_CLCF_PRG_H_ID", Value = pKNT_SCO_CLCF_PRG_H_ID},
                                          
                     new CommandParameter() {ParameterName = "pOption", Value = 3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.TTL_REQ_QTY = (dr["TTL_REQ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_REQ_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_HModel> GetYrnReqList4YD(Int64 pKNT_YD_PRG_H_ID)
        {
            string sp = "pkg_knt_yd_prg.knt_yd_prg_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YD_PRG_H_ID", Value = pKNT_YD_PRG_H_ID},
                                          
                     new CommandParameter() {ParameterName = "pOption", Value = 3019},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);

                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.KNT_SCO_CLCF_PRG_H_ID = (dr["KNT_SCO_CLCF_PRG_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SCO_CLCF_PRG_H_ID"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);

                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.TTL_REQ_QTY = (dr["TTL_REQ_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_REQ_QTY"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RemoveRequisition4YD()
        {
            const string sp = "pkg_knt_yd_prg.remove_store_requisition";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
                     
                     //new CommandParameter() {ParameterName = "pOption", Value =4000},
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

        public List<KNT_YRN_STR_REQ_HModel> PendingRequisiotion(Int64? pRF_REQ_TYPE_ID = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.PRG_ISS_NO = (dr["PRG_ISS_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PRG_ISS_NO"]);

                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);

                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_STR_REQ_HModel Select(Int64? ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var ob = new KNT_YRN_STR_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_HModel> GetYarnStoreReq(Int64 pKNT_YRN_STR_REQ_H_ID, Int32? pOption = null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_jc_item_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value =pKNT_YRN_STR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3001:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_JOB_CRD_H_ID = (dr["KNT_JOB_CRD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_JOB_CRD_H_ID"]);
                    ob.YARN_ITEM_ID = (dr["YARN_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["YARN_ITEM_ID"]);
                    ob.KNT_YRN_LOT_ID = (dr["KNT_YRN_LOT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_LOT_ID"]);
                    ob.LK_YRN_COLR_GRP_ID = (dr["LK_YRN_COLR_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_YRN_COLR_GRP_ID"]);
                    ob.RF_BRAND_ID = (dr["RF_BRAND_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_BRAND_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.KNT_YDP_D_COL_ID = (dr["KNT_YDP_D_COL_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YDP_D_COL_ID"]);
                    
                    ob.ITEM_NAME_EN = (dr["ITEM_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_NAME_EN"]);

                    ob.BRAND_NAME_EN = (dr["BRAND_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BRAND_NAME_EN"]);
                    ob.YRN_LOT_NO = (dr["YRN_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_LOT_NO"]);
                    ob.YRN_COLR_GRP = (dr["YRN_COLR_GRP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["YRN_COLR_GRP"]);
                    ob.RQD_YRN_QTY = (dr["RQD_YRN_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RQD_YRN_QTY"]);
                    ob.REQ_QTY = ob.RQD_YRN_QTY;
                    ob.STOCK_QTY = (dr["STOCK_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STOCK_QTY"]);
                    ob.PACK_MOU_ID = (dr["PACK_MOU_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PACK_MOU_ID"]);
                    ob.QTY_PER_PACK = (dr["QTY_PER_PACK"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["QTY_PER_PACK"]);
                    ob.PRE_ISS_QTY = (dr["PRE_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PRE_ISS_QTY"]);
                    ob.RQD_CONE_QTY = (dr["RQD_CONE_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_CONE_QTY"]);
                    ob.ADV_ISS_QTY = (dr["ADV_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ADV_ISS_QTY"]);

                    ob.ORDER_INFO = (dr["ORDER_INFO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_INFO"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_HModel> GetYarnTestReq(long? pRF_REQ_TYPE_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_HModel> GetYarnIssueReq(long? pRF_REQ_TYPE_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_str_req_h_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.KNT_SC_PRG_ISS_ID = (dr["KNT_SC_PRG_ISS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SC_PRG_ISS_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.LK_REQ_STATUS_ID = (dr["LK_REQ_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_REQ_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ORDER_LIST = (dr["ORDER_LIST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_LIST"]);

                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<KNT_YRN_STR_REQ_HModel> getRequisitionDataByJobCardH(long? pKNT_JOB_CRD_H_ID, Int64? pKNT_PLAN_H_ID)
        {
            string sp = "pkg_knit_plan.knt_job_crd_d_select";
            try
            {
                var obList = new List<KNT_YRN_STR_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_JOB_CRD_H_ID", Value =  pKNT_JOB_CRD_H_ID},
                     new CommandParameter() {ParameterName = "pKNT_PLAN_H_ID", Value =  pKNT_PLAN_H_ID },
                     new CommandParameter() {ParameterName = "pOption", Value =3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_STR_REQ_HModel ob = new KNT_YRN_STR_REQ_HModel();
                    ob.KNT_YRN_STR_REQ_H_ID = (dr["KNT_YRN_STR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_STR_REQ_H_ID"]);

                    ob.KNT_YRN_ISS_H_ID = (dr["KNT_YRN_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_H_ID"]);
                    ob.ISS_CHALAN_NO = (dr["ISS_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_CHALAN_NO"]);

                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.UNASSIGN_TYPE = "I";


                    
                    ob.yarn_list = new List<KNT_YRN_STR_REQ_DModel>();
                    ob.yarn_list = new KNT_YRN_STR_REQ_DModel().getRequisitionYarnDetails(pKNT_JOB_CRD_H_ID, ob.KNT_YRN_ISS_H_ID, ob.KNT_YRN_STR_REQ_H_ID, pKNT_PLAN_H_ID);

                    ob.TTL_RQD_YRN_QTY_JC = ob.yarn_list.Sum(x => x.RQD_YRN_QTY_JC);
                    ob.TTL_REQ_DN_YRN_QTY_JC = ob.yarn_list.Sum(x => x.REQ_DN_YRN_QTY_JC);
                    ob.TTL_ISSUE_QTY_JC = ob.yarn_list.Sum(x => x.ISSUE_QTY_JC);
                    ob.TTL_ISSUE_RET_QTY = ob.yarn_list.Sum(x => x.ISSUE_RET_QTY);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 LK_REQ_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public Int64 PACK_MOU_ID { get; set; }
        public decimal QTY_PER_PACK { get; set; }
        public decimal PRE_ISS_QTY { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }

        public string ORDER_INFO { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public decimal RQD_CONE_QTY { get; set; }



        public int PERMISSION { get; set; }

        public decimal TTL_ISU_QTY { get; set; }

        public decimal TTL_REQ_QTY { get; set; }

        public decimal BALANCE_QTY { get; set; }

        public long SCM_SUPPLIER_ID { get; set; }

        public string PRG_ISS_NO { get; set; }

        public string SUP_TRD_NAME_EN { get; set; }

        public string ORDER_LIST { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string STYLE_NO { get; set; }

        public Int64? MC_BYR_ACC_GRP_ID { get; set; }

        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }

        public string ORDER_NO { get; set; }

        public string BUYER_NAME_EN { get; set; }

        public string YR_COUNT_NO { get; set; }



        public decimal TTL_RQD_YRN_QTY_JC { get; set; }

        public decimal TTL_REQ_DN_YRN_QTY_JC { get; set; }

        public decimal TTL_ISSUE_QTY_JC { get; set; }

        public string ISS_CHALAN_NO { get; set; }

        public decimal TTL_ISSUE_RET_QTY { get; set; }

        public string UNASSIGN_TYPE { get; set; }

        public string deleteStoreReq()
        {
            const string sp = "pkg_knit_plan.delete_store_req";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                    
                     new CommandParameter() {ParameterName = "pKNT_YRN_STR_REQ_H_ID", Value = ob.KNT_YRN_STR_REQ_H_ID},
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

        public long KNT_YDP_D_COL_ID { get; set; }

        public decimal ADV_ISS_QTY { get; set; }
    }
}