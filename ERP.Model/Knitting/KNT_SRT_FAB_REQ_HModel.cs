using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
//using System.Web.Hosting;
//using System.IO;
//using System.Web.Mvc;
//using Postal;

namespace ERP.Model
{
    public class KNT_SRT_FAB_REQ_HModel
    {
        public Int64 KNT_SRT_FAB_REQ_H_ID { get; set; }
        public Int64 MC_FAB_PROD_ORD_H_ID { get; set; }
        public string SFAB_REQ_NO { get; set; }
        public DateTime SFAB_REQ_DT { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public Int64 SFAB_REQ_BY { get; set; }
        public string SFAB_REQ_ATTN { get; set; }
        public string[] SFAB_REQ_ATTN_LIST { get; set; }
        public string REMARKS { get; set; }
        public DateTime? LAST_REV_DT { get; set; }
        public Int64? LAST_REV_NO { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }


        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MC_BYR_ACC_GRP_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public string MC_ORDER_H_ID_LST { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string SFAB_REQ_BY_NAME { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }

        public string FAB_REQ_D1_XML { get; set; }
        public string FAB_REQ_D11_XML { get; set; }
        public string FAB_REQ_D2_XML { get; set; }
        public string FAB_REQ_D3_XML { get; set; }

        public string USER_DRAFT_NAME { get; set; }
        public string USER_VERIFIER_NAME { get; set; }
        public string USER_APPROVER_NAME { get; set; }
        public string USER_CORR_AFTER_APPROVE { get; set; }
        
        


        public string Save()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_NO", Value = ob.SFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_DT", Value = ob.SFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_BY", Value = ob.SFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_ATTN", Value = ob.SFAB_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pFAB_REQ_D1_XML", Value = ob.FAB_REQ_D1_XML},
                     new CommandParameter() {ParameterName = "pFAB_REQ_D11_XML", Value = ob.FAB_REQ_D11_XML},
                     new CommandParameter() {ParameterName = "pFAB_REQ_D2_XML", Value = ob.FAB_REQ_D2_XML},
                     new CommandParameter() {ParameterName = "pFAB_REQ_D3_XML", Value = ob.FAB_REQ_D3_XML},

                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1000},
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

        public string SubmitSrtFabProdOrdH()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_NO", Value = ob.SFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_DT", Value = ob.SFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_BY", Value = ob.SFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_ATTN", Value = ob.SFAB_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1004},
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

        public string ApproveSrtFabProdOrdH()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_NO", Value = ob.SFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_DT", Value = ob.SFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_BY", Value = ob.SFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_ATTN", Value = ob.SFAB_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1005},
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
        
        public string RevisionSrtFabProdOrdH()
        {
            const string sp = "pkg_fab_prod_order.knt_srt_fab_prod_ord_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = ob.KNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_NO", Value = ob.SFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_DT", Value = ob.SFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_BY", Value = ob.SFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_ATTN", Value = ob.SFAB_REQ_ATTN},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                    
                     new CommandParameter() {ParameterName = "pOption", Value = 1006},
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

        public object GetSrtFabBookingList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SRT_FAB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize}, 
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value = 3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_HModel ob = new KNT_SRT_FAB_REQ_HModel();
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SFAB_REQ_NO = (dr["SFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_NO"]);
                    ob.SFAB_REQ_DT = (dr["SFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SFAB_REQ_DT"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SFAB_REQ_BY = (dr["SFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SFAB_REQ_BY"]);
                    ob.SFAB_REQ_ATTN = (dr["SFAB_REQ_ATTN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_ATTN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    if (dr["LAST_REV_DT"] != DBNull.Value)
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                    if (dr["LAST_REV_NO"] != DBNull.Value)
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_APPROVER_NAME = (dr["USER_APPROVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_APPROVER_NAME"]);

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

        public object GetSrtFabBookingAprovList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_GRP_ID = null, Int64? pMC_STYLE_H_EXT_ID = null, string pSFAB_REQ_NO = null)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<KNT_SRT_FAB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize}, 
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_GRP_ID", Value = pMC_BYR_ACC_GRP_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pSFAB_REQ_NO", Value = pSFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value = 3008},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_HModel ob = new KNT_SRT_FAB_REQ_HModel();
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SFAB_REQ_NO = (dr["SFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_NO"]);
                    ob.SFAB_REQ_DT = (dr["SFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SFAB_REQ_DT"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SFAB_REQ_BY = (dr["SFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SFAB_REQ_BY"]);
                    ob.SFAB_REQ_ATTN = (dr["SFAB_REQ_ATTN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_ATTN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    if (dr["LAST_REV_DT"] != DBNull.Value)
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                    if (dr["LAST_REV_NO"] != DBNull.Value)
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_APPROVER_NAME = (dr["USER_APPROVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_APPROVER_NAME"]);
                    ob.USER_CORR_AFTER_APPROVE = (dr["USER_CORR_AFTER_APPROVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_CORR_AFTER_APPROVE"]);

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

        public KNT_SRT_FAB_REQ_HModel GetSrtFabProdOrdHdr(Int64 pKNT_SRT_FAB_REQ_H_ID)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = pKNT_SRT_FAB_REQ_H_ID},
                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_SRT_FAB_REQ_H_ID = (dr["KNT_SRT_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_SRT_FAB_REQ_H_ID"]);
                    ob.MC_FAB_PROD_ORD_H_ID = (dr["MC_FAB_PROD_ORD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROD_ORD_H_ID"]);
                    ob.SFAB_REQ_NO = (dr["SFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_NO"]);
                    ob.SFAB_REQ_DT = (dr["SFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SFAB_REQ_DT"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.SFAB_REQ_BY = (dr["SFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SFAB_REQ_BY"]);
                    ob.SFAB_REQ_ATTN = (dr["SFAB_REQ_ATTN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_ATTN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                    if (dr["LAST_REV_DT"] != DBNull.Value)
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                    if (dr["LAST_REV_NO"] != DBNull.Value)
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.MC_ORDER_H_ID_LST = (dr["MC_ORDER_H_ID_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_H_ID_LST"]);

                    if (ob.SFAB_REQ_ATTN != null)
                        ob.SFAB_REQ_ATTN_LIST = ob.SFAB_REQ_ATTN.Split(',');

                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_BYR_ACC_GRP_ID = (dr["MC_BYR_ACC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_GRP_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.SFAB_REQ_BY_NAME = (dr["SFAB_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_BY_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);

                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_APPROVER_NAME = (dr["USER_APPROVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_APPROVER_NAME"]);
                    ob.USER_CORR_AFTER_APPROVE = (dr["USER_CORR_AFTER_APPROVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_CORR_AFTER_APPROVE"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_SRT_FAB_REQ_HModel GetSrtFabReqUserLavel()
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var ob = new KNT_SRT_FAB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3006},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.USER_DRAFT_NAME = (dr["USER_DRAFT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DRAFT_NAME"]);
                    ob.USER_VERIFIER_NAME = (dr["USER_VERIFIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_VERIFIER_NAME"]);
                    ob.USER_APPROVER_NAME = (dr["USER_APPROVER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_APPROVER_NAME"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class KNT_SRT_FAB_REQ_RPT_VM
    {
        public List<KNT_SRT_FAB_REQ_RPTModel> fabList { get; set; }
        public List<KNT_SRT_FAB_REQ_RPT_CLCFModel> clcfList { get; set; }
        public List<KNT_SRT_FAB_REQ_RSN_RPT_FABDModel> rsnList { get; set; }
        public List<KNT_SRT_FAB_REQ_RESP_RPT_FABDModel> respList { get; set; }

        //var dataListClcf = new List<KNT_SRT_FAB_REQ_RPT_CLCFModel>();
        //var obRsnList = new List<KNT_SRT_FAB_REQ_RSN_RPT_FABDModel>();
        //var obRespList = new List<KNT_SRT_FAB_REQ_RESP_RPT_FABDModel>();
        //return new { fabList = obList, clcfList = obClcfList, rsnList = obRsnList, respList = obRespList };
    }

    public class KNT_SRT_FAB_REQ_RPTModel
    {
        public string COMP_NAME_EN { get; set; }
        public string ADDRESS_EN { get; set; }
        public string SFAB_REQ_NO { get; set; }
        public DateTime SFAB_REQ_DT { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public string REMARKS { get; set; }
        public string MAIL_ADD_LST { get; set; }

        public string QTY_MOU_CODE { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string DYE_LOT_NO { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME { get; set; }
        public Decimal? ROW_TOT_RQD_FAB_QTY { get; set; }
        private List<KNT_SRT_FAB_REQ_RPT_FABDModel> _itemsDiaList = null;
        public List<KNT_SRT_FAB_REQ_RPT_FABDModel> itemsDiaList
        {
            get
            {
                if (_itemsDiaList == null)
                {
                    _itemsDiaList = new List<KNT_SRT_FAB_REQ_RPT_FABDModel>();
                }
                return _itemsDiaList;
            }
            set
            {
                _itemsDiaList = value;
            }
        }

        //public List<KNT_SRT_FAB_REQ_RPT_FABDModel> itemsDiaList { get; set; }
        //public string FIN_DIA { get; set; }
        //public string FAB_TYPE_SNAME { get; set; }
        //public Int64 RQD_FAB_QTY { get; set; }


        public KNT_SRT_FAB_REQ_RPT_VM GetSrtFabReqRpt(Int64 pKNT_SRT_FAB_REQ_H_ID, string pIS_EMAIL_NOTIF = null)
        {
            string sp = "pkg_fab_prod_order.srt_fab_req_h_select";
            try
            {
                var obList = new List<KNT_SRT_FAB_REQ_RPTModel>();

                var distictData = new List<KNT_SRT_FAB_REQ_RPT_FABDModel>();
                var dataList = new List<KNT_SRT_FAB_REQ_RPT_FABDModel>();
                               
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {               
                     new CommandParameter() {ParameterName = "pKNT_SRT_FAB_REQ_H_ID", Value = pKNT_SRT_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pIS_EMAIL_NOTIF", Value = pIS_EMAIL_NOTIF},

                     new CommandParameter() {ParameterName = "pOption", Value = 3007},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                var obClcfList = new List<KNT_SRT_FAB_REQ_RPT_CLCFModel>();
                var dataListClcf = new List<KNT_SRT_FAB_REQ_RPT_CLCFModel>();
                var obRsnList = new List<KNT_SRT_FAB_REQ_RSN_RPT_FABDModel>();
                var obRespList = new List<KNT_SRT_FAB_REQ_RESP_RPT_FABDModel>();

                //KNT_SRT_FAB_REQ_RPT_CLCFModel obClcf = new KNT_SRT_FAB_REQ_RPT_CLCFModel();
                foreach (DataRow dr1 in ds.Tables[1].Rows)
                {
                    KNT_SRT_FAB_REQ_RPT_CLCFModel ob1 = new KNT_SRT_FAB_REQ_RPT_CLCFModel();

                    ob1.MC_COLOR_ID = (dr1["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MC_COLOR_ID"]);
                    ob1.COLOR_NAME_EN = (dr1["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["COLOR_NAME_EN"]);
                    ob1.MESUREMENT_INDEX = (dr1["MESUREMENT_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["MESUREMENT_INDEX"]);
                    ob1.MESUREMENT = (dr1["MESUREMENT"] == DBNull.Value) ? string.Empty : Convert.ToString(dr1["MESUREMENT"]);

                    ob1.RF_GARM_PART_ID = (dr1["RF_GARM_PART_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["RF_GARM_PART_ID"]);

                    ob1.RQD_PC_QTY = (dr1["RQD_PC_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr1["RQD_PC_QTY"]);

                    dataListClcf.Add(ob1);
                }

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_SRT_FAB_REQ_RPT_FABDModel ob = new KNT_SRT_FAB_REQ_RPT_FABDModel();

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.CORE_DEPARTMENT_NAME = (dr["CORE_DEPARTMENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CORE_DEPARTMENT_NAME"]);

                    ob.SFAB_REQ_NO = (dr["SFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_REQ_NO"]);
                    ob.SFAB_REQ_DT = (dr["SFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["SFAB_REQ_DT"]);

                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_LOT_NO = (dr["DYE_LOT_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_LOT_NO"]);
                    ob.DIA_INDEX = (dr["DIA_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_INDEX"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);
                    ob.RQD_FAB_QTY = (dr["RQD_FAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FAB_QTY"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MAIL_ADD_LST = (dr["MAIL_ADD_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADD_LST"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);
                    
                    dataList.Add(ob);
                }

                var list = dataList.Distinct(new ProductComparare(a => a.MC_COLOR_ID + a.DYE_LOT_NO));


                foreach (var x in list)
                {
                    KNT_SRT_FAB_REQ_RPTModel ob = new KNT_SRT_FAB_REQ_RPTModel();

                    ob.COMP_NAME_EN = HttpContext.Current.Session["multiCurrCompanyNameE"].ToString();
                    ob.ADDRESS_EN = HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString();

                    ob.EMP_FULL_NAME_EN = x.EMP_FULL_NAME_EN;
                    ob.DESIGNATION_NAME_EN = x.DESIGNATION_NAME_EN;
                    ob.CORE_DEPARTMENT_NAME = x.CORE_DEPARTMENT_NAME;

                    ob.SFAB_REQ_NO = x.SFAB_REQ_NO;
                    ob.SFAB_REQ_DT = x.SFAB_REQ_DT;
                    ob.BYR_ACC_GRP_NAME_EN = x.BYR_ACC_GRP_NAME_EN;
                    ob.STYLE_NO = x.STYLE_NO;
                    ob.MC_ORDER_NO_LST = x.MC_ORDER_NO_LST;
                    ob.TOT_ORD_QTY = x.TOT_ORD_QTY;
                    ob.REMARKS = x.REMARKS;
                    ob.MAIL_ADD_LST = x.MAIL_ADD_LST;

                    ob.QTY_MOU_CODE = x.QTY_MOU_CODE;

                    ob.MC_COLOR_ID = x.MC_COLOR_ID;
                    ob.COLOR_NAME_EN = x.COLOR_NAME_EN; // == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.DYE_LOT_NO = x.DYE_LOT_NO;
                    var dia = dataList.Distinct(new ProductComparare(a => a.DIA_INDEX)); //dataList.ToList(); //dataList.Where(p => p.MC_COLOR_ID == x.MC_COLOR_ID).ToList();
                    
                    foreach (var itmDia in dia)
                    {
                        KNT_SRT_FAB_REQ_RPT_FABDModel obDia = new KNT_SRT_FAB_REQ_RPT_FABDModel();

                        obDia.SFAB_REQ_NO = itmDia.SFAB_REQ_NO;
                        obDia.SFAB_REQ_DT = itmDia.SFAB_REQ_DT;
                        
                        obDia.BYR_ACC_GRP_NAME_EN = itmDia.BYR_ACC_GRP_NAME_EN;
                        obDia.STYLE_NO = itmDia.STYLE_NO;
                        obDia.MC_ORDER_NO_LST = itmDia.MC_ORDER_NO_LST;
                        obDia.TOT_ORD_QTY = itmDia.TOT_ORD_QTY;

                        obDia.MC_COLOR_ID = itmDia.MC_COLOR_ID;
                        obDia.COLOR_NAME_EN = itmDia.COLOR_NAME_EN;
                        obDia.DIA_INDEX = itmDia.DIA_INDEX;
                        obDia.FIN_DIA = itmDia.FIN_DIA;
                        obDia.FAB_TYPE_SNAME = itmDia.FAB_TYPE_SNAME;
                        obDia.RQD_FAB_QTY = 0;
                        obDia.QTY_MOU_CODE = itmDia.QTY_MOU_CODE;
                        
                        obDia.REMARKS = itmDia.REMARKS;

                        var diaItem = dataList.Where(p => p.MC_COLOR_ID == x.MC_COLOR_ID && p.DYE_LOT_NO == x.DYE_LOT_NO && p.DIA_INDEX == itmDia.DIA_INDEX).ToList();
                        foreach (var i in diaItem)
                        {
                            obDia.RQD_FAB_QTY = i.RQD_FAB_QTY;
                        }
                                       
                        ob.itemsDiaList.Add(obDia);                      
                    }                    
                   
                    ob.ROW_TOT_RQD_FAB_QTY = ob.itemsDiaList.Sum(p => p.RQD_FAB_QTY);                   

                    obList.Add(ob);
                }

                

                var clcfList = dataListClcf.Distinct(new ColorComparare(a => a.MC_COLOR_ID));
                foreach (var x in clcfList)
                {
                    KNT_SRT_FAB_REQ_RPT_CLCFModel ob = new KNT_SRT_FAB_REQ_RPT_CLCFModel();

                    ob.MESUREMENT_INDEX = x.MESUREMENT_INDEX;
                    ob.MESUREMENT = x.MESUREMENT;
                    ob.RQD_PC_QTY = x.RQD_PC_QTY;
                   
                    ob.MC_COLOR_ID = x.MC_COLOR_ID;
                    ob.COLOR_NAME_EN = x.COLOR_NAME_EN; // == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    var meas = dataListClcf.Where(p => p.MC_COLOR_ID == x.MC_COLOR_ID).ToList();
                    ob.itemsDtl = (List<KNT_SRT_FAB_REQ_RPT_CLCFModel>)meas;

                    ob.ROW_TOT_RQD_PC_QTY = meas.Sum(p => p.RQD_PC_QTY);

                    obClcfList.Add(ob);
                }


                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    KNT_SRT_FAB_REQ_RSN_RPT_FABDModel ob = new KNT_SRT_FAB_REQ_RSN_RPT_FABDModel();

                    ob.SFAB_RSN_TYPE_NAME = (dr["SFAB_RSN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SFAB_RSN_TYPE_NAME"]);
                    ob.REASON_DESC = (dr["REASON_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_DESC"]);
                    
                    obRsnList.Add(ob);
                }

                foreach (DataRow dr in ds.Tables[3].Rows)
                {
                    KNT_SRT_FAB_REQ_RESP_RPT_FABDModel ob = new KNT_SRT_FAB_REQ_RESP_RPT_FABDModel();

                    ob.RESP_DEPT_NAME = (dr["RESP_DEPT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_DEPT_NAME"]);
                    ob.PCT_DIST_QTY = (dr["PCT_DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PCT_DIST_QTY"]);
                    ob.DIST_QTY = (dr["DIST_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["DIST_QTY"]);

                    obRespList.Add(ob);
                }


                KNT_SRT_FAB_REQ_RPT_VM ObRpt = new KNT_SRT_FAB_REQ_RPT_VM();
                ObRpt.fabList = obList;
                ObRpt.clcfList = obClcfList;
                ObRpt.rsnList = obRsnList;
                ObRpt.respList = obRespList;

                return ObRpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }

    class ProductComparare : IEqualityComparer<KNT_SRT_FAB_REQ_RPT_FABDModel>
    {
        private Func<KNT_SRT_FAB_REQ_RPT_FABDModel, object> _funcDistinct;
        public ProductComparare(Func<KNT_SRT_FAB_REQ_RPT_FABDModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }
        
        public bool Equals(KNT_SRT_FAB_REQ_RPT_FABDModel x, KNT_SRT_FAB_REQ_RPT_FABDModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }
                
        public int GetHashCode(KNT_SRT_FAB_REQ_RPT_FABDModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }

    public class KNT_SRT_FAB_REQ_RPT_FABDModel
    {
        public string SFAB_REQ_NO { get; set; }
        public DateTime SFAB_REQ_DT { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public string REMARKS { get; set; }
        public string MAIL_ADD_LST { get; set; }

        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string DYE_LOT_NO { get; set; }
        public Int64 DIA_INDEX { get; set; }
        public string FIN_DIA { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public Decimal RQD_FAB_QTY { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME { get; set; }
    }

    public class KNT_SRT_FAB_REQ_RSN_RPT_FABDModel
    {
        public string SFAB_RSN_TYPE_NAME { get; set; }
        public string REASON_DESC { get; set; }        
    }

    public class KNT_SRT_FAB_REQ_RESP_RPT_FABDModel
    {
        public string RESP_DEPT_NAME { get; set; }
        public decimal PCT_DIST_QTY { get; set; }
        public decimal DIST_QTY { get; set; }
    }

    public class KNT_SRT_FAB_REQ_RPT_CLCFModel
    {
        
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64? MESUREMENT_INDEX { get; set; }
        public string MESUREMENT { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public Int64? RQD_PC_QTY { get; set; }
        public Int64? ROW_TOT_RQD_PC_QTY { get; set; }
        
        
        private List<KNT_SRT_FAB_REQ_RPT_CLCFModel> _itemsDtl = null;
        public List<KNT_SRT_FAB_REQ_RPT_CLCFModel> itemsDtl
        {
            get
            {
                if (_itemsDtl == null)
                {
                    _itemsDtl = new List<KNT_SRT_FAB_REQ_RPT_CLCFModel>();
                }
                return _itemsDtl;
            }
            set
            {
                _itemsDtl = value;
            }
        }
        
    }

    class ColorComparare : IEqualityComparer<KNT_SRT_FAB_REQ_RPT_CLCFModel>
    {
        private Func<KNT_SRT_FAB_REQ_RPT_CLCFModel, object> _funcDistinct;
        public ColorComparare(Func<KNT_SRT_FAB_REQ_RPT_CLCFModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }

        public bool Equals(KNT_SRT_FAB_REQ_RPT_CLCFModel x, KNT_SRT_FAB_REQ_RPT_CLCFModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }

        public int GetHashCode(KNT_SRT_FAB_REQ_RPT_CLCFModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }


    public class SrtFabAprovMailModel
    {
        public Int64 MC_ORD_TNA_ID { get; set; }
        public long MC_BYR_ACC_ID { get; set; }
        public Int64 MC_ORDER_H_ID { get; set; }
        public String PLAN_START_DT { get; set; }
        public String SHIP_DT { get; set; }
        public Int64? LAG_DAYS { get; set; }
        public string REMARKS { get; set; }
        public string TA_TASK_NAME_EN { get; set; }
        public string ORDER_NO { get; set; }
        public string WORK_STYLE { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }

        public Int32 BYR_ACC_SPAN { get; set; }
        public Int32 BYR_ACC_SL { get; set; }
        public Int32 STYL_EXT_SPAN { get; set; }
        public Int32 STYL_EXT_SL { get; set; }
        public Int32 ORDER_SPAN { get; set; }
        public Int32 ORDER_SL { get; set; }

        public Int32 SC_USER_ID { get; set; }
        public String MAIL_ADDRESS { get; set; }


        public List<TnASummaryMail> SelectAll(Int32 pSC_USER_ID)
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new List<TnASummaryMail>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = pSC_USER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TnASummaryMail ob = new TnASummaryMail();
                    ob.MC_ORD_TNA_ID = (dr["MC_ORD_TNA_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORD_TNA_ID"]);
                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_ORDER_H_ID = (dr["MC_ORDER_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_ORDER_H_ID"]);
                    ob.PLAN_START_DT = (dr["PLAN_START_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr["PLAN_START_DT"]).ToString("dd-MMM-yyyy");
                    ob.SHIP_DT = (dr["SHIP_DT"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr["SHIP_DT"]).ToString("dd-MMM-yyyy");


                    if (dr["LAG_DAYS"] != DBNull.Value)
                    {
                        ob.LAG_DAYS = Convert.ToInt64(dr["LAG_DAYS"]);
                    }
                    ob.TA_TASK_NAME_EN = (dr["TA_TASK_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TA_TASK_NAME_EN"]);
                    ob.WORK_STYLE = (dr["WORK_STYLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE"]);
                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.ORDER_NO = (dr["ORDER_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORDER_NO"]);
                    ob.BYR_ACC_SPAN = (dr["BYR_ACC_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["BYR_ACC_SPAN"]);
                    ob.BYR_ACC_SL = (dr["BYR_ACC_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["BYR_ACC_SL"]);
                    ob.STYL_EXT_SPAN = (dr["STYL_EXT_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYL_EXT_SPAN"]);
                    ob.STYL_EXT_SL = (dr["STYL_EXT_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["STYL_EXT_SL"]);
                    ob.ORDER_SPAN = (dr["ORDER_SPAN"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ORDER_SPAN"]);
                    ob.ORDER_SL = (dr["ORDER_SL"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["ORDER_SL"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TnASummaryMail> getUserAndMailAddress()
        {
            string sp = "pkg_tna.mc_order_tna_select_grid";
            try
            {
                var obList = new List<TnASummaryMail>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    TnASummaryMail ob = new TnASummaryMail();
                    ob.MAIL_ADDRESS = (dr["MAIL_ADDRESS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADDRESS"]);
                    ob.SC_USER_ID = (dr["SC_USER_ID"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["SC_USER_ID"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}