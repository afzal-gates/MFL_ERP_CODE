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
    public class CM_IMP_PO_H_HISTModel
    {
        public Int64 CM_IMP_PO_H_HIST_ID { get; set; }
        public Int64 CM_IMP_PO_H_ID { get; set; }
        public Int64 HR_COMPANY_ID { get; set; }
        public string PO_NO_IMP { get; set; }
        public DateTime PO_DT_IMP { get; set; }
        public string IS_LOCAL_CASH_P { get; set; }
        public Int64 SCM_SUPPLIER_ID { get; set; }
        public string CASH_SUPL_NAME { get; set; }
        public Int64 LK_LOC_SRC_TYPE_ID { get; set; }
        public Int64 RF_PAY_MTHD_ID { get; set; }
        public Int64 DELV_STORE_ID { get; set; }
        public Int64 BILL_TO_COMP_ID { get; set; }
        public Int64 RF_INCO_TERM_ID { get; set; }
        public Int64 RF_DELV_PLC_ID { get; set; }
        public Int64 RF_PAYM_TERM_ID { get; set; }
        public Int64 RF_SHIP_MODE_ID { get; set; }
        public Int64 RF_CURRENCY_ID { get; set; }
        public string TERMS_DESC { get; set; }
        public string REMARKS { get; set; }
        public Int64 REVISION_NO { get; set; }
        public DateTime REVISION_DT { get; set; }
        public string REV_REASON { get; set; }
        public Int64 LK_PO_STATUS_ID { get; set; }
        public DateTime CREATION_DATE { get; set; }
        public Int64 CREATED_BY { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public Int64 LAST_UPDATED_BY { get; set; }

        public string Save()
        {
            const string sp = "SP_CM_IMP_PO_H_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPO_NO_IMP", Value = ob.PO_NO_IMP},
                     new CommandParameter() {ParameterName = "pPO_DT_IMP", Value = ob.PO_DT_IMP},
                     new CommandParameter() {ParameterName = "pIS_LOCAL_CASH_P", Value = ob.IS_LOCAL_CASH_P},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCASH_SUPL_NAME", Value = ob.CASH_SUPL_NAME},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pDELV_STORE_ID", Value = ob.DELV_STORE_ID},
                     new CommandParameter() {ParameterName = "pBILL_TO_COMP_ID", Value = ob.BILL_TO_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pLK_PO_STATUS_ID", Value = ob.LK_PO_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =1000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
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
            const string sp = "SP_CM_IMP_PO_H_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPO_NO_IMP", Value = ob.PO_NO_IMP},
                     new CommandParameter() {ParameterName = "pPO_DT_IMP", Value = ob.PO_DT_IMP},
                     new CommandParameter() {ParameterName = "pIS_LOCAL_CASH_P", Value = ob.IS_LOCAL_CASH_P},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCASH_SUPL_NAME", Value = ob.CASH_SUPL_NAME},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pDELV_STORE_ID", Value = ob.DELV_STORE_ID},
                     new CommandParameter() {ParameterName = "pBILL_TO_COMP_ID", Value = ob.BILL_TO_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pLK_PO_STATUS_ID", Value = ob.LK_PO_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =2000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
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
            const string sp = "SP_CM_IMP_PO_H_HIST";
            string jsonStr="{";
            var ob = this;
            var i = 1;
            try
             {
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value = ob.CM_IMP_PO_H_HIST_ID},
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_ID", Value = ob.CM_IMP_PO_H_ID},
                     new CommandParameter() {ParameterName = "pHR_COMPANY_ID", Value = ob.HR_COMPANY_ID},
                     new CommandParameter() {ParameterName = "pPO_NO_IMP", Value = ob.PO_NO_IMP},
                     new CommandParameter() {ParameterName = "pPO_DT_IMP", Value = ob.PO_DT_IMP},
                     new CommandParameter() {ParameterName = "pIS_LOCAL_CASH_P", Value = ob.IS_LOCAL_CASH_P},
                     new CommandParameter() {ParameterName = "pSCM_SUPPLIER_ID", Value = ob.SCM_SUPPLIER_ID},
                     new CommandParameter() {ParameterName = "pCASH_SUPL_NAME", Value = ob.CASH_SUPL_NAME},
                     new CommandParameter() {ParameterName = "pLK_LOC_SRC_TYPE_ID", Value = ob.LK_LOC_SRC_TYPE_ID},
                     new CommandParameter() {ParameterName = "pRF_PAY_MTHD_ID", Value = ob.RF_PAY_MTHD_ID},
                     new CommandParameter() {ParameterName = "pDELV_STORE_ID", Value = ob.DELV_STORE_ID},
                     new CommandParameter() {ParameterName = "pBILL_TO_COMP_ID", Value = ob.BILL_TO_COMP_ID},
                     new CommandParameter() {ParameterName = "pRF_INCO_TERM_ID", Value = ob.RF_INCO_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_DELV_PLC_ID", Value = ob.RF_DELV_PLC_ID},
                     new CommandParameter() {ParameterName = "pRF_PAYM_TERM_ID", Value = ob.RF_PAYM_TERM_ID},
                     new CommandParameter() {ParameterName = "pRF_SHIP_MODE_ID", Value = ob.RF_SHIP_MODE_ID},
                     new CommandParameter() {ParameterName = "pRF_CURRENCY_ID", Value = ob.RF_CURRENCY_ID},
                     new CommandParameter() {ParameterName = "pTERMS_DESC", Value = ob.TERMS_DESC},
                     new CommandParameter() {ParameterName = "pREMARKS", Value = ob.REMARKS},
                     new CommandParameter() {ParameterName = "pREVISION_NO", Value = ob.REVISION_NO},
                     new CommandParameter() {ParameterName = "pREVISION_DT", Value = ob.REVISION_DT},
                     new CommandParameter() {ParameterName = "pREV_REASON", Value = ob.REV_REASON},
                     new CommandParameter() {ParameterName = "pLK_PO_STATUS_ID", Value = ob.LK_PO_STATUS_ID},
                     new CommandParameter() {ParameterName = "pCREATION_DATE", Value = ob.CREATION_DATE},
                     new CommandParameter() {ParameterName = "pCREATED_BY", Value = ob.CREATED_BY},
                     new CommandParameter() {ParameterName = "pLAST_UPDATE_DATE", Value = ob.LAST_UPDATE_DATE},
                     new CommandParameter() {ParameterName = "pLAST_UPDATED_BY", Value = ob.LAST_UPDATED_BY},
                     new CommandParameter() {ParameterName = "pOption", Value =4000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);

                 foreach (DataRow dr in ds.Tables["OUTPARAM"].Rows)
                 {
                    jsonStr += dr["KEY"].ToString() + ":"+ dr["VALUE"].ToString()+",";
                    if (i < ds.Tables["OUTPARAM"].Rows.Count) 
                     { 
                      jsonStr += ",";
                     }  
                     else  
                      {  
                          jsonStr += "}"; 
                      }  
                         i++ ;
                 }
                 return jsonStr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CM_IMP_PO_H_HISTModel> SelectAll()
        {
            string sp = "Select_CM_IMP_PO_H_HIST";
            try
            {
                var obList = new List<CM_IMP_PO_H_HISTModel>();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                            CM_IMP_PO_H_HISTModel ob = new CM_IMP_PO_H_HISTModel();
                            ob.CM_IMP_PO_H_HIST_ID = (dr["CM_IMP_PO_H_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_HIST_ID"]);
                            ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                            ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                            ob.PO_NO_IMP = (dr["PO_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NO_IMP"]);
                            ob.PO_DT_IMP = (dr["PO_DT_IMP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PO_DT_IMP"]);
                            ob.IS_LOCAL_CASH_P = (dr["IS_LOCAL_CASH_P"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL_CASH_P"]);
                            ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                            ob.CASH_SUPL_NAME = (dr["CASH_SUPL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CASH_SUPL_NAME"]);
                            ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                            ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                            ob.DELV_STORE_ID = (dr["DELV_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_STORE_ID"]);
                            ob.BILL_TO_COMP_ID = (dr["BILL_TO_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BILL_TO_COMP_ID"]);
                            ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                            ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                            ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                            ob.RF_SHIP_MODE_ID = (dr["RF_SHIP_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SHIP_MODE_ID"]);
                            ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                            ob.TERMS_DESC = (dr["TERMS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERMS_DESC"]);
                            ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                            ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                            ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                            ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                            ob.LK_PO_STATUS_ID = (dr["LK_PO_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PO_STATUS_ID"]);
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

        public CM_IMP_PO_H_HISTModel Select(long ID)
        {
            string sp = "Select_CM_IMP_PO_H_HIST";
            try
            {
                var ob = new CM_IMP_PO_H_HISTModel();
                OraDatabase db = new OraDatabase();
                var ds = db.ExecuteStoredProcedure(new List<CommandParameter>()
                {
                     new CommandParameter() {ParameterName = "pCM_IMP_PO_H_HIST_ID", Value =0},
                     new CommandParameter() {ParameterName = "pOption", Value =3000},
                     new CommandParameter() {ParameterName = "pMsg", Value =500, Direction = ParameterDirection.Output}
                 }, sp);
              foreach (DataRow dr in ds.Tables[0].Rows)
               {
                        ob.CM_IMP_PO_H_HIST_ID = (dr["CM_IMP_PO_H_HIST_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_HIST_ID"]);
                        ob.CM_IMP_PO_H_ID = (dr["CM_IMP_PO_H_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["CM_IMP_PO_H_ID"]);
                        ob.HR_COMPANY_ID = (dr["HR_COMPANY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["HR_COMPANY_ID"]);
                        ob.PO_NO_IMP = (dr["PO_NO_IMP"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["PO_NO_IMP"]);
                        ob.PO_DT_IMP = (dr["PO_DT_IMP"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["PO_DT_IMP"]);
                        ob.IS_LOCAL_CASH_P = (dr["IS_LOCAL_CASH_P"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["IS_LOCAL_CASH_P"]);
                        ob.SCM_SUPPLIER_ID = (dr["SCM_SUPPLIER_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["SCM_SUPPLIER_ID"]);
                        ob.CASH_SUPL_NAME = (dr["CASH_SUPL_NAME"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["CASH_SUPL_NAME"]);
                        ob.LK_LOC_SRC_TYPE_ID = (dr["LK_LOC_SRC_TYPE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_LOC_SRC_TYPE_ID"]);
                        ob.RF_PAY_MTHD_ID = (dr["RF_PAY_MTHD_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAY_MTHD_ID"]);
                        ob.DELV_STORE_ID = (dr["DELV_STORE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["DELV_STORE_ID"]);
                        ob.BILL_TO_COMP_ID = (dr["BILL_TO_COMP_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["BILL_TO_COMP_ID"]);
                        ob.RF_INCO_TERM_ID = (dr["RF_INCO_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_INCO_TERM_ID"]);
                        ob.RF_DELV_PLC_ID = (dr["RF_DELV_PLC_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_DELV_PLC_ID"]);
                        ob.RF_PAYM_TERM_ID = (dr["RF_PAYM_TERM_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_PAYM_TERM_ID"]);
                        ob.RF_SHIP_MODE_ID = (dr["RF_SHIP_MODE_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_SHIP_MODE_ID"]);
                        ob.RF_CURRENCY_ID = (dr["RF_CURRENCY_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["RF_CURRENCY_ID"]);
                        ob.TERMS_DESC = (dr["TERMS_DESC"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["TERMS_DESC"]);
                        ob.REMARKS = (dr["REMARKS"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REMARKS"]);
                        ob.REVISION_NO = (dr["REVISION_NO"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["REVISION_NO"]);
                        ob.REVISION_DT = (dr["REVISION_DT"] == DBNull.Value) ? Convert.ToDateTime("01/01/1900") : Convert.ToDateTime(dr["REVISION_DT"]);
                        ob.REV_REASON = (dr["REV_REASON"] == DBNull.Value) ? string.Empty : Convert.ToString(dr["REV_REASON"]);
                        ob.LK_PO_STATUS_ID = (dr["LK_PO_STATUS_ID"] == DBNull.Value) ? 0 : Convert.ToInt64(dr["LK_PO_STATUS_ID"]);
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