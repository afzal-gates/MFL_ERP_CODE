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
    public class DF_SC_PT_ISS_HModel
    {
        public Int64 DF_SC_PT_ISS_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64? DYE_GSTR_REQ_H_ID { get; set; }
        public string SC_PRG_NO { get; set; }
        public Int64 PRG_ISS_BY { get; set; }
        public Int64 PRG_ISS_TO { get; set; }
        public DateTime PRG_ISS_DT { get; set; }
        public string DF_PROC_TYPE_LST { get; set; }
        public DateTime EXP_DELV_DT { get; set; }
        public Int64? SCM_SUPPLIER_ID { get; set; }
        public Int64 ISS_STORE_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public string CHALAN_NO { get; set; }
        public DateTime? CHALAN_DT { get; set; }
        public string GATE_PASS_NO { get; set; }
        public string VEHICLE_NO { get; set; }
        public string CARRIER_NAME { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public Int64? LK_FAB_PRG_TYPE { get; set; }
                

        public Int64? REVISION_NO { get; set; }
        public Int64? DF_SC_PT_RCV_H_ID { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }


        public string COMP_NAME_EN { get; set; }
        public string SUP_TRD_NAME_EN { get; set; }
        public string STORE_NAME_EN { get; set; }
        public string SC_PRG_STS_NAME { get; set; }
        public string DYE_BATCH_NO { get; set; }
        public int TOTAL_REC { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public long PERMISSION { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public string XML_REQ_D { get; set; }
        public string XML_PROC_D { get; set; }
        public string XML_PROC_PRM { get; set; }
        public string IS_UPDATE { get; set; }
        public string IS_SC { get; set; }
        public string IS_BATCH { get; set; }

        public Int64? DYE_BT_CARD_H_ID { get; set; }
        public Int64? RF_FAB_PROD_CAT_ID { get; set; }


        public string Save()
        {
            const string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID==0?null:ob.DYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pDF_SC_PT_RCV_H_ID", Value = ob.DF_SC_PT_RCV_H_ID==0?null:ob.DF_SC_PT_RCV_H_ID},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value = ob.SC_PRG_NO},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pPRG_ISS_BY", Value = ob.PRG_ISS_BY},
                     new CommandParameter() {ParameterName = "pPRG_ISS_TO", Value = ob.PRG_ISS_TO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value = ob.PRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID==0?1:ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pIS_SC", Value = ob.IS_SC},
                     new CommandParameter() {ParameterName = "pIS_BATCH", Value = ob.IS_BATCH},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID==0?null:ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_PRG_TYPE", Value = ob.LK_FAB_PRG_TYPE==0?null:ob.LK_FAB_PRG_TYPE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
                     new CommandParameter() {ParameterName = "pXML_PROC_D", Value = ob.XML_PROC_D},
                     new CommandParameter() {ParameterName = "pXML_PROC_PRM", Value = ob.XML_PROC_PRM},
                     new CommandParameter() {ParameterName = "pIS_UPDATE", Value = ob.IS_UPDATE==null?"N":ob.IS_UPDATE},
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
            const string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value = ob.SC_PRG_NO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_BY", Value = ob.PRG_ISS_BY},
                     new CommandParameter() {ParameterName = "pPRG_ISS_TO", Value = ob.PRG_ISS_TO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value = ob.PRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pIS_SC", Value = ob.IS_SC},
                     new CommandParameter() {ParameterName = "pIS_BATCH", Value = ob.IS_BATCH},
                     new CommandParameter() {ParameterName = "pDYE_BT_CARD_H_ID", Value = ob.DYE_BT_CARD_H_ID==0?null:ob.DYE_BT_CARD_H_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_PRG_TYPE", Value = ob.LK_FAB_PRG_TYPE==0?null:ob.LK_FAB_PRG_TYPE},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
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
            const string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value = ob.DF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value = ob.DYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value = ob.SC_PRG_NO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_BY", Value = ob.PRG_ISS_BY},
                     new CommandParameter() {ParameterName = "pPRG_ISS_TO", Value = ob.PRG_ISS_TO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value = ob.PRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pDF_PROC_TYPE_LST", Value = ob.DF_PROC_TYPE_LST},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value = ob.EXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pISS_STORE_ID", Value = ob.ISS_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCHALAN_NO", Value = ob.CHALAN_NO},
                     new CommandParameter() {ParameterName = "pCHALAN_DT", Value = ob.CHALAN_DT},
                     new CommandParameter() {ParameterName = "pGATE_PASS_NO", Value = ob.GATE_PASS_NO},
                     new CommandParameter() {ParameterName = "pVEHICLE_NO", Value = ob.VEHICLE_NO},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_FAB_PROD_CAT_ID", Value = ob.RF_FAB_PROD_CAT_ID},
                     new CommandParameter() {ParameterName = "pLK_FAB_PRG_TYPE", Value = ob.LK_FAB_PRG_TYPE==0?null:ob.LK_FAB_PRG_TYPE},
                     new CommandParameter() {ParameterName = "pIS_SC", Value = ob.IS_SC},
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

        public List<DF_SC_PT_ISS_HModel> SelectAll(int pageNo, int pageSize, string pSC_PRG_NO = null, string pPRG_ISS_DT = null, string pEXP_DELV_DT = null, Int64? pHR_COMPANY_ID = null, Int64? pSCM_SUPPLIER_ID = null, Int64? pSCM_STORE_ID = null, Int64? pDYE_GSTR_REQ_H_ID = null, Int64? pUSER_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pSC_PRG_NO", Value =pSC_PRG_NO},
                     new CommandParameter() {ParameterName = "pPRG_ISS_DT", Value =pPRG_ISS_DT},
                     new CommandParameter() {ParameterName = "pEXP_DELV_DT", Value =pEXP_DELV_DT},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value =pHR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value =pSCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pDYE_GSTR_REQ_H_ID", Value =pDYE_GSTR_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_HModel ob = new DF_SC_PT_ISS_HModel();
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
                    ob.PRG_ISS_BY = (dr["PRG_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_BY"]);
                    ob.PRG_ISS_TO = (dr["PRG_ISS_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_TO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.LK_FAB_PRG_TYPE = (dr["LK_FAB_PRG_TYPE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_PRG_TYPE"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? "" : Convert.ToString(dr["REV_REASON"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.IS_SC = (dr["IS_SC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC"]);
                    ob.IS_BATCH = (dr["IS_BATCH"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BATCH"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SC_PRG_STS_NAME = (dr["SC_PRG_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_STS_NAME"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_PT_ISS_HModel> ScChallanBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_HModel ob = new DF_SC_PT_ISS_HModel();
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
                    ob.PRG_ISS_BY = (dr["PRG_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_BY"]);
                    ob.PRG_ISS_TO = (dr["PRG_ISS_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_TO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.IS_SC = (dr["IS_SC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC"]);
                    ob.IS_BATCH = (dr["IS_BATCH"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BATCH"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LK_FAB_PRG_TYPE = (dr["LK_FAB_PRG_TYPE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_PRG_TYPE"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? "" : Convert.ToString(dr["REV_REASON"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SC_PRG_STS_NAME = (dr["SC_PRG_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_STS_NAME"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DF_SC_PT_ISS_HModel> ScProgramBySupplierID(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_SELECT";
            try
            {
                var obList = new List<DF_SC_PT_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3003},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SC_PT_ISS_HModel ob = new DF_SC_PT_ISS_HModel();
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
                    ob.PRG_ISS_BY = (dr["PRG_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_BY"]);
                    ob.PRG_ISS_TO = (dr["PRG_ISS_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_TO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_FAB_PROD_CAT_ID = (dr["RF_FAB_PROD_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_FAB_PROD_CAT_ID"]);
                    ob.IS_SC = (dr["IS_SC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC"]);
                    ob.IS_BATCH = (dr["IS_BATCH"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BATCH"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.LK_FAB_PRG_TYPE = (dr["LK_FAB_PRG_TYPE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_PRG_TYPE"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? "" : Convert.ToString(dr["REV_REASON"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    ob.SC_PRG_STS_NAME = (dr["SC_PRG_STS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_STS_NAME"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.DF_SC_PT_ISS_D1_ID = (dr["DF_SC_PT_ISS_D1_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_D1_ID"]);
                    ob.KNT_STYL_FAB_ITEM_ID = (dr["KNT_STYL_FAB_ITEM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_STYL_FAB_ITEM_ID"]);

                    ob.BUYER_NAME_EN = (dr["BUYER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BUYER_NAME_EN"]);
                    ob.STYLE_NO = (dr["STYLE_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STYLE_NO"]);
                    ob.MC_ORDER_NO_LST = (dr["MC_ORDER_NO_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_ORDER_NO_LST"]);
                    ob.FAB_ITEM_DESC = (dr["FAB_ITEM_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ITEM_DESC"]);
                    ob.FAB_ROLL_LST = (dr["FAB_ROLL_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_ROLL_LST"]);

                    ob.RQD_QTY = (dr["RQD_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["RQD_QTY"]);
                    ob.ISS_QTY = (dr["ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_QTY"]);
                    ob.ISS_ROLL_QTY = (dr["ISS_ROLL_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["ISS_ROLL_QTY"]);


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SC_PT_ISS_HModel Select(Int64? pDF_SC_PT_ISS_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SC_PT_ISS_H_SELECT";
            try
            {
                var ob = new DF_SC_PT_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SC_PT_ISS_H_ID", Value =pDF_SC_PT_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SC_PT_ISS_H_ID = (dr["DF_SC_PT_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_ISS_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.DYE_GSTR_REQ_H_ID = (dr["DYE_GSTR_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_GSTR_REQ_H_ID"]);
                    ob.SC_PRG_NO = (dr["SC_PRG_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SC_PRG_NO"]);
                    ob.PRG_ISS_BY = (dr["PRG_ISS_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_BY"]);
                    ob.PRG_ISS_TO = (dr["PRG_ISS_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PRG_ISS_TO"]);
                    ob.PRG_ISS_DT = (dr["PRG_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PRG_ISS_DT"]);
                    ob.DF_PROC_TYPE_LST = (dr["DF_PROC_TYPE_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DF_PROC_TYPE_LST"]);
                    ob.EXP_DELV_DT = (dr["EXP_DELV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["EXP_DELV_DT"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.ISS_STORE_ID = (dr["ISS_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_SC = (dr["IS_SC"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_SC"]);
                    ob.IS_BATCH = (dr["IS_BATCH"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_BATCH"]);
                    ob.DYE_BT_CARD_H_ID = (dr["DYE_BT_CARD_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DYE_BT_CARD_H_ID"]);
                    ob.DYE_BATCH_NO = (dr["DYE_BATCH_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DYE_BATCH_NO"]);
                    ob.LK_FAB_PRG_TYPE = (dr["LK_FAB_PRG_TYPE"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_FAB_PRG_TYPE"]);

                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? "" : Convert.ToString(dr["REV_REASON"]);
                    ob.DF_SC_PT_RCV_H_ID = (dr["DF_SC_PT_RCV_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SC_PT_RCV_H_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);

                    if (dr["REVISION_DT"] != DBNull.Value)
                        ob.REVISION_DT = Convert.ToDateTime(dr["REVISION_DT"]);

                    ob.IS_UPDATE = "N";

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BUYER_NAME_EN { get; set; }
        public string STYLE_NO { get; set; }
        public string MC_ORDER_NO_LST { get; set; }
        public string FAB_ITEM_DESC { get; set; }
        public string FAB_ROLL_LST { get; set; }
        public decimal RQD_QTY { get; set; }
        public decimal ISS_QTY { get; set; }
        public decimal ISS_ROLL_QTY { get; set; }
        public long DF_SC_PT_ISS_D1_ID { get; set; }
        public long KNT_STYL_FAB_ITEM_ID { get; set; }
    }
}