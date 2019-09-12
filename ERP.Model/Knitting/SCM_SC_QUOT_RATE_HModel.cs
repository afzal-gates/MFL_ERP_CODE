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
    public class SCM_SC_QUOT_RATE_HModel
    {
        public Int64 SCM_SC_QUOT_RATE_H_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public Int64 MC_FAB_PROC_GRP_ID { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISED_DT { get; set; }
        public Int64 APROVED_BY { get; set; }
        public string QUOT_REF_NO { get; set; }
        public DateTime QUOT_REF_DT { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public Int64 LK_APRVL_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public string QUOT_DOC_PATH { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string DOC_PATH_REF { get; set; }
        public HttpPostedFileBase ATT_FILE { get; set; }

        public string XML_D { get; set; }

        public string Save()
        {
            const string sp = "PKG_SCM_SC_RATE.SCM_SC_QUOT_RATE_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_QUOT_RATE_H_ID", Value = ob.SCM_SC_QUOT_RATE_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     //new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     //new CommandParameter() {ParameterName = "pREVISED_DT", Value = ob.REVISED_DT},
                     new CommandParameter() {ParameterName = "pAPROVED_BY", Value = ob.APROVED_BY},
                     new CommandParameter() {ParameterName = "pQUOT_REF_NO", Value = ob.QUOT_REF_NO},
                     new CommandParameter() {ParameterName = "pQUOT_REF_DT", Value = ob.QUOT_REF_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_APRVL_STATUS_ID", Value = ob.LK_APRVL_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_D", Value = ob.XML_D},
                     new CommandParameter() {ParameterName = "pQUOT_DOC_PATH", Value = ob.QUOT_DOC_PATH},
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
            const string sp = "SP_SCM_SC_QUOT_RATE_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_QUOT_RATE_H_ID", Value = ob.SCM_SC_QUOT_RATE_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISED_DT", Value = ob.REVISED_DT},
                     new CommandParameter() {ParameterName = "pAPROVED_BY", Value = ob.APROVED_BY},
                     new CommandParameter() {ParameterName = "pQUOT_REF_NO", Value = ob.QUOT_REF_NO},
                     new CommandParameter() {ParameterName = "pQUOT_REF_DT", Value = ob.QUOT_REF_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_APRVL_STATUS_ID", Value = ob.LK_APRVL_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pQUOT_DOC_PATH", Value = ob.QUOT_DOC_PATH},
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
            const string sp = "SP_SCM_SC_QUOT_RATE_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_QUOT_RATE_H_ID", Value = ob.SCM_SC_QUOT_RATE_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value = ob.MC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISED_DT", Value = ob.REVISED_DT},
                     new CommandParameter() {ParameterName = "pAPROVED_BY", Value = ob.APROVED_BY},
                     new CommandParameter() {ParameterName = "pQUOT_REF_NO", Value = ob.QUOT_REF_NO},
                     new CommandParameter() {ParameterName = "pQUOT_REF_DT", Value = ob.QUOT_REF_DT},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pLK_APRVL_STATUS_ID", Value = ob.LK_APRVL_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pQUOT_DOC_PATH", Value = ob.QUOT_DOC_PATH},
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

        public List<SCM_SC_QUOT_RATE_HModel> SelectByID(Int64? pSCM_SUPPLIER_ID = null, Int64? pMC_FAB_PROC_GRP_ID = null)
        {
            string sp = "pkg_scm_sc_rate.scm_sc_quot_rate_h_select";
            try
            {
                List<SCM_SC_QUOT_RATE_HModel> List = new List<SCM_SC_QUOT_RATE_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pMC_FAB_PROC_GRP_ID", Value =pMC_FAB_PROC_GRP_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_SC_QUOT_RATE_HModel ob = new SCM_SC_QUOT_RATE_HModel();
                    ob.SCM_SC_QUOT_RATE_H_ID = (dr["SCM_SC_QUOT_RATE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_QUOT_RATE_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    //ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    //ob.REVISED_DT = (dr["REVISED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISED_DT"]);
                    ob.APROVED_BY = (dr["APROVED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVED_BY"]);
                    ob.QUOT_REF_NO = (dr["QUOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOT_REF_NO"]);
                    ob.QUOT_REF_DT = (dr["QUOT_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QUOT_REF_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_APRVL_STATUS_ID = (dr["LK_APRVL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_APRVL_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.QUOT_DOC_PATH = (dr["QUOT_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOT_DOC_PATH"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.FAB_PROC_GRP_NAME = (dr["FAB_PROC_GRP_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FAB_PROC_GRP_NAME"]);
                    List.Add(ob);
                }
                return List;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_SC_QUOT_RATE_HModel Select(long ID)
        {
            string sp = "Select_SCM_SC_QUOT_RATE_H";
            try
            {
                var ob = new SCM_SC_QUOT_RATE_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_SC_QUOT_RATE_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_SC_QUOT_RATE_H_ID = (dr["SCM_SC_QUOT_RATE_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SC_QUOT_RATE_H_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.MC_FAB_PROC_GRP_ID = (dr["MC_FAB_PROC_GRP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["MC_FAB_PROC_GRP_ID"]);
                    ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                    ob.REVISED_DT = (dr["REVISED_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISED_DT"]);
                    ob.APROVED_BY = (dr["APROVED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVED_BY"]);
                    ob.QUOT_REF_NO = (dr["QUOT_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOT_REF_NO"]);
                    ob.QUOT_REF_DT = (dr["QUOT_REF_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["QUOT_REF_DT"]);
                    ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                    ob.LK_APRVL_STATUS_ID = (dr["LK_APRVL_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_APRVL_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.QUOT_DOC_PATH = (dr["QUOT_DOC_PATH"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["QUOT_DOC_PATH"]);
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

        public string SUP_TRD_NAME_EN { get; set; }

        public string FAB_PROC_GRP_NAME { get; set; }
    }
}