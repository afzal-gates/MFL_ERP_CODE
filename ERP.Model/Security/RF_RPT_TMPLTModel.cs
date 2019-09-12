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
    public class RF_RPT_TMPLTModel
    {
        public Int64 RF_RPT_TMPLT_ID { get; set; }
        public string RPT_TMPLT_CODE { get; set; }
        public string RPT_TMPLT_NAME { get; set; }
        public Int64 LK_RPT_DOC_TYPE_ID { get; set; }
        public string RPT_PATH_URL { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_TMP_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_security.rf_rpt_tmplt_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RPT_TMPLT_ID", Value = ob.RF_RPT_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_CODE", Value = ob.RPT_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_NAME", Value = ob.RPT_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "pLK_RPT_DOC_TYPE_ID", Value = ob.LK_RPT_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRPT_PATH_URL", Value = ob.RPT_PATH_URL},
                     new CommandParameter() {ParameterName = "pXML_TMP_D", Value = ob.XML_TMP_D},
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
            const string sp = "SP_RF_RPT_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RPT_TMPLT_ID", Value = ob.RF_RPT_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_CODE", Value = ob.RPT_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_NAME", Value = ob.RPT_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "pLK_RPT_DOC_TYPE_ID", Value = ob.LK_RPT_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRPT_PATH_URL", Value = ob.RPT_PATH_URL},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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
            const string sp = "SP_RF_RPT_TMPLT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RPT_TMPLT_ID", Value = ob.RF_RPT_TMPLT_ID},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_CODE", Value = ob.RPT_TMPLT_CODE},
                     new CommandParameter() {ParameterName = "pRPT_TMPLT_NAME", Value = ob.RPT_TMPLT_NAME},
                     new CommandParameter() {ParameterName = "pLK_RPT_DOC_TYPE_ID", Value = ob.LK_RPT_DOC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRPT_PATH_URL", Value = ob.RPT_PATH_URL},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                {
                    jsonStr += dr["KEY"].ToString() + ":" + dr["VALUE"].ToString() + ",";
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

        public List<RF_RPT_TMPLTModel> SelectAll()
        {
            string sp = "pkg_security.rf_rpt_tmplt_select";
            try
            {
                var obList = new List<RF_RPT_TMPLTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RPT_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_RPT_TMPLTModel ob = new RF_RPT_TMPLTModel();
                    ob.RF_RPT_TMPLT_ID = (dr["RF_RPT_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RPT_TMPLT_ID"]);
                    ob.RPT_TMPLT_CODE = (dr["RPT_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_TMPLT_CODE"]);
                    ob.RPT_TMPLT_NAME = (dr["RPT_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_TMPLT_NAME"]);
                    ob.LK_RPT_DOC_TYPE_ID = (dr["LK_RPT_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RPT_DOC_TYPE_ID"]);
                    ob.RPT_PATH_URL = (dr["RPT_PATH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_PATH_URL"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_RPT_TMPLTModel Select(long ID)
        {
            string sp = "pkg_security.rf_rpt_tmplt_select";
            try
            {
                var ob = new RF_RPT_TMPLTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_RPT_TMPLT_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_RPT_TMPLT_ID = (dr["RF_RPT_TMPLT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_RPT_TMPLT_ID"]);
                    ob.RPT_TMPLT_CODE = (dr["RPT_TMPLT_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_TMPLT_CODE"]);
                    ob.RPT_TMPLT_NAME = (dr["RPT_TMPLT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_TMPLT_NAME"]);
                    ob.LK_RPT_DOC_TYPE_ID = (dr["LK_RPT_DOC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_RPT_DOC_TYPE_ID"]);
                    ob.RPT_PATH_URL = (dr["RPT_PATH_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RPT_PATH_URL"]);
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
    }
}