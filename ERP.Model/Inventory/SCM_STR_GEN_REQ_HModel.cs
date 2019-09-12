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
    public class SCM_STR_GEN_REQ_HModel
    {
        public Int64 SCM_STR_GEN_REQ_H_ID { get; set; }
        public string STR_REQ_NO { get; set; }
        public DateTime STR_REQ_DT { get; set; }
        public Int64 STR_REQ_BY { get; set; }
        public Int64 STR_REQ_TO { get; set; }
        public Int64 SCM_STORE_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public Int64 HR_DEPARTMENT_ID { get; set; }
        public string INV_ITEM_CAT_LST { get; set; }
        public Int64 RF_REQ_TYPE_ID { get; set; }
        public Int64 RF_ACTN_STATUS_ID { get; set; }
        public string REMARKS { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }
        public string IS_FINALIZED { get; set; }
        public string XML_REQ_D { get; set; }
        

        public string Save()
        {
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_REQ_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value = ob.SCM_STR_GEN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
                     new CommandParameter() {ParameterName = "pAPROVER_LVL_NO", Value = ob.APROVER_LVL_NO},
                     new CommandParameter() {ParameterName = "pXML_REQ_D", Value = ob.XML_REQ_D},
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
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_REQ_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value = ob.SCM_STR_GEN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pIS_FINALIZED", Value = ob.IS_FINALIZED},
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
            const string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_REQ_H_INSERT";
            string jsonStr = "{";
            var ob = this;
            var i = 1;
            try
            {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value = ob.SCM_STR_GEN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = ob.STR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = ob.STR_REQ_DT},
                     new CommandParameter() {ParameterName = "pSTR_REQ_BY", Value = ob.STR_REQ_BY},
                     new CommandParameter() {ParameterName = "pSTR_REQ_TO", Value = ob.STR_REQ_TO},
                     new CommandParameter() {ParameterName = "pSCM_STORE_ID", Value = ob.SCM_STORE_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pHR_DEPARTMENT_ID", Value = ob.HR_DEPARTMENT_ID},
                     new CommandParameter() {ParameterName = "pINV_ITEM_CAT_LST", Value = ob.INV_ITEM_CAT_LST},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = ob.RF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_ACTN_STATUS_ID", Value = ob.RF_ACTN_STATUS_ID},
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

        public List<SCM_STR_GEN_REQ_HModel> SelectAll(int pageNo, int pageSize, string pSTR_REQ_NO = null, string pSTR_REQ_DT = null, string pREQ_TYPE_NAME = null, Int64? pRF_REQ_TYPE_ID = null, Int64? pUSER_ID = null, Int64? pOption = null)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_REQ_H_SELECT";
            try
            {
                var obList = new List<SCM_STR_GEN_REQ_HModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value =0},
                     new CommandParameter() {ParameterName = "pageNo", Value = pageNo},
                     new CommandParameter() {ParameterName = "pageSize", Value = pageSize},
                     
                     new CommandParameter() {ParameterName = "pSTR_REQ_NO", Value = pSTR_REQ_NO},
                     new CommandParameter() {ParameterName = "pSTR_REQ_DT", Value = pSTR_REQ_DT},
                     new CommandParameter() {ParameterName = "pREQ_TYPE_NAME", Value = pREQ_TYPE_NAME},
                     new CommandParameter() {ParameterName = "pRF_REQ_TYPE_ID", Value = pRF_REQ_TYPE_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value = pUSER_ID},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SCM_STR_GEN_REQ_HModel ob = new SCM_STR_GEN_REQ_HModel();
                    ob.SCM_STR_GEN_REQ_H_ID = (dr["SCM_STR_GEN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_REQ_H_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);
                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.IS_VISIBLE = (dr["IS_VISIBLE"] == DBNull.Value) ? "Y" : Convert.ToString(dr["IS_VISIBLE"]);
                    
                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);

                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    ob.EVENT_NAME = (dr["EVENT_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["EVENT_NAME"]);
                    ob.NXT_ACTION_NAME = (dr["NXT_ACTION_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["NXT_ACTION_NAME"]);

                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);

                    ob.ACTN_ROLE_FLAG = (dr["ACTN_ROLE_FLAG"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_ROLE_FLAG"]);
                    
                    ob.TOTAL_REC = (dr["TOTAL_REC"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["TOTAL_REC"]);
                    ob.PERMISSION = ob.IS_VISIBLE == "Y" ? (dr["PERMISSION"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["PERMISSION"]) : 0;
                    ob.TTL_RQD = (dr["TTL_RQD"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_RQD"]);
                    ob.TTL_ISS_QTY = (dr["TTL_ISS_QTY"] == DBNull.Value) ? 0 : Convert.ToDecimal(dr["TTL_ISS_QTY"]);
                    ob.BALANCE_QTY = ob.TTL_RQD - ob.TTL_ISS_QTY;


                    obList.Add(ob);
                }
                return obList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SCM_STR_GEN_REQ_HModel Select(Int64? pSCM_STR_GEN_REQ_H_ID = null)
        {
            string sp = "PKG_SCM_STR_REQ.SCM_STR_GEN_REQ_H_SELECT";
            try
            {
                var ob = new SCM_STR_GEN_REQ_HModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pSCM_STR_GEN_REQ_H_ID", Value =pSCM_STR_GEN_REQ_H_ID},
                     new CommandParameter() {ParameterName = "pUSER_ID", Value =  HttpContext.Current.Session["multiScUserId"]},
                     new CommandParameter() {ParameterName = "pOption", Value =3001},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ob.SCM_STR_GEN_REQ_H_ID = (dr["SCM_STR_GEN_REQ_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STR_GEN_REQ_H_ID"]);
                    ob.STR_REQ_NO = (dr["STR_REQ_NO"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_NO"]);
                    ob.STR_REQ_DT = (dr["STR_REQ_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["STR_REQ_DT"]);
                    ob.STR_REQ_BY = (dr["STR_REQ_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_BY"]);
                    ob.STR_REQ_TO = (dr["STR_REQ_TO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["STR_REQ_TO"]);
                    ob.SCM_STORE_ID = (dr["SCM_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_STORE_ID"]);
                    ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                    ob.HR_DEPARTMENT_ID = (dr["HR_DEPARTMENT_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_DEPARTMENT_ID"]);
                    ob.INV_ITEM_CAT_LST = (dr["INV_ITEM_CAT_LST"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["INV_ITEM_CAT_LST"]);
                    ob.RF_REQ_TYPE_ID = (dr["RF_REQ_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_REQ_TYPE_ID"]);
                    ob.RF_ACTN_STATUS_ID = (dr["RF_ACTN_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_ACTN_STATUS_ID"]);
                    ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                    ob.CREATION_DATE = (dr["CREATION_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["CREATION_DATE"]);
                    ob.CREATED_BY = (dr["CREATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CREATED_BY"]);
                    ob.LAST_UPDATE_DATE = (dr["LAST_UPDATE_DATE"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["LAST_UPDATE_DATE"]);
                    ob.LAST_UPDATED_BY = (dr["LAST_UPDATED_BY"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LAST_UPDATED_BY"]);
                    ob.IS_FINALIZED = (dr["IS_FINALIZED"] == DBNull.Value) ? "N" : Convert.ToString(dr["IS_FINALIZED"]);

                    ob.APROVER_LVL_NO = (dr["APROVER_LVL_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["APROVER_LVL_NO"]);
                    ob.ACTN_STATUS_NAME = (dr["ACTN_STATUS_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["ACTN_STATUS_NAME"]);
                    
                    ob.INV_ITEM_CAT_ID = Convert.ToInt64(ob.INV_ITEM_CAT_LST);

                    ob.COMP_NAME_EN = (dr["COMP_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["COMP_NAME_EN"]);
                    ob.DEPARTMENT_NAME_EN = (dr["DEPARTMENT_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["DEPARTMENT_NAME_EN"]);
                    ob.REQ_TYPE_NAME = (dr["REQ_TYPE_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REQ_TYPE_NAME"]);
                    ob.STORE_NAME_EN = (dr["STORE_NAME_EN"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STORE_NAME_EN"]);                    
                    ob.STR_REQ_BY_NAME = (dr["STR_REQ_BY_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["STR_REQ_BY_NAME"]);
                    
                }
                return ob;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string COMP_NAME_EN { get; set; }

        public string DEPARTMENT_NAME_EN { get; set; }

        public string REQ_TYPE_NAME { get; set; }

        public string STORE_NAME_EN { get; set; }

        public string ACTN_STATUS_NAME { get; set; }

        public string EVENT_NAME { get; set; }

        public string NXT_ACTION_NAME { get; set; }

        public string STR_REQ_BY_NAME { get; set; }

        public string ACTN_ROLE_FLAG { get; set; }

        public int TOTAL_REC { get; set; }

        public long PERMISSION { get; set; }

        public decimal TTL_RQD { get; set; }

        public decimal TTL_ISS_QTY { get; set; }

        public decimal BALANCE_QTY { get; set; }

        public long INV_ITEM_CAT_ID { get; set; }

        public long APROVER_LVL_NO { get; set; }

        public string IS_VISIBLE { get; set; }
    }
}