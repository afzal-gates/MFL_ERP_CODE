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
    public class SCM_STR_NDL_ISS_HModel
    {
        public Int64 SCM_STR_NDL_ISS_H_ID { get; set; }
        public Int64 SCM_STR_NDL_REQ_H_ID { get; set; }
        public string ISS_REF_NO { get; set; }
        public DateTime STR_ISS_DT { get; set; }
        public Int64 ISSUE_BY { get; set; }
        public Int64 ISS_STR_ID { get; set; }
        public Int64 RCV_STR_ID { get; set; }
        public string IS_FINALIZED { get; set; }
        public string IS_CLOSED { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string XML_ISS_D { get; set; }
        public string XML_ISS_H { get; set; }
        
        public string Save()
        {
            const string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pRF_NDL_REQ_TYPE_ID", Value = ob.RF_NDL_REQ_TYPE_ID},                     
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pSTR_ISS_DT", Value = ob.STR_ISS_DT},
                     new CommandParameter() {ParameterName = "pISSUE_BY", Value = ob.ISSUE_BY},
                     new CommandParameter() {ParameterName = "pISS_STR_ID", Value = ob.ISS_STR_ID},
                     new CommandParameter() {ParameterName = "pRCV_STR_ID", Value = ob.RCV_STR_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},                     
                     new CommandParameter() {ParameterName = "pXML_ISS_D", Value = ob.XML_ISS_D},
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


        public string Close()
        {
            const string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_H_CLOSE";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {             
                     new CommandParameter() {ParameterName = "pXML_ISS_H", Value = ob.XML_ISS_H},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = HttpContext.Current.Session["multiScUserId"]},
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
            const string sp = "SP_SCM_STR_NDL_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pSTR_ISS_DT", Value = ob.STR_ISS_DT},
                     new CommandParameter() {ParameterName = "pISSUE_BY", Value = ob.ISSUE_BY},
                     new CommandParameter() {ParameterName = "pISS_STR_ID", Value = ob.ISS_STR_ID},
                     new CommandParameter() {ParameterName = "pRCV_STR_ID", Value = ob.RCV_STR_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
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
            const string sp = "SP_SCM_STR_NDL_ISS_H";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value = ob.SCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value = ob.SCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pISS_REF_NO", Value = ob.ISS_REF_NO},
                     new CommandParameter() {ParameterName = "pSTR_ISS_DT", Value = ob.STR_ISS_DT},
                     new CommandParameter() {ParameterName = "pISSUE_BY", Value = ob.ISSUE_BY},
                     new CommandParameter() {ParameterName = "pISS_STR_ID", Value = ob.ISS_STR_ID},
                     new CommandParameter() {ParameterName = "pRCV_STR_ID", Value = ob.RCV_STR_ID},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pIS_CLOSED", Value = ob.IS_CLOSED},
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

        public List<SCM_STR_NDL_ISS_HModel> SelectAll(Int64? pSCM_STR_NDL_REQ_H_ID = null)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_H_SELECT";
            try
            {
                var obList = new List<SCM_STR_NDL_ISS_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_REQ_H_ID", Value =pSCM_STR_NDL_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_NDL_ISS_HModel ob = new SCM_STR_NDL_ISS_HModel();
                    ob.SCM_STR_NDL_ISS_H_ID = (dr["SCM_STR_NDL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_H_ID"]);
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.STR_ISS_DT = (dr["STR_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_ISS_DT"]);
                    ob.ISSUE_BY = (dr["ISSUE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISSUE_BY"]);
                    ob.ISS_STR_ID = (dr["ISS_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STR_ID"]);
                    ob.RCV_STR_ID = (dr["RCV_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_STR_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    ob.FROM_STORE_NAME_EN = (dr["FROM_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_STORE_NAME_EN"]);
                    ob.TO_STORE_NAME_EN = (dr["TO_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_STORE_NAME_EN"]);
                    
                    //ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    //ob.OFFICE_NAME_EN = (dr["OFFICE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["OFFICE_NAME_EN"]);
                    //ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    //ob.ITEM_CAT_NAME_EN = (dr["ITEM_CAT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ITEM_CAT_NAME_EN"]);
                    //ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    //ob.TO_STORE_NAME_EN = (dr["TO_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TO_STORE_NAME_EN"]);
                    //ob.FROM_STORE_NAME_EN = (dr["FROM_STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["FROM_STORE_NAME_EN"]);
                    //ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    //ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    //ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    //ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);
                    //ob.REASON_FOR = (dr["REASON_FOR"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REASON_FOR"]);
                    //ob.PERMISSION = (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]);
                    //ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);

                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_NDL_ISS_HModel Select(Int64? pSCM_STR_NDL_ISS_H_ID = null)
        {
            string sp = "PKG_KNT_MC_NDL.SCM_STR_NDL_ISS_H_SELECT";
            try
            {
                var ob = new SCM_STR_NDL_ISS_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_NDL_ISS_H_ID", Value =pSCM_STR_NDL_ISS_H_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_NDL_ISS_H_ID = (dr["SCM_STR_NDL_ISS_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_ISS_H_ID"]);
                    ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.ISS_REF_NO = (dr["ISS_REF_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ISS_REF_NO"]);
                    ob.STR_ISS_DT = (dr["STR_ISS_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_ISS_DT"]);
                    ob.ISSUE_BY = (dr["ISSUE_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISSUE_BY"]);
                    ob.ISS_STR_ID = (dr["ISS_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["ISS_STR_ID"]);
                    ob.RCV_STR_ID = (dr["RCV_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RCV_STR_ID"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.IS_CLOSED = (dr["IS_CLOSED"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_CLOSED"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);

                    //ob.SCM_STR_NDL_REQ_H_ID = (dr["SCM_STR_NDL_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_NDL_REQ_H_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_OFFICE_ID = (dr["HR_OFFICE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_OFFICE_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.INV_ITEM_CAT_ID = (dr["INV_ITEM_CAT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["INV_ITEM_CAT_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.REQ_ATTN_MAIL = (dr["REQ_ATTN_MAIL"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_ATTN_MAIL"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.FRM_REQ_STR_ID = (dr["FRM_REQ_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["FRM_REQ_STR_ID"]);
                    ob.TO_REQ_STR_ID = (dr["TO_REQ_STR_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["TO_REQ_STR_ID"]);
                    ob.RF_NDL_REQ_TYPE_ID = (dr["RF_NDL_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_NDL_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);

                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COMP_NAME_EN { get; set; }
        public string OFFICE_NAME_EN { get; set; }
        public string DEPARTMENT_NAME_EN { get; set; }
        public string ITEM_CAT_NAME_EN { get; set; }
        public string REQ_TYPE_NAME { get; set; }
        public string TO_STORE_NAME_EN { get; set; }
        public string FROM_STORE_NAME_EN { get; set; }
        public string ACTN_ROLE_FLAG { get; set; }
        public string ACTN_STATUS_NAME { get; set; }
        public string EVENT_NAME { get; set; }
        public string NXT_ACTION_NAME { get; set; }
        public string REASON_FOR { get; set; }
        public long PERMISSION { get; set; }
        public int TOTAL_REC { get; set; }

        public long HR_COMPANY_ID { get; set; }
        public long HR_OFFICE_ID { get; set; }
        public long HR_DEPARTMENT_ID { get; set; }
        public long INV_ITEM_CAT_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public long STR_REQ_BY { get; set; }
        public long STR_REQ_TO { get; set; }
        public string REQ_ATTN_MAIL { get; set; }
        public long RF_REQ_TYPE_ID { get; set; }
        public long FRM_REQ_STR_ID { get; set; }
        public long TO_REQ_STR_ID { get; set; }
        public long RF_NDL_REQ_TYPE_ID { get; set; }
        public long RF_ACTN_STATUS_ID { get; set; }

    }
}