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
    public class CM_IMP_PI_HModel
    {
        public Int64 CM_IMP_PI_H_ID { get; set; }
        public Int64 CM_IMP_PO_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string PI_NO_IMP { get; set; }
        public DateTime PI_DT_IMP { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public Decimal PI_NET_AMT { get; set; }
        public Decimal PI_REV_AMT { get; set; }
        public string TERMS_DESC { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string RPT_PATH_URL { get; set; }
        
        public HttpPostedFileBase ATT_FILE { get; set; }

        public string XML_PI { get; set; }
        public string XML_PI_D { get; set; }

        public string Save()
        {
            const string sp = "pkg_cm_import_pi.cm_imp_pi_h_insert";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_IMP", Value = ob.PI_NO_IMP},
                     new CommandParameter() {ParameterName = "pPI_DT_IMP", Value = ob.PI_DT_IMP},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pPI_REV_AMT", Value = ob.PI_REV_AMT},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pXML_PI", Value = ob.XML_PI},
                     new CommandParameter() {ParameterName = "pXML_PI_D", Value = ob.XML_PI_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opCM_IMP_PI_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "pkg_cm_import_pi.cm_imp_pi_h_update";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_IMP", Value = ob.PI_NO_IMP},
                     new CommandParameter() {ParameterName = "pPI_DT_IMP", Value = ob.PI_DT_IMP},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pPI_REV_AMT", Value = ob.PI_REV_AMT},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pXML_PI", Value = ob.XML_PI},
                     new CommandParameter() {ParameterName = "pXML_PI_D", Value = ob.XML_PI_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opCM_IMP_PI_H_ID", Value =0, Direction = ParameterDirection.Output}
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


        public string RevisePO()
        {
            const string sp = "pkg_cm_import_pi.cm_imp_pi_h_revise";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_IMP", Value = ob.PI_NO_IMP},
                     new CommandParameter() {ParameterName = "pPI_DT_IMP", Value = ob.PI_DT_IMP},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pPI_REV_AMT", Value = ob.PI_REV_AMT},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pXML_PI", Value = ob.XML_PI},
                     new CommandParameter() {ParameterName = "pXML_PI_D", Value = ob.XML_PI_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output},
                     new CommandParameter() {ParameterName = "opCM_IMP_PI_H_ID", Value =0, Direction = ParameterDirection.Output}
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
            const string sp = "SP_CM_IMP_PI_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value = ob.CM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_IMP", Value = ob.PI_NO_IMP},
                     new CommandParameter() {ParameterName = "pPI_DT_IMP", Value = ob.PI_DT_IMP},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pPI_NET_AMT", Value = ob.PI_NET_AMT},
                     new CommandParameter() {ParameterName = "pPI_REV_AMT", Value = ob.PI_REV_AMT},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
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

        public List<CM_IMP_PI_HModel> SelectAll(int pageNo, int pageSize, Int64? pSCM_SUPPLIER_ID = null, string pPI_NO_IMP = null, string pPO_NO_IMP = null, string pPI_DT_IMP = null)
        {
            string sp = "pkg_cm_import_pi.cm_imp_pi_h_select";
            try
            {
                var obList = new List<CM_IMP_PI_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pPI_NO_IMP", Value =pPI_NO_IMP},
                     new CommandParameter() {ParameterName = "pPI_DT_IMP", Value =pPI_DT_IMP},
                     new CommandParameter() {ParameterName = "pPO_NO_IMP", Value =pPO_NO_IMP},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PI_HModel ob = new CM_IMP_PI_HModel();
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);
                    ob.PI_DT_IMP = (dr["PI_DT_IMP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PI_DT_IMP"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.PI_NET_AMT = (dr["PI_NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_NET_AMT"]);
                    ob.PI_REV_AMT = (dr["PI_REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_REV_AMT"]);
                    ob.TERMS_DESC = (dr["TERMS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERMS_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CM_IMP_PI_HModel Select(Int64? pCM_IMP_PI_H_ID = null)
        {
            string sp = "pkg_cm_import_pi.cm_imp_pi_h_select";
            try
            {
                var ob = new CM_IMP_PI_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value =pCM_IMP_PI_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);
                    ob.PI_DT_IMP = (dr["PI_DT_IMP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PI_DT_IMP"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.PI_NET_AMT = (dr["PI_NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_NET_AMT"]);
                    ob.PI_REV_AMT = (dr["PI_REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_REV_AMT"]);
                    ob.TERMS_DESC = (dr["TERMS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERMS_DESC"]);
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

        public List<CM_IMP_PI_HModel> SelectByID(Int64? pSCM_SUPPLIER_ID = null)
        {
            string sp = "pkg_cm_import_pi.cm_imp_pi_h_select";
            try
            {
                var obList = new List<CM_IMP_PI_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PI_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3002},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CM_IMP_PI_HModel ob = new CM_IMP_PI_HModel();
                    ob.CM_IMP_PI_H_ID = (dr["CM_IMP_PI_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PI_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.PI_NO_IMP = (dr["PI_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PI_NO_IMP"]);
                    ob.PI_DT_IMP = (dr["PI_DT_IMP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PI_DT_IMP"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                    ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                    ob.PI_NET_AMT = (dr["PI_NET_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_NET_AMT"]);
                    ob.PI_REV_AMT = (dr["PI_REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["PI_REV_AMT"]);
                    ob.TERMS_DESC = (dr["TERMS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERMS_DESC"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TOTAL_REC { get; set; }


        public string SUP_TRD_NAME_EN { get; set; }
    }
}