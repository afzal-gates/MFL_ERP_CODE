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
    public class KNT_YRN_ISS_RET_HModel
    {
        public Int64 KNT_YRN_ISS_RET_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 ITEM_RET_BY { get; set; }
        public string RET_CHALAN_NO { get; set; }
        public DateTime RET_CHALAN_DT { get; set; }
        public DateTime RCV_DT { get; set; }
        public Int64 ITEM_RCV_BY { get; set; }
        public Int64 LK_RET_STATUS_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string CARRIER_NAME { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_FINALIZED { get; set; }
        public string XML_RTN_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_knit_yarn_issue.knt_yrn_iss_ret_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RET_BY", Value = ob.ITEM_RET_BY},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pLK_RET_STATUS_ID", Value = ob.LK_RET_STATUS_ID==0?491:ob.LK_RET_STATUS_ID==491?492:ob.LK_RET_STATUS_ID==492?493:ob.LK_RET_STATUS_ID==493?494:ob.LK_RET_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID==0?1:ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED==null?"N":ob.IS_FINALIZED},
                     
                     new CommandParameter() {ParameterName = "pXML_RTN_D", Value = ob.XML_RTN_D},
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
            const string sp = "SP_KNT_YRN_ISS_RET_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RET_BY", Value = ob.ITEM_RET_BY},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pLK_RET_STATUS_ID", Value = ob.LK_RET_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_KNT_YRN_ISS_RET_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value = ob.KNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RET_BY", Value = ob.ITEM_RET_BY},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value = ob.RET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value = ob.RET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pRCV_DT", Value = ob.RCV_DT},
                     new CommandParameter() {ParameterName = "pITEM_RCV_BY", Value = ob.ITEM_RCV_BY},
                     new CommandParameter() {ParameterName = "pLK_RET_STATUS_ID", Value = ob.LK_RET_STATUS_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCARRIER_NAME", Value = ob.CARRIER_NAME},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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

        public List<KNT_YRN_ISS_RET_HModel> SelectAll(int pageNo, int pageSize, string pRET_CHALAN_NO = null, string pRET_CHALAN_DT = null, string pCOMP_NAME_EN = null,
             string pSTORE_NAME_EN = null, string pREQ_STATUS = null, string pREMARKS = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption=null)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_ret_h_select";
            try
            {
                var obList = new List<KNT_YRN_ISS_RET_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =pUSER_ID},
                     
                     new CommandParameter() {ParameterName = "pRET_CHALAN_NO", Value =pRET_CHALAN_NO},
                     new CommandParameter() {ParameterName = "pRET_CHALAN_DT", Value =pRET_CHALAN_DT},
                     new CommandParameter() {ParameterName = "pCOMP_NAME_EN", Value =pCOMP_NAME_EN},
                     new CommandParameter() {ParameterName = "pSTORE_NAME_EN", Value =pSTORE_NAME_EN},
                     new CommandParameter() {ParameterName = "pREQ_STATUS", Value =pREQ_STATUS},
                     new CommandParameter() {ParameterName = "pREMARKS", Value =pREMARKS},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value =pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =pOption==null?3000:pOption},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    KNT_YRN_ISS_RET_HModel ob = new KNT_YRN_ISS_RET_HModel();
                    ob.KNT_YRN_ISS_RET_H_ID = (dr["KNT_YRN_ISS_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.ITEM_RET_BY = (dr["ITEM_RET_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RET_BY"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.LK_RET_STATUS_ID = (dr["LK_RET_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RET_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.USER_NAME_EN = (dr["USER_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["USER_NAME_EN"]);
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    //ob.REQ_STATUS = (dr["REQ_STATUS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_STATUS"]);
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["PERMISSION"]);

                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    if (dr["ACTN_ROLE_FLAG"] != DBNull.Value)
                        ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);

                    ob.TTL_RTN_QTY = (dr["TTL_RTN_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RTN_QTY"]);
                    ob.TTL_RCV_QTY = (dr["TTL_RCV_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RCV_QTY"]);
                    ob.BALANCE_QTY = TTL_RTN_QTY - ob.TTL_RCV_QTY;

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public KNT_YRN_ISS_RET_HModel Select(Int64? pKNT_YRN_ISS_RET_H_ID)
        {
            string sp = "pkg_knit_yarn_issue.knt_yrn_iss_ret_h_select";
            try
            {
                var ob = new KNT_YRN_ISS_RET_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pKNT_YRN_ISS_RET_H_ID", Value =pKNT_YRN_ISS_RET_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.KNT_YRN_ISS_RET_H_ID = (dr["KNT_YRN_ISS_RET_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["KNT_YRN_ISS_RET_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_RET_BY = (dr["ITEM_RET_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RET_BY"]);
                    ob.RET_CHALAN_NO = (dr["RET_CHALAN_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RET_CHALAN_NO"]);
                    ob.RET_CHALAN_DT = (dr["RET_CHALAN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RET_CHALAN_DT"]);
                    ob.RCV_DT = (dr["RCV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RCV_DT"]);
                    ob.ITEM_RCV_BY = (dr["ITEM_RCV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RCV_BY"]);
                    ob.LK_RET_STATUS_ID = (dr["LK_RET_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RET_STATUS_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CARRIER_NAME = (dr["CARRIER_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CARRIER_NAME"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
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

        public string ACTN_STATUS_NAME { get; set; }

        public string USER_NAME_EN { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string REQ_STATUS { get; set; }

        public int TOTAL_REC { get; set; }

        public decimal TTL_RTN_QTY { get; set; }

        public decimal TTL_RCV_QTY { get; set; }

        public decimal BALANCE_QTY { get; set; }

        public int PERMISSION { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }
    }
}