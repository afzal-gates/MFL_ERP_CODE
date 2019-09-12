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
    public class HR_AUD_CERT_CAPModel
    {
        public Int64 HR_AUD_CERT_CAP_ID { get; set; }
        public Int64 HR_AUD_CERT_REG_ID { get; set; }
        public string CAP_DOC_PATH { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_CAP { get; set; }
        public HttpPostedFileBase ATT_FILE { get; set; }


        public string Save()
        {
            const string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_CAP_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_CAP_ID", Value = ob.HR_AUD_CERT_CAP_ID},
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pCAP_DOC_PATH", Value = ob.CAP_DOC_PATH},
                     new CommandParameter() {ParameterName = "pXML_CAP", Value = ob.XML_CAP},                     
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
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
            const string sp = "SP_HR_AUD_CERT_CAP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_CAP_ID", Value = ob.HR_AUD_CERT_CAP_ID},
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pCAP_DOC_PATH", Value = ob.CAP_DOC_PATH},
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
            const string sp = "SP_HR_AUD_CERT_CAP";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_CAP_ID", Value = ob.HR_AUD_CERT_CAP_ID},
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value = ob.HR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pCAP_DOC_PATH", Value = ob.CAP_DOC_PATH},
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

        public List<HR_AUD_CERT_CAPModel> SelectAll(Int32? pHR_AUD_CERT_REG_ID = null)
        {
            string sp = "PKG_HR_AUDIT_CERT.HR_AUD_CERT_CAP_SELECT";
            try
            {
                var obList = new List<HR_AUD_CERT_CAPModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_REG_ID", Value =pHR_AUD_CERT_REG_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    HR_AUD_CERT_CAPModel ob = new HR_AUD_CERT_CAPModel();
                    ob.HR_AUD_CERT_CAP_ID = (dr["HR_AUD_CERT_CAP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_CAP_ID"]);
                    ob.HR_AUD_CERT_REG_ID = (dr["HR_AUD_CERT_REG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_REG_ID"]);
                    ob.CAP_DOC_PATH = (dr["CAP_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CAP_DOC_PATH"]);
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

        public HR_AUD_CERT_CAPModel Select(long ID)
        {
            string sp = "PKG_PUR_SUPPLIER.HR_AUD_CERT_CAP_SELECT";
            try
            {
                var ob = new HR_AUD_CERT_CAPModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pHR_AUD_CERT_CAP_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.HR_AUD_CERT_CAP_ID = (dr["HR_AUD_CERT_CAP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_CAP_ID"]);
                    ob.HR_AUD_CERT_REG_ID = (dr["HR_AUD_CERT_REG_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_AUD_CERT_REG_ID"]);
                    ob.CAP_DOC_PATH = (dr["CAP_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CAP_DOC_PATH"]);
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