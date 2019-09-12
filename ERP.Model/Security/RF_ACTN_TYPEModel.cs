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
    public class RF_ACTN_TYPEModel
    {
        public Int64 RF_ACTN_TYPE_ID { get; set; }
        public string ACTN_TYPE_CODE { get; set; }
        public string ACTN_TYPE_NAME { get; set; }
        public string IS_ACTIVE { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string ACTN_TYPE_SNAME { get; set; }
        public string PAGE_URL { get; set; }

        public string Save()
        {
            const string sp = "pkg_common.rf_actn_type_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_CODE", Value = ob.ACTN_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_NAME", Value = ob.ACTN_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_SNAME", Value = ob.ACTN_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pPAGE_URL", Value = ob.PAGE_URL},
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
            const string sp = "SP_RF_ACTN_TYPE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_CODE", Value = ob.ACTN_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_NAME", Value = ob.ACTN_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_SNAME", Value = ob.ACTN_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pPAGE_URL", Value = ob.PAGE_URL},
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
            const string sp = "pkg_common.rf_actn_type_delete";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value = ob.RF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_CODE", Value = ob.ACTN_TYPE_CODE},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_NAME", Value = ob.ACTN_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pIS_ACTIVE", Value = ob.IS_ACTIVE},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pACTN_TYPE_SNAME", Value = ob.ACTN_TYPE_SNAME},
                     new CommandParameter() {ParameterName = "pPAGE_URL", Value = ob.PAGE_URL},
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

        public List<RF_ACTN_TYPEModel> SelectAll()
        {
            string sp = "pkg_common.rf_actn_type_select";
            try
            {
                var obList = new List<RF_ACTN_TYPEModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RF_ACTN_TYPEModel ob = new RF_ACTN_TYPEModel();
                    ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
                    ob.ACTN_TYPE_CODE = (dr["ACTN_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_CODE"]);
                    ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ACTN_TYPE_SNAME = (dr["ACTN_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_SNAME"]);
                    ob.PAGE_URL = (dr["PAGE_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAGE_URL"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RF_ACTN_TYPEModel Select(Int64? pRF_ACTN_TYPE_ID)
        {
            string sp = "pkg_common.rf_actn_type_select";
            try
            {
                var ob = new RF_ACTN_TYPEModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pRF_ACTN_TYPE_ID", Value =pRF_ACTN_TYPE_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.RF_ACTN_TYPE_ID = (dr["RF_ACTN_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_TYPE_ID"]);
                    ob.ACTN_TYPE_CODE = (dr["ACTN_TYPE_CODE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_CODE"]);
                    ob.ACTN_TYPE_NAME = (dr["ACTN_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_NAME"]);
                    ob.IS_ACTIVE = (dr["IS_ACTIVE"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_ACTIVE"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.ACTN_TYPE_SNAME = (dr["ACTN_TYPE_SNAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_TYPE_SNAME"]);
                    ob.PAGE_URL = (dr["PAGE_URL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PAGE_URL"]);

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