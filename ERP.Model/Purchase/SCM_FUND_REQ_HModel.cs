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
    public class SCM_FUND_REQ_HModel
    {
        public Int64 SCM_FUND_REQ_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public string REQ_NO { get; set; }
        public DateTime REQ_DT { get; set; }
        public Int64 REQ_BY { get; set; }
        public Int64 REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Decimal TOT_REQ_AMT { get; set; }
        public DateTime FND_ARPV_DT { get; set; }
        public Int64? FND_ARPV_BY { get; set; }
        public Decimal TOT_ARPV_AMT { get; set; }
        public string REMARKS_REQ { get; set; }
        public string REMARKS_APRV { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_FUND_D { get; set; }
        public string XML_FUND_ITEM { get; set; }
        

        public string Save()
        {
            const string sp = "PKG_SCM_PURCHASE.SCM_FUND_REQ_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_NO", Value = ob.REQ_NO},
                     new CommandParameter() {ParameterName = "pREQ_DT", Value = ob.REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_BY", Value = ob.REQ_BY},
                     new CommandParameter() {ParameterName = "pREQ_TO", Value = ob.REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTOT_REQ_AMT", Value = ob.TOT_REQ_AMT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_DT", Value = ob.FND_ARPV_DT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_BY", Value = ob.FND_ARPV_BY},
                     new CommandParameter() {ParameterName = "pTOT_ARPV_AMT", Value = ob.TOT_ARPV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS_REQ", Value = ob.REMARKS_REQ},
                     new CommandParameter() {ParameterName = "pREMARKS_APRV", Value = ob.REMARKS_APRV},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pXML_FUND_D", Value = ob.XML_FUND_D},
                     new CommandParameter() {ParameterName = "pXML_FUND_ITEM", Value = ob.XML_FUND_ITEM},
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
            const string sp = "PKG_SCM_PURCHASE.SP_SCM_FUND_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_NO", Value = ob.REQ_NO},
                     new CommandParameter() {ParameterName = "pREQ_DT", Value = ob.REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_BY", Value = ob.REQ_BY},
                     new CommandParameter() {ParameterName = "pREQ_TO", Value = ob.REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTOT_REQ_AMT", Value = ob.TOT_REQ_AMT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_DT", Value = ob.FND_ARPV_DT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_BY", Value = ob.FND_ARPV_BY},
                     new CommandParameter() {ParameterName = "pTOT_ARPV_AMT", Value = ob.TOT_ARPV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS_REQ", Value = ob.REMARKS_REQ},
                     new CommandParameter() {ParameterName = "pREMARKS_APRV", Value = ob.REMARKS_APRV},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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
            const string sp = "SP_SCM_FUND_REQ_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value = ob.SCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pREQ_NO", Value = ob.REQ_NO},
                     new CommandParameter() {ParameterName = "pREQ_DT", Value = ob.REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_BY", Value = ob.REQ_BY},
                     new CommandParameter() {ParameterName = "pREQ_TO", Value = ob.REQ_TO},
                     new CommandParameter() {ParameterName = "pREQ_ATTN_MAIL", Value = ob.REQ_ATTN_MAIL},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTOT_REQ_AMT", Value = ob.TOT_REQ_AMT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_DT", Value = ob.FND_ARPV_DT},
                     new CommandParameter() {ParameterName = "pFND_ARPV_BY", Value = ob.FND_ARPV_BY},
                     new CommandParameter() {ParameterName = "pTOT_ARPV_AMT", Value = ob.TOT_ARPV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS_REQ", Value = ob.REMARKS_REQ},
                     new CommandParameter() {ParameterName = "pREMARKS_APRV", Value = ob.REMARKS_APRV},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<SCM_FUND_REQ_HModel> SelectAll()
        {
            string sp = "PKG_SCM_PURCHASE.SCM_FUND_REQ_H_Select";
            try
            {
                var obList = new List<SCM_FUND_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_FUND_REQ_HModel ob = new SCM_FUND_REQ_HModel();
                    ob.SCM_FUND_REQ_H_ID = (dr["SCM_FUND_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_NO = (dr["REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_NO"]);
                    ob.REQ_DT = (dr["REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DT"]);
                    ob.REQ_BY = (dr["REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_BY"]);
                    ob.REQ_TO = (dr["REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.TOT_REQ_AMT = (dr["TOT_REQ_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_REQ_AMT"]);
                    ob.FND_ARPV_DT = (dr["FND_ARPV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FND_ARPV_DT"]);
                    ob.FND_ARPV_BY = (dr["FND_ARPV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FND_ARPV_BY"]);
                    ob.TOT_ARPV_AMT = (dr["TOT_ARPV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_ARPV_AMT"]);
                    ob.REMARKS_REQ = (dr["REMARKS_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_REQ"]);
                    ob.REMARKS_APRV = (dr["REMARKS_APRV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_APRV"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.CURR_NAME_EN = (dr["CURR_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CURR_NAME_EN"]);
                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_FUND_REQ_HModel Select(Int64? pSCM_FUND_REQ_H_ID = null)
        {
            string sp = "PKG_SCM_PURCHASE.SCM_FUND_REQ_H_Select";
            try
            {
                var ob = new SCM_FUND_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_FUND_REQ_H_ID", Value =pSCM_FUND_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_FUND_REQ_H_ID = (dr["SCM_FUND_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_FUND_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.REQ_NO = (dr["REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_NO"]);
                    ob.REQ_DT = (dr["REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REQ_DT"]);
                    ob.REQ_BY = (dr["REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_BY"]);
                    ob.REQ_TO = (dr["REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.TOT_REQ_AMT = (dr["TOT_REQ_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_REQ_AMT"]);
                    ob.FND_ARPV_DT = (dr["FND_ARPV_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["FND_ARPV_DT"]);
                    ob.FND_ARPV_BY = (dr["FND_ARPV_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FND_ARPV_BY"]);
                    ob.TOT_ARPV_AMT = (dr["TOT_ARPV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TOT_ARPV_AMT"]);
                    ob.REMARKS_REQ = (dr["REMARKS_REQ"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_REQ"]);
                    ob.REMARKS_APRV = (dr["REMARKS_APRV"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS_APRV"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
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

        public int TOTAL_REC { get; set; }

        public string COMP_NAME_EN { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string CURR_NAME_EN { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }
    }
}