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
    public class RF_AUD_CERT_TYPEModel
    {
        public Int64 RF_AUD_CERT_TYPE_ID { get; set; }
        public string CERT_TYPE_TITLE { get; set; }
        public string CERT_TYPE_DESC { get; set; }
        public string ISS_BY_ORG_NAME { get; set; }
        public string ORG_WEB_LINK { get; set; }
        public string CERT_LOGO { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string CERT_TYPE_CODE { get; set; }
        public Int64 LK_AUD_DOC_TYPE_ID { get; set; }
        public Int64 RF_DOC_ISS_ORG_ID { get; set; }
        public Int64 DISPLAY_ORDER { get; set; }
        public string IS_NOTIFY_EMAIL { get; set; }
        public Int64 NOTIFY_BF_DAYS { get; set; }
        public DateTime LAST_ISSUE_DT { get; set; }
        public Int64? RESP_EMP_ID { get; set; }

        public int TOTAL_REC { get; set; }
        public string AUD_DOC_TYPE_NAME { get; set; }
        public string ISS_ORG_NAME_EN { get; set; }
        public string RESP_EMP_NAME { get; set; }
        public string MC_BUYER_NAME_LST { get; set; }
        public string MC_BUYER_ID_LST { get; set; }

        public HttpPostedFileBase ATT_FILE { get; set; }
        
        public string Save()
        {
            const string sp = "PKG_PUR_SUPPLIER.RF_AUD_CERT_TYPE_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_TITLE", Value = ob.CERT_TYPE_TITLE},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_DESC", Value = ob.CERT_TYPE_DESC},
                     new CommandParameter() {ParameterName = "pISS_BY_ORG_NAME", Value = ob.ISS_BY_ORG_NAME},
                     new CommandParameter() {ParameterName = "pORG_WEB_LINK", Value = ob.ORG_WEB_LINK},
                     new CommandParameter() {ParameterName = "pCERT_LOGO", Value = ob.CERT_LOGO},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_CODE", Value = ob.CERT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pLK_AUD_DOC_TYPE_ID", Value = ob.LK_AUD_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_DOC_ISS_ORG_ID", Value = ob.RF_DOC_ISS_ORG_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                     new CommandParameter() {ParameterName = "pNOTIFY_BF_DAYS", Value = ob.NOTIFY_BF_DAYS},
                     new CommandParameter() {ParameterName = "pLAST_ISSUE_DT", Value = ob.LAST_ISSUE_DT},
                     new CommandParameter() {ParameterName = "pRESP_EMP_ID", Value = ob.RESP_EMP_ID},
                     new CommandParameter() {ParameterName = "pMC_BUYER_ID_LST", Value = ob.MC_BUYER_ID_LST},
                     
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
            const string sp = "PKG_PUR_SUPPLIER.RF_AUD_CERT_TYPE_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_TITLE", Value = ob.CERT_TYPE_TITLE},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_DESC", Value = ob.CERT_TYPE_DESC},
                     new CommandParameter() {ParameterName = "pISS_BY_ORG_NAME", Value = ob.ISS_BY_ORG_NAME},
                     new CommandParameter() {ParameterName = "pORG_WEB_LINK", Value = ob.ORG_WEB_LINK},
                     new CommandParameter() {ParameterName = "pCERT_LOGO", Value = ob.CERT_LOGO},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_CODE", Value = ob.CERT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pLK_AUD_DOC_TYPE_ID", Value = ob.LK_AUD_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_DOC_ISS_ORG_ID", Value = ob.RF_DOC_ISS_ORG_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                     new CommandParameter() {ParameterName = "pNOTIFY_BF_DAYS", Value = ob.NOTIFY_BF_DAYS},
                     new CommandParameter() {ParameterName = "pLAST_ISSUE_DT", Value = ob.LAST_ISSUE_DT},
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
            const string sp = "PKG_PUR_SUPPLIER.RF_AUD_CERT_TYPE_DELETE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value = ob.RF_AUD_CERT_TYPE_ID},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_TITLE", Value = ob.CERT_TYPE_TITLE},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_DESC", Value = ob.CERT_TYPE_DESC},
                     new CommandParameter() {ParameterName = "pISS_BY_ORG_NAME", Value = ob.ISS_BY_ORG_NAME},
                     new CommandParameter() {ParameterName = "pORG_WEB_LINK", Value = ob.ORG_WEB_LINK},
                     new CommandParameter() {ParameterName = "pCERT_LOGO", Value = ob.CERT_LOGO},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pCERT_TYPE_CODE", Value = ob.CERT_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pLK_AUD_DOC_TYPE_ID", Value = ob.LK_AUD_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_DOC_ISS_ORG_ID", Value = ob.RF_DOC_ISS_ORG_ID},
                     new CommandParameter() {ParameterName = "pDISPLAY_ORDER", Value = ob.DISPLAY_ORDER},
                     new CommandParameter() {ParameterName = "pIS_NOTIFY_EMAIL", Value = ob.IS_NOTIFY_EMAIL},
                     new CommandParameter() {ParameterName = "pNOTIFY_BF_DAYS", Value = ob.NOTIFY_BF_DAYS},
                     new CommandParameter() {ParameterName = "pLAST_ISSUE_DT", Value = ob.LAST_ISSUE_DT},
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

        public List<RF_AUD_CERT_TYPEModel> SelectAll()
        {
            string sp = "PKG_PUR_SUPPLIER.RF_AUD_CERT_TYPE_SELECT";
            try
            {
                var obList = new List<RF_AUD_CERT_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_AUD_CERT_TYPEModel ob = new RF_AUD_CERT_TYPEModel();
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.CERT_TYPE_DESC = (dr["CERT_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_DESC"]);
                    ob.ISS_BY_ORG_NAME = (dr["ISS_BY_ORG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_BY_ORG_NAME"]);
                    ob.ORG_WEB_LINK = (dr["ORG_WEB_LINK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_WEB_LINK"]);
                    ob.CERT_LOGO = (dr["CERT_LOGO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_LOGO"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CERT_TYPE_CODE = (dr["CERT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_CODE"]);
                    ob.LK_AUD_DOC_TYPE_ID = (dr["LK_AUD_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AUD_DOC_TYPE_ID"]);
                    ob.RF_DOC_ISS_ORG_ID = (dr["RF_DOC_ISS_ORG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DOC_ISS_ORG_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.NOTIFY_BF_DAYS = (dr["NOTIFY_BF_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NOTIFY_BF_DAYS"]);
                    ob.LAST_ISSUE_DT = (dr["LAST_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_ISSUE_DT"]);
                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);

                    ob.AUD_DOC_TYPE_NAME = (dr["AUD_DOC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUD_DOC_TYPE_NAME"]);
                    ob.ISS_ORG_NAME_EN = (dr["ISS_ORG_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_ORG_NAME_EN"]);
                    ob.RESP_EMP_NAME = (dr["RESP_EMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_EMP_NAME"]);
                    ob.MC_BUYER_NAME_LST = (dr["MC_BUYER_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_NAME_LST"]);
                    ob.MC_BUYER_ID_LST = (dr["CERT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_ID_LST"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_AUD_CERT_TYPEModel Select(long ID)
        {
            string sp = "PKG_PUR_SUPPLIER.RF_AUD_CERT_TYPE_SELECT";
            try
            {
                var ob = new RF_AUD_CERT_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_AUD_CERT_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_AUD_CERT_TYPE_ID = (dr["RF_AUD_CERT_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_AUD_CERT_TYPE_ID"]);
                    ob.CERT_TYPE_TITLE = (dr["CERT_TYPE_TITLE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_TITLE"]);
                    ob.CERT_TYPE_DESC = (dr["CERT_TYPE_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_DESC"]);
                    ob.ISS_BY_ORG_NAME = (dr["ISS_BY_ORG_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_BY_ORG_NAME"]);
                    ob.ORG_WEB_LINK = (dr["ORG_WEB_LINK"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ORG_WEB_LINK"]);
                    ob.CERT_LOGO = (dr["CERT_LOGO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_LOGO"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.CERT_TYPE_CODE = (dr["CERT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CERT_TYPE_CODE"]);
                    ob.LK_AUD_DOC_TYPE_ID = (dr["LK_AUD_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_AUD_DOC_TYPE_ID"]);
                    ob.RF_DOC_ISS_ORG_ID = (dr["RF_DOC_ISS_ORG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DOC_ISS_ORG_ID"]);
                    ob.DISPLAY_ORDER = (dr["DISPLAY_ORDER"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DISPLAY_ORDER"]);
                    ob.IS_NOTIFY_EMAIL = (dr["IS_NOTIFY_EMAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_NOTIFY_EMAIL"]);
                    ob.NOTIFY_BF_DAYS = (dr["NOTIFY_BF_DAYS"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["NOTIFY_BF_DAYS"]);
                    ob.LAST_ISSUE_DT = (dr["LAST_ISSUE_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_ISSUE_DT"]);

                    ob.RESP_EMP_ID = (dr["RESP_EMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RESP_EMP_ID"]);

                    ob.AUD_DOC_TYPE_NAME = (dr["AUD_DOC_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["AUD_DOC_TYPE_NAME"]);
                    ob.ISS_ORG_NAME_EN = (dr["ISS_ORG_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_ORG_NAME_EN"]);
                    ob.RESP_EMP_NAME = (dr["RESP_EMP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RESP_EMP_NAME"]);
                    ob.MC_BUYER_NAME_LST = (dr["MC_BUYER_NAME_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_NAME_LST"]);
                    ob.MC_BUYER_ID_LST = (dr["CERT_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["MC_BUYER_ID_LST"]);
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