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
    public class SCM_STR_GEN_ISS_RTN_HModel
    {
        public Int64 SCM_STR_GEN_ISS_RTN_H_ID { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 ITEM_RTN_BY { get; set; }
        public string RTN_REF_NO { get; set; }
        public DateTime RTN_DT { get; set; }
        public string IS_FINALIZED { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_RTN_D { get; set; }


        public string Save()
        {
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RTN_BY", Value = ob.ITEM_RTN_BY},
                     new CommandParameter() {ParameterName = "pRTN_REF_NO", Value = ob.RTN_REF_NO},
                     new CommandParameter() {ParameterName = "pRTN_DT", Value = ob.RTN_DT},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_RTN_D", Value = ob.XML_RTN_D},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "opSCM_STR_GEN_ISS_RTN_H_ID", Value =0, Direction = ParameterDirection.Output},
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
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RTN_BY", Value = ob.ITEM_RTN_BY},
                     new CommandParameter() {ParameterName = "pRTN_REF_NO", Value = ob.RTN_REF_NO},
                     new CommandParameter() {ParameterName = "pRTN_DT", Value = ob.RTN_DT},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value = ob.SCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pITEM_RTN_BY", Value = ob.ITEM_RTN_BY},
                     new CommandParameter() {ParameterName = "pRTN_REF_NO", Value = ob.RTN_REF_NO},
                     new CommandParameter() {ParameterName = "pRTN_DT", Value = ob.RTN_DT},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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

        public List<SCM_STR_GEN_ISS_RTN_HModel> SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO, string pSTR_REQ_DT, string pREQ_TYPE_NAME, long? pRF_REQ_TYPE_ID, long? pUSER_ID, long? pOption)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_SELECT";
            try
            {
                var obList = new List<SCM_STR_GEN_ISS_RTN_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     //new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     //new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     //new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     //new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_GEN_ISS_RTN_HModel ob = new SCM_STR_GEN_ISS_RTN_HModel();
                    ob.SCM_STR_GEN_ISS_RTN_H_ID = (dr["SCM_STR_GEN_ISS_RTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_RTN_BY = (dr["ITEM_RTN_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RTN_BY"]);
                    ob.RTN_REF_NO = (dr["RTN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RTN_REF_NO"]);
                    ob.RTN_DT = (dr["RTN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RTN_DT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_GEN_ISS_RTN_HModel Select(Int64? pSCM_STR_GEN_ISS_RTN_H_ID = null)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_ISS_RTN_H_SELECT";
            try
            {
                var ob = new SCM_STR_GEN_ISS_RTN_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_ISS_RTN_H_ID", Value =pSCM_STR_GEN_ISS_RTN_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_GEN_ISS_RTN_H_ID = (dr["SCM_STR_GEN_ISS_RTN_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_ISS_RTN_H_ID"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.ITEM_RTN_BY = (dr["ITEM_RTN_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ITEM_RTN_BY"]);
                    ob.RTN_REF_NO = (dr["RTN_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["RTN_REF_NO"]);
                    ob.RTN_DT = (dr["RTN_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["RTN_DT"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
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


        public int TOTAL_REC { get; set; }

        public string STORE_NAME_EN { get; set; }
    }
}