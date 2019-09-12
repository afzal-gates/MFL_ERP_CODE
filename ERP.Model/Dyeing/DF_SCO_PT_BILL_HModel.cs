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
    public class DF_SCO_PT_BILL_HModel
    {
        public Int64 DF_SCO_PT_BILL_H_ID { get; set; }
        public Int64? PARENT_ID { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string BILL_NO { get; set; }
        public DateTime BILL_DT { get; set; }
        public string IS_DM_CM { get; set; }
        public Int64? LK_BL_CR_RSN_TYP_ID { get; set; }
        public string OTH_RSN_DESC { get; set; }
        public Decimal REV_AMT { get; set; }
        public string REMARKS { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string XML_BILL_D { get; set; }

        public string Save()
        {
            const string sp = "PKG_DF_PT.DF_SCO_PT_BILL_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID==0?null:ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = ob.BILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = ob.BILL_DT},
                     new CommandParameter() {ParameterName = "pIS_DM_CM", Value = ob.IS_DM_CM==null?"CM":ob.IS_DM_CM},
                     new CommandParameter() {ParameterName = "pLK_BL_CR_RSN_TYP_ID", Value = ob.LK_BL_CR_RSN_TYP_ID==0?null:ob.LK_BL_CR_RSN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOTH_RSN_DESC", Value = ob.OTH_RSN_DESC},
                     new CommandParameter() {ParameterName = "pREV_AMT", Value = ob.REV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pXML_BILL_D", Value = ob.XML_BILL_D},
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

        public string Update()
        {
            const string sp = "PKG_DF_PT.DF_SCO_PT_BILL_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = ob.BILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = ob.BILL_DT},
                     new CommandParameter() {ParameterName = "pIS_DM_CM", Value = ob.IS_DM_CM},
                     new CommandParameter() {ParameterName = "pLK_BL_CR_RSN_TYP_ID", Value = ob.LK_BL_CR_RSN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOTH_RSN_DESC", Value = ob.OTH_RSN_DESC},
                     new CommandParameter() {ParameterName = "pREV_AMT", Value = ob.REV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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
            const string sp = "PKG_DF_PT.DF_SCO_PT_BILL_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value = ob.DF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pPARENT_ID", Value = ob.PARENT_ID},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value = ob.BILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value = ob.BILL_DT},
                     new CommandParameter() {ParameterName = "pIS_DM_CM", Value = ob.IS_DM_CM},
                     new CommandParameter() {ParameterName = "pLK_BL_CR_RSN_TYP_ID", Value = ob.LK_BL_CR_RSN_TYP_ID},
                     new CommandParameter() {ParameterName = "pOTH_RSN_DESC", Value = ob.OTH_RSN_DESC},
                     new CommandParameter() {ParameterName = "pREV_AMT", Value = ob.REV_AMT},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<DF_SCO_PT_BILL_HModel> SelectAll(int pageNo, int pageSize, Int64 pSCM_SUPPLIER_ID, string pBILL_NO = null, DateTime? pBILL_DT = null)
        {
            string sp = "PKG_DF_PT.DF_SCO_PT_BILL_H_SELECT";
            try
            {
                var obList = new List<DF_SCO_PT_BILL_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pageNo", Value =pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value =pageSize},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value =pSCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pBILL_NO", Value =pBILL_NO},
                     new CommandParameter() {ParameterName = "pBILL_DT", Value =pBILL_DT},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DF_SCO_PT_BILL_HModel ob = new DF_SCO_PT_BILL_HModel();
                    ob.DF_SCO_PT_BILL_H_ID = (dr["DF_SCO_PT_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.BILL_NO = (dr["BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BILL_NO"]);
                    ob.BILL_DT = (dr["BILL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BILL_DT"]);
                    ob.IS_DM_CM = (dr["IS_DM_CM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DM_CM"]);
                    ob.LK_BL_CR_RSN_TYP_ID = (dr["LK_BL_CR_RSN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BL_CR_RSN_TYP_ID"]);
                    ob.OTH_RSN_DESC = (dr["OTH_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTH_RSN_DESC"]);
                    ob.REV_AMT = (dr["REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_AMT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    ob.SUP_TRD_NAME_EN = (dr["SUP_TRD_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["SUP_TRD_NAME_EN"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    
                    
                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DF_SCO_PT_BILL_HModel Select(Int64? pDF_SCO_PT_BILL_H_ID = null)
        {
            string sp = "PKG_DF_PT.DF_SCO_PT_BILL_H_SELECT";
            try
            {
                var ob = new DF_SCO_PT_BILL_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pDF_SCO_PT_BILL_H_ID", Value =pDF_SCO_PT_BILL_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.DF_SCO_PT_BILL_H_ID = (dr["DF_SCO_PT_BILL_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DF_SCO_PT_BILL_H_ID"]);
                    ob.PARENT_ID = (dr["PARENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PARENT_ID"]);
                    ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                    ob.BILL_NO = (dr["BILL_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["BILL_NO"]);
                    ob.BILL_DT = (dr["BILL_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["BILL_DT"]);
                    ob.IS_DM_CM = (dr["IS_DM_CM"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_DM_CM"]);
                    ob.LK_BL_CR_RSN_TYP_ID = (dr["LK_BL_CR_RSN_TYP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_BL_CR_RSN_TYP_ID"]);
                    ob.OTH_RSN_DESC = (dr["OTH_RSN_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OTH_RSN_DESC"]);
                    ob.REV_AMT = (dr["REV_AMT"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["REV_AMT"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
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

        public string SaveRevision()
        {
            throw new NotImplementedException();
        }

        public string SUP_TRD_NAME_EN { get; set; }

        public int TOTAL_REC { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }
    }
}