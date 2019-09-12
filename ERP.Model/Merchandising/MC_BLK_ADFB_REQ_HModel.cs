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
    public class MC_BLK_ADFB_REQ_HModel
    {
        public Int64 MC_BLK_ADFB_REQ_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 MC_BLK_FAB_REQ_H_ID { get; set; }
        public string AFAB_REQ_NO { get; set; }
        public string AFAB_REF_REQ_NO { get; set; }
        public DateTime AFAB_REQ_DT { get; set; }
        public Int64 AFAB_REQ_BY { get; set; }
        public DateTime? LAST_REV_DT { get; set; }
        public Int64? LAST_REV_NO { get; set; }
        public string REV_REASON { get; set; }
        public string REMARKS { get; set; }
        public string IS_ACTIVE { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string BLK_ADFB_DTL_XML { get; set; }
        public string BLK_ADFB_CLCF_DTL_XML { get; set; }


        public string MC_ORDER_H_ID_LST { get; set; }
        public Int64? MC_FAB_PROD_ORD_H_ID { get; set; }
        public Int64? MC_STYLE_H_EXT_ID { get; set; }
        public Int64? MC_STYLE_H_ID { get; set; }
        public Int64? MC_BYR_ACC_ID { get; set; }
        public string BYR_ACC_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string WORK_STYLE_NO_LST { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public string AFAB_REQ_BY_NAME { get; set; }


        public string BatchSave()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = ob.MC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_NO", Value = ob.AFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_DT", Value = ob.AFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_BY", Value = ob.AFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_DTL_XML", Value = ob.BLK_ADFB_DTL_XML},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_CLCF_DTL_XML", Value = ob.BLK_ADFB_CLCF_DTL_XML},
                     
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

        public string SubmitAddFabBk()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = ob.MC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_NO", Value = ob.AFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_DT", Value = ob.AFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_BY", Value = ob.AFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_DTL_XML", Value = ob.BLK_ADFB_DTL_XML},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_CLCF_DTL_XML", Value = ob.BLK_ADFB_CLCF_DTL_XML},
                     
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

        public string ApproveAddFabBk()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = ob.MC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pMC_BLK_FAB_REQ_H_ID", Value = ob.MC_BLK_FAB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROD_ORD_H_ID", Value = ob.MC_FAB_PROD_ORD_H_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = ob.MC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pMC_ORDER_H_ID_LST", Value = ob.MC_ORDER_H_ID_LST},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_NO", Value = ob.AFAB_REQ_NO},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_DT", Value = ob.AFAB_REQ_DT},
                     new CommandParameter() {ParameterName = "pAFAB_REQ_BY", Value = ob.AFAB_REQ_BY},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pLAST_REV_NO", Value = ob.LAST_REV_NO},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_DTL_XML", Value = ob.BLK_ADFB_DTL_XML},
                     new CommandParameter() {ParameterName = "pBLK_ADFB_CLCF_DTL_XML", Value = ob.BLK_ADFB_CLCF_DTL_XML},
                     
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                   
                     new CommandParameter() {ParameterName = "pOption", Value =1002},
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

        public string ReviseAddFabBk()
        {
            const string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_batch_save";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = ob.MC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pLAST_REV_DT", Value = ob.LAST_REV_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                                         
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                   
                     new CommandParameter() {ParameterName = "pOption", Value =1003},
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

        public RF_PAGERModel GetAddFabBkingList(Int64 pageNumber, Int64 pageSize, Int64? pMC_BYR_ACC_ID, Int64? pMC_STYLE_H_ID, Int64? pMC_STYLE_H_EXT_ID, string pSTYLE_ORDER_NO = null)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";
            try
            {
                Int64 vTotalRec = 0;
                var obList = new List<MC_BLK_ADFB_REQ_HModel>();
                var obj = new RF_PAGERModel();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNumber", Value = pageNumber},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize}, 
                     new CommandParameter() {ParameterName = "pMC_BYR_ACC_ID", Value = pMC_BYR_ACC_ID},
                     new CommandParameter() {ParameterName = "pMC_STYLE_H_EXT_ID", Value = pMC_STYLE_H_EXT_ID},
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},

                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    MC_BLK_ADFB_REQ_HModel ob = new MC_BLK_ADFB_REQ_HModel();
                    ob.MC_BLK_ADFB_REQ_H_ID = (dr["MC_BLK_ADFB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_H_ID"]);
                    if (dr["PARENT_ID"] != DBNull.Value)
                        ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.AFAB_REQ_NO = (dr["AFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REQ_NO"]);
                    ob.AFAB_REF_REQ_NO = (dr["AFAB_REF_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REF_REQ_NO"]);
                    ob.AFAB_REQ_DT = (dr["AFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AFAB_REQ_DT"]);
                    ob.AFAB_REQ_BY = (dr["AFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AFAB_REQ_BY"]);
                    if (dr["LAST_REV_NO"] != DBNull.Value)
                    {
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.AFAB_REQ_BY_NAME = (dr["AFAB_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REQ_BY_NAME"]);

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

        public MC_BLK_ADFB_REQ_HModel GetAddFabBkingHdr(long pMC_BLK_ADFB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";
            try
            {
                var ob = new MC_BLK_ADFB_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value =pMC_BLK_ADFB_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.MC_BLK_ADFB_REQ_H_ID = (dr["MC_BLK_ADFB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_ADFB_REQ_H_ID"]);
                    if (dr["PARENT_ID"] != DBNull.Value)
                        ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);

                    ob.MC_BYR_ACC_ID = (dr["MC_BYR_ACC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BYR_ACC_ID"]);
                    ob.MC_STYLE_H_ID = (dr["MC_STYLE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_ID"]);
                    ob.MC_STYLE_H_EXT_ID = (dr["MC_STYLE_H_EXT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_STYLE_H_EXT_ID"]);

                    ob.MC_BLK_FAB_REQ_H_ID = (dr["MC_BLK_FAB_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_BLK_FAB_REQ_H_ID"]);
                    ob.AFAB_REQ_NO = (dr["AFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REQ_NO"]);
                    ob.AFAB_REQ_DT = (dr["AFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AFAB_REQ_DT"]);
                    ob.AFAB_REQ_BY = (dr["AFAB_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["AFAB_REQ_BY"]);

                    if (dr["LAST_REV_NO"] != DBNull.Value)
                    {
                        ob.LAST_REV_DT = (dr["LAST_REV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_REV_DT"]);
                        ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_REV_NO"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    }

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_ACTIVE"]); 
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.BYR_ACC_NAME_EN = (dr["BYR_ACC_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.WORK_STYLE_NO_LST = (dr["WORK_STYLE_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["WORK_STYLE_NO_LST"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.AFAB_REQ_BY_NAME = (dr["AFAB_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REQ_BY_NAME"]);
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
    }





    public class MC_BLK_ADFB_REQ_USR_LAVELModel
    {
        public string USER_DRAFTER_NAME { get; set; }
        public string USER_APPROVER_NAME { get; set; }


        public MC_BLK_ADFB_REQ_USR_LAVELModel GetAddFabBkUserLavel()
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";// "pkg_knt_yd_prg.knt_yd_pl_adj_h_select";
            try
            {
                var ob = new MC_BLK_ADFB_REQ_USR_LAVELModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {                     
                     new CommandParameter() {ParameterName = "pSC_USER_ID", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3004},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.USER_DRAFTER_NAME = (dr["USER_DRAFTER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_DRAFTER_NAME"]);
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






    public class MC_BLK_ADFB_REQ_RPT_VM
    {
        public List<MC_BLK_ADFB_REQ_RPTModel> finFabList { get; set; }
        public List<MC_BLK_ADFB_REQ_RPTModel> greyFabList { get; set; }
        public List<MC_BLK_ADFB_REQ_RPT_CLCFModel> clcfList { get; set; }    
    }

    public class MC_BLK_ADFB_REQ_RPTModel
    {
        public string COMP_NAME_EN { get; set; }
        public string ADDRESS_EN { get; set; }
        public string AFAB_REQ_NO { get; set; }
        public DateTime AFAB_REQ_DT { get; set; }
        public long RF_ACTN_STATUS_ID { get; set; }
        public Int32? LAST_REV_NO { get; set; }
        public string REV_REASON { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public string REMARKS { get; set; }
        public string MAIL_ADD_LST { get; set; }

        public string QTY_MOU_CODE { get; set; }
        public string ROW_NAME { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME { get; set; }
        public Decimal? ROW_TOT_RQD_FFAB_QTY { get; set; }
        public Decimal? ROW_TOT_RQD_GFAB_QTY { get; set; }
        private List<MC_BLK_ADFB_REQ_RPT_FABDModel> _itemsDiaList = null;
        public List<MC_BLK_ADFB_REQ_RPT_FABDModel> itemsDiaList
        {
            get
            {
                if (_itemsDiaList == null)
                {
                    _itemsDiaList = new List<MC_BLK_ADFB_REQ_RPT_FABDModel>();
                }
                return _itemsDiaList;
            }
            set
            {
                _itemsDiaList = value;
            }
        }

        //public List<MC_BLK_ADFB_REQ_RPT_FABDModel> itemsDiaList { get; set; }
        //public string FIN_DIA { get; set; }
        //public string FAB_TYPE_SNAME { get; set; }
        //public Int64 RQD_FAB_QTY { get; set; }


        public MC_BLK_ADFB_REQ_RPT_VM GetAddFabBkRpt(Int64 pMC_BLK_ADFB_REQ_H_ID)
        {
            string sp = "pkg_mc_fab_booking.mc_blk_adfb_req_h_select";
            try
            {
                var obList = new List<MC_BLK_ADFB_REQ_RPTModel>();

                var distictData = new List<MC_BLK_ADFB_REQ_RPT_FABDModel>();
                var dataList = new List<MC_BLK_ADFB_REQ_RPT_FABDModel>();

                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {               
                     new CommandParameter() {ParameterName = "pMC_BLK_ADFB_REQ_H_ID", Value = pMC_BLK_ADFB_REQ_H_ID},

                     new CommandParameter() {ParameterName = "pOption", Value = 3005},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                var obClcfList = new List<MC_BLK_ADFB_REQ_RPT_CLCFModel>();
                var dataListClcf = new List<MC_BLK_ADFB_REQ_RPT_CLCFModel>();
                

                //MC_BLK_ADFB_REQ_RPT_CLCFModel obClcf = new MC_BLK_ADFB_REQ_RPT_CLCFModel();
                foreach (DataRow dr1 in ds.Tables[1].Rows)
                {
                    MC_BLK_ADFB_REQ_RPT_CLCFModel ob1 = new MC_BLK_ADFB_REQ_RPT_CLCFModel();

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
                    MC_BLK_ADFB_REQ_RPT_FABDModel ob = new MC_BLK_ADFB_REQ_RPT_FABDModel();

                    ob.EMP_FULL_NAME_EN = (dr["EMP_FULL_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EMP_FULL_NAME_EN"]);
                    ob.DESIGNATION_NAME_EN = (dr["DESIGNATION_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DESIGNATION_NAME_EN"]);
                    ob.CORE_DEPARTMENT_NAME = (dr["CORE_DEPARTMENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CORE_DEPARTMENT_NAME"]);

                    ob.AFAB_REQ_NO = (dr["AFAB_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AFAB_REQ_NO"]);
                    ob.AFAB_REQ_DT = (dr["AFAB_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["AFAB_REQ_DT"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);

                    ob.LAST_REV_NO = (dr["LAST_REV_NO"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["LAST_REV_NO"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    
                    ob.BYR_ACC_GRP_NAME_EN = (dr["BYR_ACC_GRP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BYR_ACC_GRP_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.TOT_ORD_QTY = (dr["TOT_ORD_QTY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TOT_ORD_QTY"]);

                    ob.ROW_NAME = (dr["ROW_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ROW_NAME"]);

                    ob.MC_COLOR_ID = (dr["MC_COLOR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_COLOR_ID"]);
                    ob.COLOR_NAME_EN = (dr["COLOR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SP_INSTRUCTION = (dr["SP_INSTRUCTION"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SP_INSTRUCTION"]);
                    ob.DIA_INDEX = (dr["DIA_INDEX"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DIA_INDEX"]);
                    ob.FIN_DIA = (dr["FIN_DIA"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FIN_DIA"]);
                    ob.FAB_TYPE_SNAME = (dr["FAB_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_TYPE_SNAME"]);

                    ob.RQD_FFAB_QTY = (dr["RQD_FFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_FFAB_QTY"]);
                    ob.RQD_GFAB_QTY = (dr["RQD_GFAB_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_GFAB_QTY"]);

                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.MAIL_ADD_LST = (dr["MAIL_ADD_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MAIL_ADD_LST"]);
                    ob.QTY_MOU_CODE = (dr["QTY_MOU_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QTY_MOU_CODE"]);

                    dataList.Add(ob);
                }

                var list = dataList.Distinct(new AddBkFabItmComparare(a => a.ROW_NAME));


                foreach (var x in list)
                {
                    MC_BLK_ADFB_REQ_RPTModel ob = new MC_BLK_ADFB_REQ_RPTModel();

                    //ob.COMP_NAME_EN = HttpContext.Current.Session["multiCurrCompanyNameE"].ToString();
                    //ob.ADDRESS_EN = HttpContext.Current.Session["multiCurrCompanyAddressE"].ToString();

                    ob.EMP_FULL_NAME_EN = x.EMP_FULL_NAME_EN;
                    ob.DESIGNATION_NAME_EN = x.DESIGNATION_NAME_EN;
                    ob.CORE_DEPARTMENT_NAME = x.CORE_DEPARTMENT_NAME;

                    ob.AFAB_REQ_NO = x.AFAB_REQ_NO;
                    ob.AFAB_REQ_DT = x.AFAB_REQ_DT;
                    ob.RF_ACTN_STATUS_ID = x.RF_ACTN_STATUS_ID;
                    ob.LAST_REV_NO = x.LAST_REV_NO;
                    ob.REV_REASON = x.REV_REASON;
                    ob.BYR_ACC_GRP_NAME_EN = x.BYR_ACC_GRP_NAME_EN;
                    ob.STYLE_NO = x.STYLE_NO;
                    ob.MC_ORDER_NO_LST = x.MC_ORDER_NO_LST;
                    ob.TOT_ORD_QTY = x.TOT_ORD_QTY;
                    ob.REMARKS = x.REMARKS;
                    ob.MAIL_ADD_LST = x.MAIL_ADD_LST;

                    ob.QTY_MOU_CODE = x.QTY_MOU_CODE;

                    ob.ROW_NAME = x.ROW_NAME;

                    ob.MC_COLOR_ID = x.MC_COLOR_ID;
                    ob.COLOR_NAME_EN = x.COLOR_NAME_EN; // == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    ob.SP_INSTRUCTION = x.SP_INSTRUCTION;
                    var dia = dataList.Distinct(new AddBkFabItmComparare(a => a.DIA_INDEX)); //dataList.ToList(); //dataList.Where(p => p.MC_COLOR_ID == x.MC_COLOR_ID).ToList();

                    foreach (var itmDia in dia)
                    {
                        MC_BLK_ADFB_REQ_RPT_FABDModel obDia = new MC_BLK_ADFB_REQ_RPT_FABDModel();

                        obDia.AFAB_REQ_NO = itmDia.AFAB_REQ_NO;
                        obDia.AFAB_REQ_DT = itmDia.AFAB_REQ_DT;
                        obDia.RF_ACTN_STATUS_ID = itmDia.RF_ACTN_STATUS_ID;
                        obDia.LAST_REV_NO = itmDia.LAST_REV_NO;
                        obDia.REV_REASON = itmDia.REV_REASON;

                        obDia.BYR_ACC_GRP_NAME_EN = itmDia.BYR_ACC_GRP_NAME_EN;
                        obDia.STYLE_NO = itmDia.STYLE_NO;
                        obDia.MC_ORDER_NO_LST = itmDia.MC_ORDER_NO_LST;
                        obDia.TOT_ORD_QTY = itmDia.TOT_ORD_QTY;

                        obDia.ROW_NAME = itmDia.ROW_NAME;

                        obDia.MC_COLOR_ID = itmDia.MC_COLOR_ID;
                        obDia.COLOR_NAME_EN = itmDia.COLOR_NAME_EN;
                        obDia.SP_INSTRUCTION = itmDia.SP_INSTRUCTION;
                        obDia.DIA_INDEX = itmDia.DIA_INDEX;
                        obDia.FIN_DIA = itmDia.FIN_DIA;
                        obDia.FAB_TYPE_SNAME = itmDia.FAB_TYPE_SNAME;
                        
                        obDia.RQD_FFAB_QTY = 0;
                        obDia.RQD_GFAB_QTY = 0;

                        obDia.QTY_MOU_CODE = itmDia.QTY_MOU_CODE;

                        obDia.REMARKS = itmDia.REMARKS;

                        var diaItem = dataList.Where(p => p.ROW_NAME == x.ROW_NAME && p.DIA_INDEX == itmDia.DIA_INDEX).ToList();
                        foreach (var i in diaItem)
                        {
                            obDia.RQD_FFAB_QTY = i.RQD_FFAB_QTY;
                            obDia.RQD_GFAB_QTY = i.RQD_GFAB_QTY;
                        }

                        ob.itemsDiaList.Add(obDia);
                    }

                    ob.ROW_TOT_RQD_FFAB_QTY = ob.itemsDiaList.Sum(p => p.RQD_FFAB_QTY);
                    ob.ROW_TOT_RQD_GFAB_QTY = ob.itemsDiaList.Sum(p => p.RQD_GFAB_QTY);

                    obList.Add(ob);
                }



                var clcfList = dataListClcf.Distinct(new AddBkColorComparare(a => a.MC_COLOR_ID));
                foreach (var x in clcfList)
                {
                    MC_BLK_ADFB_REQ_RPT_CLCFModel ob = new MC_BLK_ADFB_REQ_RPT_CLCFModel();

                    ob.MESUREMENT_INDEX = x.MESUREMENT_INDEX;
                    ob.MESUREMENT = x.MESUREMENT;
                    ob.RQD_PC_QTY = x.RQD_PC_QTY;

                    ob.MC_COLOR_ID = x.MC_COLOR_ID;
                    ob.COLOR_NAME_EN = x.COLOR_NAME_EN; // == DBNull.Value) ? string.Empty : Convert.ToString(dr["COLOR_NAME_EN"]);
                    var meas = dataListClcf.Where(p => p.MC_COLOR_ID == x.MC_COLOR_ID).ToList();
                    ob.itemsDtl = (List<MC_BLK_ADFB_REQ_RPT_CLCFModel>)meas;

                    ob.ROW_TOT_RQD_PC_QTY = meas.Sum(p => p.RQD_PC_QTY);

                    obClcfList.Add(ob);
                }

               
                MC_BLK_ADFB_REQ_RPT_VM ObRpt = new MC_BLK_ADFB_REQ_RPT_VM();
                ObRpt.finFabList = obList;
                ObRpt.greyFabList = obList;
                ObRpt.clcfList = obClcfList;
                

                return ObRpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        
    }

    class AddBkFabItmComparare : IEqualityComparer<MC_BLK_ADFB_REQ_RPT_FABDModel>
    {
        private Func<MC_BLK_ADFB_REQ_RPT_FABDModel, object> _funcDistinct;
        public AddBkFabItmComparare(Func<MC_BLK_ADFB_REQ_RPT_FABDModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }

        public bool Equals(MC_BLK_ADFB_REQ_RPT_FABDModel x, MC_BLK_ADFB_REQ_RPT_FABDModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }

        public int GetHashCode(MC_BLK_ADFB_REQ_RPT_FABDModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }

    public class MC_BLK_ADFB_REQ_RPT_FABDModel
    {
        public string AFAB_REQ_NO { get; set; }
        public DateTime AFAB_REQ_DT { get; set; }
        public string BYR_ACC_GRP_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public Int64? TOT_ORD_QTY { get; set; }
        public string REMARKS { get; set; }
        
        public string MAIL_ADD_LST { get; set; }

        public string ROW_NAME { get; set; }
        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public string SP_INSTRUCTION { get; set; }
        public Int64 DIA_INDEX { get; set; }
        public string FIN_DIA { get; set; }
        public string FAB_TYPE_SNAME { get; set; }
        public Decimal RQD_FFAB_QTY { get; set; }
        public Decimal RQD_GFAB_QTY { get; set; }
        public string QTY_MOU_CODE { get; set; }
        public string EMP_FULL_NAME_EN { get; set; }
        public string DESIGNATION_NAME_EN { get; set; }
        public string CORE_DEPARTMENT_NAME { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public Int32 LAST_REV_NO { get; set; }
        public string REV_REASON { get; set; }
    }

   

    public class MC_BLK_ADFB_REQ_RPT_CLCFModel
    {

        public Int64? MC_COLOR_ID { get; set; }
        public string COLOR_NAME_EN { get; set; }
        public Int64? MESUREMENT_INDEX { get; set; }
        public string MESUREMENT { get; set; }
        public Int64? RF_GARM_PART_ID { get; set; }
        public Int64? RQD_PC_QTY { get; set; }
        public Int64? ROW_TOT_RQD_PC_QTY { get; set; }


        private List<MC_BLK_ADFB_REQ_RPT_CLCFModel> _itemsDtl = null;
        public List<MC_BLK_ADFB_REQ_RPT_CLCFModel> itemsDtl
        {
            get
            {
                if (_itemsDtl == null)
                {
                    _itemsDtl = new List<MC_BLK_ADFB_REQ_RPT_CLCFModel>();
                }
                return _itemsDtl;
            }
            set
            {
                _itemsDtl = value;
            }
        }

    }

    class AddBkColorComparare : IEqualityComparer<MC_BLK_ADFB_REQ_RPT_CLCFModel>
    {
        private Func<MC_BLK_ADFB_REQ_RPT_CLCFModel, object> _funcDistinct;
        public AddBkColorComparare(Func<MC_BLK_ADFB_REQ_RPT_CLCFModel, object> funcDistinct)
        {
            this._funcDistinct = funcDistinct;
        }

        public bool Equals(MC_BLK_ADFB_REQ_RPT_CLCFModel x, MC_BLK_ADFB_REQ_RPT_CLCFModel y)
        {
            return _funcDistinct(x).Equals(_funcDistinct(y));
        }

        public int GetHashCode(MC_BLK_ADFB_REQ_RPT_CLCFModel obj)
        {
            return this._funcDistinct(obj).GetHashCode();
        }
    }


}